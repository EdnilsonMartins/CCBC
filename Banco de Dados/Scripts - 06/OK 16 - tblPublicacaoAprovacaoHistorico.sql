Alter Table tblPublicacaoAprovacaoHistorico
Add Descricao VarChar(80) Null

Alter Table tblPublicacaoAprovacaoHistorico
Add UsuarioId Int Null
Go

Alter Table tblPublicacaoAprovacaoHistorico
Add Constraint FK_tblPublicacaoAprovacaoHistorico_tblUsuario Foreign Key(UsuarioId) References tblUsuario(UsuarioId)
Go

Alter Table tblPublicacaoAprovacaoHistorico
Alter Column Liberado Bit Null
Go