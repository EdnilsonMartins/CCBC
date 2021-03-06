
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_INS_Editoria												*
* Objetivo  : Manutenção das Editorias.										*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	:   /04/2015												*
* Descrição	    :															*
****************************************************************************/

Create Procedure [dbo].[USP_INS_Editoria](
	@EditoriaId			Int = 0,
	@SiteId				Int
)
As
Begin

	If @EditoriaId <> 0 Begin
		--Update 
		--	tblEditoria
		--Set 
		--	Descricao = @Descricao
		--Where 
		--	EditoriaId = @EditoriaId
		Select @EditoriaId EditoriaId
	End Else Begin

		Insert Into tblEditoria(SiteId) 
		Values(@SiteId)
		Select Cast(@@Identity As int) EditoriaId
	End

End