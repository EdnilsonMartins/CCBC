
/********
USP_SEL_MenuArquivo @MenuId = 508, @ArquivoCategoriaId = 0
*********/
Create Procedure [dbo].[USP_SEL_MenuArquivo](
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
		dbo.fnRetornaArquivoCategoria(A.ArquivoId) ListaCategoria
	From
		tblMenuArquivo PA
		join tblArquivo A On A.ArquivoId = PA.ArquivoId
		join tblArquivoArquivoCategoria AAC On AAC.ArquivoId = A.ArquivoId
	Where
		PA.MenuId = @MenuId
		And ((@ArquivoCategoriaId Is Null) Or (AAC.ArquivoCategoriaId = @ArquivoCategoriaId))

End

