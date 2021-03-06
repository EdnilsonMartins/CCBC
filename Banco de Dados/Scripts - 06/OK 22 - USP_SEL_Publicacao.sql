
/****** Object:  StoredProcedure [dbo].[USP_SEL_Publicacao]    Script Date: 08/03/2015 20:17:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_SEL_Publicacao											*
* Objetivo  : Retorna as Publicacoes.										*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 21/02/2015	                                            *
* Descrição	    : Adicionar retorno da foto destaque quando houver.			*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 08/03/2015	                                            *
* Descrição	    : Retornar o campo Liberado.								*
*				  Retornar o campo Privado da tblPublicacao.				*
*				  Retornar DataLiberado.									*
*                 Uso da função fnRetornaUsuarioGrupo.                      *
*                                                                           *
****************************************************************************/
ALTER Procedure [dbo].[USP_SEL_Publicacao](
	@SiteId				Int,
	@PublicacaoId		Int = Null,
	@PublicacaoTipoId	Int = Null,
	@Destaque			Bit = Null,
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
		P.PublicacaoId,
			P.PublicacaoTipoId,
			P.Destaque,
		P.Data,
		P.DataValidade,
		P.Posicao,
		Case When PIE.Titulo Is Null And @RetornarDefault = 1 Then PD.Titulo Else PIE.Titulo	End Titulo,
		Case When PIE.Resumo Is Null And @RetornarDefault = 1 Then PD.Resumo Else PIE.Resumo	End Resumo,
		Case When IsNull(PIE.Conteudo,'') = ''  And @RetornarDefault = 1 Then PD.Conteudo Else PIE.Conteudo	End Conteudo,
		Case When PIE.Editora Is Null Then PD.Editora Else PIE.Editora	End Editora,
		Case When PIE.Fonte Is Null Then PD.Fonte Else PIE.Fonte	End Fonte,
		Case When PIE.Tags Is Null Then PD.Tags Else PIE.Tags	End Tags,
		--Case When PIE.ArquivoDestaqueId Is Null Then PD.ArquivoDestaqueId Else PIE.ArquivoDestaqueId	End ArquivoDestaqueId,
		(Select dbo.fnGetPublicacaoArquivoDestaque(P.PublicacaoId)) As ArquivoDestaqueId,
		P.Privado,
		P.Liberado,
		P.DataLiberado,
		dbo.fnRetornaUsuarioGrupo(P.PublicacaoId) As ListaUsuarioGrupo
	From
		tblPublicacao P
		join tblPublicacaoRestricao PR On PR.PublicacaoId = P.PublicacaoId
	
		Left Join tblPublicacaoIdiomaExcecao PIE On PIE.PublicacaoId = P.PublicacaoId and PIE.IdiomaId = @IdiomaId
		Left Join tblPublicacaoIdiomaExcecao PD On PD.PublicacaoId = P.PublicacaoId And PD.IdiomaId = 1
	Where
		P.SiteId = @SiteId
		And ((@PublicacaoId Is Null) Or (P.PublicacaoId = @PublicacaoId))
		And ((@Destaque Is Null) Or (P.Destaque = @Destaque))
		And ((@PublicacaoTipoId Is Null) Or (P.PublicacaoTipoId = @PublicacaoTipoId))
		And ((@DataValidade Is Null) or ((@DataValidade Is Not Null) And (P.DataValidade Is Null Or (P.DataValidade Is Not Null And P.DataValidade <= @DataValidade))))
		And dbo.GetPublicacaoAcessoUsuario(P.PublicacaoId, @UsuarioId)=1
	Order By
		P.Posicao

End
