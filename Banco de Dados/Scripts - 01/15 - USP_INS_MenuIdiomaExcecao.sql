Create Procedure [dbo].[USP_INS_MenuIdiomaExcecao](
	@MenuIdiomaExcecaoId	Int,
	@MenuId 				Int,
	@IdiomaId				Int,
	@Rotulo					VarChar(50) = Null
)
As
Begin

	If Exists(Select 1 From tblMenuIdiomaExcecao Where MenuId = @MenuId And IdiomaId = @IdiomaId) Begin
		Update 
			tblMenuIdiomaExcecao 
		Set 
			Rotulo = @Rotulo					
		Where 
			MenuId = @MenuId
			And IdiomaId = @IdiomaId
		Select MenuIdiomaExcecaoId From tblMenuIdiomaExcecao Where MenuId = @MenuId And IdiomaId = @IdiomaId
	End Else Begin

		Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo)
		Values(@MenuId, @IdiomaId, @Rotulo)
		Select Cast(@@Identity As int) MenuIdiomaExcecaoId
	End

End