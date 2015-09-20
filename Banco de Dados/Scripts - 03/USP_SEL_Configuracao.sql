Create Procedure USP_SEL_Configuracao(
	@SiteId Int
)
As
Begin

	Select 
		ConfiguracaoId, SiteId, ContatoTelefonePrincipal, ContatoEmailPrincipal, Localizacao, LocalizacaoComplemento, EmailHost, EmailUsername, EmailPassword, EmailDisplayName, LinkMapa
	From 
		tblConfiguracao
	Where
		SiteId = @SiteId

End