

/****** Object:  StoredProcedure [dbo].[USP_DEL_Arquivo]    Script Date: 03/05/2015 21:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER Procedure [dbo].[USP_DEL_Arquivo](
	@ArquivoId BigInt
) As
Begin

	Declare @indErro bit,
			@msgErro VarChar(120)


	Set @indErro = 0
	Set @msgErro = ''

	Delete From tblBAnnerEvento Where ArquivoId = @ArquivoId

	Delete From tblPublicacaoArquivo Where ArquivoId = @ArquivoId 
	Delete From tblBannerArquivo Where ArquivoId = @ArquivoId 
	Delete From tblMenuArquivo Where ArquivoId = @ArquivoId 

	Delete From tblArquivoArquivoCategoria Where ArquivoId = @ArquivoId 
	Delete From tblArquivo Where ArquivoId = @ArquivoId 
	
	Select @indErro indErro, @msgErro msgErro

End