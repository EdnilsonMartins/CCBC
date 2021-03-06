
/****** Object:  StoredProcedure [dbo].[USP_DEL_Menu]    Script Date: 29/03/2015 11:31:21 ******/
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
* Data Criação	:				                                            *
* Descrição	    :															*
*                                                                           *
****************************************************************************/
CREATE Procedure [dbo].[USP_DEL_UsuarioGrupo](
	@UsuarioGrupoId Int
) As
Begin

	Declare @indErro bit,
			@msgErro VarChar(120)


	Set @indErro = 0
	Set @msgErro = ''

	Delete From tblUsuarioGrupo Where UsuarioGrupoId = @UsuarioGrupoId

	Select @indErro indErro, @msgErro msgErro

End