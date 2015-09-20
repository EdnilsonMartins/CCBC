
Alter Table tblUsuarioGrupo
Add SiteId Int Null,
Constraint FK_tblUsuarioGrupo_tblSite Foreign Key(SiteId) References tblSite(SiteId)
Go


Alter Table tblUsuario
Add SiteId Int,
Constraint FK_tblUsuario_tblSite Foreign Key(SiteId) References tblSite(SiteId)
Go


Update tblUsuario Set SiteId = 1


Alter Table tblRegra
Add SiteId Int
Constraint FK_tblRegra_tblSite Foreign Key(SiteId) References tblSite(SiteId)
Go

Update tblRegra Set SiteId = 1

Alter Table tblRegra
Alter Column SiteId Int Not Null