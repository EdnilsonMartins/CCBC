
/****** Object:  StoredProcedure [dbo].[USP_SEL_Regra]    Script Date: 29/03/2015 20:18:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_SEL_RegraPassoCondicao									*
* Objetivo  : Retorna as condições dos passos de uma regra.					*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 										                    *
* Descrição	    :															*

USP_SEL_RegraPassoCondicao null, 5
USP_SEL_RegraPassoCondicao 6, 5
****************************************************************************/
Create Procedure [dbo].[USP_SEL_RegraPassoCondicao](
	@RegraPassoCondicaoId	Int = Null,
	@RegraPassoId			Int = Null
)
As
Begin

	Select
		RPC.RegraPassoCondicaoId, 
		RPC.RegraPassoId, 
		RPC.UsuarioGrupoId, 
		RPC.UsuarioId, 
		RPC.QuantidadeMinimaUsuarios,
		U.Nome Usuario,
		UG.Nome UsuarioGrupo
	From 
		tblRegraPassoCondicao RPC
		Left Join tblUsuarioGrupo UG On UG.UsuarioGrupoId = RPC.UsuarioGrupoId
		Left Join tblUsuario U On U.UsuarioId = RPC.UsuarioId
		
	Where
		RPC.RegraPassoId = @RegraPassoId
		And ((@RegraPassoCondicaoId Is Null) Or (RPC.RegraPassoCondicaoId = @RegraPassoCondicaoId))

End