
/****** Object:  StoredProcedure [dbo].[USP_DEL_Banner]    Script Date: 03/05/2015 21:42:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_DEL_Banner												*
* Objetivo  : Exclusão de banner											*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 04/03/2015	                                            *
* Descrição	    : Remover os vínculos de restricao.							*
*                                                                           *
****************************************************************************/
ALTER Procedure [dbo].[USP_DEL_Banner](
	@BannerId Int
) As
Begin

	Declare @indErro bit,
			@msgErro VarChar(120)

	Set @indErro = 0
	Set @msgErro = ''

	Delete From tblBannerEvento Where BannerId = @BannerId

	Delete From tblBannerRestricaoUsuarioGrupo Where BannerId = @BannerId
	Delete From tblBannerRestricaoUsuario Where BannerId = @BannerId
	Delete From tblBannerAprovacaoHistorico Where BannerId = @BannerId
	Delete From tblBannerAprovacaoItem Where BannerId = @BannerId

	Delete From tblBannerArquivo Where BannerId = @BannerId
	Delete From tblBannerIdiomaExcecao Where BannerId = @BannerId 
	Delete From tblBanner Where BannerId = @BannerId

	Select @indErro indErro, @msgErro msgErro

End