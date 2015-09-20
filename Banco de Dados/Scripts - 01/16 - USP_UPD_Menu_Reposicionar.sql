alter Procedure USP_UPD_Menu_Reposicionar(
	@MenuId		Int,
	@MenuPaiId	Int = null,
	@Posicao	Int
)
As
Begin

	If @MenuPaiId = 0 Set @MenuPaiId = Null

	declare @MenuTipoId Int
	Declare @SiteId Int

	Select @MenuTipoId = MenuTipoId, @SiteId = SiteId From tblMenu Where MenuId = @MenuId

	Update tblMenu
	Set
		MenuPaiId = @MenuPaiId
	Where
		MenuId = @MenuId
		
	/*
	Declare @MenuId int = 20
	Declare @MenuPaiId int = 2
	Declare @Posicao int = 2
	*/
	
	Declare @NovaPosicao Int = @Posicao
	

	-- Atualiza quem nao tem posicao definida
	Update tblMenu
	Set Posicao = A.Row
	From
		(Select 
			MenuId, ROW_NUMBER() OVER(ORDER BY Posicao asc) Row, Posicao
		From 
			tblMenu
		Where 
			((@MenuPaiId Is Null And MenuPaiId Is Null) Or (@MenuPaiId Is Not Null And (MenuPaiId = @MenuPaiId)))
			And (SiteId = @SiteId) And (MenuTipoId = @MenuTipoId)
		) A join tblMenu M On M.MenuId = A.MenuId


	Declare @PosicaoAtual Int
	Select @PosicaoAtual = Posicao From tblMenu Where MenuId = @MenuId

	If (@NovaPosicao < @PosicaoAtual) begin
		Update tblMenu
		Set Posicao = Posicao + 1
		Where
			((@MenuPaiId Is Null And MenuPaiId Is Null) Or (@MenuPaiId Is Not Null And (MenuPaiId = @MenuPaiId)))
			And (SiteId = @SiteId) And (MenuTipoId = @MenuTipoId)
			And Posicao >= @NovaPosicao and Posicao < @PosicaoAtual
	End Else Begin
		Update tblMenu
		Set Posicao = Posicao - 1
		Where
			((@MenuPaiId Is Null And MenuPaiId Is Null) Or (@MenuPaiId Is Not Null And (MenuPaiId = @MenuPaiId)))
			And (SiteId = @SiteId) And (MenuTipoId = @MenuTipoId)
			And Posicao <= @NovaPosicao and Posicao > @PosicaoAtual
	End

	Update tblMenu
	Set Posicao = @NovaPosicao
	Where MenuId = @MenuId

	


--select * from tblMenu where menupaiId=2 order by posicao
/*
5	1
6	2
7	3
8	4
20	5
524	6
*/

End