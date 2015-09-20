

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_SEL_Associado												*
* Objetivo  : Retorna os dados do Menu										*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Alteração:   /03/2015	                                            *
* Descrição	    :															*
	USP_SEL_Associado 1, 1, 'A', 0
****************************************************************************/

Alter Procedure USP_SEL_Associado(
	@SiteId	Int,
	@AssociadoCategoriaId Int,
	@LetraInicial VarChar(50) = null,
	@ExibirTodos Bit = 0
)
As
Begin

	/*
	Declare	@SiteId	Int = 2,
			@AssociadoCategoriaId Int = 2,
			@LetraInicial VarChar(1) = '',
			@ExibirTodos Bit = 0
	*/
	Select
		AssociadoId,
		SiteId,
		Nome,
		Resumo,
		AssociadoCategoriaId,
		TipoPessoaId,
		PaisId
	From
		tblAssociado
	Where
		SiteId = @SiteId
		And ((@AssociadoCategoriaId Is Null) Or (AssociadoCategoriaId = @AssociadoCategoriaId))
		And ((@ExibirTodos = 1) Or (Nome Like '' + @LetraInicial + '%'))
	
End