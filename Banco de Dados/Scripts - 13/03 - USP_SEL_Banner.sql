USE [dbCCBC]
GO
/****** Object:  StoredProcedure [dbo].[USP_SEL_Banner]    Script Date: 03/05/2015 10:13:55 ******/
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
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 03/03/2015	                                            *
* Descrição	    : Retornar Referencia na query.								*
*				  Filtrar banners exclusivos da Publicação.					*
*				  Ajustado o Order By das queries para dar preferência aos	*
*				  banner exclusivos da publicação.							*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 06/03/2015	                                            *
* Descrição	    : Retornar apenas Banner liberado.							*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 08/04/2015	                                            *
* Descrição	    : Retornar campo Ativo e BannerTipo.						*
*			      Adicionado parâmetro para @FiltrarPrivacidade.			*
*                 Uso da função fnRetornaUsuarioGrupoBanner                 *
*				  Retornar apenas Ativos quando FiltroPrivacidade = 1		*

USP_SEL_Banner @SiteId = 1, @IdiomaId = 1, @BannerTipoId = 1

USP_SEL_Banner @SiteId = 2, @IdiomaId = 1, @BannerTipoId = 2, @Apenas1 = 1
****************************************************************************/

ALTER Procedure [dbo].[USP_SEL_Banner](
	@SiteId				Int,
	@BannerId		Int = Null,
	@BannerTipoId	Int = Null,
	@DataValidade		DateTime = Null,
	@UsuarioId			Int = Null,
	@IdiomaId			Int,
	@RetornarDefault	Bit = 1,
	@Apenas1			Bit = 0,
	@PublicidadeId		Int = Null,
	@FiltrarPrivacidade Bit = 1
) As

Begin

	/*
	-- select * from tblbannertipo
	--select * from tblBanner where SiteId = 1
	Declare @SiteId				Int = 1,
			@BannerId			Int = Null,
			@BannerTipoId		Int = Null,
			@DataValidade		DateTime = Null,
			@UsuarioId			Int = Null,
			@IdiomaId			Int = 1,
			@RetornarDefault	Bit = 1,
			@Apenas1			Bit = 0,
			@PublicidadeId		Int = Null
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
			B.Referencia,
			dbo.fnRetornaUsuarioGrupoBanner(B.BannerId) As ListaUsuarioGrupo,
			B.Ativo,
			BT.Descricao BannerTipo,


			--O uso da função para retornar imagem considera que será retornado sempre 1 imagem primária por Banner.
			--Neste caso não são consideradas o sorteio aleatório de várias imagens para o banner.
			--Este foi o primeiro conceito desenvolvido, onde 1 banner só teria 1 imagem primária.
			--Neste primeiro conceito a mesma regra é válida para a imagem secundária.
			/*
			dbo.fnGetBannerArquivo(B.BannerId, 5) As ArquivoId_Primaria,   -- 5 = Imagem Primária
			dbo.fnGetBannerArquivo(B.BannerId, 6) As ArquivoId_Secundaria, -- 6 = Imagem Secundária
			*/


			--Novo conceito aplicado, um banner por conter mais de 1 imagem primária e também mais de 1 imagem secundária.
			--Neste novo conceito a seguinte situação poderá ocorrer: imagens primárias misturando-se com imagens secundárias, pois não existe um controle que vincule imagem primária com secundária.
			-- Retorna uma das Imagens primária do Banner:
			(Select 
					Top 1
					A.ArquivoId ArquivoId
				From
					(
					Select
						--Top 1 
						BA1.ArquivoId ArquivoId,
						B1.BannerId
					From
						tblBanner B1
						inner join tblBannerArquivo BA1 On BA1.BannerId = B1.BannerId 
						inner join tblArquivoArquivoCategoria AAC1 On AAC1.ArquivoId = BA1.ArquivoId
					Where
						B1.BannerId = B.BannerId
						And AAC1.ArquivoCategoriaId = 5
		
					) A
				Order by
					NewId()
			) ArquivoId_Primaria,

			-- Retorna uma das Imagens secundária do Banner:
			(Select 
					Top 1
					A.ArquivoId ArquivoId
				From
					(
					Select
						--Top 1 
						BA1.ArquivoId ArquivoId,
						B1.BannerId
					From
						tblBanner B1
						inner join tblBannerArquivo BA1 On BA1.BannerId = B1.BannerId 
						inner join tblArquivoArquivoCategoria AAC1 On AAC1.ArquivoId = BA1.ArquivoId
					Where
						B1.BannerId = B.BannerId
						And AAC1.ArquivoCategoriaId = 6
		
					) A
				Order by
					NewId()
			) ArquivoId_Secundaria


		From
			tblBanner B
			join tblBannerTipo BT On BT.BannerTipoId = B.BannerTipoId
			Left Join tblBannerIdiomaExcecao BIE On BIE.BannerId = B.BannerId and BIE.IdiomaId = @IdiomaId
			Left Join tblBannerIdiomaExcecao BD On BD.BannerId = B.BannerId And BD.IdiomaId = 1

		Where
			B.SiteId = @SiteId
			And ((@BannerId Is Null) Or (B.BannerId = @BannerId))
			And ((@BannerTipoId Is Null) Or (B.BannerTipoId = @BannerTipoId))
			And ((@DataValidade Is Null) or ((@DataValidade Is Not Null) And (B.DataValidade Is Null Or (B.DataValidade Is Not Null And B.DataValidade <= @DataValidade))))
			And ((@FiltrarPrivacidade = 0) Or (@FiltrarPrivacidade = 1 And B.Ativo = 1 And dbo.GetBannerAcessoUsuario(B.BannerId, @UsuarioId)=1))
			And ((		 @PublicidadeId Is Null) 
					Or ((@PublicidadeId Is Not Null) And (IsNull(B.Referencia, '') = '')) 
					Or ((@PublicidadeId Is Not Null) And (IsNull(B.Referencia, '') <> '') And (@PublicidadeId = Cast(B.Referencia As Int)))
				)
			And ((@PublicidadeId Is Null)
				Or (IsNull(B.Referencia, '') = '')
				Or ((IsNull(B.Referencia, '') <> '') And (@PublicidadeId Is Not Null) And (@PublicidadeId = Cast(B.Referencia As Int)))
			)
			--And IsNull(B.Liberado, 0) = 1
		Order By
			Referencia Desc,
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
			dbo.fnGetBannerArquivo(B.BannerId, 6) As ArquivoId_Secundaria, -- 6->Imagem Secundária
			B.Referencia,
			dbo.fnRetornaUsuarioGrupoBanner(B.BannerId) As ListaUsuarioGrupo,
			B.Ativo,
			BT.Descricao BannerTipo
		From
			tblBanner B
			join tblBannerTipo BT On BT.BannerTipoId = B.BannerTipoId
			Left Join tblBannerIdiomaExcecao BIE On BIE.BannerId = B.BannerId and BIE.IdiomaId = @IdiomaId
			Left Join tblBannerIdiomaExcecao BD On BD.BannerId = B.BannerId And BD.IdiomaId = 1
		Where
			B.SiteId = @SiteId
			And ((@BannerId Is Null) Or (B.BannerId = @BannerId))
			And ((@BannerTipoId Is Null) Or (B.BannerTipoId = @BannerTipoId))
			And ((@DataValidade Is Null) or ((@DataValidade Is Not Null) And (B.DataValidade Is Null Or (B.DataValidade Is Not Null And B.DataValidade <= @DataValidade))))
			And ((@FiltrarPrivacidade = 0) Or (@FiltrarPrivacidade = 1 And B.Ativo = 1 And dbo.GetBannerAcessoUsuario(B.BannerId, @UsuarioId)=1))
			And ((		 @PublicidadeId Is Null) 
					Or ((@PublicidadeId Is Not Null) And (IsNull(B.Referencia, '') = '')) 
					Or ((@PublicidadeId Is Not Null) And (IsNull(B.Referencia, '') <> '') And (@PublicidadeId = Cast(B.Referencia As Int)))
				)
			And ((@PublicidadeId Is Null)
				Or (IsNull(B.Referencia, '') = '')
				Or ((IsNull(B.Referencia, '') <> '') And (@PublicidadeId Is Not Null) And (@PublicidadeId = Cast(B.Referencia As Int)))
				)
			--And IsNull(B.Liberado, 0) = 1
		Order By
			--Referencia Desc,
			B.Posicao
	End

End
