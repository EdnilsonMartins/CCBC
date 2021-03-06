--prod 22:00

/****** Object:  StoredProcedure [dbo].[USP_SEL_Menu]    Script Date: 31/05/2015 18:59:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_SEL_Menu													*
* Objetivo  : Retorna os dados do Menu										*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Alteração: 22/02/2015	                                            *
* Descrição	    : Retornar o Target do Menu.								*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Alteração: 13/03/2015	                                            *
* Descrição	    : Ajuste no MenuTipoId quando já for informato o parametro	*
*				  MenuTipoId.												*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Alteração: 15/03/2015	                                            *
* Descrição	    : Ajuste para trazer ordenado o MenuTipoId					*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Alteração: 06/04/2015	                                            *
* Descrição	    : Ajuste para trazer o rótulo conforme Idioma selecionado.	*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 08/04/2015	                                            *
* Descrição	    : Adicionado parâmetro para @FiltrarPrivacidade.			*
*                 Uso da função fnRetornaUsuarioGrupoMenu	                *
*				  Adicionado parâmetro @UsuarioId							*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 31/05/2015	                                            *
* Descrição	    : Adicionado coluna no retorno da query ListaUsuario.		*
*                                                                           *
****************************************************************************/
ALTER Procedure [dbo].[USP_SEL_Menu](
	@SiteId		Int = Null,
	@MenuTipoId	Int = Null,
	@IdiomaId	Int,
	@PublicacaoId Int = Null,
	@MenuId		Int = Null,
	@ExibirTodos Bit = 0,
	@UsuarioId			Int = Null,
	@FiltrarPrivacidade Bit = 1
)
As

Begin

	
	/*
	Declare @MenuTipoId Int, @IdiomaId Int, @SiteId Int,
			@PublicacaoId Int, @ExibeIrmaos Bit
	Set @SiteId = 2
	Set @MenuTipoId = 1
	Set @IdiomaId = 1
	Set @PublicacaoId = 1
	Set @ExibeIrmaos = 0
	*/

	Declare @MenuPaiId Int

	if (@PublicacaoId Is Not Null) Begin
		Select @MenuPaiId = MenuId, @MenuTipoId = MenuTipoId From tblMenu Where PublicacaoId = @PublicacaoId
		If Not Exists(Select 1 From tblMenu Where MenuPaiId = @MenuPaiId) Begin
			Select @MenuPaiId = MenuPaiId From tblMenu Where PublicacaoId = @PublicacaoId
		end
	End 
	
	If @ExibirTodos = 1 Begin
		Select
			M.MenuId,
			M.SiteId,
			M.MenuPaiId,
			M.MenuTipoId,
			M.MenuTipoAcaoId,
			M.PublicacaoId,
			M.TargetId,

			--MD.Rotulo,
			Case When MIE.Rotulo Is Null Then MD.Rotulo Else MIE.Rotulo	End Rotulo,
			M.LinkURL,
			(select 'File?Id=' + cast(dbo.fnGetMenuIcone(M.MenuId) as varchar)) As ImageURL,
			M.Privado,
			dbo.fnRetornaUsuarioGrupoMenu(M.MenuId) As ListaUsuarioGrupo,
			dbo.fnRetornaUsuarioMenu(M.MenuId) As ListaUsuario

		From
			tblMenu M
			Left Join tblMenuIdiomaExcecao MIE On MIE.MenuId = M.MenuId and MIE.IdiomaId = @IdiomaId
			Left Join tblMenuIdiomaExcecao MD On MD.MenuId = M.MenuId And MD.IdiomaId = 1
	
		Where
			1=1
			And M.SiteId = @SiteId
			And M.MenuTipoId = @MenuTipoId
			And ((@FiltrarPrivacidade = 0) Or (@FiltrarPrivacidade = 1 And dbo.GetMenuAcessoUsuario(M.MenuId, @UsuarioId)=1))
		Order By
			M.MenuTipoId,
			M.Posicao
	End Else Begin
		Select
			M.MenuId,
			M.SiteId,
			M.MenuPaiId,
			M.MenuTipoId,
			M.MenuTipoAcaoId,
			M.PublicacaoId,
			M.TargetId,

			Case When MIE.Rotulo Is Null Then MD.Rotulo Else MIE.Rotulo	End Rotulo,
			M.LinkURL,
			(select 'File?Id=' + cast(dbo.fnGetMenuIcone(M.MenuId) as varchar)) As ImageURL,
			M.Privado,
			dbo.fnRetornaUsuarioGrupoMenu(M.MenuId) As ListaUsuarioGrupo,
			dbo.fnRetornaUsuarioMenu(M.MenuId) As ListaUsuario
		From
			tblMenu M
			Left Join tblMenuIdiomaExcecao MIE On MIE.MenuId = M.MenuId and MIE.IdiomaId = @IdiomaId
			Left Join tblMenuIdiomaExcecao MD On MD.MenuId = M.MenuId And MD.IdiomaId = 1
	
		Where
			1=1
			And (
					(@MenuId Is Not Null And M.MenuId = @MenuId)
					Or
					(		@MenuId Is Null 
						And 
							(M.SiteId = @SiteId)

						And
							(
								(@MenuPaiId Is Null And M.MenuPaiId Is Null And @PublicacaoId Is Null)
								Or 
								(M.MenuPaiId = @MenuPaiId )
							)
		
						And 
							((IsNull(@MenuTipoId,0)=0) Or (M.MenuTipoId = @MenuTipoId ))
					)
			)
			And ((@FiltrarPrivacidade = 0) Or (@FiltrarPrivacidade = 1 And dbo.GetMenuAcessoUsuario(M.MenuId, @UsuarioId)=1))
		Order By 
			M.MenuTipoId,
			M.Posicao
	End
End
