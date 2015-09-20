Create Table tblConfiguracao(
	ConfiguracaoId				Int Identity Not Null,
	SiteId						Int Not Null,
	ContatoTelefonePrincipal	VarChar(100),
	ContatoEmailPrincipal		VarChar(100),
	Localizacao					VarChar(100),
	LocalizacaoComplemento		VarChar(100),
	EmailHost					VarChar(100),
	EmailUsername				VarChar(100),
	EmailPassword				VarChar(100),
	EmailDisplayName			VarChar(100),
	Constraint PK_tblConfiguracao Primary Key(ConfiguracaoId),
	Constraint FK_tblConfiguracao_tblSite Foreign Key(SiteId) References tblSite(SiteId)
)
Go
