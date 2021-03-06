
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_SEL_Editoria												*
* Objetivo  : Carga de dados / Listagem das Editorias.						*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	:   /04/2015												*
* Descrição	    :															*
****************************************************************************/

Create Procedure [dbo].[USP_SEL_Editoria](
	@EditoriaId			Int = Null,
	@SiteId				Int = Null,
	@IdiomaId			Int = Null,
	@RetornarDefault		Bit = 1
) As

Begin
	
	Select
		E.EditoriaId,
		Case When IsNull(EIE.Descricao,'') = ''  And @RetornarDefault = 1 Then ED.Descricao Else EIE.Descricao	End Descricao,
		E.SiteId
	From
		tblEditoria E
		Left Join tblEditoriaIdiomaExcecao EIE On EIE.EditoriaId = E.EditoriaId and EIE.IdiomaId = @IdiomaId
		Left Join tblEditoriaIdiomaExcecao ED On ED.EditoriaId = E.EditoriaId And ED.IdiomaId = 1
	Where
		(@EditoriaId Is Not Null And E.EditoriaId = @EditoriaId)
		Or
		(@EditoriaId Is Null And E.SiteId = @SiteId)
End
