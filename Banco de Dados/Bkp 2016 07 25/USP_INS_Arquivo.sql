USE [cdbc_prod]
GO
/****** Object:  StoredProcedure [dbo].[USP_INS_Arquivo]    Script Date: 25/07/2016 23:26:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****
Select * From tblArquivo Where ArquivoId = 10202
Select * From tblArquivoArquivoCategoria
Select * From tblPublicacaoArquivo
USP_INS_Arquivo @ArquivoId=0, @Legenda='Teste do Ed', @ListaCategoria='2,3'
USP_DEL_Arquivo @ArquivoId=10184

13/05/2015 - Armazenar nome do arquivo.

*****/

ALTER Procedure [dbo].[USP_INS_Arquivo](
	@ArquivoId		BigInt			= Null,
	@Content		VarBinary(Max)	= Null,
	@Legenda		VarChar(80)		= Null,
	@ListaCategoria VarChar(20)		= Null,
	@NovoContent	Bit				= 0,
	@Tipo			VarChar(255)	= Null,
	@PastaId		Int				= Null,
	@NomeArquivo	VarChar(255)	= Null
)
As
Begin

	If @PastaId = 0 Set @PastaId = Null

	--If Not Exists(Select 1 From tblArquivo Where ArquivoId = @ArquivoId) and isnull(@ArquivoId,0) <> 0 Begin

	--	Set Identity_Insert tblArquivo On
	--	Insert Into tblArquivo(ArquivoId, Content, Legenda, Tipo, PastaId, NomeArquivo) Values(@ArquivoId, @Content, @Legenda, @Tipo, @PastaId, @NomeArquivo)
	--	Set Identity_Insert tblArquivo Off
	--	Select @ArquivoId = @@IDENTITY

	--End Else Begin

	If IsNull(@ArquivoId, 0) = 0 Begin

		Insert Into tblArquivo(Content, Legenda, Tipo, PastaId, NomeArquivo) Values(@Content, @Legenda, @Tipo, @PastaId, @NomeArquivo)
		Select @ArquivoId = @@IDENTITY

	End Else Begin

		If @NovoContent = 1 Begin
			Update 
				tblArquivo 
			Set 
				Legenda = @Legenda,
				Content = @Content,
				Tipo = @Tipo,
				NomeArquivo = @NomeArquivo
			Where 
				ArquivoId = @ArquivoId
		End Else Begin
			Update 
				tblArquivo 
			Set 
				Legenda = @Legenda 
			Where 
				ArquivoId = @ArquivoId
		End
	
	End

	Delete 
		tblArquivoArquivoCategoria
	From
		tblArquivoArquivoCategoria C
	Where
		C.ArquivoId = @ArquivoId	
		And C.ArquivoCategoriaId Not In (Select Item From dbo.fnSplit(@ListaCategoria, ','))


	Insert Into tblArquivoArquivoCategoria(ArquivoId,ArquivoCategoriaId)
	Select
		@ArquivoId, Item
	From
		dbo.fnSplit(@ListaCategoria, ',') C
	Where
		C.Item Not In (Select AAC.ArquivoCategoriaId From tblArquivoArquivoCategoria AAC Where AAC.ArquivoId = @ArquivoId)

	Select @ArquivoId As ArquivoId

End
