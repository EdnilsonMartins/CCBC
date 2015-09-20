

/****** Object:  StoredProcedure [dbo].[USP_SEL_GeralArquivo]    Script Date: 13/05/2015 22:45:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER Procedure [dbo].[USP_SEL_GeralArquivo](
	@PastaId	Int = Null
) As
Begin

	Select
		A.ArquivoId,
		A.Content,
		A.Legenda,
		--dbo.fnRetornaArquivoCategoria(A.ArquivoId) ListaCategoria,
		'' ListaCategoria,
		A.Tipo,
		A.NomeArquivo
	From
		tblArquivo A 
		--join tblArquivoArquivoCategoria AAC On AAC.ArquivoId = A.ArquivoId
	Where
		(@PastaId Is Null Or (@PastaId Is Not Null And A.PastaId = @PastaId))

End

