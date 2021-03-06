
/****** Object:  StoredProcedure [dbo].[USP_SEL_Configuracao]    Script Date: 03/03/2015 19:54:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_SEL_Configuracao											*
* Objetivo  : Retorna dados de configuração por site.						*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 03/03/2015	                                            *
* Descrição	    : Adicionado campo HostBase									*
USP_SEL_Configuracao @SiteId = 1
****************************************************************************/

ALTER Procedure [dbo].[USP_SEL_Configuracao](
	@SiteId Int
)
As
Begin

	Select 
		ConfiguracaoId, SiteId, ContatoTelefonePrincipal, ContatoEmailPrincipal, Localizacao, LocalizacaoComplemento, EmailHost, EmailUsername, EmailPassword, EmailDisplayName, LinkMapa, HostBase
	From 
		tblConfiguracao
	Where
		SiteId = @SiteId

End