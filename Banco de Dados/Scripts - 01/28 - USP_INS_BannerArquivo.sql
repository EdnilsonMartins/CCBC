

Create Procedure [dbo].[USP_INS_BannerArquivo](
	@BannerId		Int,
	@ArquivoId	Int
)
As
Begin
 
	Declare @BannerArquivoId BigInt
	Select @BannerArquivoId = BannerArquivoId From tblBannerArquivo Where BannerId = @BannerId And ArquivoId = @ArquivoId

	If @BannerArquivoId Is Null Begin
		Insert Into tblBannerArquivo(BannerId, ArquivoId) Values(@BannerId, @ArquivoId)
	End

	Select @@IDENTITY BannerArquivoId

End
