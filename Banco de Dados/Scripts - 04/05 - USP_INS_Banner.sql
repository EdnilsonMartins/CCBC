
/****** Object:  StoredProcedure [dbo].[USP_INS_Banner]    Script Date: 03/03/2015 20:46:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_INS_Banner												*
* Objetivo  : Manutenção dos Banners.										*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 03/03/2015	                                            *
* Descrição	    : Adicionado parâmetro @Referencia							*
****************************************************************************/

ALTER Procedure [dbo].[USP_INS_Banner](
	@BannerId			Int,
	@SiteId				Int,
	@BannerTipoId		Int = Null,
	@TargetId			Int = Null,
	@Data				DateTime = Null,
	@DataValidade		DateTime = Null,
	@Posicao			Int = Null,
	@LinkURL			VarChar(500) = Null,
	@Privado			Bit = 0,
	@Referencia			VarChar(255) = Null
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
			Privado = @Privado,
			Referencia = @Referencia
		Where 
			BannerId = @BannerId
		Select @BannerId BannerId
	End Else Begin

		Insert Into tblBanner(SiteId, BannerTipoId, TargetId, Data, DataValidade, Posicao, LinkURL, Privado, Referencia) 
		Values(@SiteId, @BannerTipoId, @TargetId, @Data, @DataValidade, @Posicao, @LinkURL, @Privado, @Referencia)
		Select Cast(@@Identity As int) BannerId
	End

End