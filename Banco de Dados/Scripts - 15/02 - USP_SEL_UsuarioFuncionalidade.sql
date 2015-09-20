

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_SEL_UsuarioFuncionalidade									*
* Objetivo  : Retorna as funcionalidades do Usuário							*
*                                                                           *
* Creiada por	: Ednilson Martins	                                        *
* Data Criação	: 10/05/2015	                                            *
*                                                                           *
****************************************************************************/
Create Procedure USP_SEL_UsuarioFuncionalidade(
	@UsuarioId	Int
)
As
Begin


	Select 
		FuncionalidadeId,
		Ativo,
		Parametro
	From
		tblUsuarioFuncionalidade
	Where
		UsuarioId = @UsuarioId

End