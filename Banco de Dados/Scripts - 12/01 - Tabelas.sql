Create Table tblEditoria(
	EditoriaId	Int Identity Not Null,
	SiteId		Int Not Null,
	Constraint PK_tblEditoria Primary Key(EditoriaId),
	Constraint FK_tblEditoria_tblSite Foreign Key(SiteId) References tblSite(SiteId)
)
Go

Create Table tblEditoriaIdiomaExcecao(
	EditoriaIdiomaExcecaoId	Int Identity Not Null,
	EditoriaId				Int,
	IdiomaId				Int,
	Descricao				VarChar(255),
	Constraint PK_tblEditoriaIdiomaExcecao Primary Key(EditoriaIdiomaExcecaoId),
	Constraint FK_tblEditoriaIdiomaExcecao_tblEditoria Foreign Key(EditoriaId) References tblEditoria(EditoriaId),
	Constraint FK_tblEditoriaIdiomaExcecao_tblIdioma Foreign Key(IdiomaId) References tblIdioma(IdiomaId)
)
Go

Alter Table tblPublicacao
Add EditoriaId Int Null,
Constraint FK_tblPublicidade_tblEditoria Foreign Key(EditoriaId) References tblEditoria(EditoriaId)
Go

Alter Table tblArquivo
Add PastaId Int Null,
Constraint FK_tblArquivo_tblPasta Foreign Key(PastaId) References tblPasta(PastaId)
Go