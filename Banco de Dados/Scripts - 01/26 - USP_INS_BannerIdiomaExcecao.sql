
Create Procedure [dbo].[USP_INS_BannerIdiomaExcecao](
	@BannerIdiomaExcecaoId	Int,
	@BannerId 				Int,
	@IdiomaId				Int,
	@Titulo					VarChar(80) = Null,
	@Resumo 				VarChar(200) = Null,
	@Descricao				VarChar(500) = Null
)
As
Begin

	If Exists(Select 1 From tblBannerIdiomaExcecao Where BannerId = @BannerId And IdiomaId = @IdiomaId) Begin
		Update 
			tblBannerIdiomaExcecao 
		Set 
			Titulo = @Titulo,
			Resumo = @Resumo,
			Descricao = @Descricao				
		Where 
			BannerId = @BannerId
			And IdiomaId = @IdiomaId
		Select BannerIdiomaExcecaoId From tblBannerIdiomaExcecao Where BannerIdiomaExcecaoId = @BannerIdiomaExcecaoId
	End Else Begin

		Insert Into tblBannerIdiomaExcecao(BannerId, IdiomaId, Titulo, Resumo, Descricao)
		Values(@BannerId, @IdiomaId, @Titulo, @Resumo, @Descricao)
		Select Cast(@@Identity As int) BannerIdiomaExcecaoId
	End

End