--prod 22:00

/****** Object:  StoredProcedure [dbo].[USP_INS_Menu]    Script Date: 31/05/2015 19:03:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_INS_Menu													*
* Objetivo  : Inserção a atualização dos Menus								*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Alteração: 22/02/2015	                                            *
* Descrição	    : Permitir a atualização do campo PublicacaoId				*
*			      Adicionada opção de Target no menu.						*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Alteração: 15/03/2015	                                            *
* Descrição	    : Tratamento para gravação de PublicacaoId = Null em deter- *
*				  minada situação do menu do Rodapé.						*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 08/04/2015	                                            *
* Descrição	    : Adicionado parâmetro @Privado								*
*                 Gravação dos Grupos de Usuários da tabela de restrições.  *
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 31/05/2015	                                            *
* Descrição	    : Adicionado parâmetro @ListaUsuario						*
****************************************************************************/
ALTER Procedure [dbo].[USP_INS_Menu](
	@MenuId				Int = Null, 
	@MenuPaiId			Int = Null, 
	@SiteId				Int = Null, 
	@MenuTipoId			Int = Null, 
	@MenuTipoAcaoId		Int = Null, 
	@PublicacaoId		Int = Null, 
	@LinkURL			VarChar(500) = Null, 
	@ImageURL			VarChar(500) = Null,
	@TargetId			Int = Null,
	@Privado			Bit = 0,
	@ListaUsuarioGrupo	VarChar(80) = Null,
	@ListaUsuario	VarChar(80) = Null
)
As
Begin

	Declare @Posicao Int = 0

	If @MenuPaiId = 0 Begin
		Set @MenuPaiId = Null
	End

	If (@MenuTipoId = 3) Begin
		If Exists(Select 1 From tblMenu Where PublicacaoId = @PublicacaoId And MenuTipoId <> @MenuTipoId) Begin
			-- Cairá nesta situação quando já houver um outro menu com esta publicação, então será mantido como nullo. 
			-- Isto servirá para que o publicador sempre gere a mesma estrutura de menu.
			Set @PublicacaoId = Null
		End
	End

	If @MenuId <> 0 Begin
		Update 
			tblMenu 
		Set 
			MenuTipoAcaoId = @MenuTipoAcaoId,
			LinkURL = @LinkURL,
			PublicacaoId = @PublicacaoId,
			TargetId = @TargetId,
			Privado = @Privado
		Where 
			MenuId = @MenuId
		Select @MenuId MenuId
	End Else Begin
		
		Declare @P int 
		--declare @MenuPaiId int = 2 
		
		Select @P = Max(isnull(Posicao,0))  From tblMenu Where  (@MenuPaiId Is Null And MenuPaiId Is Null) Or ((@MenuPaiId Is Not Null) And (MenuPaiId = @MenuPaiId))
		Set @P = @P + 1

		Insert Into tblMenu(MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL, ImageURL, Posicao, TargetId, Privado) 
		Values(@MenuPaiId, @SiteId, @MenuTipoId, @MenuTipoAcaoId, @PublicacaoId, @LinkURL, @ImageURL, @P, @TargetId, @Privado)
		Select Cast(@@Identity As int) MenuId
	End

	/*** Restrição por Grupo de Usuário ***/
	Delete 
		tblMenuRestricaoUsuarioGrupo
	From
		tblMenuRestricaoUsuarioGrupo B
	Where
		B.MenuId = @MenuId	
		And B.UsuarioGrupoId Not In (Select Item From dbo.fnSplit(@ListaUsuarioGrupo, ','))


	Insert Into tblMenuRestricaoUsuarioGrupo(MenuId, UsuarioGrupoId)
	Select
		@MenuId, Item
	From
		dbo.fnSplit(@ListaUsuarioGrupo, ',') C
	Where
		C.Item Not In (Select AAC.UsuarioGrupoId From tblMenuRestricaoUsuarioGrupo AAC Where AAC.MenuId = @MenuId)
	/**************************************/


	/*** Restrição por Usuário ***/
	Delete 
		tblMenuRestricaoUsuario
	From
		tblMenuRestricaoUsuario B
	Where
		B.MenuId = @MenuId	
		And B.UsuarioId Not In (Select Item From dbo.fnSplit(@ListaUsuario, ','))


	Insert Into tblMenuRestricaoUsuario(MenuId, UsuarioId)
	Select
		@MenuId, Item
	From
		dbo.fnSplit(@ListaUsuario, ',') C
	Where
		C.Item Not In (Select AAC.UsuarioId From tblMenuRestricaoUsuario AAC Where AAC.MenuId = @MenuId)
	/*****************************/
End