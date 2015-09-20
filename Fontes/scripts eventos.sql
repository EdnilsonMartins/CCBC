Select * From tblPublicacaoTipo





--- GESTÃO
Declare @PublicacaoId Int,
		@Privado Bit,
		@MenuId Int
--Set @MenuId = 16
Set @Privado = 0

Insert Into tblPublicacao(SiteId,PublicacaoTipoId,Data,DataValidade,Posicao,Destaque)
Values(2, 1, '2015-01-10', Null, 1, 0)
Select @PublicacaoId = @@Identity
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 1, 'Dia do Atleta', '', '<p>Dia do pagamento</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 2, 'Atletismo', '', '<p>Receber um tostao</p>')
Insert Into tblPublicacaoIdiomaExcecao(PublicacaoId, IdiomaId, Titulo, Resumo, Conteudo) Values(@PublicacaoId, 3, 'Athletic Day', '', '<p>This is a good day, always!</p>')

Insert Into tblPublicacaoRestricao(PublicacaoId, Privado) Values(@PublicacaoId, @Privado)

Select * From tblPublicacaoRestricao
Insert Into tblPublicacaoRestricaoUsuarioGrupo(PublicacaoRestricaoId, UsuarioGrupoId) Values(26, 1)

---

Update tblPublicacaoIdiomaExcecao
Set ArquivoDestaqueId = 13
--Select PIE.* 
From tblPublicacao P join tblPublicacaoIdiomaExcecao PIE on PIE.PublicacaoId = P.PublicacaoId
where P.PublicacaoTipoId = 1 And PIE.ArquivoDestaqueId Is Null