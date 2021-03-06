
/****** Object:  StoredProcedure [dbo].[USP_SEL_LoginEmail]    Script Date: 29/03/2015 16:08:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_SEL_LoginEmail											*
* Objetivo  : Retorna cadastro do usuário.									*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 29/03/2015	                                            *
* Descrição	    : Retornar os grupos do usuário.							*
*                                                                           *
****************************************************************************/
ALTER Procedure [dbo].[USP_SEL_LoginEmail](
	@Email	VarChar(255)
)
As
Begin

	Select 
		UsuarioId, Nome, Login, Senha, Ativo, Email, dbo.fnRetornaUsuarioUsuarioGrupo(UsuarioId) As ListaUsuarioGrupo
	From	
		tblUsuario
	Where
		Email = @Email

End