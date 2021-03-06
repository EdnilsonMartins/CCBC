
/****** Object:  StoredProcedure [dbo].[USP_SEL_MenuArquivo]    Script Date: 21/04/2015 20:31:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/********
USP_SEL_MenuArquivo @MenuId = 508, @ArquivoCategoriaId = 4
*********/
ALTER Procedure [dbo].[USP_SEL_MenuArquivo](
	@MenuId				Int = Null,
	@ArquivoCategoriaId	Int = Null
) As
Begin

	Select
		PA.MenuId,
		PA.ArquivoId,
		PA.DataInclusao,
		A.Content,
		A.Legenda,
		dbo.fnRetornaArquivoCategoria(A.ArquivoId) ListaCategoria,
		A.Tipo
	From
		tblMenuArquivo PA
		join tblArquivo A On A.ArquivoId = PA.ArquivoId
		left join tblArquivoArquivoCategoria AAC On AAC.ArquivoId = A.ArquivoId
	Where
		PA.MenuId = @MenuId
		And ((@ArquivoCategoriaId Is Null) Or (AAC.ArquivoCategoriaId = @ArquivoCategoriaId))

End

