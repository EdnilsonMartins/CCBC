Create Table tblPublicacaoRestricaoUsuario(
	PublicacaoRestricaoUsuarioId	Int Identity Not Null,
	PublicacaoId						Int Not Null,
	UsuarioId					Int,
	Constraint PK_tblPublicacaoRestricaoUsuario Primary Key(PublicacaoRestricaoUsuarioId),
	Constraint FK_tblPublicacaoRestricaoUsuario_tblPublicacao Foreign Key(PublicacaoId) References tblPublicacao(PublicacaoId),
	Constraint FK_tblPublicacaoRestricaoUsuario_tblUsuario Foreign Key(UsuarioId) References tblUsuario(UsuarioId)
)
Go

Create Table tblPublicacaoAprovacaoHistorico(
	PublicacaoAprovacaoHistoricoId	Int Identity Not Null,
	PublicacaoId					Int Not Null,
	DataLiberacao				DateTime Not Null,
	Liberado					Bit Not Null,
	Constraint PK_tblPublicacaoAprovacaoHistorico Primary Key(PublicacaoAprovacaoHistoricoId),
	Constraint FK_tblPublicacaoAprovacaoHistorico_tblPublicacao Foreign Key(PublicacaoId) References tblPublicacao(PublicacaoId)
)
Go

Create Table tblPublicacaoAprovacaoItem(
	PublicacaoAprovacaoItemId	Int Identity	Not Null,
	PublicacaoId			Int				Not Null,
	UsuarioId			Int				Not Null,
	DataAprovacao		DateTime		Not Null,
	Liberado			Bit				Null,
	Constraint PK_tblPublicacaoAprovacao Primary Key(PublicacaoAprovacaoItemId),
	Constraint FK_tblPublicacaoAprovacao_tblBanner Foreign Key(PublicacaoId) References tblPublicacao(PublicacaoId),
	Constraint FK_tblPublicacaoAprovacao_tblUsuario Foreign Key(UsuarioId) References tblUsuario(UsuarioId)
)
Go