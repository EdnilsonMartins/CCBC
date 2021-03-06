
/****** Object:  StoredProcedure [dbo].[USP_SEL_Login]    Script Date: 29/03/2015 15:01:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_SEL_Login													*
* Objetivo  : Retorna os usuários.											*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 29/03/2015	                                            *
* Descrição	    : Retornar os grupos do usuário.							*
*                                                                           *
****************************************************************************/
ALTER Procedure [dbo].[USP_SEL_Login](
	@Login	VarChar(80) = null,
	@Senha	VarChar(32) = null,
	@UsuarioId Int = Null,
	@Email VarChar(255) = Null,
	@ListarTodos Bit = 0,
	@SiteId Int = 0
)
As
Begin


	Select
		UsuarioId,
		Nome,
		Login,
		Senha,
		Ativo,
		Email,
		dbo.fnRetornaUsuarioUsuarioGrupo(UsuarioId) As ListaUsuarioGrupo
	From
		tblUsuario
	Where
		(
		((@UsuarioId Is Null) Or (@UsuarioId Is Not Null And UsuarioId = @UsuarioId))
		And ((@Email Is Null) Or (@Email Is Not Null And Email = @Email))
		And (	(@Login Is Null And @Senha Is Null 
				And (@UsuarioId Is Not Null Or @Email Is Not Null)
				) Or (Login = @Login And Senha = @Senha))
		)
		Or (@ListarTodos = 1 And SiteId = @SiteId)
		
	
End