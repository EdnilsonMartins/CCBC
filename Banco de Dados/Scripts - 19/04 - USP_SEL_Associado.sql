
/****** Object:  StoredProcedure [dbo].[USP_SEL_Associado]    Script Date: 26/07/2015 17:28:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_SEL_Associado												*
* Objetivo  : Retorna os dados do Menu										*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Alteração: 26/07/2015	                                            *
* Descrição	    : Alterado parâmetro @AssociadoCategoriaId como opcional.	*
*                 Adicionado parâmetro @AssociadoId.	                    *
USP_SEL_Associado 2, 3, ''
USP_SEL_Associado null, null, null, true, 1171
****************************************************************************/

ALTER Procedure [dbo].[USP_SEL_Associado](
	@SiteId	Int = Null,
	@AssociadoCategoriaId Int = Null,
	@LetraInicial VarChar(50) = null,
	@ExibirTodos Bit = 0,
	@AssociadoId Int = Null
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
		A.AssociadoId,
		A.SiteId,
		A.Nome,
		A.Resumo,
		A.AssociadoCategoriaId,
		A.TipoPessoaId,
		A.PaisId,
		P.Nome Pais,
		AC.Descricao AssociadoCategoria,
		TP.Descricao TipoPessoa
	From
		tblAssociado A
		Left Join tblPais P On P.PaisId = A.PaisId
		Left Join tblAssociadoCategoria AC On AC.AssociadoCategoriaId = A.AssociadoCategoriaId
		Left Join tblTipoPessoa TP On TP.TipoPessoaId = A.TipoPessoaId
	Where
		((@SiteId Is Null) Or (A.SiteId = @SiteId))
		And ((@AssociadoCategoriaId Is Null) Or (A.AssociadoCategoriaId = @AssociadoCategoriaId))
		And ((@ExibirTodos = 1) Or (A.Nome Like '' + @LetraInicial + '%'))
		And ((@AssociadoId Is Null) Or (A.AssociadoId = @AssociadoId))
	
End