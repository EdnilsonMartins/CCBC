Create Table tblAssociado(
	AssociadoId		Int Identity Not Null,
	SiteId			Int,
	Nome			VarChar(120),
	Resumo			VarChar(1000),
	Constraint PK_tblAssociado Primary Key(AssociadoId),
	Constraint FK_tblAssociado_tblSite Foreign Key(SiteId) References tblSite(SiteId)
)
Go