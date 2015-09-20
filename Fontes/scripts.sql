
Use [dbCCBC]
Go

/********************************************************************
*	1.	tblSite														*
********************************************************************/
-- Drop Table tblSite
Create Table tblSite(
	SiteId	Int,
	Descricao VarChar(80),
	Constraint PK_tblSite Primary Key(SiteId)
)
Go

Grant Select on tblSite to usuCCBC

Insert Into tblSite(SiteId, Descricao) Values(1, 'CCBC')
Insert Into tblSite(SiteId, Descricao) Values(2, 'CAM-CCBC')
Insert Into tblSite(SiteId, Descricao) Values(3, 'INSTITUTO CULTURAL')
Insert Into tblSite(SiteId, Descricao) Values(4, 'AMIGOS DO CANADA')

Select * From tblSite
Go


/********************************************************************
*	2.	tblIdioma													*
********************************************************************/
-- Drop Table tblIdioma
Create Table tblIdioma(
	IdiomaId	Int Not Null,
	Descricao	VarChar(20),
	Sigla		VarChar(5),
	Constraint PK_tblIdioma Primary Key(IdiomaId)
)
Go

Grant Select On tblIdioma To usuCCBC

Insert Into tblIdioma(IdiomaId, Descricao, Sigla) Values(1, 'PORTUGUES', 'PT')
Insert Into tblIdioma(IdiomaId, Descricao, Sigla) Values(2, 'ESPANHOL', 'ES')
Insert Into tblIdioma(IdiomaId, Descricao, Sigla) Values(3, 'ENGLISH', 'EN')
Go

Select * From tblIdioma


/********************************************************************
*	3.	tblMenuTipo													*
********************************************************************/
Create Table tblMenuTipo(
	MenuTipoId Int,
	Descricao VarChar(50),
	Constraint PK_tblMenuTipo Primary Key(MenuTipoId)
)
Go

Grant Select on tblMenuTipo to usuCCBC

Insert Into tblMenuTipo(MenuTipoId, Descricao) Values(1, 'Normal')
Insert Into tblMenuTipo(MenuTipoId, Descricao) Values(2, 'Quick')

Select * From tblMenuTipo


/********************************************************************
*	4.	tblMenuTipoAcao												*
********************************************************************/

Create Table tblMenuTipoAcao(
	MenuTipoAcaoId Int,
	Descricao VarChar(50),
	Constraint PK_tblMenuTipoAcao Primary Key(MenuTipoAcaoId)
)
Go

Grant Select on tblMenuTipoAcao to usuCCBC

Insert Into tblMenuTipoAcao(MenuTipoAcaoId, Descricao) Values(1, 'Fixo')
Insert Into tblMenuTipoAcao(MenuTipoAcaoId, Descricao) Values(2, 'Conteudo')

Select * From tblMenuTipoAcao


/********************************************************************
*	5.	tblPublicacaoTipo											*
********************************************************************/
-- Drop Table tblPublicacaoTipo
Create Table tblPublicacaoTipo(
	PublicacaoTipoId	Int Not Null,
	Descricao			VarChar(80),
	Constraint PK_tblPublicacaoTipo Primary Key(PublicacaoTipoId)
)
Go

Grant Select on tblPublicacaoTipo to usuCCBC

Insert Into tblPublicacaoTipo(PublicacaoTipoId, Descricao) Values(1,'Evento')
Insert Into tblPublicacaoTipo(PublicacaoTipoId, Descricao) Values(2,'Notícia')
Insert Into tblPublicacaoTipo(PublicacaoTipoId, Descricao) Values(3,'Matéria')
Insert Into tblPublicacaoTipo(PublicacaoTipoId, Descricao) Values(4,'Artigo')
Insert Into tblPublicacaoTipo(PublicacaoTipoId, Descricao) Values(5,'Página')
--Insert Into tblPublicacaoTipo(PublicacaoTipoId, Descricao) Values(6,'Banner') -- ???

Select * From tblPublicacaoTipo


/********************************************************************
*	6.	tblPublicacao												*
*   7.  tblPublicacaoIdiomaExcecao									*
********************************************************************/
-- Drop Table tblPublicacao
Create Table tblPublicacao(
	PublicacaoId		Int Identity Not Null,
	SiteId				Int	Not Null,
	PublicacaoTipoId	Int Null,
	--Titulo			VarChar(80),	-- Campo movido para a table tblPublicacaoIdiomaExcecao.
	--Resumo			VarChar(200),	-- Campo movido para a table tblPublicacaoIdiomaExcecao.
	--Conteudo			VarChar(Max),	-- Campo movido para a table tblPublicacaoIdiomaExcecao.
	--Editora			VarChar(50),	-- Campo movido para a table tblPublicacaoIdiomaExcecao.
	--Fonte				VarChar(120),	-- Campo movido para a table tblPublicacaoIdiomaExcecao.
	--Tags				VarChar(500),	-- Campo movido para a table tblPublicacaoIdiomaExcecao.
	Data				DateTime,
	DataValidade		DateTime,
	--Grupo				V
	Destaque			Bit Default 0,
	--Relacionamento
	Posicao					Int,
	Constraint PK_tblPublicacao Primary Key(PublicacaoId),
	Constraint FK_tblPublicacao_tblSite Foreign Key(SiteId) References tblSite(SiteId),
	Constraint FK_tblPublicacao_tblPublicacaoTipo Foreign Key(PublicacaoTipoId) References tblPublicacaoTipo(PublicacaoTipoId)
)
Go
Alter Table tblPublicacao
Add Destaque Bit Default 0
Go
Grant Select on tblPublicacao to usuCCBC

-- Drop Table tblPublicacaoIdiomaExcecao
Create Table tblPublicacaoIdiomaExcecao(
	PublicacaoIdiomaExcecaoId	Int Identity Not Null,
	PublicacaoId				Int Not Null,
	IdiomaId					Int Not Null,
	Titulo						VarChar(80),
	Resumo						VarChar(200),
	Conteudo					VarChar(Max),
	Editora						VarChar(50),
	Fonte						VarChar(120),
	Tags						VarChar(500),
	Constraint PK_tblPublicacaoIdiomaExcecao Primary Key(PublicacaoIdiomaExcecaoId),
	Constraint FK_tblPublicacaoIdiomaExcecao_tblMenu Foreign Key(PublicacaoId) References tblPublicacao(PublicacaoId),
	Constraint FK_tblPublicacaoIdiomaExcecao_tblIdioma Foreign Key(IdiomaId) References tblIdioma(IdiomaId)
)
Grant Select on tblPublicacaoIdiomaExcecao to usuCCBC

Set Identity_Insert tblPublicacao On
Insert Into tblPublicacao(PublicacaoId, SiteId, PublicacaoTipoId, Data, DataValidade, Posicao) Values(1, 2, 3, '2014-12-27', Null, 1)
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo, Editora, Fonte, Tags) Values(1, 1, 'Centro de Arbitragem e Medição', 'resuminho pra ajudar...', '<p>Aqui começa o parágrafo... blablabla</p><p>Mais um só pra testar.</p><p>E para finalizar um tchau tchau!!!!</p>', Null, Null, Null)
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo, Editora, Fonte, Tags) Values(1, 2, 'Centro de Ajueda dos Puertos', 'Resumito pra ti', 'yo no ablo espanhol', Null, Null, Null)
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo, Editora, Fonte, Tags) Values(1, 3, 'Rules Center', 'Summary', '<p>Here is the first line... blablabla</p><p>One more line to test the language translator.</p><p>And to conclude, I have to say bye bye to you!!!!</p>', Null, Null, Null)


Insert Into tblPublicacao(PublicacaoId, SiteId, PublicacaoTipoId, Data, DataValidade, Posicao) Values(2, 2, 2, '2014-12-28', Null, 1)
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo, Editora, Fonte, Tags) Values(2, 1, 'Título Notícia 1 PT', 'Resuminho Notícia 1 PT', '<p>Parágrafo da notícia 1 PT</p><p>Mais um parágrafo PT</p><p>E para finalizar um tchau tchau - PT!!!!</p>', Null, Null, Null)
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo, Editora, Fonte, Tags) Values(2, 2, 'Título Notícia 1 ES', 'Resuminho Notícia 1 ES', '<p>Parágrafo da notícia 1 ES</p><p>Mais um parágrafo ES</p><p>E para finalizar um tchau tchau - ES!!!!</p>', Null, Null, Null)
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo, Editora, Fonte, Tags) Values(2, 3, 'Título Notícia 1 EN', 'Resuminho Notícia 1 EN', '<p>Parágrafo da notícia 1 EN</p><p>Mais um parágrafo EN</p><p>E para finalizar um tchau tchau - EN!!!!</p>', Null, Null, Null)

Insert Into tblPublicacao(PublicacaoId, SiteId, PublicacaoTipoId, Data, DataValidade, Posicao) Values(3, 2, 2, '2014-12-28', Null, 1)
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo, Editora, Fonte, Tags) Values(3, 1, 'Título Notícia 2 PT', 'Resuminho Notícia 2 PT', '<p>Parágrafo da notícia 2 PT</p><p>Mais um parágrafo PT</p><p>E para finalizar um tchau tchau - PT!!!!</p>', Null, Null, Null)
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo, Editora, Fonte, Tags) Values(3, 2, 'Título Notícia 2 ES', 'Resuminho Notícia 2 ES', '<p>Parágrafo da notícia 2 ES</p><p>Mais um parágrafo ES</p><p>E para finalizar um tchau tchau - ES!!!!</p>', Null, Null, Null)
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo, Editora, Fonte, Tags) Values(3, 3, 'Título Notícia 2 EN', 'Resuminho Notícia 2 EN', '<p>Parágrafo da notícia 2 EN</p><p>Mais um parágrafo EN</p><p>E para finalizar um tchau tchau - EN!!!!</p>', Null, Null, Null)
Update tblPublicacao Set Destaque = 1 Where PublicacaoId = 3

Insert Into tblPublicacao(PublicacaoId, SiteId, PublicacaoTipoId, Data, DataValidade, Posicao) Values(4, 2, 2, '2014-12-28', Null, 1)
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo, Editora, Fonte, Tags) Values(4, 1, 'Título Notícia 3 PT', 'Resuminho Notícia 3 PT', '<p>Parágrafo da notícia 3 PT</p><p>Mais um parágrafo PT</p><p>E para finalizar um tchau tchau - PT!!!!</p>', Null, Null, Null)
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo, Editora, Fonte, Tags) Values(4, 2, 'Título Notícia 3 ES', 'Resuminho Notícia 3 ES', '<p>Parágrafo da notícia 3 ES</p><p>Mais um parágrafo ES</p><p>E para finalizar um tchau tchau - ES!!!!</p>', Null, Null, Null)
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo, Editora, Fonte, Tags) Values(4, 3, 'Título Notícia 3 EN', 'Resuminho Notícia 3 EN', '<p>Parágrafo da notícia 3 EN</p><p>Mais um parágrafo EN</p><p>E para finalizar um tchau tchau - EN!!!!</p>', Null, Null, Null)

Insert Into tblPublicacao(PublicacaoId, SiteId, PublicacaoTipoId, Data, DataValidade, Posicao) Values(5, 3, 2, '2014-12-28', Null, 1)
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo, Editora, Fonte, Tags) Values(5, 1, 'Título Notícia Site 2 - 1 PT', 'Resuminho Notícia 1 PT', '<p>Parágrafo da notícia 1 PT</p><p>Mais um parágrafo PT</p><p>E para finalizar um tchau tchau - PT!!!!</p>', Null, Null, Null)
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo, Editora, Fonte, Tags) Values(5, 2, 'Título Notícia Site 2 - 1 ES', 'Resuminho Notícia 1 ES', '<p>Parágrafo da notícia 1 ES</p><p>Mais um parágrafo ES</p><p>E para finalizar um tchau tchau - ES!!!!</p>', Null, Null, Null)
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo, Editora, Fonte, Tags) Values(5, 3, 'Título Notícia Site 2 - 1 EN', 'Resuminho Notícia 1 EN', '<p>Parágrafo da notícia 1 EN</p><p>Mais um parágrafo EN</p><p>E para finalizar um tchau tchau - EN!!!!</p>', Null, Null, Null)


Insert Into tblPublicacao(PublicacaoId, SiteId, PublicacaoTipoId, Data, DataValidade, Posicao) Values(6, 2, 2, '2014-12-28', Null, 1)
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo, Editora, Fonte, Tags) Values(6, 1, 'Título Notícia Site 1 - 4 PT', 'Resuminho Notícia 4 PT', '<p>Parágrafo da notícia 4 PT</p><p>Mais um parágrafo PT</p><p>E para finalizar um tchau tchau - PT!!!!</p>', Null, Null, Null)
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo, Editora, Fonte, Tags) Values(6, 2, 'Título Notícia Site 1 - 4 ES', 'Resuminho Notícia 4 ES', '<p>Parágrafo da notícia 4 ES</p><p>Mais um parágrafo ES</p><p>E para finalizar um tchau tchau - ES!!!!</p>', Null, Null, Null)
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo, Editora, Fonte, Tags) Values(6, 3, 'Título Notícia Site 1 - 4 EN', 'Resuminho Notícia 4 EN', '<p>Parágrafo da notícia 4 EN</p><p>Mais um parágrafo EN</p><p>E para finalizar um tchau tchau - EN!!!!</p>', Null, Null, Null)
--Update tblPublicacao Set DataValidade = '2014-12-28' Where PublicacaoId = 6

--Eventos (3)
Insert Into tblPublicacao(PublicacaoId, SiteId, PublicacaoTipoId, Data, DataValidade, Posicao) Values(7, 2, 1, '2014-12-02', Null, 1)
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo, Editora, Fonte, Tags) Values(7, 1, 'V COLÓQUIO DE DIREITO ADMINISTRATIVO - PT', 'Novas tendências em arbitragem e mediação envolvendo a Administração Pública PT', '<p>PTA utilização por parte da Administração Pública de métodos alternativos de controvérsias atinge, no final de 2014, um momento de muita expectativa: tramitam na Câmara dos Deputados os Projetos de Lei nº 7108/2014, para a reforma da Lei de Arbitragem, e nº 7.169/2014, que disciplina a aplicação da mediação à Administração Pública. O evento se propõe a discutir o estado da arte e as novas tendências relativas ao uso da arbitragem e da mediação em controvérsias que envolvem a Administração Pública.</p>', Null, Null, Null)
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo, Editora, Fonte, Tags) Values(7, 2, 'V COLÓQUIO DE DIREITO ADMINISTRATIVO - ES', 'Novas tendências em arbitragem e mediação envolvendo a Administração Pública ES', '<p>PTA utilização por parte da Administração Pública de métodos alternativos de controvérsias atinge, no final de 2014, um momento de muita expectativa: tramitam na Câmara dos Deputados os Projetos de Lei nº 7108/2014, para a reforma da Lei de Arbitragem, e nº 7.169/2014, que disciplina a aplicação da mediação à Administração Pública. O evento se propõe a discutir o estado da arte e as novas tendências relativas ao uso da arbitragem e da mediação em controvérsias que envolvem a Administração Pública.</p>', Null, Null, Null)
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo, Editora, Fonte, Tags) Values(7, 3, 'V COLÓQUIO DE DIREITO ADMINISTRATIVO - EN', 'Novas tendências em arbitragem e mediação envolvendo a Administração Pública EN', '<p>PTA utilização por parte da Administração Pública de métodos alternativos de controvérsias atinge, no final de 2014, um momento de muita expectativa: tramitam na Câmara dos Deputados os Projetos de Lei nº 7108/2014, para a reforma da Lei de Arbitragem, e nº 7.169/2014, que disciplina a aplicação da mediação à Administração Pública. O evento se propõe a discutir o estado da arte e as novas tendências relativas ao uso da arbitragem e da mediação em controvérsias que envolvem a Administração Pública.</p>', Null, Null, Null)

Insert Into tblPublicacao(PublicacaoId, SiteId, PublicacaoTipoId, Data, DataValidade, Posicao) Values(8, 2, 1, '2014-12-05', Null, 1)
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo, Editora, Fonte, Tags) Values(8, 1, 'V COLÓQUIO DE DIREITO ADMINISTRATIVO2 - PT', 'Revoluçao tendências em mediação envolvendo a Administração Pública PT', '<p>PTA utilização por parte da Administração Pública de métodos alternativos de controvérsias atinge, no final de 2014, um momento de muita expectativa: tramitam na Câmara dos Deputados os Projetos de Lei nº 7108/2014, para a reforma da Lei de Arbitragem, e nº 7.169/2014, que disciplina a aplicação da mediação à Administração Pública. O evento se propõe a discutir o estado da arte e as novas tendências relativas ao uso da arbitragem e da mediação em controvérsias que envolvem a Administração Pública.</p>', Null, Null, Null)
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo, Editora, Fonte, Tags) Values(8, 2, 'V COLÓQUIO DE DIREITO ADMINISTRATIVO2 - ES', 'Revoluçao tendências em mediação envolvendo a Administração Pública ES', '<p>PTA utilização por parte da Administração Pública de métodos alternativos de controvérsias atinge, no final de 2014, um momento de muita expectativa: tramitam na Câmara dos Deputados os Projetos de Lei nº 7108/2014, para a reforma da Lei de Arbitragem, e nº 7.169/2014, que disciplina a aplicação da mediação à Administração Pública. O evento se propõe a discutir o estado da arte e as novas tendências relativas ao uso da arbitragem e da mediação em controvérsias que envolvem a Administração Pública.</p>', Null, Null, Null)
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo, Editora, Fonte, Tags) Values(8, 3, 'V COLÓQUIO DE DIREITO ADMINISTRATIVO2 - EN', 'Revoluçao tendências em mediação envolvendo a Administração Pública EN', '<p>PTA utilização por parte da Administração Pública de métodos alternativos de controvérsias atinge, no final de 2014, um momento de muita expectativa: tramitam na Câmara dos Deputados os Projetos de Lei nº 7108/2014, para a reforma da Lei de Arbitragem, e nº 7.169/2014, que disciplina a aplicação da mediação à Administração Pública. O evento se propõe a discutir o estado da arte e as novas tendências relativas ao uso da arbitragem e da mediação em controvérsias que envolvem a Administração Pública.</p>', Null, Null, Null)

Insert Into tblPublicacao(PublicacaoId, SiteId, PublicacaoTipoId, Data, DataValidade, Posicao) Values(9, 2, 1, '2014-12-28', Null, 1)
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo, Editora, Fonte, Tags) Values(9, 1, 'CICLO NACIONAL DE ARBITRAGEM E MEDIAÇÃO - PT', 'LOCAL : Escola Superior da Magistratura da Paraíba / Rua Abelardo S. G. Barreto, s/n / Altiplano - João Pessoa - PB PT', '<p><br>PROGRAMA</p><p>08h30 – ABERTURA</p><p>Presidente da ESMA<br>Frederico José Straube – Presidente do CAM-CCBC<br>Henrique Lenon - Representante da ANET (Academia Nacional de Estudos<br>Transnacionais)</p><p>9h00 - PRIMEIRA MESA DE DEBATES</p><p>Presidente:  Dr. Arthur Souto (Presidente da Escola Superior da OAB/PB e<br>Coordenador Adjunto do UNIPE)</p>', Null, Null, Null)
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo, Editora, Fonte, Tags) Values(9, 2, 'CICLO NACIONAL DE ARBITRAGEM E MEDIAÇÃO - ES', 'LOCAL : Escola Superior da Magistratura da Paraíba / Rua Abelardo S. G. Barreto, s/n / Altiplano - João Pessoa - PB ES', '<p><br>PROGRAMA</p><p>08h30 – ABERTURA</p><p>Presidente da ESMA<br>Frederico José Straube – Presidente do CAM-CCBC<br>Henrique Lenon - Representante da ANET (Academia Nacional de Estudos<br>Transnacionais)</p><p>9h00 - PRIMEIRA MESA DE DEBATES</p><p>Presidente:  Dr. Arthur Souto (Presidente da Escola Superior da OAB/PB e<br>Coordenador Adjunto do UNIPE)</p>', Null, Null, Null)
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo, Editora, Fonte, Tags) Values(9, 3, 'CICLO NACIONAL DE ARBITRAGEM E MEDIAÇÃO - EN', 'LOCAL : Escola Superior da Magistratura da Paraíba / Rua Abelardo S. G. Barreto, s/n / Altiplano - João Pessoa - PB EN', '<p><br>PROGRAMA</p><p>08h30 – ABERTURA</p><p>Presidente da ESMA<br>Frederico José Straube – Presidente do CAM-CCBC<br>Henrique Lenon - Representante da ANET (Academia Nacional de Estudos<br>Transnacionais)</p><p>9h00 - PRIMEIRA MESA DE DEBATES</p><p>Presidente:  Dr. Arthur Souto (Presidente da Escola Superior da OAB/PB e<br>Coordenador Adjunto do UNIPE)</p>', Null, Null, Null)

Set Identity_Insert tblPublicacao On
Set Identity_Insert tblPublicacao Off

Select * From tblPublicacao
Select * From tblPublicacaoIdiomaExcecao


/********************************************************************
*	7.	tblMenu														*
*   8.  tblMenuIdiomaExcecao										*
********************************************************************/
-- drop table tblMenu
Create Table tblMenu(
	MenuId				Int Identity Not Null,
	MenuPaiId			Int Null,
	SiteId				Int,
	MenuTipoId			Int,
	MenuTipoAcaoId		Int,
	PublicacaoId		Int,						
	--Rotulo				VarChar(50),				-- *Armazenado na tabela tblMenuIdiomaExcecao.
	LinkURL				VarChar(500),
	ImageURL			VarChar(500),
	Constraint PK_tblMenu Primary Key(MenuId),
	Constraint FK_tblMenu_tblMenu Foreign Key(MenuPaiId) References tblMenu(MenuId),
	Constraint FK_tblMenu_tblMenuTipo Foreign Key(MenuTipoId) References tblMenuTipo(MenuTipoId),
	Constraint FK_tblMenu_tblMenuTipoAcao Foreign Key(MenuTipoAcaoId) References tblMenuTipoAcao(MenuTipoAcaoId),
	Constraint FK_tblMenu_tblPublicacao Foreign Key(PublicacaoId) References tblPublicacao(PublicacaoId),
)
Go
Grant Select on tblMenu to usuCCBC

-- Drop Table tblMenuIdiomaExcecao
Create Table tblMenuIdiomaExcecao(
	MenuIdiomaExcecaoId	Int Identity Not Null,
	MenuId			Int Not Null,
	IdiomaId		Int Not Null,
	Rotulo			VarChar(50),
	Constraint PK_tblMenuIdiomaExcecao Primary Key(MenuIdiomaExcecaoId),
	Constraint FK_tblMenuIdiomaExcecao_tblMenu Foreign Key(MenuId) References tblMenu(MenuId),
	Constraint FK_tblMenuIdiomaExcecao_tblIdioma Foreign Key(IdiomaId) References tblIdioma(IdiomaId)
)
Grant Select on tblMenuIdiomaExcecao to usuCCBC

-- Delete From tblMenuIdiomaExcecao

Set Identity_Insert tblMenu On

Insert Into tblMenu(MenuId, MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL, ImageURL) Values(1,		Null,		2,		1,			1,				Null,			'/',		Null)
Insert Into tblMenu(MenuId, MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL, ImageURL) Values(2,		Null,		2,		1,			2,				1	,			Null,		Null)
Insert Into tblMenu(MenuId, MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL, ImageURL) Values(3,		Null,		2,		1,			2,				Null,			Null,		Null)
Insert Into tblMenu(MenuId, MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL, ImageURL) Values(4,		Null,		2,		1,			1,				Null,			'Contato',	Null)
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(1, 1, 'home')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(2, 1, 'cam')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(3, 1, 'servicos')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(4, 1, 'contato')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(1, 2, 'casa')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(2, 2, 'cames')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(3, 2, 'serventia')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(4, 2, 'contacto')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(1, 3, 'home')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(2, 3, 'cam')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(3, 3, 'services')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(4, 3, 'contact')


--																												   MenuId,	MenuPaiId,	SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId,	LinkURL,	ImageURL
Insert Into tblMenu(MenuId, MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL, ImageURL) Values(5,		2,			2,		1,			2,				Null,				Null,		Null)
Insert Into tblMenu(MenuId, MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL, ImageURL) Values(6,		2,			2,		1,			2,				Null,				Null,		Null)
Insert Into tblMenu(MenuId, MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL, ImageURL) Values(7,		2,			2,		1,			2,				Null,				Null,		Null)
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(5, 1, 'Histórico')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(6, 1, 'Gestão')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(7, 1, 'ISO 9001')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(5, 2, 'Estoria')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(6, 2, 'Gerencia')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(7, 2, 'ISO 9001')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(5, 3, 'Historic')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(6, 3, 'Management')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(7, 3, 'ISO 9001')


--																												   MenuId,	MenuPaiId,	SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId,	LinkURL,	ImageURL
Insert Into tblMenu(MenuId, MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL, ImageURL) Values(8,		2,			2,		1,			2,				Null,				Null,		Null)
Insert Into tblMenu(MenuId, MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL, ImageURL) Values(9,		8,			2,		1,			2,				Null,				Null,		Null)
Insert Into tblMenu(MenuId, MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL, ImageURL) Values(16,		8,			2,		1,			2,				Null,				Null,		Null)
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(8, 1, 'Reserva de Sala')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(9, 1, '1º Andar')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(16, 1, '2º Andar')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(8, 2, 'Garanta Saleta')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(9, 2, 'I Andare')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(16, 2, 'II Andare')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(8, 3, 'Book Room')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(9, 3, 'First Floor')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(16, 3, 'Second Floor')

--Insert Into tblMenu(MenuId, MenuPaiId, MenuTipoId, MenuTipoAcaoId, MenuAcaoId, Rotulo, LinkURL, ImageURL) Values(10, Null, 2, 1, 4, 'Quick Menu 1', '/', Null)
--Insert Into tblMenu(MenuId, MenuPaiId, MenuTipoId, MenuTipoAcaoId, MenuAcaoId, Rotulo, LinkURL, ImageURL) Values(11, Null, 2, 1, 4, 'Quick Menu 2', '/', Null)
--Insert Into tblMenu(MenuId, MenuPaiId, MenuTipoId, MenuTipoAcaoId, MenuAcaoId, Rotulo, LinkURL, ImageURL) Values(12, Null, 2, 1, 4, 'Quick Menu 3', '/', Null)
--Insert Into tblMenu(MenuId, MenuPaiId, MenuTipoId, MenuTipoAcaoId, MenuAcaoId, Rotulo, LinkURL, ImageURL) Values(13, Null, 2, 1, 4, 'Quick Menu 4', '/', Null)
--Insert Into tblMenu(MenuId, MenuPaiId, MenuTipoId, MenuTipoAcaoId, MenuAcaoId, Rotulo, LinkURL, ImageURL) Values(14, Null, 2, 1, 4, 'Quick Menu 5', '/', Null)
--Insert Into tblMenu(MenuId, MenuPaiId, MenuTipoId, MenuTipoAcaoId, MenuAcaoId, Rotulo, LinkURL, ImageURL) Values(15, Null, 2, 1, 4, 'Quick Menu 6', '/', Null)

--																												   MenuId,	MenuPaiId,	SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId,	LinkURL,	ImageURL
Insert Into tblMenu(MenuId, MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL, ImageURL) Values(20,		2,			2,		1,			2,				Null,				Null,		Null)
Insert Into tblMenu(MenuId, MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL, ImageURL) Values(21,		20,			2,		1,			2,				Null,				Null,		Null)
Insert Into tblMenu(MenuId, MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL, ImageURL) Values(22,		20,			2,		1,			2,				Null,				Null,		Null)
Insert Into tblMenu(MenuId, MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL, ImageURL) Values(23,		21,			2,		1,			2,				Null,				Null,		Null)
Insert Into tblMenu(MenuId, MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL, ImageURL) Values(24,		21,			2,		1,			2,				Null,				Null,		Null)
Insert Into tblMenu(MenuId, MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL, ImageURL) Values(25,		23,			2,		1,			2,				Null,				Null,		Null)
Insert Into tblMenu(MenuId, MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL, ImageURL) Values(26,		23,			2,		1,			2,				Null,				Null,		Null)
Insert Into tblMenu(MenuId, MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL, ImageURL) Values(27,		24,			2,		1,			2,				Null,				Null,		Null)
Insert Into tblMenu(MenuId, MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL, ImageURL) Values(28,		24,			2,		1,			2,				Null,				Null,		Null)
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(20, 1, 'Premiação')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(21, 1, '2013')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(22, 1, '2014')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(23, 1, 'Artes')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(24, 1, 'Alimentação')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(25, 1, 'Machadinho')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(26, 1, 'Leonardo da Vinci')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(27, 1, 'Fast Food')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(28, 1, 'Italian Food')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(20, 2, 'Consagração')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(21, 2, '2013')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(22, 2, '2014')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(23, 2, 'Belas Artes')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(24, 2, 'Comida')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(25, 2, 'Machadinho')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(26, 2, 'Leonardo da Vinci')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(27, 2, 'Lanche Rápido')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(28, 2, 'Italiano')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(20, 3, 'Awards')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(21, 3, '2013')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(22, 3, '2014')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(23, 3, 'Arts')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(24, 3, 'Food')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(25, 3, 'Machadinho')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(26, 3, 'Leonardo da Vinci')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(27, 3, 'Chilli')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(28, 3, 'Chicken Alfredo')

Set Identity_Insert tblMenu Off


--- Site 1: CCBC
Set Identity_Insert tblMenu On
Insert Into tblMenu(MenuId, MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL, ImageURL) Values(500,		Null,		1,		1,			1,				Null,			'/',		Null)
Insert Into tblMenu(MenuId, MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL, ImageURL) Values(501,		Null,		1,		1,			2,				Null,			Null,		Null)
Insert Into tblMenu(MenuId, MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL, ImageURL) Values(503,		Null,		1,		1,			2,				Null,			Null,		Null)
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(500, 1, 'Principal')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(501, 1, 'CCBC')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(503, 1, 'Contatos')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(500, 2, 'Entre')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(501, 2, 'CCBC')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(503, 2, 'Pessoal')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(500, 3, 'Main')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(501, 3, 'CCBC')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(503, 3, 'Contacts')
Set Identity_Insert tblMenu Off
Delete tblMenuIdiomaExcecao Where MenuId Between 500 and 503
Delete tblMenu Where MenuId Between 500 and 503
----------------


Select * From tblMenuTipo
Select * From tblMenuTipoAcao
Select * From tblMenu
Select * From tblMenuIdiomaExcecao

/********************************************************************
*	?.	tblUsuarioGrupo												*
********************************************************************/
-- Drop Table tblUsuarioGrupo
Create Table tblUsuarioGrupo(
	UsuarioGrupoId	Int Identity Not Null,
	Nome			VarChar(80),
	Constraint PK_tblUsuarioGrupo Primary Key(UsuarioGrupoId)
)
Go

Grant Select On tblUsuarioGrupo To usuCCBC

Set Identity_Insert tblUsuarioGrupo On
Insert Into tblUsuarioGrupo(UsuarioGrupoId, Nome) Values(1, 'DIRETORIA')
Insert Into tblUsuarioGrupo(UsuarioGrupoId, Nome) Values(2, 'GERENCIA')
Insert Into tblUsuarioGrupo(UsuarioGrupoId, Nome) Values(3, 'INTERNO')
Set Identity_Insert tblUsuarioGrupo Off

Select * From tblUsuarioGrupo
Go


/********************************************************************
*	?.	tblUsuario													*
********************************************************************/
-- Drop Table tblUsuario
Create Table tblUsuario(
	UsuarioId		Int Identity Not Null,
	--UsuarioGrupoId	Int Null,
	Nome			VarChar(80),
	Login			VarChar(80),
	Senha			VarChar(32),
	Constraint PK_tblUsuario Primary Key(UsuarioId)--,
	--Constraint FK_tblUsuario_tblUsuarioGrupo Foreign Key(UsuarioGrupoId) References tblUsuarioGrupo(UsuarioGrupoId)
)
Go

Grant Select On tblUsuario To usuCCBC
Go

Set Identity_Insert tblUsuario On
Insert Into tblUsuario(UsuarioId, Nome, Login, Senha) Values(1, 'Ednilson H', 'ed', '123')
Insert Into tblUsuario(UsuarioId, Nome, Login, Senha) Values(2, 'João Dias', 'jo', '123')
Insert Into tblUsuario(UsuarioId, Nome, Login, Senha) Values(3, 'Marcelo Alves', 'ma', '123')
Set Identity_Insert tblUsuario Off

Select * From tblUsuario

/********************************************************************
*	?.	tblUsuarioUsuarioGrupo										*
********************************************************************/
-- Drop Table tblUsuarioUsuarioGrupo
Create Table tblUsuarioUsuarioGrupo(
	UsuarioUsuarioGrupoId		Int Identity Not Null,
	UsuarioId					Int Null,
	UsuarioGrupoId				Int Null,
	Constraint PK_tblUsuarioUsuarioGrupo Primary Key(UsuarioUsuarioGrupoId),
	Constraint FK_tblUsuarioUsuarioGrupo_tblUsuario Foreign Key(UsuarioId) References tblUsuario(UsuarioId),
	Constraint FK_tblUsuarioUsuarioGrupo_tblUsuarioGrupo Foreign Key(UsuarioGrupoId) References tblUsuarioGrupo(UsuarioGrupoId)
)
Go

Grant Select On tblUsuarioUsuarioGrupo To usuCCBC
Go

--Select * From tblUsuario
--Select * From tblUsuarioGrupo
Insert Into tblUsuarioUsuarioGrupo(UsuarioId, UsuarioGrupoId) Values(1, 1)
Insert Into tblUsuarioUsuarioGrupo(UsuarioId, UsuarioGrupoId) Values(1, 2)
Insert Into tblUsuarioUsuarioGrupo(UsuarioId, UsuarioGrupoId) Values(2, 2)
Insert Into tblUsuarioUsuarioGrupo(UsuarioId, UsuarioGrupoId) Values(3, 3)

Select * From tblUsuarioUsuarioGrupo


/********************************************************************
*	?.	tblPublicacaoRestricao										*
********************************************************************/
-- Drop Table tblPublicacaoRestricao
Create Table tblPublicacaoRestricao(
	PublicacaoRestricaoId	Int Identity Not Null,
	PublicacaoId			Int Not Null,
	Privado					Bit Default 0,
	Constraint PK_tblPublicacaoRestricao Primary Key(PublicacaoRestricaoId),
	Constraint FK_tblPublicacaoRestricao_tblPublicacao Foreign Key(PublicacaoId) References tblPublicacao(PublicacaoId)
)
Go

Grant Select on tblPublicacaoRestricao to usuCCBC
Go

Insert Into tblPublicacaoRestricao(PublicacaoId, Privado) Values(1, 0)
Insert Into tblPublicacaoRestricao(PublicacaoId, Privado) Values(2, 1)
Insert Into tblPublicacaoRestricao(PublicacaoId, Privado) Values(3, 0)
Insert Into tblPublicacaoRestricao(PublicacaoId, Privado) Values(4, 0)
Insert Into tblPublicacaoRestricao(PublicacaoId, Privado) Values(6, 0)

Insert Into tblPublicacaoRestricao(PublicacaoId, Privado) Values(7, 0)
Insert Into tblPublicacaoRestricao(PublicacaoId, Privado) Values(8, 1)
Insert Into tblPublicacaoRestricao(PublicacaoId, Privado) Values(9, 0)

Select * From tblPublicacaoRestricao

Select * From tblPublicacaoIdiomaExcecao

/********************************************************************
*	?.	tblPublicacaoRestricaoUsuarioGrupo							*
********************************************************************/
-- Drop Table tblPublicacaoRestricaoUsuarioGrupo
Create Table tblPublicacaoRestricaoUsuarioGrupo(
	PublicacaoRestricaoUsuarioGrupoId	Int Identity Not Null,
	PublicacaoRestricaoId				Int Not Null,
	UsuarioGrupoId						Int
)
Go

Grant Select on tblPublicacaoRestricaoUsuarioGrupo to usuCCBC
Go

Insert Into tblPublicacaoRestricaoUsuarioGrupo(PublicacaoRestricaoId, UsuarioGrupoId) Values(1, 1)
Insert Into tblPublicacaoRestricaoUsuarioGrupo(PublicacaoRestricaoId, UsuarioGrupoId) Values(1, 2)

Insert Into tblPublicacaoRestricaoUsuarioGrupo(PublicacaoRestricaoId, UsuarioGrupoId) Values(7, 1)

Select * From tblPublicacaoRestricaoUsuarioGrupo

--------------------------------------------------------------------

Set Identity_Insert tblUsuarioGrupo On
Insert Into tblUsuarioGrupo(UsuarioGrupoId, Nome) Values(1, 'DIRETORIA')
Set Identity_Insert tblUsuarioGrupo Off

Select * From tblUsuarioGrupo
Go


Create Table tblConteudo(
	ConteudoId	Int Identity Not Null,
	Titulo		VarChar(120),
	Conteudo	VarChar(4000),
	Constraint PK_tblConteudo Primary Key(ConteudoId)
)

Grant Select on tblConteudo to usuCCBC


Set Identity_Insert tblConteudo On
Insert Into tblConteudo(ConteudoId, Titulo, Conteudo) Values(1, 'centro de arbitragem e medição', 'o centro de arbitragem e blablabla')
Insert Into tblConteudo(ConteudoId, Titulo, Conteudo) Values(2, 'histórico', 'a câmara de comércio blablabla')
Insert Into tblConteudo(ConteudoId, Titulo, Conteudo) Values(3, 'gestão', 'presidente / vice.... blablabla')
Insert Into tblConteudo(ConteudoId, Titulo, Conteudo) Values(4, 'iso 9001', 'o cam/ccbc blablabla')
Set Identity_Insert tblConteudo Off

Select * From tblConteudo

select * from tblconteudo
update tblconteudo set conteudo = '<p>O centro de arbitragem e blablabla</p><p><img src="HandlerFile.ashx?id=19" /></p>' where conteudoid = 1

SELECT * FROM TBLARQUIVO
usp_sel_arquivo null
--delete from tblArquivo
------
Grant Execute on USP_SEL_Menu to usuCCBC
Grant Execute on USP_SEL_Menu_Tree to usuCCBC
Grant Execute on USP_SEL_Conteudo to usuCCBC
Grant Execute on USP_SEL_Arquivo to usuCCBC
Grant Execute on USP_SEL_Publicacao to usuCCBC
Grant Execute on USP_SEL_Login to usuCCBC
Grant Execute on USP_INS_Arquivo to usuCCBC

Grant Insert on tblArquivo to usuCCBC

Grant Select on GetUsuarioGrupo to usuCCBC
Grant Execute on GetPublicacaoAcessoUsuario to usuCCBC

select * from tblmenu



Select * From tblMenu

USP_SEL_Menu 
	@MenuPaiId	= null, 
	@MenuTipoId	= 1

----
Select * From tblConteudo

----

