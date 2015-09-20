Alter Table tblMenu
add TargetId Int Null
Go

Alter Table tblMenu
Add Constraint FK_tblMenu_tblTarget Foreign Key(TargetId) References tblTarget(TargetId)
Go