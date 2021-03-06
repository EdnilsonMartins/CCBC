--prod 22:00

/****** Object:  StoredProcedure [dbo].[USP_DEL_UsuarioGrupo]    Script Date: 31/05/2015 20:45:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_DEL_UsuarioGrupo											*
* Objetivo  : Exclusão de grupo de usuário									*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 31/05/2015			                                    *
* Descrição	    : Excluir também o registro da tblUsuarioUsuarioGrupo		*
*                 Excluir também das tabelas de privacidade das pubs.       *
****************************************************************************/
ALTER Procedure [dbo].[USP_DEL_UsuarioGrupo](
	@UsuarioGrupoId Int
) As
Begin

	Declare @indErro bit,
			@msgErro VarChar(120)


	Set @indErro = 0
	Set @msgErro = ''

	Delete From tblMenuRestricaoUsuarioGrupo Where UsuarioGrupoId = @UsuarioGrupoId
	Delete From tblBannerRestricaoUsuarioGrupo Where UsuarioGrupoId = @UsuarioGrupoId
	Delete From tblPublicacaoRestricaoUsuarioGrupo Where UsuarioGrupoId = @UsuarioGrupoId

	Delete From tblUsuarioUsuarioGrupo Where UsuarioGrupoId = @UsuarioGrupoId
	Delete From tblUsuarioGrupo Where UsuarioGrupoId = @UsuarioGrupoId

	Select @indErro indErro, @msgErro msgErro

End