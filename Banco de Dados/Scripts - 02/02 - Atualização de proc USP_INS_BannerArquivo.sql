
/****** Object:  StoredProcedure [dbo].[USP_INS_BannerArquivo]    Script Date: 21/02/2015 13:27:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbUSA-SMART													*
* Procedure : USP_INS_BannerArquivo											*
* Objetivo  : Inclusão de Arquivo no media center de Banner.				*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 21/02/2015	                                            *
* Descrição	    : Data de inclusão.											*
****************************************************************************/
ALTER Procedure [dbo].[USP_INS_BannerArquivo](
	@BannerId		Int,
	@ArquivoId	Int
)
As
Begin
 
	Declare @BannerArquivoId BigInt
	Select @BannerArquivoId = BannerArquivoId From tblBannerArquivo Where BannerId = @BannerId And ArquivoId = @ArquivoId

	If @BannerArquivoId Is Null Begin
		Insert Into tblBannerArquivo(BannerId, ArquivoId, DataInclusao) Values(@BannerId, @ArquivoId, GetDate())
	End

	Select @@IDENTITY BannerArquivoId

End
