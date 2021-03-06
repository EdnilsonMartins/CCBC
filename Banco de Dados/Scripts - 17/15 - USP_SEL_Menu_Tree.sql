--prod 22:00

/****** Object:  StoredProcedure [dbo].[USP_SEL_Menu_Tree]    Script Date: 31/05/2015 19:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_SEL_Menu_Tree												*
* Objetivo  : Retorna os dados do Menu para (tree of navigation - navbar)	*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Alteração: 22/02/2015	                                            *
* Descrição	    : Retornar o Target do Menu.								*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 08/04/2015	                                            *
* Descrição	    : Retornar os campos										*
*				  - Privado													*
*				  - ListaUsuarioGrupo										*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 31/05/2015	                                            *
* Descrição	    : Adicionado coluna no retorno da query ListaUsuario.		*
*                                                                           *
****************************************************************************/
ALTER Procedure [dbo].[USP_SEL_Menu_Tree](
	@IdiomaId	Int,
	@PublicacaoId Int = Null
) As
Begin

	Declare @MenuId Int

	/*
	Declare @IdiomaId Int, @PublicacaoId Int
	Set @PublicacaoId = 7
	Set @IdiomaId = 1
	*/

	Select @MenuId = MenuId From tblMenu Where PublicacaoId = @PublicacaoId
	
	Declare @MenuPaiId Int,
			@Rotulo VarChar(50)

	Create Table #tmpMenu(
		Id Int Identity,
		MenuId	Int
	)

	Insert Into #tmpMenu(MenuId) Values(@MenuId)

	While (@MenuId Is Not Null) Begin
		Select @MenuPaiId = MenuPaiId From tblMenu Where MenuId = @MenuId
		Set @MenuId = @MenuPaiId
		If (@MenuId Is Not Null) Begin
			Insert Into #tmpMenu(MenuId) Values(@MenuPaiId)
		End
	End

	Select 
		M1.Id, 
		M2.MenuId, 
		M2.SiteId,
		M2.MenuPaiId, 
		M2.MenuTipoId, 
		M2.MenuTipoAcaoId, 
		M2.PublicacaoId, 
		Case When MIE.Rotulo Is Null Then MD.Rotulo Else MIE.Rotulo	End Rotulo,
		M2.LinkURL, 
		M2.ImageURL,
		M2.TargetId,
		M2.Privado,
		dbo.fnRetornaUsuarioGrupoMenu(M2.MenuId) As ListaUsuarioGrupo,
		dbo.fnRetornaUsuarioMenu(M2.MenuId) As ListaUsuario
	From 
		#tmpMenu M1 
		Join tblMenu M2 on M1.MenuId = M2.MenuId
		Left Join tblMenuIdiomaExcecao MIE On MIE.MenuId = M2.MenuId and MIE.IdiomaId = @IdiomaId
		Left Join tblMenuIdiomaExcecao MD On MD.MenuId = M2.MenuId And MD.IdiomaId = 1


	Drop Table #tmpMenu

End
