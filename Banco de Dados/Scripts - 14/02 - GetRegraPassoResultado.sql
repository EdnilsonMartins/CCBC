

/****** Object:  UserDefinedFunction [dbo].[GetRegraPassoResultado]    Script Date: 09/05/2015 10:04:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco			:	dbCCBC													*
* Procedure		:	GetRegraPassoResultado									*
* Objetivo		:	Retorna resultado de aprovação para cada				*
*					passo de uma regra da publicação.						*
*																			*
* Criada por	: Ednilson Martins	                                        *
* Data Criação	: 08/03/2015	                                            *
*                                                                           *
* Alterada por	: Ednilson Martins											*
* Data Alteração: 29/03/2015												*
* Descrição		: Considerar regras distintas para Publico e Privado.		*
*																			*
* Alterada por	: Ednilson Martins											*
* Data Alteração: 09/05/2015												*
* Descrição		: Ajustes no cálculo do valor Default do AprovadoPasso.		*
*				  
select * from dbo.GetRegraPassoResultado(3112)
****************************************************************************/

ALTER FUNCTION [dbo].[GetRegraPassoResultado](
    @PublicacaoId Int
) RETURNS @result TABLE (RegraId Int, RegraPassoId Int, Sequencia Int, Descricao VarChar(200), TotalUsuarios Int, CondicaoResultado Bit, AprovadoPasso Bit, Liberado Bit)

BEGIN

	--Declare @PublicacaoId Int = 3112
	--Select * From dbo.GetRegraPassoCondicaoResultado(@PublicacaoId)

	Insert Into @result
	Select
		RP.RegraId
		, RP.RegraPassoId
		, RP.Sequencia
		, RP.Descricao
		, (select sum(TotalUsuario) from dbo.GetRegraPassoCondicaoResultado(P.PublicacaoId) Where RegraPassoId = RP.RegraPassoId) TotalUsuarios
	
		, Case When Exists(Select 1 From dbo.GetRegraPassoCondicaoResultado(P.PublicacaoId) Where RegraPassoId = RP.RegraPassoId And AprovadoCondicao = 0)  
					Then 0 Else 1 End CondicaoResultado

		, Case When Exists(Select 1 From dbo.GetRegraPassoCondicaoResultado(P.PublicacaoId) Where RegraPassoId = RP.RegraPassoId And AprovadoCondicao = 0)  
					Then 0
			   When RP.QuantidadeMinimaUsuariosDoGrupo Is Null	
					And Exists(Select 1 From dbo.GetRegraPassoCondicaoResultado(P.PublicacaoId) Where RegraPassoId = RP.RegraPassoId And AprovadoCondicao = 1)
					Then 1
			   When RP.QuantidadeMinimaUsuariosDoGrupo Is Not Null 
					And (select sum(TotalUsuario) from dbo.GetRegraPassoCondicaoResultado(P.PublicacaoId) Where RegraPassoId = RP.RegraPassoId And AprovadoCondicao = 1) >= RP.QuantidadeMinimaUsuariosDoGrupo
					Then 1
			   Else Null End AprovadoPasso --Alterado Default de 0 para Null.
		, Case When Not Exists(select 1 from dbo.GetRegraPassoCondicaoResultado(P.PublicacaoId) Where RegraPassoId = RP.RegraPassoId)
					Then Null
			   When Exists(select 1 from dbo.GetRegraPassoCondicaoResultado(P.PublicacaoId) Where RegraPassoId = RP.RegraPassoId And Liberado = 0) 
					Then 0 
			   Else 1 End Liberado
	From
		tblPublicacao P 
		join tblPublicacaoTipoRegra PTR On	PTR.PublicacaoTipoId = P.PublicacaoTipoId
											And PTR.Privado = P.Privado
		join tblRegraPasso RP On RP.RegraId = PTR.RegraId
	Where
		P.PublicacaoId = @PublicacaoId

	RETURN

END

