

/****** Object:  StoredProcedure [dbo].[USP_SEL_Arquivo]    Script Date: 13/05/2015 22:35:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER Procedure [dbo].[USP_SEL_Arquivo](
	@ArquivoId	Int = Null
) As
Begin

	Select
		A.ArquivoId,
		A.Content,
		A.Legenda,
		dbo.fnRetornaArquivoCategoria(A.ArquivoId) ListaCategoria,
		A.Tipo,
		--Convert(VarChar(max), content) Content64
		A.NomeArquivo
	From
		tblArquivo A
	Where
		((@ArquivoId Is Null) Or (ArquivoId = @ArquivoId))

End
