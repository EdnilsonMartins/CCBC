
/****** Object:  StoredProcedure [dbo].[USP_SEL_Menu]    Script Date: 22/02/2015 17:47:25 ******/
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
****************************************************************************/
ALTER Procedure [dbo].[USP_SEL_Menu](
	@SiteId		Int = Null,
	@MenuTipoId	Int = Null,
	@IdiomaId	Int,
	@PublicacaoId Int = Null,
	@MenuId		Int = Null,
	@ExibirTodos Bit = 0
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
		Select @MenuPaiId = MenuId From tblMenu Where PublicacaoId = @PublicacaoId
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

			MD.Rotulo,
			M.LinkURL,
			(select 'File?Id=' + cast(dbo.fnGetMenuIcone(M.MenuId) as varchar)) As ImageURL
	
			--M.IdiomaId,
			--M.MenuReferenciaId

		From
			tblMenu M
			--Left Join tblMenuIdiomaExcecao MIE On MIE.MenuId = M.MenuId and MIE.IdiomaId = @IdiomaId
			Left Join tblMenuIdiomaExcecao MD On MD.MenuId = M.MenuId And MD.IdiomaId = 1
	
		Where
			1=1
			And M.SiteId = @SiteId
			And M.MenuTipoId = @MenuTipoId
		Order By
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
			(select 'File?Id=' + cast(dbo.fnGetMenuIcone(M.MenuId) as varchar)) As ImageURL
		
			--M.IdiomaId,
			--M.MenuReferenciaId

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
		Order By 
			M.Posicao
	End
End
