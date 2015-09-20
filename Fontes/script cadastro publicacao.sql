

Select * from tblMenu
Select * From tblMenuIdiomaExcecao

[dbo].[USP_SEL_Menu]
@SiteId=2,
@MenuTipoId=2,
@IdiomaId=1,
@PublicacaoId=null

[dbo].[USP_SEL_Menu_Tree]
@IdiomaId = 3, 
@PublicacaoId = 7

Select * From tblMenu Where MenuId = 2
Update tblMenu Set MenuTipoAcaoId=1, LinkURL = 'Materia/1' where MenuId = 2


Select * From tblPublicacao
Select * From tblPublicacaoIdiomaExcecao

Declare @PublicacaoId Int,
		@Privado Bit
Set @PublicacaoId = 13
Set @Privado = 0

Insert Into tblPublicacao(SiteId,PublicacaoTipoId,Data,DataValidade,Posicao,Destaque)
Values(2,3,'2015-01-04',Null,1,0)
Select @PublicacaoId = @@Identity
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 1, 'HIST�RICO', '', '<p>A C�mara de Com�rcio Brasil-Canad� foi pioneira no Brasil em mat�ria de arbitragem, pois j� em 26 de julho de 1979 criara a sua Comiss�o de Arbitragem, hoje Centro de Arbitragem, com vistas a proporcionar meios f�ceis e �geis para a solu��o de lit�gios, quer entre pessoas f�sicas, quer, sobretudo, entre pessoas jur�dicas em mat�ria contratual.</p><p>Devido � falta de legisla��o adequada e tamb�m de uma cultura prop�cia a esse tipo de solu��o de controv�rsias, n�o foram muitos os casos levados � ent�o Comiss�o de Arbitragem.</p><p>O quadro, por�m, est� se alterando rapidamente com a supera��o daqueles dois fatores negativos. Em primeiro lugar, o Brasil conta, desde 1996, com uma lei moderna e bastante funcional: a Lei 9.307, que oferece o respaldo jur�dico necess�rio tanto para as partes quanto para os �rbitros. Por outro lado, a mentalidade refrat�ria � ado��o de solu��es arbitrais vem mudando em fun��o dos requisitos de celeridade, tecnicidade e seguran�a exigidos pela vida econ�mica moderna. Tudo isso tem levado a uma crescente ado��o de cl�usulas arbitrais nos mais variados tipos de contratos celebrados por empresas e mesmo por particulares.</p><p>Dentro desse quadro, a CCBC promoveu a completa reforma do seu regulamento de arbitragem ao qual foi introduzido igualmente um roteiro para procedimentos de media��o. Ambos est�o perfeitamente adaptados � nova legisla��o e seguem os padr�es vigentes nos modernos centros de arbitragem do mundo desenvolvido.</p><p>N�o � necess�rio repisar as incont�veis vantagens trazidas pelo sistema arbitral de solu��o ou composi��o de conflitos. Os tr�s requisitos acima mencionados - celeridade, tecnicidade e seguran�a falam por si. Com efeito, as controv�rsias no mundo dos neg�cios envolvem cada vez mais aspectos t�cnicos de alta complexidade cujo equacionamento nem sempre se ajusta aos formalismos puramente processuais. Exigem n�o apenas profici�ncia jur�dica, mas conhecimentos especializados dos mercados relevantes e de suas formas de opera��o. Da� poderem integrar os tribunais arbitrais n�o apenas juristas mas especialistas de outros ramos do conhecimento.</p><p>Um ponto importante a ser real�ado � o fato de em uma arbitragem reduzir-se em muito o car�ter de animosidade que s�i marcar as partes nos lit�gios forenses. Muitas vezes essas partes, que num dado momento levam as suas diferen�as de avalia��o a um �rbitro ou �rbitros, estar�o dentro em breve juntas, lado a lado, em outro importante neg�cio como, por exemplo, a forma��o de um cons�rcio para o suprimento de equipamentos. Interessa-lhes, portanto, evitar o atrito e as seq�elas pr�prias das intermin�veis batalhas judiciais. Acrescente-se a tudo isto o sigilo e a confian�a pessoal de cada parte no �rbitro por ela escolhido, deixando-a tranq�ila quanto � escolha do terceiro �rbitro, isto quando as duas partes n�o convierem na indica��o de um �nico julgador.</p><p>A essas e tantas outras vantagens do instituto, deve-se somar uma outra de car�ter eminentemente social: a sua contribui��o para o descongestionamento do Judici�rio, mal que aflige a grande maioria dos pa�ses, mas que � indubitavelmente grav�ssimo, sendo mesmo objeto de preocupa��o junto a �rg�os internacionais que acompanham a evolu��o da sociedade brasileira.</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 2, 'ESTORIA', '', 'Es <p>A C�mara de Com�rcio Brasil-Canad� foi pioneira no Brasil em mat�ria de arbitragem, pois j� em 26 de julho de 1979 criara a sua Comiss�o de Arbitragem, hoje Centro de Arbitragem, com vistas a proporcionar meios f�ceis e �geis para a solu��o de lit�gios, quer entre pessoas f�sicas, quer, sobretudo, entre pessoas jur�dicas em mat�ria contratual.</p><p>Devido � falta de legisla��o adequada e tamb�m de uma cultura prop�cia a esse tipo de solu��o de controv�rsias, n�o foram muitos os casos levados � ent�o Comiss�o de Arbitragem.</p><p>O quadro, por�m, est� se alterando rapidamente com a supera��o daqueles dois fatores negativos. Em primeiro lugar, o Brasil conta, desde 1996, com uma lei moderna e bastante funcional: a Lei 9.307, que oferece o respaldo jur�dico necess�rio tanto para as partes quanto para os �rbitros. Por outro lado, a mentalidade refrat�ria � ado��o de solu��es arbitrais vem mudando em fun��o dos requisitos de celeridade, tecnicidade e seguran�a exigidos pela vida econ�mica moderna. Tudo isso tem levado a uma crescente ado��o de cl�usulas arbitrais nos mais variados tipos de contratos celebrados por empresas e mesmo por particulares.</p><p>Dentro desse quadro, a CCBC promoveu a completa reforma do seu regulamento de arbitragem ao qual foi introduzido igualmente um roteiro para procedimentos de media��o. Ambos est�o perfeitamente adaptados � nova legisla��o e seguem os padr�es vigentes nos modernos centros de arbitragem do mundo desenvolvido.</p><p>N�o � necess�rio repisar as incont�veis vantagens trazidas pelo sistema arbitral de solu��o ou composi��o de conflitos. Os tr�s requisitos acima mencionados - celeridade, tecnicidade e seguran�a falam por si. Com efeito, as controv�rsias no mundo dos neg�cios envolvem cada vez mais aspectos t�cnicos de alta complexidade cujo equacionamento nem sempre se ajusta aos formalismos puramente processuais. Exigem n�o apenas profici�ncia jur�dica, mas conhecimentos especializados dos mercados relevantes e de suas formas de opera��o. Da� poderem integrar os tribunais arbitrais n�o apenas juristas mas especialistas de outros ramos do conhecimento.</p><p>Um ponto importante a ser real�ado � o fato de em uma arbitragem reduzir-se em muito o car�ter de animosidade que s�i marcar as partes nos lit�gios forenses. Muitas vezes essas partes, que num dado momento levam as suas diferen�as de avalia��o a um �rbitro ou �rbitros, estar�o dentro em breve juntas, lado a lado, em outro importante neg�cio como, por exemplo, a forma��o de um cons�rcio para o suprimento de equipamentos. Interessa-lhes, portanto, evitar o atrito e as seq�elas pr�prias das intermin�veis batalhas judiciais. Acrescente-se a tudo isto o sigilo e a confian�a pessoal de cada parte no �rbitro por ela escolhido, deixando-a tranq�ila quanto � escolha do terceiro �rbitro, isto quando as duas partes n�o convierem na indica��o de um �nico julgador.</p><p>A essas e tantas outras vantagens do instituto, deve-se somar uma outra de car�ter eminentemente social: a sua contribui��o para o descongestionamento do Judici�rio, mal que aflige a grande maioria dos pa�ses, mas que � indubitavelmente grav�ssimo, sendo mesmo objeto de preocupa��o junto a �rg�os internacionais que acompanham a evolu��o da sociedade brasileira.</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 3, 'HISTORY', '', 'EN <p>A C�mara de Com�rcio Brasil-Canad� foi pioneira no Brasil em mat�ria de arbitragem, pois j� em 26 de julho de 1979 criara a sua Comiss�o de Arbitragem, hoje Centro de Arbitragem, com vistas a proporcionar meios f�ceis e �geis para a solu��o de lit�gios, quer entre pessoas f�sicas, quer, sobretudo, entre pessoas jur�dicas em mat�ria contratual.</p><p>Devido � falta de legisla��o adequada e tamb�m de uma cultura prop�cia a esse tipo de solu��o de controv�rsias, n�o foram muitos os casos levados � ent�o Comiss�o de Arbitragem.</p><p>O quadro, por�m, est� se alterando rapidamente com a supera��o daqueles dois fatores negativos. Em primeiro lugar, o Brasil conta, desde 1996, com uma lei moderna e bastante funcional: a Lei 9.307, que oferece o respaldo jur�dico necess�rio tanto para as partes quanto para os �rbitros. Por outro lado, a mentalidade refrat�ria � ado��o de solu��es arbitrais vem mudando em fun��o dos requisitos de celeridade, tecnicidade e seguran�a exigidos pela vida econ�mica moderna. Tudo isso tem levado a uma crescente ado��o de cl�usulas arbitrais nos mais variados tipos de contratos celebrados por empresas e mesmo por particulares.</p><p>Dentro desse quadro, a CCBC promoveu a completa reforma do seu regulamento de arbitragem ao qual foi introduzido igualmente um roteiro para procedimentos de media��o. Ambos est�o perfeitamente adaptados � nova legisla��o e seguem os padr�es vigentes nos modernos centros de arbitragem do mundo desenvolvido.</p><p>N�o � necess�rio repisar as incont�veis vantagens trazidas pelo sistema arbitral de solu��o ou composi��o de conflitos. Os tr�s requisitos acima mencionados - celeridade, tecnicidade e seguran�a falam por si. Com efeito, as controv�rsias no mundo dos neg�cios envolvem cada vez mais aspectos t�cnicos de alta complexidade cujo equacionamento nem sempre se ajusta aos formalismos puramente processuais. Exigem n�o apenas profici�ncia jur�dica, mas conhecimentos especializados dos mercados relevantes e de suas formas de opera��o. Da� poderem integrar os tribunais arbitrais n�o apenas juristas mas especialistas de outros ramos do conhecimento.</p><p>Um ponto importante a ser real�ado � o fato de em uma arbitragem reduzir-se em muito o car�ter de animosidade que s�i marcar as partes nos lit�gios forenses. Muitas vezes essas partes, que num dado momento levam as suas diferen�as de avalia��o a um �rbitro ou �rbitros, estar�o dentro em breve juntas, lado a lado, em outro importante neg�cio como, por exemplo, a forma��o de um cons�rcio para o suprimento de equipamentos. Interessa-lhes, portanto, evitar o atrito e as seq�elas pr�prias das intermin�veis batalhas judiciais. Acrescente-se a tudo isto o sigilo e a confian�a pessoal de cada parte no �rbitro por ela escolhido, deixando-a tranq�ila quanto � escolha do terceiro �rbitro, isto quando as duas partes n�o convierem na indica��o de um �nico julgador.</p><p>A essas e tantas outras vantagens do instituto, deve-se somar uma outra de car�ter eminentemente social: a sua contribui��o para o descongestionamento do Judici�rio, mal que aflige a grande maioria dos pa�ses, mas que � indubitavelmente grav�ssimo, sendo mesmo objeto de preocupa��o junto a �rg�os internacionais que acompanham a evolu��o da sociedade brasileira.</p>')
Update tblMenu Set PublicacaoId = @PublicacaoId, MenuTipoAcaoId=1, LinkURL = 'Materia/' + cAST(@PublicacaoId AS VARCHAR) Where MenuId = 5


Insert Into tblPublicacaoRestricao(PublicacaoId, Privado) Values(@PublicacaoId, @Privado)

--- GEST�O
Declare @PublicacaoId Int,
		@Privado Bit
Set @Privado = 0

Insert Into tblPublicacao(SiteId,PublicacaoTipoId,Data,DataValidade,Posicao,Destaque)
Values(2,3,'2015-01-04',Null,1,0)
Select @PublicacaoId = @@Identity
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 1, 'GEST�O', '', '<p>Conte�do da Gest�o.</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 2, 'CHEFIA', '', '<p>Conte�do da Gest�o em Espanhol.</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 3, 'MANAGEMENT', '', '<p>Conte�do da Gest�o em Ingl�s.</p>')
Update tblMenu Set PublicacaoId = @PublicacaoId, MenuTipoAcaoId=1, LinkURL = 'Materia/' + cAST(@PublicacaoId AS VARCHAR) Where MenuId = 6

Insert Into tblPublicacaoRestricao(PublicacaoId, Privado) Values(@PublicacaoId, @Privado)


--- GEST�O
Declare @PublicacaoId Int,
		@Privado Bit,
		@MenuId Int
Set @MenuId = 7
Set @Privado = 0

Insert Into tblPublicacao(SiteId,PublicacaoTipoId,Data,DataValidade,Posicao,Destaque)
Values(2,3,'2015-01-04',Null,1,0)
Select @PublicacaoId = @@Identity
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 1, 'ISO 9001', '', '<p>Conte�do da ISO 9001.</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 2, 'ISO 9001 ES', '', '<p>Conte�do da ISO 9001 em Espanhol.</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 3, 'ISO 9001 EN', '', '<p>Conte�do da ISO 9001 em Ingl�s.</p>')
Update tblMenu Set PublicacaoId = @PublicacaoId, MenuTipoAcaoId=1, LinkURL = 'Materia/' + cAST(@PublicacaoId AS VARCHAR) Where MenuId = @MenuId

Insert Into tblPublicacaoRestricao(PublicacaoId, Privado) Values(@PublicacaoId, @Privado)

--- GEST�O
Declare @PublicacaoId Int,
		@Privado Bit,
		@MenuId Int
Set @MenuId = 8
Set @Privado = 0

Insert Into tblPublicacao(SiteId,PublicacaoTipoId,Data,DataValidade,Posicao,Destaque)
Values(2,3,'2015-01-04',Null,1,0)
Select @PublicacaoId = @@Identity
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 1, 'rESERVA DE SAL', '', '<p>Conte�do da rESERVA DE SAL.</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 2, 'rESERVA DE SAL ES', '', '<p>Conte�do da rESERVA DE SAL em Espanhol.</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 3, 'rESERVA DE SAL EN', '', '<p>Conte�do da rESERVA DE SAL em Ingl�s.</p>')
Update tblMenu Set PublicacaoId = @PublicacaoId, MenuTipoAcaoId=1, LinkURL = 'Materia/' + cAST(@PublicacaoId AS VARCHAR) Where MenuId = @MenuId

Insert Into tblPublicacaoRestricao(PublicacaoId, Privado) Values(@PublicacaoId, @Privado)

--- GEST�O
Declare @PublicacaoId Int,
		@Privado Bit,
		@MenuId Int
Set @MenuId = 16
Set @Privado = 0

Insert Into tblPublicacao(SiteId,PublicacaoTipoId,Data,DataValidade,Posicao,Destaque)
Values(2,3,'2015-01-04',Null,1,0)
Select @PublicacaoId = @@Identity
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 1, '2o. Andar', '', '<p>Conte�do da 2o. Andar.</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 2, '2o. Andar ES', '', '<p>Conte�do da 2o. Andar em Espanhol.</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 3, '2o. Andar EN', '', '<p>Conte�do da 2o. Andar em Ingl�s.</p>')
Update tblMenu Set PublicacaoId = @PublicacaoId, MenuTipoAcaoId=1, LinkURL = 'Materia/' + cAST(@PublicacaoId AS VARCHAR) Where MenuId = @MenuId

Insert Into tblPublicacaoRestricao(PublicacaoId, Privado) Values(@PublicacaoId, @Privado)


--- GEST�O
Declare @PublicacaoId Int,
		@Privado Bit,
		@MenuId Int,
		@SiteId Int,
		@PublicacaoTipoId Int
Set @SiteId = 2
Set @MenuId = 508
Set @Privado = 0
Set @PublicacaoTipoId = 5
Insert Into tblPublicacao(SiteId,PublicacaoTipoId,Data,DataValidade,Posicao,Destaque)
Values(@SiteId,@PublicacaoTipoId,'2015-01-04',Null,1,0)
Select @PublicacaoId = @@Identity
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 1, 'MODELOS DE CL�USULAS', '', '<p><em><strong>Sugest�o de cl�usulas adotadas pelo CAM/CCBC:</strong></em></p><p><strong>I - Cl�usula Padr�o:</strong><br />�Qualquer lit�gio origin�rio do presente contrato, inclusive quanto � sua interpreta��o ou execu��o, ser� definitivamente resolvido por arbitragem, administrada pelo Centro de Arbitragem e Media��o da C�mara de Com�rcio Brasil-Canad� (�CAM/CCBC�), de acordo com o seu Regulamento, constituindo-se o tribunal arbitral de [um/tr�s] �rbitros, indicados na forma do citado Regulamento.�</p><p>&nbsp;<strong>II. Cl�usula Detalhada:</strong><br />1- Qualquer disputa oriunda deste contrato ou com ele relacionada ser� definitivamente resolvida por arbitragem.<br />1.1- A arbitragem ser� administrada pelo Centro de Arbitragem e Media��o da C�mara de Com�rcio Brasil-Canad� (�CAM/CCBC�) e obedecer� �s normas estabelecidas no seu Regulamento, cujas disposi��es integram o presente contrato.1.2- O tribunal arbitral ser� constitu�do por [um/tr�s] �rbitros, indicados na forma prevista no Regulamento do CAM/CCBC.<br />1.3-. A arbitragem ter� sede em [Cidade, Estado].<br />1.4-. O procedimento arbitral ser� conduzido em [idioma].<br />1.5-. [lei aplic�vel]</p><p><strong>III - Cl�usula Padr�o Escalona Med-Arb.:</strong><br />Qualquer conflito origin�rio do presente contrato, inclusive quanto � sua interpreta��o ou execu��o, ser� submetido obrigatoriamente � Media��o, administrada pelo Centro de Arbitragem e Media��o da C�mara de Com�rcio Brasil-Canad� (�CAM/CCBC�), de acordo com o seu Roteiro e Regimento de Media��o, a ser coordenada por Mediador participante da Lista de Mediadores do CAM/CCBC, indicado na forma das citadas normas.O conflito n�o resolvido pela media��o, conforme a cl�usula de media��o acima, ser� definitivamente resolvido por arbitragem, administrada pelo mesmo CAM/CCBC, de acordo com o seu Regulamento, constituindo-se o tribunal arbitral de tr�s �rbitros, indicados na forma do citado Regulamento.</p><p>&nbsp;<strong>IV. Cl�usula Detalhada Escalonada Med-Arb.:</strong><br />1- Qualquer conflito origin�rio do presente contrato, inclusive quanto � sua interpreta��o ou execu��o, ser� submetido obrigatoriamente � Media��o, administrada pelo Centro de Arbitragem e Media��o da C�mara de Com�rcio Brasil-Canad� (�CAM/CCBC�), de acordo com o seu Roteiro e Regimento de Media��o, a ser coordenada por Mediador participante da Lista de Mediadores do CAM/CCBC, indicado na forma das citadas normas.<br />1.1- O conflito n�o resolvido pela media��o, conforme a cl�usula de media��o acima, ser� definitivamente resolvido por arbitragem, administrada pelo mesmo CAM/CCBC, de acordo com o seu Regulamento.<br />2.1- A arbitragem ser� administrada pelo CAM/CCBC e obedecer� �s normas estabelecidas no seu Regulamento, cujas disposi��es integram o presente contrato.2.2- O tribunal arbitral ser� constitu�do por [um/tr�s] �rbitros, indicados na forma prevista no Regulamento do CAM/CCBC.<br />2.3-. A arbitragem ter� sede em [Cidade, Estado].<br />2.4-. O procedimento arbitral ser� conduzido em [idioma].<br />2.5-. [lei aplic�vel]</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 2, 'MODELOS DE CL�USULAS ES', '', '<p>ES<em><strong>Sugest�o de cl�usulas adotadas pelo CAM/CCBC:</strong></em></p><p><strong>I - Cl�usula Padr�o:</strong><br />�Qualquer lit�gio origin�rio do presente contrato, inclusive quanto � sua interpreta��o ou execu��o, ser� definitivamente resolvido por arbitragem, administrada pelo Centro de Arbitragem e Media��o da C�mara de Com�rcio Brasil-Canad� (�CAM/CCBC�), de acordo com o seu Regulamento, constituindo-se o tribunal arbitral de [um/tr�s] �rbitros, indicados na forma do citado Regulamento.�</p><p>&nbsp;<strong>II. Cl�usula Detalhada:</strong><br />1- Qualquer disputa oriunda deste contrato ou com ele relacionada ser� definitivamente resolvida por arbitragem.<br />1.1- A arbitragem ser� administrada pelo Centro de Arbitragem e Media��o da C�mara de Com�rcio Brasil-Canad� (�CAM/CCBC�) e obedecer� �s normas estabelecidas no seu Regulamento, cujas disposi��es integram o presente contrato.1.2- O tribunal arbitral ser� constitu�do por [um/tr�s] �rbitros, indicados na forma prevista no Regulamento do CAM/CCBC.<br />1.3-. A arbitragem ter� sede em [Cidade, Estado].<br />1.4-. O procedimento arbitral ser� conduzido em [idioma].<br />1.5-. [lei aplic�vel]</p><p><strong>III - Cl�usula Padr�o Escalona Med-Arb.:</strong><br />Qualquer conflito origin�rio do presente contrato, inclusive quanto � sua interpreta��o ou execu��o, ser� submetido obrigatoriamente � Media��o, administrada pelo Centro de Arbitragem e Media��o da C�mara de Com�rcio Brasil-Canad� (�CAM/CCBC�), de acordo com o seu Roteiro e Regimento de Media��o, a ser coordenada por Mediador participante da Lista de Mediadores do CAM/CCBC, indicado na forma das citadas normas.O conflito n�o resolvido pela media��o, conforme a cl�usula de media��o acima, ser� definitivamente resolvido por arbitragem, administrada pelo mesmo CAM/CCBC, de acordo com o seu Regulamento, constituindo-se o tribunal arbitral de tr�s �rbitros, indicados na forma do citado Regulamento.</p><p>&nbsp;<strong>IV. Cl�usula Detalhada Escalonada Med-Arb.:</strong><br />1- Qualquer conflito origin�rio do presente contrato, inclusive quanto � sua interpreta��o ou execu��o, ser� submetido obrigatoriamente � Media��o, administrada pelo Centro de Arbitragem e Media��o da C�mara de Com�rcio Brasil-Canad� (�CAM/CCBC�), de acordo com o seu Roteiro e Regimento de Media��o, a ser coordenada por Mediador participante da Lista de Mediadores do CAM/CCBC, indicado na forma das citadas normas.<br />1.1- O conflito n�o resolvido pela media��o, conforme a cl�usula de media��o acima, ser� definitivamente resolvido por arbitragem, administrada pelo mesmo CAM/CCBC, de acordo com o seu Regulamento.<br />2.1- A arbitragem ser� administrada pelo CAM/CCBC e obedecer� �s normas estabelecidas no seu Regulamento, cujas disposi��es integram o presente contrato.2.2- O tribunal arbitral ser� constitu�do por [um/tr�s] �rbitros, indicados na forma prevista no Regulamento do CAM/CCBC.<br />2.3-. A arbitragem ter� sede em [Cidade, Estado].<br />2.4-. O procedimento arbitral ser� conduzido em [idioma].<br />2.5-. [lei aplic�vel]</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 3, 'MODELOS DE CL�USULAS EN', '', '<p>EN<em><strong>Sugest�o de cl�usulas adotadas pelo CAM/CCBC:</strong></em></p><p><strong>I - Cl�usula Padr�o:</strong><br />�Qualquer lit�gio origin�rio do presente contrato, inclusive quanto � sua interpreta��o ou execu��o, ser� definitivamente resolvido por arbitragem, administrada pelo Centro de Arbitragem e Media��o da C�mara de Com�rcio Brasil-Canad� (�CAM/CCBC�), de acordo com o seu Regulamento, constituindo-se o tribunal arbitral de [um/tr�s] �rbitros, indicados na forma do citado Regulamento.�</p><p>&nbsp;<strong>II. Cl�usula Detalhada:</strong><br />1- Qualquer disputa oriunda deste contrato ou com ele relacionada ser� definitivamente resolvida por arbitragem.<br />1.1- A arbitragem ser� administrada pelo Centro de Arbitragem e Media��o da C�mara de Com�rcio Brasil-Canad� (�CAM/CCBC�) e obedecer� �s normas estabelecidas no seu Regulamento, cujas disposi��es integram o presente contrato.1.2- O tribunal arbitral ser� constitu�do por [um/tr�s] �rbitros, indicados na forma prevista no Regulamento do CAM/CCBC.<br />1.3-. A arbitragem ter� sede em [Cidade, Estado].<br />1.4-. O procedimento arbitral ser� conduzido em [idioma].<br />1.5-. [lei aplic�vel]</p><p><strong>III - Cl�usula Padr�o Escalona Med-Arb.:</strong><br />Qualquer conflito origin�rio do presente contrato, inclusive quanto � sua interpreta��o ou execu��o, ser� submetido obrigatoriamente � Media��o, administrada pelo Centro de Arbitragem e Media��o da C�mara de Com�rcio Brasil-Canad� (�CAM/CCBC�), de acordo com o seu Roteiro e Regimento de Media��o, a ser coordenada por Mediador participante da Lista de Mediadores do CAM/CCBC, indicado na forma das citadas normas.O conflito n�o resolvido pela media��o, conforme a cl�usula de media��o acima, ser� definitivamente resolvido por arbitragem, administrada pelo mesmo CAM/CCBC, de acordo com o seu Regulamento, constituindo-se o tribunal arbitral de tr�s �rbitros, indicados na forma do citado Regulamento.</p><p>&nbsp;<strong>IV. Cl�usula Detalhada Escalonada Med-Arb.:</strong><br />1- Qualquer conflito origin�rio do presente contrato, inclusive quanto � sua interpreta��o ou execu��o, ser� submetido obrigatoriamente � Media��o, administrada pelo Centro de Arbitragem e Media��o da C�mara de Com�rcio Brasil-Canad� (�CAM/CCBC�), de acordo com o seu Roteiro e Regimento de Media��o, a ser coordenada por Mediador participante da Lista de Mediadores do CAM/CCBC, indicado na forma das citadas normas.<br />1.1- O conflito n�o resolvido pela media��o, conforme a cl�usula de media��o acima, ser� definitivamente resolvido por arbitragem, administrada pelo mesmo CAM/CCBC, de acordo com o seu Regulamento.<br />2.1- A arbitragem ser� administrada pelo CAM/CCBC e obedecer� �s normas estabelecidas no seu Regulamento, cujas disposi��es integram o presente contrato.2.2- O tribunal arbitral ser� constitu�do por [um/tr�s] �rbitros, indicados na forma prevista no Regulamento do CAM/CCBC.<br />2.3-. A arbitragem ter� sede em [Cidade, Estado].<br />2.4-. O procedimento arbitral ser� conduzido em [idioma].<br />2.5-. [lei aplic�vel]</p>')
Update tblMenu Set PublicacaoId = @PublicacaoId, MenuTipoAcaoId=1, LinkURL = 'Interna/' + cAST(@PublicacaoId AS VARCHAR) Where MenuId = @MenuId

Insert Into tblPublicacaoRestricao(PublicacaoId, Privado) Values(@PublicacaoId, @Privado)




--- GEST�O
Declare @PublicacaoId Int,
		@Privado Bit,
		@MenuId Int,
		@SiteId Int,
		@PublicacaoTipoId Int
Set @SiteId = 2
Set @MenuId = 1
Set @Privado = 0
Set @PublicacaoTipoId = 3
Insert Into tblPublicacao(SiteId,PublicacaoTipoId,Data,DataValidade,Posicao,Destaque)
Values(@SiteId,@PublicacaoTipoId,'2015-01-04',Null,1,0)
Select @PublicacaoId = @@Identity
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 1, 'SCOTIABANK', '', '<p>O Scotiabank, uma das principais institui��es financeiras das Am�ricas, fornece aos seus clientes uma ampla gama de produtos e servi�os de capital markets, e de corporate e investment banking. Nossa equipe de profissionais no Brasil e ao redor do mundo ajudam nossos clientes a aproveitarem oportunidades de neg�cios, agregando valores, e atingindo metas financeiras.</p><p><strong>Comunicado Importante</strong> Esclarecemos que o Scotiabank Brasil atua exclusivamente como banco de atacado, especializado em opera��es de cr�dito para grandes empresas (Corporate Banking), financiamento ao com�rcio exterior (Trade Finance), e em opera��es de mercado de capitais local e internacional, bem como opera��es de c�mbio e de tesouraria. Nossos clientes s�o empresas e institui��es financeiras de origem dom�stica e internacional de m�dio e grande portes.</p><p>N�o oferecemos produtos ou servi�os para pessoas f�sicas, tampouco enviamos e-mails ou qualquer tipo de mensagem ofertando quaisquer produtos ou servi�os.</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 2, 'SCOTIABANK ES', '', '<p>O Scotiabank, uma das principais institui��es financeiras das Am�ricas, fornece aos seus clientes uma ampla gama de produtos e servi�os de capital markets, e de corporate e investment banking. Nossa equipe de profissionais no Brasil e ao redor do mundo ajudam nossos clientes a aproveitarem oportunidades de neg�cios, agregando valores, e atingindo metas financeiras.</p><p><strong>Comunicado Importante</strong> Esclarecemos que o Scotiabank Brasil atua exclusivamente como banco de atacado, especializado em opera��es de cr�dito para grandes empresas (Corporate Banking), financiamento ao com�rcio exterior (Trade Finance), e em opera��es de mercado de capitais local e internacional, bem como opera��es de c�mbio e de tesouraria. Nossos clientes s�o empresas e institui��es financeiras de origem dom�stica e internacional de m�dio e grande portes.</p><p>N�o oferecemos produtos ou servi�os para pessoas f�sicas, tampouco enviamos e-mails ou qualquer tipo de mensagem ofertando quaisquer produtos ou servi�os.</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 3, 'SCOTIABANK EN', '', '<p>O Scotiabank, uma das principais institui��es financeiras das Am�ricas, fornece aos seus clientes uma ampla gama de produtos e servi�os de capital markets, e de corporate e investment banking. Nossa equipe de profissionais no Brasil e ao redor do mundo ajudam nossos clientes a aproveitarem oportunidades de neg�cios, agregando valores, e atingindo metas financeiras.</p><p><strong>Comunicado Importante</strong> Esclarecemos que o Scotiabank Brasil atua exclusivamente como banco de atacado, especializado em opera��es de cr�dito para grandes empresas (Corporate Banking), financiamento ao com�rcio exterior (Trade Finance), e em opera��es de mercado de capitais local e internacional, bem como opera��es de c�mbio e de tesouraria. Nossos clientes s�o empresas e institui��es financeiras de origem dom�stica e internacional de m�dio e grande portes.</p><p>N�o oferecemos produtos ou servi�os para pessoas f�sicas, tampouco enviamos e-mails ou qualquer tipo de mensagem ofertando quaisquer produtos ou servi�os.</p>')
Update tblMenu Set PublicacaoId = @PublicacaoId, MenuTipoAcaoId=1, LinkURL = 'Materia/' + cAST(@PublicacaoId AS VARCHAR) Where MenuId = @MenuId

Insert Into tblPublicacaoRestricao(PublicacaoId, Privado) Values(@PublicacaoId, @Privado)



---- PUBLICAR MATERIA CRIANDO MENU
Declare @NovoMenuId Int
Declare @MenuPaiId Int
Set @MenuPaiId = 1
Insert Into tblMenu(MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL)
Values			   (@MenuPaiId		 , 2		 , 1			 , 1			 , Null		   , Null)
Select @NovoMenuId = @@Identity
	Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(@NovoMenuId, 1, 'Aluguel')
	Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(@NovoMenuId, 2, 'Aluguel')
	Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(@NovoMenuId, 3, 'Aluguel')

Declare @PublicacaoId Int,
		@Privado Bit,
		@MenuId Int,
		@SiteId Int,
		@PublicacaoTipoId Int
Set @SiteId = 2
Set @MenuId = @NovoMenuId
Set @Privado = 0
Set @PublicacaoTipoId = 3
Insert Into tblPublicacao(SiteId,PublicacaoTipoId,Data,DataValidade,Posicao,Destaque)
Values(@SiteId,@PublicacaoTipoId,'2015-01-04',Null,1,0)
Select @PublicacaoId = @@Identity
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 1, 'ALuguel de Casa', '', '<p>O Scotiabank, uma das principais institui��es financeiras das Am�ricas, fornece aos seus clientes uma ampla gama de produtos e servi�os de capital markets, e de corporate e investment banking. Nossa equipe de profissionais no Brasil e ao redor do mundo ajudam nossos clientes a aproveitarem oportunidades de neg�cios, agregando valores, e atingindo metas financeiras.</p><p><strong>Comunicado Importante</strong> Esclarecemos que o Scotiabank Brasil atua exclusivamente como banco de atacado, especializado em opera��es de cr�dito para grandes empresas (Corporate Banking), financiamento ao com�rcio exterior (Trade Finance), e em opera��es de mercado de capitais local e internacional, bem como opera��es de c�mbio e de tesouraria. Nossos clientes s�o empresas e institui��es financeiras de origem dom�stica e internacional de m�dio e grande portes.</p><p>N�o oferecemos produtos ou servi�os para pessoas f�sicas, tampouco enviamos e-mails ou qualquer tipo de mensagem ofertando quaisquer produtos ou servi�os.</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 2, 'ALuguel de Casa', '', '<p>O Scotiabank, uma das principais institui��es financeiras das Am�ricas, fornece aos seus clientes uma ampla gama de produtos e servi�os de capital markets, e de corporate e investment banking. Nossa equipe de profissionais no Brasil e ao redor do mundo ajudam nossos clientes a aproveitarem oportunidades de neg�cios, agregando valores, e atingindo metas financeiras.</p><p><strong>Comunicado Importante</strong> Esclarecemos que o Scotiabank Brasil atua exclusivamente como banco de atacado, especializado em opera��es de cr�dito para grandes empresas (Corporate Banking), financiamento ao com�rcio exterior (Trade Finance), e em opera��es de mercado de capitais local e internacional, bem como opera��es de c�mbio e de tesouraria. Nossos clientes s�o empresas e institui��es financeiras de origem dom�stica e internacional de m�dio e grande portes.</p><p>N�o oferecemos produtos ou servi�os para pessoas f�sicas, tampouco enviamos e-mails ou qualquer tipo de mensagem ofertando quaisquer produtos ou servi�os.</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 3, 'ALuguel de Casa', '', '<p>O Scotiabank, uma das principais institui��es financeiras das Am�ricas, fornece aos seus clientes uma ampla gama de produtos e servi�os de capital markets, e de corporate e investment banking. Nossa equipe de profissionais no Brasil e ao redor do mundo ajudam nossos clientes a aproveitarem oportunidades de neg�cios, agregando valores, e atingindo metas financeiras.</p><p><strong>Comunicado Importante</strong> Esclarecemos que o Scotiabank Brasil atua exclusivamente como banco de atacado, especializado em opera��es de cr�dito para grandes empresas (Corporate Banking), financiamento ao com�rcio exterior (Trade Finance), e em opera��es de mercado de capitais local e internacional, bem como opera��es de c�mbio e de tesouraria. Nossos clientes s�o empresas e institui��es financeiras de origem dom�stica e internacional de m�dio e grande portes.</p><p>N�o oferecemos produtos ou servi�os para pessoas f�sicas, tampouco enviamos e-mails ou qualquer tipo de mensagem ofertando quaisquer produtos ou servi�os.</p>')
Update tblMenu Set PublicacaoId = @PublicacaoId, MenuTipoAcaoId=1, LinkURL = 'Materia/' + cast(@PublicacaoId as varchar) Where MenuId = @MenuId

Insert Into tblPublicacaoRestricao(PublicacaoId, Privado) Values(@PublicacaoId, @Privado)


---- PUBLICAR MATERIA CRIANDO MENU
Declare @NovoMenuId Int
Declare @MenuPaiId Int
Set @MenuPaiId = 515
Insert Into tblMenu(MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL)
Values			   (@MenuPaiId		 , 2		 , 1			 , 1			 , Null		   , Null)
Select @NovoMenuId = @@Identity
	Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(@NovoMenuId, 1, 'Contrato (PDF)')
	Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(@NovoMenuId, 2, 'Regula (PDF)')
	Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(@NovoMenuId, 3, 'Contract (PDF)')

Declare @PublicacaoId Int,
		@Privado Bit,
		@MenuId Int,
		@SiteId Int,
		@PublicacaoTipoId Int
Set @SiteId = 2
Set @MenuId = @NovoMenuId
Set @Privado = 0
Set @PublicacaoTipoId = 3
Insert Into tblPublicacao(SiteId,PublicacaoTipoId,Data,DataValidade,Posicao,Destaque)
Values(@SiteId,@PublicacaoTipoId,'2015-01-04',Null,1,0)
Select @PublicacaoId = @@Identity
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 1, Null, Null, Null)
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 2, Null, Null, Null)
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 3, Null, Null, Null)
Update tblMenu Set PublicacaoId = @PublicacaoId, MenuTipoAcaoId=1, LinkURL = 'File?id=10053' Where MenuId = @MenuId

Insert Into tblPublicacaoRestricao(PublicacaoId, Privado) Values(@PublicacaoId, @Privado)


--- 1.CRIAR MENU
Declare @NovoMenuId Int
Declare @MenuPaiId Int
Set @MenuPaiId = 6
Insert Into tblMenu(MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL)
Values			   (@MenuPaiId, 2	 , 1		 , 1			 , Null		   , Null)
Select @NovoMenuId = @@Identity
	Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(@NovoMenuId, 1, 'Stackholder')
	Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(@NovoMenuId, 2, 'Stackholder')
	Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(@NovoMenuId, 3, 'Stackholder')
-- sELECT * FROM TBLMENU
--- 2.CRIAR PUBLICACAO
Declare @PublicacaoId Int,
		@Privado Bit,
		@MenuId Int,
		@SiteId Int,
		@PublicacaoTipoId Int
Set @SiteId = 2
Set @MenuId = 517
Set @PublicacaoTipoId = 3
Insert Into tblPublicacao(SiteId,PublicacaoTipoId,Data,DataValidade,Posicao,Destaque)
Values(@SiteId,@PublicacaoTipoId,'2015-01-04',Null,1,0)
Select @PublicacaoId = @@Identity
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 1, 'Gestao de Pessoal', '', 'conteudo blablabla')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 2, 'Gerencia de gente', '', 'conteudo ES blablabla')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 3, 'People Management', '', 'conteudo EN blablabla')

--- 4.DEFINIR PUBLICO
Declare @PublicacaoId Int, @Privado Bit
Set @PublicacaoId = 24
Set @Privado = 1
Insert Into tblPublicacaoRestricao(PublicacaoId, Privado) Values(@PublicacaoId, @Privado)
select * from tblPublicacaoRestricao
-- Adiciona as configuracoes de privacidade da publicacao
Insert Into tblPublicacaoRestricaoUsuarioGrupo(PublicacaoRestricaoId, UsuarioGrupoId) Values(24, 1)

-- Remover Regra de Restricao da publicacao (Sem regra a publicao nao pode ser carregada)
delete tblPublicacaoRestricao where PublicacaoId = 24
--select * from tblPublicacao
--- 5.VINCULAR NO MENU
Declare @MenuId Int, @PublicacaoId Int
Set @PublicacaoId = 24
Set @MenuId = 517
Update tblMenu Set PublicacaoId = @PublicacaoId, MenuTipoAcaoId=1, LinkURL = 'Materia/' + cast(@PublicacaoId as varchar) Where MenuId = @MenuId

select * from tblMenu


-----
Update tblMenu Set LinkURL = 'File?id=10053' where MenuId = 516


-------
Insert Into tblMenu(MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL)
Values			   (1		 , 2	 , 1		 , 1			 , 20		   , 'Materia/20')

Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(513, 1, 'Scotiabank')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(513, 2, 'Scotiabank')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(513, 3, 'Scotiabank')

Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(514, 1, 'Relat�rio (PDF)')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(514, 2, 'Relato (PDF)')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(514, 3, 'Report (PDF)')

----


----
Select * From tblPublicacao Where PublicacaoId = 20
Update tblPublicacao Set PublicacaoTipoId = 3 Where PublicacaoId = 20
---
select * from tblMenu

update tblMenu set PublicacaoId = Null, linkurl = '' where menuid = 1

----
select * from tblBanner