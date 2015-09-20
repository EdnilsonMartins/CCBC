

Create Procedure [dbo].[USP_SEL_GeralArquivo](
	@PastaId	Int = Null
) As
Begin

	Select
		A.ArquivoId,
		A.Content,
		A.Legenda,
		--dbo.fnRetornaArquivoCategoria(A.ArquivoId) ListaCategoria,
		'' ListaCategoria,
		A.Tipo
	From
		tblArquivo A 
		--join tblArquivoArquivoCategoria AAC On AAC.ArquivoId = A.ArquivoId
	Where
		(@PastaId Is Null Or (@PastaId Is Not Null And A.PastaId = @PastaId))

End

