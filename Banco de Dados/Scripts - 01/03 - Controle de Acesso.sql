Create Table tblSistema(
	SistemaId	Int Not Null,
	Descricao	VarChar(50),
	Constraint PK_tblSistema Primary Key(SistemaId)
)
Go

Insert Into tblSistema(SistemaId, Descricao) Values(1, 'Backoffice CMS')
Go

Create Table tblFuncionalidadeGrupo(
	FuncionalidadeGrupoId	Int Not Null,
	SistemaId			Int Not Null,
	Nome				VarChar(80) Null,
	Constraint PK_tblFuncionalidadeGrupo Primary Key(FuncionalidadeGrupoId),
	Constraint FK_tblFuncionalidadeGrupo_tblSistema Foreign Key(SistemaId) References tblSistema(SistemaId)
)
Go

Create Table tblFuncionalidade(
	FuncionalidadeId	Int Not Null,
	FuncionalidadeGrupoId			Int Not Null,
	Nome				VarChar(80) Null,
	Descricao			VarChar(80) Null,
	Constraint PK_tblFuncionalidade Primary Key(FuncionalidadeId),
	Constraint FK_tblFuncionalidade_tblFuncionalidadeGrupo Foreign Key(FuncionalidadeGrupoId) References tblFuncionalidadeGrupo(FuncionalidadeGrupoId)
)
Go

Create Table tblUsuarioSistema(
	UsuarioSistemaId	Int Identity Not Null,
	UsuarioId			Int Not Null,
	SistemaId			Int Not Null,
	Ativo				Bit Null,
	Constraint PK_tblUsuarioSistema Primary Key(UsuarioSistemaId),
	Constraint FK_tblUsuarioSistema_tblUsuario Foreign Key(UsuarioId) References tblUsuario(UsuarioId),
	Constraint FK_tblUsuarioSistema_tblSistema Foreign Key(SistemaId) References tblSistema(SistemaId)
)
Go

Create Table tblUsuarioFuncionalidade(
	UsuarioFuncionalidadeId	Int Identity Not Null,
	UsuarioId			Int Not Null,
	FuncionalidadeId	Int Not Null,
	Ativo				Bit Null,
	Parametro			VarChar(80),
	Constraint PK_tblUsuarioFuncionalidade Primary Key(UsuarioFuncionalidadeId),
	Constraint FK_tblUsuarioFuncionalidade_tblUsuario Foreign Key(UsuarioId) References tblUsuario(UsuarioId),
	Constraint FK_tblUsuarioFuncionalidade_tblSistema Foreign Key(FuncionalidadeId) References tblFuncionalidade(FuncionalidadeId)
)
Go

