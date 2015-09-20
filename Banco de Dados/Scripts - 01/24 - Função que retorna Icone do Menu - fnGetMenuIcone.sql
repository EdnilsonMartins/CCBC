

Create Function [dbo].[fnGetMenuIcone](
	@MenuId		Int
) 
Returns BigInt As
Begin

	Declare @Saida BigInt
	
	Select 
		@Saida = A.ArquivoId
	From
		(
		Select
			Top 1 MA.ArquivoId ArquivoId
		From
			tblMenu M
			inner join tblMenuArquivo MA On MA.MenuId = M.MenuId 
			inner join tblArquivoArquivoCategoria AAC On AAC.ArquivoId = MA.ArquivoId
		Where
			M.MenuId = @MenuId
			And AAC.ArquivoCategoriaId In (4) --> 4 = Arquivos de Ícone de Menu.
		) A

	Return @Saida
End