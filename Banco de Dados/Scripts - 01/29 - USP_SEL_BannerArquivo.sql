
Create Procedure [dbo].[USP_SEL_BannerArquivo](
	@BannerId				Int = Null,
	@ArquivoCategoriaId	Int = Null
) As
Begin

	Select
		PA.BannerId,
		PA.ArquivoId,
		PA.DataInclusao,
		A.Content,
		A.Legenda,
		dbo.fnRetornaArquivoCategoria(A.ArquivoId) ListaCategoria
	From
		tblBannerArquivo PA
		join tblArquivo A On A.ArquivoId = PA.ArquivoId
		join tblArquivoArquivoCategoria AAC On AAC.ArquivoId = A.ArquivoId
	Where
		PA.BannerId = @BannerId
		And ((@ArquivoCategoriaId Is Null) Or (AAC.ArquivoCategoriaId = @ArquivoCategoriaId))

End

