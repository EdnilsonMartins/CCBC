

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_SEL_UsuarioFuncionalidade									*
* Objetivo  : Retorna as funcionalidades do Usu�rio							*
*                                                                           *
* Creiada por	: Ednilson Martins	                                        *
* Data Cria��o	: 10/05/2015	                                            *
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