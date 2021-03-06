
/****** Object:  StoredProcedure [dbo].[USP_INS_Regra]    Script Date: 29/03/2015 11:36:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_INS_UsuarioGrupo											*
* Objetivo  : Manutenção dos Grupos de Usuários.							*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 															*
* Descrição	    : 															*
****************************************************************************/

Create Procedure [dbo].[USP_INS_UsuarioGrupo](
	@UsuarioGrupoId		Int,
	@SiteId				Int,
	@Nome				VarChar(80) = Null
)
As
Begin

	If @UsuarioGrupoId <> 0 Begin
		Update 
			tblUsuarioGrupo
		Set 
			Nome = @Nome
		Where 
			UsuarioGrupoId = @UsuarioGrupoId
		Select @UsuarioGrupoId UsuarioGrupoId
	End Else Begin

		Insert Into tblUsuarioGrupo(SiteId, Nome) 
		Values(@SiteId, @Nome)
		Select Cast(@@Identity As int) UsuarioGrupoId
	End

End