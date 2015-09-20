
-- prod 21:56
Alter Table tblUsuarioGrupo
Add UsuarioGrupoPaiId Int,
Constraint FK_tblUsuarioGrupo_tblUsuarioGrupo Foreign Key(UsuarioGrupoPaiId) References tblUsuarioGrupo(UsuarioGrupoId)
Go


Create Table tblMenuRestricaoUsuario(
	MenuRestricaoUsuarioId	Int Identity Not Null,
	MenuId Int Null,
	UsuarioId Int Null,
	Constraint PK_tblMenuRestricaoUsuario Primary Key(MenuRestricaoUsuarioId),
	Constraint FK_tblMenuRestricaoUsuario_tblMenu Foreign Key(MenuId) References tblMenu(MenuId),
	Constraint FK_tblMenuRestricaoUsuario_tblUsuario Foreign Key(UsuarioId) References tblUsuario(UsuarioId)
)
Go


Alter Table tblPublicacao
Add Tags VarChar(4000)
Go