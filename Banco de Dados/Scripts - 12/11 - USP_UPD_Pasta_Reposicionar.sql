
Create Procedure [dbo].[USP_UPD_Pasta_Reposicionar](
	@PastaId	Int,
	@PastaPaiId	Int = null,
	@Posicao	Int
)
As
Begin

	
	--Declare @PastaId int = 5
	--Declare @PastaPaiId int = null
	--Declare @Posicao int = 1
	

	If @PastaPaiId = 0 Set @PastaPaiId = Null

	Declare @SiteId Int
	Select  @SiteId = SiteId From tblPasta Where PastaId = @PastaId


	Update tblPasta
	Set
		PastaPaiId = @PastaPaiId
	Where
		PastaId = @PastaId
		
	
	Declare @NovaPosicao Int = @Posicao
	
	
	-- Atualiza quem nao tem posicao definida
	Update tblPasta
	Set Posicao = A.Row
	From
		(Select 
			PastaId, ROW_NUMBER() OVER(ORDER BY Posicao asc) Row, Posicao
		From 
			tblPasta
		Where 
			((@PastaPaiId Is Null And PastaPaiId Is Null) Or (@PastaPaiId Is Not Null And (PastaPaiId = @PastaPaiId)))
			And (SiteId = @SiteId)
		) A join tblPasta P On P.PastaId = A.PastaId


	Declare @PosicaoAtual Int
	Select @PosicaoAtual = Posicao From tblPasta Where PastaId = @PastaId

	If (@NovaPosicao < @PosicaoAtual) begin
		Update tblPasta
		Set Posicao = Posicao + 1
		Where
			((@PastaPaiId Is Null And PastaPaiId Is Null) Or (@PastaPaiId Is Not Null And (PastaPaiId = @PastaPaiId)))
			And (SiteId = @SiteId)
			And Posicao >= @NovaPosicao and Posicao < @PosicaoAtual
	End Else Begin
		Update tblPasta
		Set Posicao = Posicao - 1
		Where
			((@PastaPaiId Is Null And PastaPaiId Is Null) Or (@PastaPaiId Is Not Null And (PastaPaiId = @PastaPaiId)))
			And (SiteId = @SiteId) 
			And Posicao <= @NovaPosicao and Posicao > @PosicaoAtual
	End

	Update tblPasta
	Set Posicao = @NovaPosicao
	Where PastaId = @PastaId

	


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