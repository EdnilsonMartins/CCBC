

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_SEL_Pasta													*
* Objetivo  : Carga de dados / Listagem das Pastas.							*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	:   /04/2015												*
* Descrição	    :															*
****************************************************************************/

Create Procedure [dbo].[USP_SEL_Pasta](
	@PastaId			Int = Null,
	@SiteId				Int = Null
) As

Begin
	
	Select
		P.PastaId,
		P.PastaPaiId,
		P.SiteId,
		P.Descricao,
		P.Posicao
	From
		tblPasta P
	Where
		(@PastaId Is Not Null And P.PastaId = @PastaId)
		Or
		(@PastaId Is Null And P.SiteId = @SiteId)
	Order By
		P.Posicao

End
