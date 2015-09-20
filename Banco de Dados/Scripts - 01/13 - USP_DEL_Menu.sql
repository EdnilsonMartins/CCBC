Create Procedure [dbo].[USP_DEL_Menu](
	@MenuId Int
) As
Begin

	Declare @indErro bit,
			@msgErro VarChar(120)


	Set @indErro = 0
	Set @msgErro = ''

	--Delete From tblMenuRestricao Where MenuId = @MenuId 
	Delete From tblMenuIdiomaExcecao Where MenuId = @MenuId 
	Delete From tblMenu Where MenuId = @MenuId

	Select @indErro indErro, @msgErro msgErro

End