
/****** Object:  StoredProcedure [dbo].[USP_SEL_PublicacaoArquivo]    Script Date: 21/04/2015 20:25:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********
USP_SEL_PublicacaoArquivo @PublicacaoId = 34, @ArquivoCategoriaId = 3
*********/
ALTER Procedure [dbo].[USP_SEL_PublicacaoArquivo](
	@PublicacaoId		Int = Null,
	@ArquivoCategoriaId	Int = Null
) As
Begin

	Select
		PA.PublicacaoId,
		PA.ArquivoId,
		PA.DataInclusao,
		A.Content,
		A.Legenda,
		dbo.fnRetornaArquivoCategoria(A.ArquivoId) ListaCategoria,
		A.Tipo
	From
		tblPublicacaoArquivo PA
		join tblArquivo A On A.ArquivoId = PA.ArquivoId
		left join tblArquivoArquivoCategoria AAC On AAC.ArquivoId = A.ArquivoId
	Where
		PA.PublicacaoId = @PublicacaoId
		And ((@ArquivoCategoriaId Is Null) Or (AAC.ArquivoCategoriaId = @ArquivoCategoriaId))

End
