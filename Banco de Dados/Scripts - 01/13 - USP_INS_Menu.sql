alter Procedure [dbo].[USP_INS_Menu](
	@MenuId				Int = Null, 
	@MenuPaiId			Int = Null, 
	@SiteId				Int = Null, 
	@MenuTipoId			Int = Null, 
	@MenuTipoAcaoId		Int = Null, 
	@PublicacaoId		Int = Null, 
	@LinkURL			VarChar(500) = Null, 
	@ImageURL			VarChar(500) = Null
)
As
Begin

	Declare @Posicao Int = 0

	If @MenuPaiId = 0 Begin
		Set @MenuPaiId = Null
	End

	If @MenuId <> 0 Begin
		Update 
			tblMenu 
		Set 
			MenuTipoAcaoId = @MenuTipoAcaoId,
			LinkURL = @LinkURL
		Where 
			MenuId = @MenuId
		Select @MenuId MenuId
	End Else Begin
		
		Declare @P int 
		--declare @MenuPaiId int = 2 
		
		Select @P = Max(isnull(Posicao,0))  From tblMenu Where  (@MenuPaiId Is Null And MenuPaiId Is Null) Or ((@MenuPaiId Is Not Null) And (MenuPaiId = @MenuPaiId))
		Set @P = @P + 1

		Insert Into tblMenu(MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL, ImageURL, Posicao) 
		Values(@MenuPaiId, @SiteId, @MenuTipoId, @MenuTipoAcaoId, @PublicacaoId, @LinkURL, @ImageURL, @P)
		Select Cast(@@Identity As int) MenuId
	End

End