Create Table tblMailing(
	MailingId		Int Identity Not Null,
	SiteId			Int Not Null,
	Nome			VarChar(100),
	Email			VarChar(100),
	Segmento		VarChar(50),
	DataInclusao	DateTime,
	Ativo			Bit,
	Constraint PK_tblMailing Primary Key(MailingId),
	Constraint FK_tblMailing_tblSite Foreign Key(SiteId) References tblSite(SiteId)
)
Go