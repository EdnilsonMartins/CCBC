

/****** Object:  UserDefinedFunction [dbo].[GetRegraPublicacaoAprovacaoItem]    Script Date: 08/03/2015 09:05:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco			:	dbCCBC													*
* Procedure		:	GetRegraPassoCondicaoResultado							*
* Objetivo		:	Retorna resultado de aprovação para cada condição do	*
*					passo de uma regra da publicação.						*
*																			*
* Criada por	: Ednilson Martins	                                        *
* Data Criação	: 08/03/2015	                                            *
*                                                                           *
****************************************************************************/

CREATE FUNCTION [dbo].[GetRegraPassoCondicaoResultado](
    @PublicacaoId Int
) RETURNS @result TABLE (RegraPassoId int, AprovadoCondicao bit, TotalUsuario int, Liberado Bit)

BEGIN

	--Declare @PublicacaoId Int
	--Set @PublicacaoId = 3074

	Insert Into @result
	Select
		AnalisaCondicao.RegraPassoId			As RegraPassoId
		, AnalisaCondicao.AprovadoCondicao		As AprovadoCondicao
		, sum(AnalisaCondicao.TotalUsuario)		As TotalUsuario
		, AnalisaCondicao.Liberado				As Liberado
	From
		(
		Select
			C.RegraPassoId
			, C.RegraPassoCondicaoId RegraPassoCondicaoId
			, C.TotalUsuario TotalUsuario
			, Case When Exists(select * from dbo.GetRegraPublicacaoAprovacaoItem(@PublicacaoId) where RegraPassoId = C.RegraPassoId And Liberado = 0) Then 0
				   When RPC.QuantidadeMinimaUsuarios Is Null Then 1
				   When RPC.QuantidadeMinimaUsuarios Is Not Null 
						And C.TotalUsuario > RPC.QuantidadeMinimaUsuarios Then 1
				   Else 0 End AprovadoCondicao
			, Case When Exists(select * from dbo.GetRegraPublicacaoAprovacaoItem(@PublicacaoId) where RegraPassoId = C.RegraPassoId And Liberado = 0) Then 0 Else 1 End Liberado
		from
			(Select 
				L.RegraPassoId						As RegraPassoId
				, L.RegraPassoCondicaoId			As RegraPassoCondicaoId
				, Count(L.UsuarioId)				As TotalUsuario
				, L.Liberado						As Liberado
			From 
				dbo.GetRegraPublicacaoAprovacaoItem(@PublicacaoId) L
				--dbo.GetRegraPublicacaoAprovacaoItem(3074) L
				--select * from dbo.GetRegraPublicacaoAprovacaoItem(3074) L
			Group By
				RegraPassoId, RegraPassoCondicaoId, Liberado
			) C
			join tblRegraPassoCondicao RPC On RPC.RegraPassoCondicaoId = C.RegraPassoCondicaoId

		) AnalisaCondicao
		join tblRegraPasso RP On RP.RegraPassoId = AnalisaCondicao.RegraPassoId

	Group By
		AnalisaCondicao.RegraPassoId
		, AnalisaCondicao.AprovadoCondicao
		, AnalisaCondicao.Liberado
		

	RETURN

END

