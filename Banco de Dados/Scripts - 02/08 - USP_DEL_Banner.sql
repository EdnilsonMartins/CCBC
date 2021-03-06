
/****** Object:  StoredProcedure [dbo].[USP_DEL_Banner]    Script Date: 21/02/2015 23:48:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER Procedure [dbo].[USP_DEL_Banner](
	@BannerId Int
) As
Begin

	Declare @indErro bit,
			@msgErro VarChar(120)

	Set @indErro = 0
	Set @msgErro = ''

	Delete From tblBannerArquivo Where BannerId = @BannerId
	Delete From tblBannerIdiomaExcecao Where BannerId = @BannerId 
	Delete From tblBanner Where BannerId = @BannerId

	Select @indErro indErro, @msgErro msgErro

End