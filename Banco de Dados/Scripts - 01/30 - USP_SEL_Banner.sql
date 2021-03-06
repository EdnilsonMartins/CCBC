
Create Procedure [dbo].[USP_SEL_Banner](
	@SiteId				Int,
	@BannerId		Int = Null,
	@BannerTipoId	Int = Null,
	@DataValidade		DateTime = Null,
	@UsuarioId			Int = Null,
	@IdiomaId			Int,
	@RetornarDefault	Bit = 1
) As
Begin

	/*
	Declare @SiteId				Int,
			@PublicacaoId		Int,
			@PublicacaoTipoId	Int,
			@Destaque			Bit,
			@DataValidade		DateTime,
			@UsuarioId			Int,
			@IdiomaId			Int
			
	Set @PublicacaoId = null
	Set @SiteId = 2
	Set @PublicacaoTipoId = Null
	Set @UsuarioId = 1
	Set @IdiomaId = 2
	Set @Destaque = Null
	Set @DataValidade = '2014-12-29'
	*/

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
		B.LinkURL
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
	Order By
		B.Posicao

End
