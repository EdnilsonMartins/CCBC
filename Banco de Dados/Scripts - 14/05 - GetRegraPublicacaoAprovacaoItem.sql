

/****** Object:  UserDefinedFunction [dbo].[GetRegraPublicacaoAprovacaoItem]    Script Date: 09/05/2015 10:16:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco			:	dbCCBC													*
* Procedure		:	GetRegraPublicacaoAprovacaoItem							*
* Objetivo		:	Retorna uma tabela contendo todos os passos e condições	*
*                   com base na regra que deverá ser aplicada na publicação.*
*					Esta tabela apresenta também as aprovações relacionadas *
*					com cada passo/condicao.								*
*																			*
* Criada por	: Ednilson Martins	                                        *
* Data Criação	: 07/03/2015	                                            *
*                                                                           *
* Alterada por	: Ednilson Martins											*
* Data Alteração: 09/05/2015												*
* Descrição		: Filtrar regra conforme controle de Privacidade.			*
****************************************************************************/

ALTER FUNCTION [dbo].[GetRegraPublicacaoAprovacaoItem](
    @PublicacaoId Int
) RETURNS @result TABLE (PublicacaoId Int, RegraPassoId Int, Descricao VarChar(200), Sequencia Int, RegraPassoCondicaoId Int, UsuarioId Int, Liberado Bit)

BEGIN

	--Declare @PublicacaoId int = 3111

	Insert Into @result
	Select 
		P.PublicacaoId,
		RP.RegraPassoId,
		RP.Descricao,
		RP.Sequencia
		,RPC.RegraPassoCondicaoId
		,PAI.UsuarioId
		,PAI.Liberado					
	From 
		tblPublicacao P --On P.PublicacaoId = PAI.PublicacaoId
		join tblPublicacaoTipoRegra PTR On	PTR.PublicacaoTipoId = P.PublicacaoTipoId
											And PTR.Privado = P.Privado
		join tblRegra R On R.RegraId = PTR.RegraId
		join tblRegraPasso RP On RP.RegraId = R.RegraId
		join tblRegraPassoCondicao RPC On RPC.RegraPassoId = RP.RegraPassoId
							
		left join tblPublicacaoAprovacaoItem PAI On PAI.PublicacaoId = P.PublicacaoId 
			And (RPC.UsuarioGrupoId In (Select UsuarioGrupoId From dbo.GetUsuarioGrupo(PAI.UsuarioId))
					Or
					RPC.UsuarioId = PAI.UsuarioId
				)

	Where
		P.PublicacaoId = @PublicacaoId

	RETURN

END

