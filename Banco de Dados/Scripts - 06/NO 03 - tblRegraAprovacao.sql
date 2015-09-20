Alter Table tblRegraAprovacao
Drop Column Privado
Go

Alter Table tblRegraAprovacao
Add Descricao VarChar(200) Null
Go

Alter Table tblRegraAprovacao
Drop Constraint FK_tblRegraAprovacao_tblRegraAprovacaoCondicao
Go

Alter Table tblRegraAprovacao
Drop Column RegraAprovacaoCondicaoId
Go


Delete From tblBannerRegra
Delete From tblRegraAprovacaoItem
Delete From tblRegraAprovacao
