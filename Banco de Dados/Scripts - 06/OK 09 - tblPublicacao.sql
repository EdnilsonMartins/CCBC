Alter Table tblPublicacao
Add Privado Bit Null
Go

Drop Table tblPublicacaoRestricaoUsuarioGrupo
Go

Create Table tblPublicacaoRestricaoUsuarioGrupo(
	PublicacaoRestricaoUsuarioGrupoId Int Identity Not Null,
	PublicacaoId Int Not Null,
	UsuarioGrupoId	Int Not Null,
	Constraint PK_tblPublicacaoRestricaoUsuarioGrupo Primary Key(PublicacaoRestricaoUsuarioGrupoId),
	Constraint FK_tblPublicacaoRestricaoUsuarioGrupo_tblPublicacao Foreign Key(PublicacaoId) References tblPublicacao(PublicacaoId),
	Constraint FK_tblPublicacaoRestricaoUsuarioGrupo_tblUsuarioGrupo Foreign Key(UsuarioGrupoId) References tblUsuarioGrupo(UsuarioGrupoId)
)
Go

Alter Table tblPublicacao
Alter Column Liberado Bit Null
Go

Alter Table tblPublicacao
Add  DataLiberado DateTime null
Go