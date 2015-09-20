

/****** Object:  StoredProcedure [dbo].[USP_INS_Publicacao]    Script Date: 07/03/2015 22:28:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_SEL_PublicacaoRegraPasso_Usuario							*
* Objetivo  : Retorna se o usuário é elegível a responder o processo de 	*
*			  de liberação e ao mesmo tempo retorna o momento da última		*
*			  liberação realizada pelo userário dentro da publicação.		*
*                                                                           *
* Criada por	: Ednilson Martins	                                        *
* Data Criação	: 08/03/2015	                                            *
*                                                                           *
****************************************************************************/

CREATE Procedure [dbo].[USP_SEL_PublicacaoRegraPasso_Usuario](
	@PublicacaoId	Int,
	@UsuarioId		Int
)
As

Begin

	--Declare @PublicacaoId Int
	--Set @PublicacaoId = 3077

	--Declare @UsuarioId Int
	--Set @UsuarioId = 30

	Select 
		Cast(S.Ok_Usuario As Bit) As Ok_Usuario,
		Cast(S.Ok_UsuarioGrupo As Bit) As Ok_UsuarioGrupo,
		Cast(Case When (S.Ok_Usuario = 1) Or (S.Ok_UsuarioGrupo = 1) Then 1 Else 0 End As Bit) UsuarioElegivel,
		L.Liberado As Liberado,
		L.DataAprovacao As DataAprovacao
	From
		(
		Select
			top 1
			F.RegraId,
			F.RegraPassoId,
			F.Sequencia,
			F.Descricao,
			F.TotalUsuarios,
			F.CondicaoResultado,
			F.AprovadoPasso,
			F.Liberado,
			F.Resultado, 
		
			RPC.UsuarioGrupoId,
			RPC.UsuarioId,

			Case When RPC.UsuarioId = @UsuarioId Then 1 Else 0 End Ok_Usuario,
			Case When Exists(select 1 from dbo.GetUsuarioGrupo(@UsuarioId) where UsuarioGrupoId = RPC.UsuarioGrupoId) Then 1 Else 0 End Ok_UsuarioGrupo
		From
			dbo.GetRegraPassoResultadoFinal(@PublicacaoId) F
			join tblRegraPassoCondicao RPC On RPC.RegraPassoId = F.RegraPassoId
		Order By 
			F.Resultado Asc, F.Sequencia Asc
		) S
		outer apply (Select UsuarioId, Liberado, DataAprovacao From tblPublicacaoAprovacaoItem Where PublicacaoId = @PublicacaoId And UsuarioId = @UsuarioId) L



End