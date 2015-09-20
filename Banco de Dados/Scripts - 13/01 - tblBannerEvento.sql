Drop Table tblBannerClique
Go

Create Table tblBannerEventoTipo(
	BannerEventoTipoId	Int Not null,
	Descricao			VarChar(80),
	Constraint PK_tblBannerEventoTipo Primary Key(BannerEventoTipoId)
)
Go

Insert Into tblBannerEventoTipo(BannerEventoTipoId, Descricao) Values(1, 'View')
Insert Into tblBannerEventoTipo(BannerEventoTipoId, Descricao) Values(2, 'Clique')
Go

Create Table tblBannerEvento(
	BannerEventoId		BigInt Identity not Null,
	BannerEventoTipoId	Int Not Null,
	BannerId			Int Not Null,
	ArquivoId			BigInt Not Null,
	DataEvento			DateTime Not Null,		
	Constraint PK_tblBannerEvento Primary Key(BannerEventoId),
	Constraint FK_tblBannerEvento_tblBannerEventoTipo Foreign Key(BannerEventoTipoId) References tblBannerEventoTipo(BannerEventoTipoId),
	Constraint FK_tblBannerEvento_tblBanner Foreign Key(BannerId) References tblBanner(BannerId),
	Constraint FK_tblBannerEvento_tblArquivo Foreign Key(ArquivoId) References tblArquivo(ArquivoId)
)
Go


Select * From tblBannerTipo
Insert Into tblBannerTipo(BannerTipoId, Descricao) Values(9,  'Home Inferior Lateral Rotativo')
Insert Into tblBannerTipo(BannerTipoId, Descricao) Values(10, 'Home Inferior Rotativo')