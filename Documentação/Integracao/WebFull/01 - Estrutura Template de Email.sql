

Create Table tblEmailGrupoTemplate(
	EmailGrupoTemplateId	Int Not Null,
	Descricao				VarChar(60) Not Null,
	Comentario				VarChar(500) Null,
	Constraint PK_tblEmailGrupoTemplate Primary Key(EmailGrupoTemplateId)
)

Create Table tblEmailTemplate(
	EmailTemplateId			BigInt Identity Not Null,
	SiteId					Int Not Null,
	EmailGrupoTemplateId	Int Not Null,
	Comentario				VarChar(500) Not Null,
	Assunto					VarChar(120) Null,
	Corpo					VarChar(Max) Null,
	Constraint PK_tblEmailTemplate Primary Key(EmailTemplateId),
	Constraint FK_tblEmailTemplate_tblSite Foreign Key(SiteId) References tblSite(SiteId),
	Constraint FK_tblEmailTemplate_tblEmailGrupoTemplate Foreign Key(EmailGrupoTemplateId) References tblEmailGrupoTemplate(EmailGrupoTemplateId)
)

Create Table tblEmailTemplateCampo(
	EmailTemplateCampoId	BigInt Identity Not Null,
	EmailTemplateId			BigInt Not Null,
	Nome					VarChar(80) Not Null,
	Tag						VarChar(80) Not Null,
	Constraint PK_tblEmailTemplateCampo Primary Key(EmailTemplateCampoId),
	Constraint FK_tblEmailTemplateCampo_tblEmailTemplate Foreign Key(EmailTemplateId) References tblEmailTemplate(EmailTemplateId)
)


Create Table tblTedescoStatus(
	TedescoStatusId Int Not Null,
	Descricao		VarChar(30) Not Null,
	Constraint PK_tblTedescoStatus Primary Key(TedescoStatusId)
)

Insert Into tblTedescoStatus(TedescoStatusId, Descricao) Values(1, 'PRE-CADASTRO')
Insert Into tblTedescoStatus(TedescoStatusId, Descricao) Values(2, 'CONFIRMADO')



Alter Table tblUsuario
Add TedescoUltimaNotificacao DateTime Null,
    TedescoStatusId Int Null,
    TedescoDataConfirmacao DateTime Null

Alter Table tblUsuario
Add Constraint FK_tblUsuario_tblTedescoStatus Foreign Key(TedescoStatusId) References tblTedescoStatus(TedescoStatusId)


Insert Into tblEmailGrupoTemplate(EmailGrupoTemplateId, Descricao, Comentario) Values(1, 'Administrativo/Sistema', 'Diversos templates de e-mail utilizados pelo sistema para envio de senha, procedimentos e outros')
Select * From tblEmailGrupoTemplate


-- Templates de E-mail.
Set Identity_Insert tblEmailTemplate On
Insert Into tblEmailTemplate(EmailTemplateId, SiteId, EmailGrupoTemplateId, Comentario, Assunto, Corpo) Values(1, 2, 1, 'Instruções enviadas ao usuário para conclusão do cadastro via Portal', 'Complete seu cadastro', 'Bem-vindo <%Nome%><br /><br />Clique <%Link%> para completar seu cadastro e tenha acesso a conteúdo exclusivo.')
Insert Into tblEmailTemplate(EmailTemplateId, SiteId, EmailGrupoTemplateId, Comentario, Assunto, Corpo) Values(2, 2, 1, 'Instruções de uso do portal enviado logo após conclusão do cadastro do usuário', 'Dicas do Portal', 'Bem-vindo <%Nome%><br /><br />Confira as dicas para utilização do portal.')
Insert Into tblEmailTemplate(EmailTemplateId, SiteId, EmailGrupoTemplateId, Comentario, Assunto, Corpo) Values(3, 2, 1, 'Link para troca de senha', 'Redefinição de senha', 'Olá <%Nome%><br /><br />Clique no link abaixo para redefinir sua senha no portal <%Link%>')
Insert Into tblEmailTemplate(EmailTemplateId, SiteId, EmailGrupoTemplateId, Comentario, Assunto, Corpo) Values(4, 2, 1, 'Notificação ao Administrador de contas dos usuários', 'Usuário Cadastrado', 'Administrador,<br /><br />O seguinte usuário completou o cadastro no portal: <%Nome%>')
Set Identity_Insert tblEmailTemplate Off
Select * From tblEmailTemplate


-- Registro dos campos dos Templates
Select * From tblEmailTemplateCampo

Insert Into tblEmailTemplateCampo(EmailTemplateId, Nome, Tag) Values(1, 'Nome', '<%Nome%>')
Insert Into tblEmailTemplateCampo(EmailTemplateId, Nome, Tag) Values(1, 'Link', '<%Link%>')

Insert Into tblEmailTemplateCampo(EmailTemplateId, Nome, Tag) Values(2, 'Nome', '<%Nome%>')

Insert Into tblEmailTemplateCampo(EmailTemplateId, Nome, Tag) Values(3, 'Nome', '<%Nome%>')
Insert Into tblEmailTemplateCampo(EmailTemplateId, Nome, Tag) Values(3, 'Link', '<%Link%>')

Insert Into tblEmailTemplateCampo(EmailTemplateId, Nome, Tag) Values(4, 'Nome', '<%Nome%>')



