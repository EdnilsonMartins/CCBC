
/****** Object:  StoredProcedure [dbo].[USP_SEL_BannerArquivo]    Script Date: 21/04/2015 20:30:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER Procedure [dbo].[USP_SEL_BannerArquivo](
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
		dbo.fnRetornaArquivoCategoria(A.ArquivoId) ListaCategoria,
		A.Tipo
	From
		tblBannerArquivo PA
		join tblArquivo A On A.ArquivoId = PA.ArquivoId
		left join tblArquivoArquivoCategoria AAC On AAC.ArquivoId = A.ArquivoId
	Where
		PA.BannerId = @BannerId
		And ((@ArquivoCategoriaId Is Null) Or (AAC.ArquivoCategoriaId = @ArquivoCategoriaId))

End

