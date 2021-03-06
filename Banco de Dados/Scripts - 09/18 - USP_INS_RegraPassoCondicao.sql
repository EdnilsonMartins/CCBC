
/****** Object:  StoredProcedure [dbo].[USP_INS_Regra]    Script Date: 29/03/2015 22:48:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_INS_RegraPassoCondicao									*
* Objetivo  : Manutenção das Condições.										*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 															*
* Descrição	    : 															*
****************************************************************************/

Create Procedure [dbo].[USP_INS_RegraPassoCondicao](
	@RegraPassoCondicaoId		Int,
	@RegraPassoId				Int,
	@UsuarioGrupoId				Int = Null,
	@UsuarioId					Int = Null,
	@QuantidadeMinimaUsuarios	Int = Null
)
As
Begin

	If @RegraPassoCondicaoId <> 0 Begin
		Update 
			tblRegraPassoCondicao
		Set 
			UsuarioGrupoId = @UsuarioGrupoId, 
			UsuarioId = @UsuarioId, 
			QuantidadeMinimaUsuarios = @QuantidadeMinimaUsuarios
		Where 
			RegraPassoCondicaoId = @RegraPassoCondicaoId
		Select @RegraPassoCondicaoId RegraPassoCondicaoId
	End Else Begin

		Insert Into tblRegraPassoCondicao(RegraPassoId, UsuarioGrupoId, UsuarioId, QuantidadeMinimaUsuarios) 
		Values(@RegraPassoId, @UsuarioGrupoId, @UsuarioId, @QuantidadeMinimaUsuarios)
		Select Cast(@@Identity As int) RegraPassoCondicaoId
	End

End