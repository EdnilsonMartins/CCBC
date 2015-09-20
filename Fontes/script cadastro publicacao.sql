

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
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 1, 'HISTÓRICO', '', '<p>A Câmara de Comércio Brasil-Canadá foi pioneira no Brasil em matéria de arbitragem, pois já em 26 de julho de 1979 criara a sua Comissão de Arbitragem, hoje Centro de Arbitragem, com vistas a proporcionar meios fáceis e ágeis para a solução de litígios, quer entre pessoas físicas, quer, sobretudo, entre pessoas jurídicas em matéria contratual.</p><p>Devido à falta de legislação adequada e também de uma cultura propícia a esse tipo de solução de controvérsias, não foram muitos os casos levados à então Comissão de Arbitragem.</p><p>O quadro, porém, está se alterando rapidamente com a superação daqueles dois fatores negativos. Em primeiro lugar, o Brasil conta, desde 1996, com uma lei moderna e bastante funcional: a Lei 9.307, que oferece o respaldo jurídico necessário tanto para as partes quanto para os árbitros. Por outro lado, a mentalidade refratária à adoção de soluções arbitrais vem mudando em função dos requisitos de celeridade, tecnicidade e segurança exigidos pela vida econômica moderna. Tudo isso tem levado a uma crescente adoção de cláusulas arbitrais nos mais variados tipos de contratos celebrados por empresas e mesmo por particulares.</p><p>Dentro desse quadro, a CCBC promoveu a completa reforma do seu regulamento de arbitragem ao qual foi introduzido igualmente um roteiro para procedimentos de mediação. Ambos estão perfeitamente adaptados à nova legislação e seguem os padrões vigentes nos modernos centros de arbitragem do mundo desenvolvido.</p><p>Não é necessário repisar as incontáveis vantagens trazidas pelo sistema arbitral de solução ou composição de conflitos. Os três requisitos acima mencionados - celeridade, tecnicidade e segurança falam por si. Com efeito, as controvérsias no mundo dos negócios envolvem cada vez mais aspectos técnicos de alta complexidade cujo equacionamento nem sempre se ajusta aos formalismos puramente processuais. Exigem não apenas proficiência jurídica, mas conhecimentos especializados dos mercados relevantes e de suas formas de operação. Daí poderem integrar os tribunais arbitrais não apenas juristas mas especialistas de outros ramos do conhecimento.</p><p>Um ponto importante a ser realçado é o fato de em uma arbitragem reduzir-se em muito o caráter de animosidade que sói marcar as partes nos litígios forenses. Muitas vezes essas partes, que num dado momento levam as suas diferenças de avaliação a um árbitro ou árbitros, estarão dentro em breve juntas, lado a lado, em outro importante negócio como, por exemplo, a formação de um consórcio para o suprimento de equipamentos. Interessa-lhes, portanto, evitar o atrito e as seqüelas próprias das intermináveis batalhas judiciais. Acrescente-se a tudo isto o sigilo e a confiança pessoal de cada parte no árbitro por ela escolhido, deixando-a tranqüila quanto à escolha do terceiro árbitro, isto quando as duas partes não convierem na indicação de um único julgador.</p><p>A essas e tantas outras vantagens do instituto, deve-se somar uma outra de caráter eminentemente social: a sua contribuição para o descongestionamento do Judiciário, mal que aflige a grande maioria dos países, mas que é indubitavelmente gravíssimo, sendo mesmo objeto de preocupação junto a órgãos internacionais que acompanham a evolução da sociedade brasileira.</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 2, 'ESTORIA', '', 'Es <p>A Câmara de Comércio Brasil-Canadá foi pioneira no Brasil em matéria de arbitragem, pois já em 26 de julho de 1979 criara a sua Comissão de Arbitragem, hoje Centro de Arbitragem, com vistas a proporcionar meios fáceis e ágeis para a solução de litígios, quer entre pessoas físicas, quer, sobretudo, entre pessoas jurídicas em matéria contratual.</p><p>Devido à falta de legislação adequada e também de uma cultura propícia a esse tipo de solução de controvérsias, não foram muitos os casos levados à então Comissão de Arbitragem.</p><p>O quadro, porém, está se alterando rapidamente com a superação daqueles dois fatores negativos. Em primeiro lugar, o Brasil conta, desde 1996, com uma lei moderna e bastante funcional: a Lei 9.307, que oferece o respaldo jurídico necessário tanto para as partes quanto para os árbitros. Por outro lado, a mentalidade refratária à adoção de soluções arbitrais vem mudando em função dos requisitos de celeridade, tecnicidade e segurança exigidos pela vida econômica moderna. Tudo isso tem levado a uma crescente adoção de cláusulas arbitrais nos mais variados tipos de contratos celebrados por empresas e mesmo por particulares.</p><p>Dentro desse quadro, a CCBC promoveu a completa reforma do seu regulamento de arbitragem ao qual foi introduzido igualmente um roteiro para procedimentos de mediação. Ambos estão perfeitamente adaptados à nova legislação e seguem os padrões vigentes nos modernos centros de arbitragem do mundo desenvolvido.</p><p>Não é necessário repisar as incontáveis vantagens trazidas pelo sistema arbitral de solução ou composição de conflitos. Os três requisitos acima mencionados - celeridade, tecnicidade e segurança falam por si. Com efeito, as controvérsias no mundo dos negócios envolvem cada vez mais aspectos técnicos de alta complexidade cujo equacionamento nem sempre se ajusta aos formalismos puramente processuais. Exigem não apenas proficiência jurídica, mas conhecimentos especializados dos mercados relevantes e de suas formas de operação. Daí poderem integrar os tribunais arbitrais não apenas juristas mas especialistas de outros ramos do conhecimento.</p><p>Um ponto importante a ser realçado é o fato de em uma arbitragem reduzir-se em muito o caráter de animosidade que sói marcar as partes nos litígios forenses. Muitas vezes essas partes, que num dado momento levam as suas diferenças de avaliação a um árbitro ou árbitros, estarão dentro em breve juntas, lado a lado, em outro importante negócio como, por exemplo, a formação de um consórcio para o suprimento de equipamentos. Interessa-lhes, portanto, evitar o atrito e as seqüelas próprias das intermináveis batalhas judiciais. Acrescente-se a tudo isto o sigilo e a confiança pessoal de cada parte no árbitro por ela escolhido, deixando-a tranqüila quanto à escolha do terceiro árbitro, isto quando as duas partes não convierem na indicação de um único julgador.</p><p>A essas e tantas outras vantagens do instituto, deve-se somar uma outra de caráter eminentemente social: a sua contribuição para o descongestionamento do Judiciário, mal que aflige a grande maioria dos países, mas que é indubitavelmente gravíssimo, sendo mesmo objeto de preocupação junto a órgãos internacionais que acompanham a evolução da sociedade brasileira.</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 3, 'HISTORY', '', 'EN <p>A Câmara de Comércio Brasil-Canadá foi pioneira no Brasil em matéria de arbitragem, pois já em 26 de julho de 1979 criara a sua Comissão de Arbitragem, hoje Centro de Arbitragem, com vistas a proporcionar meios fáceis e ágeis para a solução de litígios, quer entre pessoas físicas, quer, sobretudo, entre pessoas jurídicas em matéria contratual.</p><p>Devido à falta de legislação adequada e também de uma cultura propícia a esse tipo de solução de controvérsias, não foram muitos os casos levados à então Comissão de Arbitragem.</p><p>O quadro, porém, está se alterando rapidamente com a superação daqueles dois fatores negativos. Em primeiro lugar, o Brasil conta, desde 1996, com uma lei moderna e bastante funcional: a Lei 9.307, que oferece o respaldo jurídico necessário tanto para as partes quanto para os árbitros. Por outro lado, a mentalidade refratária à adoção de soluções arbitrais vem mudando em função dos requisitos de celeridade, tecnicidade e segurança exigidos pela vida econômica moderna. Tudo isso tem levado a uma crescente adoção de cláusulas arbitrais nos mais variados tipos de contratos celebrados por empresas e mesmo por particulares.</p><p>Dentro desse quadro, a CCBC promoveu a completa reforma do seu regulamento de arbitragem ao qual foi introduzido igualmente um roteiro para procedimentos de mediação. Ambos estão perfeitamente adaptados à nova legislação e seguem os padrões vigentes nos modernos centros de arbitragem do mundo desenvolvido.</p><p>Não é necessário repisar as incontáveis vantagens trazidas pelo sistema arbitral de solução ou composição de conflitos. Os três requisitos acima mencionados - celeridade, tecnicidade e segurança falam por si. Com efeito, as controvérsias no mundo dos negócios envolvem cada vez mais aspectos técnicos de alta complexidade cujo equacionamento nem sempre se ajusta aos formalismos puramente processuais. Exigem não apenas proficiência jurídica, mas conhecimentos especializados dos mercados relevantes e de suas formas de operação. Daí poderem integrar os tribunais arbitrais não apenas juristas mas especialistas de outros ramos do conhecimento.</p><p>Um ponto importante a ser realçado é o fato de em uma arbitragem reduzir-se em muito o caráter de animosidade que sói marcar as partes nos litígios forenses. Muitas vezes essas partes, que num dado momento levam as suas diferenças de avaliação a um árbitro ou árbitros, estarão dentro em breve juntas, lado a lado, em outro importante negócio como, por exemplo, a formação de um consórcio para o suprimento de equipamentos. Interessa-lhes, portanto, evitar o atrito e as seqüelas próprias das intermináveis batalhas judiciais. Acrescente-se a tudo isto o sigilo e a confiança pessoal de cada parte no árbitro por ela escolhido, deixando-a tranqüila quanto à escolha do terceiro árbitro, isto quando as duas partes não convierem na indicação de um único julgador.</p><p>A essas e tantas outras vantagens do instituto, deve-se somar uma outra de caráter eminentemente social: a sua contribuição para o descongestionamento do Judiciário, mal que aflige a grande maioria dos países, mas que é indubitavelmente gravíssimo, sendo mesmo objeto de preocupação junto a órgãos internacionais que acompanham a evolução da sociedade brasileira.</p>')
Update tblMenu Set PublicacaoId = @PublicacaoId, MenuTipoAcaoId=1, LinkURL = 'Materia/' + cAST(@PublicacaoId AS VARCHAR) Where MenuId = 5


Insert Into tblPublicacaoRestricao(PublicacaoId, Privado) Values(@PublicacaoId, @Privado)

--- GESTÃO
Declare @PublicacaoId Int,
		@Privado Bit
Set @Privado = 0

Insert Into tblPublicacao(SiteId,PublicacaoTipoId,Data,DataValidade,Posicao,Destaque)
Values(2,3,'2015-01-04',Null,1,0)
Select @PublicacaoId = @@Identity
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 1, 'GESTÃO', '', '<p>Conteúdo da Gestão.</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 2, 'CHEFIA', '', '<p>Conteúdo da Gestão em Espanhol.</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 3, 'MANAGEMENT', '', '<p>Conteúdo da Gestão em Inglês.</p>')
Update tblMenu Set PublicacaoId = @PublicacaoId, MenuTipoAcaoId=1, LinkURL = 'Materia/' + cAST(@PublicacaoId AS VARCHAR) Where MenuId = 6

Insert Into tblPublicacaoRestricao(PublicacaoId, Privado) Values(@PublicacaoId, @Privado)


--- GESTÃO
Declare @PublicacaoId Int,
		@Privado Bit,
		@MenuId Int
Set @MenuId = 7
Set @Privado = 0

Insert Into tblPublicacao(SiteId,PublicacaoTipoId,Data,DataValidade,Posicao,Destaque)
Values(2,3,'2015-01-04',Null,1,0)
Select @PublicacaoId = @@Identity
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 1, 'ISO 9001', '', '<p>Conteúdo da ISO 9001.</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 2, 'ISO 9001 ES', '', '<p>Conteúdo da ISO 9001 em Espanhol.</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 3, 'ISO 9001 EN', '', '<p>Conteúdo da ISO 9001 em Inglês.</p>')
Update tblMenu Set PublicacaoId = @PublicacaoId, MenuTipoAcaoId=1, LinkURL = 'Materia/' + cAST(@PublicacaoId AS VARCHAR) Where MenuId = @MenuId

Insert Into tblPublicacaoRestricao(PublicacaoId, Privado) Values(@PublicacaoId, @Privado)

--- GESTÃO
Declare @PublicacaoId Int,
		@Privado Bit,
		@MenuId Int
Set @MenuId = 8
Set @Privado = 0

Insert Into tblPublicacao(SiteId,PublicacaoTipoId,Data,DataValidade,Posicao,Destaque)
Values(2,3,'2015-01-04',Null,1,0)
Select @PublicacaoId = @@Identity
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 1, 'rESERVA DE SAL', '', '<p>Conteúdo da rESERVA DE SAL.</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 2, 'rESERVA DE SAL ES', '', '<p>Conteúdo da rESERVA DE SAL em Espanhol.</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 3, 'rESERVA DE SAL EN', '', '<p>Conteúdo da rESERVA DE SAL em Inglês.</p>')
Update tblMenu Set PublicacaoId = @PublicacaoId, MenuTipoAcaoId=1, LinkURL = 'Materia/' + cAST(@PublicacaoId AS VARCHAR) Where MenuId = @MenuId

Insert Into tblPublicacaoRestricao(PublicacaoId, Privado) Values(@PublicacaoId, @Privado)

--- GESTÃO
Declare @PublicacaoId Int,
		@Privado Bit,
		@MenuId Int
Set @MenuId = 16
Set @Privado = 0

Insert Into tblPublicacao(SiteId,PublicacaoTipoId,Data,DataValidade,Posicao,Destaque)
Values(2,3,'2015-01-04',Null,1,0)
Select @PublicacaoId = @@Identity
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 1, '2o. Andar', '', '<p>Conteúdo da 2o. Andar.</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 2, '2o. Andar ES', '', '<p>Conteúdo da 2o. Andar em Espanhol.</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 3, '2o. Andar EN', '', '<p>Conteúdo da 2o. Andar em Inglês.</p>')
Update tblMenu Set PublicacaoId = @PublicacaoId, MenuTipoAcaoId=1, LinkURL = 'Materia/' + cAST(@PublicacaoId AS VARCHAR) Where MenuId = @MenuId

Insert Into tblPublicacaoRestricao(PublicacaoId, Privado) Values(@PublicacaoId, @Privado)


--- GESTÃO
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
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 1, 'MODELOS DE CLÁUSULAS', '', '<p><em><strong>Sugestão de cláusulas adotadas pelo CAM/CCBC:</strong></em></p><p><strong>I - Cláusula Padrão:</strong><br />“Qualquer litígio originário do presente contrato, inclusive quanto à sua interpretação ou execução, será definitivamente resolvido por arbitragem, administrada pelo Centro de Arbitragem e Mediação da Câmara de Comércio Brasil-Canadá (“CAM/CCBC”), de acordo com o seu Regulamento, constituindo-se o tribunal arbitral de [um/três] árbitros, indicados na forma do citado Regulamento.”</p><p>&nbsp;<strong>II. Cláusula Detalhada:</strong><br />1- Qualquer disputa oriunda deste contrato ou com ele relacionada será definitivamente resolvida por arbitragem.<br />1.1- A arbitragem será administrada pelo Centro de Arbitragem e Mediação da Câmara de Comércio Brasil-Canadá (“CAM/CCBC”) e obedecerá às normas estabelecidas no seu Regulamento, cujas disposições integram o presente contrato.1.2- O tribunal arbitral será constituído por [um/três] árbitros, indicados na forma prevista no Regulamento do CAM/CCBC.<br />1.3-. A arbitragem terá sede em [Cidade, Estado].<br />1.4-. O procedimento arbitral será conduzido em [idioma].<br />1.5-. [lei aplicável]</p><p><strong>III - Cláusula Padrão Escalona Med-Arb.:</strong><br />Qualquer conflito originário do presente contrato, inclusive quanto à sua interpretação ou execução, será submetido obrigatoriamente à Mediação, administrada pelo Centro de Arbitragem e Mediação da Câmara de Comércio Brasil-Canadá (“CAM/CCBC”), de acordo com o seu Roteiro e Regimento de Mediação, a ser coordenada por Mediador participante da Lista de Mediadores do CAM/CCBC, indicado na forma das citadas normas.O conflito não resolvido pela mediação, conforme a cláusula de mediação acima, será definitivamente resolvido por arbitragem, administrada pelo mesmo CAM/CCBC, de acordo com o seu Regulamento, constituindo-se o tribunal arbitral de três árbitros, indicados na forma do citado Regulamento.</p><p>&nbsp;<strong>IV. Cláusula Detalhada Escalonada Med-Arb.:</strong><br />1- Qualquer conflito originário do presente contrato, inclusive quanto à sua interpretação ou execução, será submetido obrigatoriamente à Mediação, administrada pelo Centro de Arbitragem e Mediação da Câmara de Comércio Brasil-Canadá (“CAM/CCBC”), de acordo com o seu Roteiro e Regimento de Mediação, a ser coordenada por Mediador participante da Lista de Mediadores do CAM/CCBC, indicado na forma das citadas normas.<br />1.1- O conflito não resolvido pela mediação, conforme a cláusula de mediação acima, será definitivamente resolvido por arbitragem, administrada pelo mesmo CAM/CCBC, de acordo com o seu Regulamento.<br />2.1- A arbitragem será administrada pelo CAM/CCBC e obedecerá às normas estabelecidas no seu Regulamento, cujas disposições integram o presente contrato.2.2- O tribunal arbitral será constituído por [um/três] árbitros, indicados na forma prevista no Regulamento do CAM/CCBC.<br />2.3-. A arbitragem terá sede em [Cidade, Estado].<br />2.4-. O procedimento arbitral será conduzido em [idioma].<br />2.5-. [lei aplicável]</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 2, 'MODELOS DE CLÁUSULAS ES', '', '<p>ES<em><strong>Sugestão de cláusulas adotadas pelo CAM/CCBC:</strong></em></p><p><strong>I - Cláusula Padrão:</strong><br />“Qualquer litígio originário do presente contrato, inclusive quanto à sua interpretação ou execução, será definitivamente resolvido por arbitragem, administrada pelo Centro de Arbitragem e Mediação da Câmara de Comércio Brasil-Canadá (“CAM/CCBC”), de acordo com o seu Regulamento, constituindo-se o tribunal arbitral de [um/três] árbitros, indicados na forma do citado Regulamento.”</p><p>&nbsp;<strong>II. Cláusula Detalhada:</strong><br />1- Qualquer disputa oriunda deste contrato ou com ele relacionada será definitivamente resolvida por arbitragem.<br />1.1- A arbitragem será administrada pelo Centro de Arbitragem e Mediação da Câmara de Comércio Brasil-Canadá (“CAM/CCBC”) e obedecerá às normas estabelecidas no seu Regulamento, cujas disposições integram o presente contrato.1.2- O tribunal arbitral será constituído por [um/três] árbitros, indicados na forma prevista no Regulamento do CAM/CCBC.<br />1.3-. A arbitragem terá sede em [Cidade, Estado].<br />1.4-. O procedimento arbitral será conduzido em [idioma].<br />1.5-. [lei aplicável]</p><p><strong>III - Cláusula Padrão Escalona Med-Arb.:</strong><br />Qualquer conflito originário do presente contrato, inclusive quanto à sua interpretação ou execução, será submetido obrigatoriamente à Mediação, administrada pelo Centro de Arbitragem e Mediação da Câmara de Comércio Brasil-Canadá (“CAM/CCBC”), de acordo com o seu Roteiro e Regimento de Mediação, a ser coordenada por Mediador participante da Lista de Mediadores do CAM/CCBC, indicado na forma das citadas normas.O conflito não resolvido pela mediação, conforme a cláusula de mediação acima, será definitivamente resolvido por arbitragem, administrada pelo mesmo CAM/CCBC, de acordo com o seu Regulamento, constituindo-se o tribunal arbitral de três árbitros, indicados na forma do citado Regulamento.</p><p>&nbsp;<strong>IV. Cláusula Detalhada Escalonada Med-Arb.:</strong><br />1- Qualquer conflito originário do presente contrato, inclusive quanto à sua interpretação ou execução, será submetido obrigatoriamente à Mediação, administrada pelo Centro de Arbitragem e Mediação da Câmara de Comércio Brasil-Canadá (“CAM/CCBC”), de acordo com o seu Roteiro e Regimento de Mediação, a ser coordenada por Mediador participante da Lista de Mediadores do CAM/CCBC, indicado na forma das citadas normas.<br />1.1- O conflito não resolvido pela mediação, conforme a cláusula de mediação acima, será definitivamente resolvido por arbitragem, administrada pelo mesmo CAM/CCBC, de acordo com o seu Regulamento.<br />2.1- A arbitragem será administrada pelo CAM/CCBC e obedecerá às normas estabelecidas no seu Regulamento, cujas disposições integram o presente contrato.2.2- O tribunal arbitral será constituído por [um/três] árbitros, indicados na forma prevista no Regulamento do CAM/CCBC.<br />2.3-. A arbitragem terá sede em [Cidade, Estado].<br />2.4-. O procedimento arbitral será conduzido em [idioma].<br />2.5-. [lei aplicável]</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 3, 'MODELOS DE CLÁUSULAS EN', '', '<p>EN<em><strong>Sugestão de cláusulas adotadas pelo CAM/CCBC:</strong></em></p><p><strong>I - Cláusula Padrão:</strong><br />“Qualquer litígio originário do presente contrato, inclusive quanto à sua interpretação ou execução, será definitivamente resolvido por arbitragem, administrada pelo Centro de Arbitragem e Mediação da Câmara de Comércio Brasil-Canadá (“CAM/CCBC”), de acordo com o seu Regulamento, constituindo-se o tribunal arbitral de [um/três] árbitros, indicados na forma do citado Regulamento.”</p><p>&nbsp;<strong>II. Cláusula Detalhada:</strong><br />1- Qualquer disputa oriunda deste contrato ou com ele relacionada será definitivamente resolvida por arbitragem.<br />1.1- A arbitragem será administrada pelo Centro de Arbitragem e Mediação da Câmara de Comércio Brasil-Canadá (“CAM/CCBC”) e obedecerá às normas estabelecidas no seu Regulamento, cujas disposições integram o presente contrato.1.2- O tribunal arbitral será constituído por [um/três] árbitros, indicados na forma prevista no Regulamento do CAM/CCBC.<br />1.3-. A arbitragem terá sede em [Cidade, Estado].<br />1.4-. O procedimento arbitral será conduzido em [idioma].<br />1.5-. [lei aplicável]</p><p><strong>III - Cláusula Padrão Escalona Med-Arb.:</strong><br />Qualquer conflito originário do presente contrato, inclusive quanto à sua interpretação ou execução, será submetido obrigatoriamente à Mediação, administrada pelo Centro de Arbitragem e Mediação da Câmara de Comércio Brasil-Canadá (“CAM/CCBC”), de acordo com o seu Roteiro e Regimento de Mediação, a ser coordenada por Mediador participante da Lista de Mediadores do CAM/CCBC, indicado na forma das citadas normas.O conflito não resolvido pela mediação, conforme a cláusula de mediação acima, será definitivamente resolvido por arbitragem, administrada pelo mesmo CAM/CCBC, de acordo com o seu Regulamento, constituindo-se o tribunal arbitral de três árbitros, indicados na forma do citado Regulamento.</p><p>&nbsp;<strong>IV. Cláusula Detalhada Escalonada Med-Arb.:</strong><br />1- Qualquer conflito originário do presente contrato, inclusive quanto à sua interpretação ou execução, será submetido obrigatoriamente à Mediação, administrada pelo Centro de Arbitragem e Mediação da Câmara de Comércio Brasil-Canadá (“CAM/CCBC”), de acordo com o seu Roteiro e Regimento de Mediação, a ser coordenada por Mediador participante da Lista de Mediadores do CAM/CCBC, indicado na forma das citadas normas.<br />1.1- O conflito não resolvido pela mediação, conforme a cláusula de mediação acima, será definitivamente resolvido por arbitragem, administrada pelo mesmo CAM/CCBC, de acordo com o seu Regulamento.<br />2.1- A arbitragem será administrada pelo CAM/CCBC e obedecerá às normas estabelecidas no seu Regulamento, cujas disposições integram o presente contrato.2.2- O tribunal arbitral será constituído por [um/três] árbitros, indicados na forma prevista no Regulamento do CAM/CCBC.<br />2.3-. A arbitragem terá sede em [Cidade, Estado].<br />2.4-. O procedimento arbitral será conduzido em [idioma].<br />2.5-. [lei aplicável]</p>')
Update tblMenu Set PublicacaoId = @PublicacaoId, MenuTipoAcaoId=1, LinkURL = 'Interna/' + cAST(@PublicacaoId AS VARCHAR) Where MenuId = @MenuId

Insert Into tblPublicacaoRestricao(PublicacaoId, Privado) Values(@PublicacaoId, @Privado)




--- GESTÃO
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
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 1, 'SCOTIABANK', '', '<p>O Scotiabank, uma das principais instituições financeiras das Américas, fornece aos seus clientes uma ampla gama de produtos e serviços de capital markets, e de corporate e investment banking. Nossa equipe de profissionais no Brasil e ao redor do mundo ajudam nossos clientes a aproveitarem oportunidades de negócios, agregando valores, e atingindo metas financeiras.</p><p><strong>Comunicado Importante</strong> Esclarecemos que o Scotiabank Brasil atua exclusivamente como banco de atacado, especializado em operações de crédito para grandes empresas (Corporate Banking), financiamento ao comércio exterior (Trade Finance), e em operações de mercado de capitais local e internacional, bem como operações de câmbio e de tesouraria. Nossos clientes são empresas e instituições financeiras de origem doméstica e internacional de médio e grande portes.</p><p>Não oferecemos produtos ou serviços para pessoas físicas, tampouco enviamos e-mails ou qualquer tipo de mensagem ofertando quaisquer produtos ou serviços.</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 2, 'SCOTIABANK ES', '', '<p>O Scotiabank, uma das principais instituições financeiras das Américas, fornece aos seus clientes uma ampla gama de produtos e serviços de capital markets, e de corporate e investment banking. Nossa equipe de profissionais no Brasil e ao redor do mundo ajudam nossos clientes a aproveitarem oportunidades de negócios, agregando valores, e atingindo metas financeiras.</p><p><strong>Comunicado Importante</strong> Esclarecemos que o Scotiabank Brasil atua exclusivamente como banco de atacado, especializado em operações de crédito para grandes empresas (Corporate Banking), financiamento ao comércio exterior (Trade Finance), e em operações de mercado de capitais local e internacional, bem como operações de câmbio e de tesouraria. Nossos clientes são empresas e instituições financeiras de origem doméstica e internacional de médio e grande portes.</p><p>Não oferecemos produtos ou serviços para pessoas físicas, tampouco enviamos e-mails ou qualquer tipo de mensagem ofertando quaisquer produtos ou serviços.</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 3, 'SCOTIABANK EN', '', '<p>O Scotiabank, uma das principais instituições financeiras das Américas, fornece aos seus clientes uma ampla gama de produtos e serviços de capital markets, e de corporate e investment banking. Nossa equipe de profissionais no Brasil e ao redor do mundo ajudam nossos clientes a aproveitarem oportunidades de negócios, agregando valores, e atingindo metas financeiras.</p><p><strong>Comunicado Importante</strong> Esclarecemos que o Scotiabank Brasil atua exclusivamente como banco de atacado, especializado em operações de crédito para grandes empresas (Corporate Banking), financiamento ao comércio exterior (Trade Finance), e em operações de mercado de capitais local e internacional, bem como operações de câmbio e de tesouraria. Nossos clientes são empresas e instituições financeiras de origem doméstica e internacional de médio e grande portes.</p><p>Não oferecemos produtos ou serviços para pessoas físicas, tampouco enviamos e-mails ou qualquer tipo de mensagem ofertando quaisquer produtos ou serviços.</p>')
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
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 1, 'ALuguel de Casa', '', '<p>O Scotiabank, uma das principais instituições financeiras das Américas, fornece aos seus clientes uma ampla gama de produtos e serviços de capital markets, e de corporate e investment banking. Nossa equipe de profissionais no Brasil e ao redor do mundo ajudam nossos clientes a aproveitarem oportunidades de negócios, agregando valores, e atingindo metas financeiras.</p><p><strong>Comunicado Importante</strong> Esclarecemos que o Scotiabank Brasil atua exclusivamente como banco de atacado, especializado em operações de crédito para grandes empresas (Corporate Banking), financiamento ao comércio exterior (Trade Finance), e em operações de mercado de capitais local e internacional, bem como operações de câmbio e de tesouraria. Nossos clientes são empresas e instituições financeiras de origem doméstica e internacional de médio e grande portes.</p><p>Não oferecemos produtos ou serviços para pessoas físicas, tampouco enviamos e-mails ou qualquer tipo de mensagem ofertando quaisquer produtos ou serviços.</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 2, 'ALuguel de Casa', '', '<p>O Scotiabank, uma das principais instituições financeiras das Américas, fornece aos seus clientes uma ampla gama de produtos e serviços de capital markets, e de corporate e investment banking. Nossa equipe de profissionais no Brasil e ao redor do mundo ajudam nossos clientes a aproveitarem oportunidades de negócios, agregando valores, e atingindo metas financeiras.</p><p><strong>Comunicado Importante</strong> Esclarecemos que o Scotiabank Brasil atua exclusivamente como banco de atacado, especializado em operações de crédito para grandes empresas (Corporate Banking), financiamento ao comércio exterior (Trade Finance), e em operações de mercado de capitais local e internacional, bem como operações de câmbio e de tesouraria. Nossos clientes são empresas e instituições financeiras de origem doméstica e internacional de médio e grande portes.</p><p>Não oferecemos produtos ou serviços para pessoas físicas, tampouco enviamos e-mails ou qualquer tipo de mensagem ofertando quaisquer produtos ou serviços.</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 3, 'ALuguel de Casa', '', '<p>O Scotiabank, uma das principais instituições financeiras das Américas, fornece aos seus clientes uma ampla gama de produtos e serviços de capital markets, e de corporate e investment banking. Nossa equipe de profissionais no Brasil e ao redor do mundo ajudam nossos clientes a aproveitarem oportunidades de negócios, agregando valores, e atingindo metas financeiras.</p><p><strong>Comunicado Importante</strong> Esclarecemos que o Scotiabank Brasil atua exclusivamente como banco de atacado, especializado em operações de crédito para grandes empresas (Corporate Banking), financiamento ao comércio exterior (Trade Finance), e em operações de mercado de capitais local e internacional, bem como operações de câmbio e de tesouraria. Nossos clientes são empresas e instituições financeiras de origem doméstica e internacional de médio e grande portes.</p><p>Não oferecemos produtos ou serviços para pessoas físicas, tampouco enviamos e-mails ou qualquer tipo de mensagem ofertando quaisquer produtos ou serviços.</p>')
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

Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(514, 1, 'Relatório (PDF)')
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