
/****** Object:  UserDefinedFunction [dbo].[fnGetMenuIcone]    Script Date: 21/02/2015 13:18:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create Function [dbo].[fnGetBannerArquivo](
	@BannerId		Int,
	@ArquivoCategoriaId Int
) 
Returns BigInt As
Begin


	--Select * From tblBannerArquivo
	--Declare @BannerId Int
	--Set @BannerId = 5

	Declare @Saida BigInt
	
	Select 
		@Saida = A.ArquivoId
	From
		(
		Select
			Top 1 BA.ArquivoId ArquivoId
		From
			tblBanner B
			inner join tblBannerArquivo BA On BA.BannerId = B.BannerId 
			inner join tblArquivoArquivoCategoria AAC On AAC.ArquivoId = BA.ArquivoId
		Where
			B.BannerId = @BannerId
			And AAC.ArquivoCategoriaId = @ArquivoCategoriaId
		) A

	--Select @Saida
	Return @Saida



End