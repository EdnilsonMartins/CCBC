
Drop Table tblRegraAprovacaoTipo

Create Table tblRegraTipo(
	RegraTipoId	Int Not Null,
	Descricao	VarChar(80) Not Null,
	Constraint PK_tblRegraTipo Primary Key(RegraTipoId)
)
Go

Insert Into tblRegraTipo Values(1, 'Banner')
Insert Into tblRegraTipo Values(2, 'Evento')
Insert Into tblRegraTipo Values(3, 'Matéria')
Go


Drop Table tblRegraAprovacao

Create Table tblRegra(
	RegraId			Int Identity Not Null,
	RegraTipoId		Int Not Null,
	Descricao		VarChar(200) Not Null,
	DataCadastro	DateTime Default GetDate(),
	Constraint PK_tblRegra Primary Key(RegraId),
	Constraint FK_tblRegra_tblRegraTipo Foreign Key(RegraTipoId) References tblRegraTipo(RegraTipoId)
)
Go

Create Table tblRegraPasso(
	RegraPassoId					Int Identity Not Null,
	RegraId							Int Not Null,
	Sequencia						Int Not Null,				
	Descricao						VarChar(200) Null,
	QuantidadeMinimaUsuariosDoGrupo	Int Null,
	Constraint PK_tblRegraPasso Primary Key(RegraPassoId),
	Constraint FK_tblRegraPasso_tblRegra Foreign Key(RegraId) References tblRegra(RegraId)
)
Go


Drop Table tblRegraAprovacaoCondicao
Drop Table tblRegraPassoCondicao

Create Table tblRegraPassoCondicao(
	RegraPassoCondicaoId	Int Identity Not Null,
	RegraPassoId			Int Not Null,
	UsuarioGrupoId			Int Null,
	UsuarioId				Int Null,
	QuantidadeMinimaUsuarios Int Null,
	Constraint PK_tblRegraPassoCondicao Primary Key(RegraPassoCondicaoId),
	Constraint FK_tblRegraPassoCondicao_tblRegraPasso Foreign Key(RegraPassoId) References tblRegraPasso(RegraPassoId),
	Constraint FK_tblRegraPassoCondicao_tblUsuarioGrupo Foreign Key(UsuarioGrupoId) References tblUsuarioGrupo(UsuarioGrupoId),
	Constraint Fk_tblRegraPassoCondicao_tblUsuario Foreign Key(UsuarioId) References tblUsuario(UsuarioId)
)

