
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[USP_INS_EditoriaIdiomaExcecao](
	@EditoriaIdiomaExcecaoId	Int,
	@EditoriaId 				Int,
	@IdiomaId				Int,
	@Descricao				VarChar(255) = Null
)
As
Begin

	If Exists(Select 1 From tblEditoriaIdiomaExcecao Where EditoriaId = @EditoriaId And IdiomaId = @IdiomaId) Begin
		Update 
			tblEditoriaIdiomaExcecao 
		Set 
			Descricao = @Descricao				
		Where 
			EditoriaId = @EditoriaId
			And IdiomaId = @IdiomaId
		Select EditoriaIdiomaExcecaoId From tblEditoriaIdiomaExcecao Where EditoriaIdiomaExcecaoId = @EditoriaIdiomaExcecaoId
	End Else Begin

		Insert Into tblEditoriaIdiomaExcecao(EditoriaId, IdiomaId, Descricao)
		Values(@EditoriaId, @IdiomaId, @Descricao)
		Select Cast(@@Identity As int) EditoriaIdiomaExcecaoId
	End

End