
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_DEL_Editoria												*
* Objetivo  : Exclusão de Editoria											*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	:   /04/2015	                                            *
* Descrição	    :															*
*                                                                           *
****************************************************************************/
Create Procedure [dbo].[USP_DEL_Editoria](
	@EditoriaId Int
) As
Begin

	Declare @indErro bit,
			@msgErro VarChar(120)

	Set @indErro = 0
	Set @msgErro = ''

	Delete From tblEditoriaIdiomaExcecao Where EditoriaId = @EditoriaId
	Delete From tblEditoria Where EditoriaId = @EditoriaId
	
	Select @indErro indErro, @msgErro msgErro

End