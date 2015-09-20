





--Drop Table tblPublicacaoTipoRegra

Create Table tblPublicacaoTipoRegra(
	PublicacaoTipoRegraId	Int Identity Not Null,
	PublicacaoTipoId		Int Not Null,
	RegraId					Int Not Null,
	Privado					Bit Null
	Constraint PK_tblPublicacaoTipoRegra Primary Key(PublicacaoTipoRegraId),
	Constraint FK_tblPublicacaoTipoRegra_tblPublicacaoTipo Foreign Key(PublicacaoTipoId) References tblPublicacaoTipo(PublicacaoTipoId),
	Constraint FK_tblPublicacaoTipoRegra_tblRegra Foreign Key(RegraId) References tblRegra(RegraId)
)
Go
