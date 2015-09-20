
Create Procedure [dbo].[USP_DEL_Banner](
	@BannerId Int
) As
Begin

	Declare @indErro bit,
			@msgErro VarChar(120)

	Set @indErro = 0
	Set @msgErro = ''

	Delete From tblBannerIdiomaExcecao Where BannerId = @BannerId 
	Delete From tblBanner Where BannerId = @BannerId

	Select @indErro indErro, @msgErro msgErro

End