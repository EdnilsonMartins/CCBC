--prod 22:00

/****** Object:  StoredProcedure [dbo].[USP_INS_UsuarioGrupo]    Script Date: 31/05/2015 20:23:13 ******/
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
* Data Criação	: 31/05/2015												*
* Descrição	    : Adicionado parâmetro @UsuarioGrupoPaiId					*
****************************************************************************/

ALTER Procedure [dbo].[USP_INS_UsuarioGrupo](
	@UsuarioGrupoId		Int,
	@SiteId				Int,
	@Nome				VarChar(80) = Null,
	@UsuarioGrupoPaiId	Int = Null
)
As
Begin

	If @UsuarioGrupoPaiId = 0 Set @UsuarioGrupoPaiId = null

	If @UsuarioGrupoId <> 0 Begin
		Update 
			tblUsuarioGrupo
		Set 
			Nome = @Nome
		Where 
			UsuarioGrupoId = @UsuarioGrupoId
		Select @UsuarioGrupoId UsuarioGrupoId
	End Else Begin

		Insert Into tblUsuarioGrupo(SiteId, Nome, UsuarioGrupoPaiId) 
		Values(@SiteId, @Nome, @UsuarioGrupoPaiId)
		Select Cast(@@Identity As int) UsuarioGrupoId
	End

End