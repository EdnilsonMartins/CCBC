
/****** Object:  StoredProcedure [dbo].[USP_SEL_Banner]    Script Date: 21/02/2015 19:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_SEL_Banner												*
* Objetivo  : Retorna consulta de Banners cadastrados.						*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 21/02/2015	                                            *
* Descrição	    : Retornar ArquivoId na query.								*
*				  Novo parâmetro @Apenas1, que significa retornar 1 único	*
*				  banner aleatoriamente.									*
USP_SEL_Banner @SiteId = 1, @IdiomaId = 1, @BannerTipoId = 2, @Apenas1 = 1
****************************************************************************/
ALTER Procedure [dbo].[USP_SEL_Banner](
	@SiteId				Int,
	@BannerId		Int = Null,
	@BannerTipoId	Int = Null,
	@DataValidade		DateTime = Null,
	@UsuarioId			Int = Null,
	@IdiomaId			Int,
	@RetornarDefault	Bit = 1,
	@Apenas1			Bit = 0
) As
Begin

	/*
	select * from tblbannertipo
	Declare @SiteId				Int = 1,
			@BannerId			Int = Null,
			@BannerTipoId		Int = 2,
			@DataValidade		DateTime = Null,
			@UsuarioId			Int = Null,
			@IdiomaId			Int = 1,
			@RetornarDefault	Bit = 1,
			@Apenas1			Bit = 1
	*/

	If (@Apenas1 = 1) Begin
		Select
			Top 1
			B.BannerId,
				B.BannerTipoId,
			B.Data,
			B.DataValidade,
			B.Posicao,
			Case When BIE.Titulo Is Null And @RetornarDefault = 1 Then BD.Titulo Else BIE.Titulo	End Titulo,
			Case When BIE.Resumo Is Null And @RetornarDefault = 1 Then BD.Resumo Else BIE.Resumo	End Resumo,
			Case When IsNull(BIE.Descricao,'') = ''  And @RetornarDefault = 1 Then BD.Descricao Else BIE.Descricao	End Descricao,
			B.Privado,
			B.TargetId,
			B.LinkURL,
			dbo.fnGetBannerArquivo(B.BannerId, 5) As ArquivoId_Primaria, -- 5->Imagem Primária
			dbo.fnGetBannerArquivo(B.BannerId, 6) As ArquivoId_Secundaria -- 6->Imagem Secundária
		From
			tblBanner B
		
			Left Join tblBannerIdiomaExcecao BIE On BIE.BannerId = B.BannerId and BIE.IdiomaId = @IdiomaId
			Left Join tblBannerIdiomaExcecao BD On BD.BannerId = B.BannerId And BD.IdiomaId = 1
		Where
			B.SiteId = @SiteId
			And ((@BannerId Is Null) Or (B.BannerId = @BannerId))
			And ((@BannerTipoId Is Null) Or (B.BannerTipoId = @BannerTipoId))
			And ((@DataValidade Is Null) or ((@DataValidade Is Not Null) And (B.DataValidade Is Null Or (B.DataValidade Is Not Null And B.DataValidade <= @DataValidade))))
			And dbo.GetBannerAcessoUsuario(B.BannerId, @UsuarioId)=1
			--And IsNull(B.Liberado, 0) = 1
		Order By
			NewId()
	End	Else Begin
		Select
			B.BannerId,
				B.BannerTipoId,
			B.Data,
			B.DataValidade,
			B.Posicao,
			Case When BIE.Titulo Is Null And @RetornarDefault = 1 Then BD.Titulo Else BIE.Titulo	End Titulo,
			Case When BIE.Resumo Is Null And @RetornarDefault = 1 Then BD.Resumo Else BIE.Resumo	End Resumo,
			Case When IsNull(BIE.Descricao,'') = ''  And @RetornarDefault = 1 Then BD.Descricao Else BIE.Descricao	End Descricao,
			B.Privado,
			B.TargetId,
			B.LinkURL,
			dbo.fnGetBannerArquivo(B.BannerId, 5) As ArquivoId_Primaria, -- 5->Imagem Primária
			dbo.fnGetBannerArquivo(B.BannerId, 6) As ArquivoId_Secundaria -- 6->Imagem Secundária
		From
			tblBanner B
		
			Left Join tblBannerIdiomaExcecao BIE On BIE.BannerId = B.BannerId and BIE.IdiomaId = @IdiomaId
			Left Join tblBannerIdiomaExcecao BD On BD.BannerId = B.BannerId And BD.IdiomaId = 1
		Where
			B.SiteId = @SiteId
			And ((@BannerId Is Null) Or (B.BannerId = @BannerId))
			And ((@BannerTipoId Is Null) Or (B.BannerTipoId = @BannerTipoId))
			And ((@DataValidade Is Null) or ((@DataValidade Is Not Null) And (B.DataValidade Is Null Or (B.DataValidade Is Not Null And B.DataValidade <= @DataValidade))))
			And dbo.GetBannerAcessoUsuario(B.BannerId, @UsuarioId)=1
			And IsNull(B.Liberado, 0) = 1
		Order By
			B.Posicao
	End

End
