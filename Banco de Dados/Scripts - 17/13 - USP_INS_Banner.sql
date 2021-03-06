--prod 22:00

/****** Object:  StoredProcedure [dbo].[USP_INS_Banner]    Script Date: 31/05/2015 19:00:40 ******/
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
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 08/04/2015	                                            *
* Descrição	    : Adicionado parâmetro @Ativo								*
*                 Gravação dos Grupos de Usuários da tabela de restrições.  *
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 31/05/2015	                                            *
* Descrição	    : Adicionado parâmetro @ListaUsuario						*
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
	@Referencia			VarChar(255) = Null,
	@ListaUsuarioGrupo	VarChar(80) = Null,
	@ListaUsuario		VarChar(80) = Null,
	@Ativo				Bit = Null
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
			Referencia = @Referencia,
			Ativo = @Ativo
		Where 
			BannerId = @BannerId
		Select @BannerId BannerId
	End Else Begin

		Insert Into tblBanner(SiteId, BannerTipoId, TargetId, Data, DataValidade, Posicao, LinkURL, Privado, Referencia, Ativo) 
		Values(@SiteId, @BannerTipoId, @TargetId, @Data, @DataValidade, @Posicao, @LinkURL, @Privado, @Referencia, @Ativo)
		Select Cast(@@Identity As int) BannerId
	End

	/*** Restrição por Grupo de Usuário ***/
	Delete 
		tblBannerRestricaoUsuarioGrupo
	From
		tblBannerRestricaoUsuarioGrupo B
	Where
		B.BannerId = @BannerId	
		And B.UsuarioGrupoId Not In (Select Item From dbo.fnSplit(@ListaUsuarioGrupo, ','))


	Insert Into tblBannerRestricaoUsuarioGrupo(BannerId, UsuarioGrupoId)
	Select
		@BannerId, Item
	From
		dbo.fnSplit(@ListaUsuarioGrupo, ',') C
	Where
		C.Item Not In (Select AAC.UsuarioGrupoId From tblBannerRestricaoUsuarioGrupo AAC Where AAC.BannerId = @BannerId)
	/**************************************/


	/*** Restrição Usuário ***/
	Delete 
		tblBannerRestricaoUsuario
	From
		tblBannerRestricaoUsuario B
	Where
		B.BannerId = @BannerId	
		And B.UsuarioId Not In (Select Item From dbo.fnSplit(@ListaUsuario, ','))


	Insert Into tblBannerRestricaoUsuario(BannerId, UsuarioId)
	Select
		@BannerId, Item
	From
		dbo.fnSplit(@ListaUsuario, ',') C
	Where
		C.Item Not In (Select AAC.UsuarioId From tblBannerRestricaoUsuario AAC Where AAC.BannerId = @BannerId)
	/*************************/

End