
Create Procedure [dbo].[USP_INS_MenuArquivo](
	@MenuId		Int,
	@ArquivoId	Int
)
As
Begin
 
	Declare @MenuArquivoId BigInt
	Select @MenuArquivoId = MenuArquivoId From tblMenuArquivo Where MenuId = @MenuId And ArquivoId = @ArquivoId

	If @MenuArquivoId Is Null Begin
		Insert Into tblMenuArquivo(MenuId, ArquivoId) Values(@MenuId, @ArquivoId)
	End

	Select @@IDENTITY MenuArquivoId

End
