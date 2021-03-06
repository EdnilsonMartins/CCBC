
Create Procedure [dbo].[USP_INS_Banner](
	@BannerId			Int,
	@SiteId				Int,
	@BannerTipoId		Int = Null,
	@TargetId			Int = Null,
	@Data				DateTime = Null,
	@DataValidade		DateTime = Null,
	@Posicao			Int = Null,
	@LinkURL			VarChar(500) = Null,
	@Privado			Bit = 0
)
As
Begin

	If @BannerId <> 0 Begin
		Update 
			tblBanner
		Set 
			BannerTipoId = @BannerTipoId,
			TargetId = @TargetId,
			Data = @Data, 
			DataValidade = @DataValidade, 
			Posicao = @Posicao,
			LinkURL = @LinkURL,
			Privado = @Privado
		Where 
			BannerId = @BannerId
		Select @BannerId BannerId
	End Else Begin

		Insert Into tblBanner(SiteId, BannerTipoId, TargetId, Data, DataValidade, Posicao, LinkURL, Privado) 
		Values(@SiteId, @BannerTipoId, @TargetId, @Data, @DataValidade, @Posicao, @LinkURL, @Privado)
		Select Cast(@@Identity As int) BannerId
	End

End