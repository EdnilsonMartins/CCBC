

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_INS_Configuracao											*
* Objetivo  : Manutenção das configurações gerais do site.					*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	:   /06/2015	                                            *
* Descrição	    :															*
****************************************************************************/

Create Procedure [dbo].[USP_INS_Configuracao](
	@SiteId Int,
	@ContatoTelefonePrincipal VarChar(100),
	@ContatoEmailPrincipal VarChar(100),
	@Localizacao VarChar(100),
	@LocalizacaoComplemento VarChar(100),
	@EmailHost VarChar(100),
	@EmailUsername VarChar(100),
	@EmailPassword VarChar(100),
	@EmailDisplayName VarChar(100),
	@LinkMapa VarChar(500),
	@HostBase VarChar(255),
	@EmailDestino VarChar(255),
	@EmailPorta Int
)
As
Begin

	Declare @ConfiguracaoId Int
	Select @ConfiguracaoId = ConfiguracaoId From tblConfiguracao Where SiteId = @SiteId

	If IsNull(@ConfiguracaoId,0) <> 0 Begin
		Update tblConfiguracao Set 
			ContatoTelefonePrincipal = @ContatoTelefonePrincipal,
			ContatoEmailPrincipal = @ContatoEmailPrincipal,
			Localizacao = @Localizacao,
			LocalizacaoComplemento = @LocalizacaoComplemento,
			EmailHost = @EmailHost,
			EmailUsername = @EmailUsername,
			EmailPassword = @EmailPassword,
			EmailDisplayName =@EmailDisplayName,
			LinkMapa = @LinkMapa,
			HostBase = @HostBase,
			EmailDestino = @EmailDestino,
			EmailPorta = @EmailPorta
		Where
			ConfiguracaoId = @ConfiguracaoId
		Select @ConfiguracaoId As ConfiguracaoId
	End Else Begin
		Insert Into tblConfiguracao(SiteId, ContatoTelefonePrincipal, ContatoEmailPrincipal, Localizacao, LocalizacaoComplemento, EmailHost, EmailUsername, EmailPassword, EmailDisplayName, LinkMapa, HostBase, EmailDestino, EmailPorta)
		Values(@SiteId, @ContatoTelefonePrincipal, @ContatoEmailPrincipal, @Localizacao, @LocalizacaoComplemento, @EmailHost, @EmailUsername, @EmailPassword, @EmailDisplayName, @LinkMapa, @HostBase, @EmailDestino, @EmailPorta)
		Set @ConfiguracaoId = @@Identity
		Select @ConfiguracaoId As ConfiguracaoId
	End

End