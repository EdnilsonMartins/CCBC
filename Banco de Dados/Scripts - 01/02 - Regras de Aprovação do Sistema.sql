Create Table tblRegraAprovacaoTipo(
	RegraAprovacaoTipoId	Int Identity Not Null,
	Descricao				VarChar(80),
	Constraint PK_tblRegraAprovacaoTipo Primary Key(RegraAprovacaoTipoId)
)
Go

Insert Into tblRegraAprovacaoTipo(Descricao) Values('Banner')
Insert Into tblRegraAprovacaoTipo(Descricao) Values('Evento')
Insert Into tblRegraAprovacaoTipo(Descricao) Values('Matéria')
Go

Create Table tblRegraAprovacaoCondicao(
	RegraAprovacaoCondicaoId	Int Not Null,
	Descricao					VarChar(80),
	Constraint PK_tblRegraAprovacaoCondicao Primary Key(RegraAprovacaoCondicaoId)
)
Go

Insert Into tblRegraAprovacaoCondicao(RegraAprovacaoCondicaoId, Descricao) Values(1, 'Uma das condições')
Insert Into tblRegraAprovacaoCondicao(RegraAprovacaoCondicaoId, Descricao) Values(2, 'Todas das condições')
Go

Create Table tblRegraAprovacao(
	RegraAprovacaoId			Int	Identity	Not Null,
	RegraAprovacaoTipoId		Int				Not Null,
	RegraAprovacaoCondicaoId	Int				Not Null,
	Data						DateTime		Not Null,
	Privado						Bit				Not Null,
	Constraint PK_tblRegraAprovacao	Primary Key(RegraAprovacaoId),
	Constraint FK_tblRegraAprovacao_tblRegraAprovacaoTipo Foreign Key(RegraAprovacaoTipoId) References tblRegraAprovacaoTipo(RegraAprovacaoTipoId),
	Constraint FK_tblRegraAprovacao_tblRegraAprovacaoCondicao Foreign Key(RegraAprovacaoCondicaoId) References tblRegraAprovacaoCondicao(RegraAprovacaoCondicaoId)
)
Go
 
Create Table tblRegraAprovacaoItem(
	RegraAprovacaoItemId	Int Identity	Not Null,
	RegraAprovacaoId		Int				Not Null,
	UsuarioGrupoId			Int				Null,
	UsuarioId				Int				Null,
	QuantidadeMinimaUsuario	Int				Null,
	Sequencia				Int				Null,
	Constraint PK_tblRegraAprovacaoItem Primary Key(RegraAprovacaoItemId),
	Constraint FK_tblRegraAprovacaoItem_tblRegraAprovacao Foreign Key(RegraAprovacaoId) References tblRegraAprovacao(RegraAprovacaoId),
	Constraint FK_tblRegraAprovacaoItem_tblUsuarioGrupo Foreign Key(UsuarioGrupoId) References tblUsuarioGrupo(UsuarioGrupoId),
	Constraint FK_tblRegraAprovacaoItem_tblUsuario Foreign Key(UsuarioId) References tblUsuario(UsuarioId)
)


