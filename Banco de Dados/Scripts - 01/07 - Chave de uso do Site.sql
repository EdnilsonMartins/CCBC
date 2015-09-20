Create Table tblSiteChave(
	SiteChaveId		Int Identity Not Null,
	SiteId			Int,
	Chave			VarChar(32),
	DataCadastro	DateTime,
	DataValidade	DateTime,
	Constraint PK_tblSiteCahve Primary Key(SiteChaveId),
	Constraint FK_tblSiteChave_tblSite Foreign Key(SiteId) References tblSite(SiteId)
)
Go