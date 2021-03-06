
/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_INS_Pasta												*
* Objetivo  : Manutenção das Editorias.										*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	:   /04/2015												*
* Descrição	    :															*
****************************************************************************/

Create Procedure [dbo].[USP_INS_Pasta](
	@PastaId			Int = 0,
	@PastaPaiId			Int = Null,
	@SiteId				Int,
	@Descricao			VarChar(255)
)
As
Begin

	If @PastaId <> 0 Begin
		Update 
			tblPasta
		Set 
			Descricao = @Descricao
		Where 
			PastaId = @PastaId
		Select @PastaId PastaId
	End Else Begin

		Declare @P int 
		--declare @MenuPaiId int = 2 
		
		Select @P = Max(isnull(Posicao,0))  From tblPasta Where  (@PastaPaiId Is Null And PastaPaiId Is Null) Or ((@PastaPaiId Is Not Null) And (PastaPaiId = @PastaPaiId))
		if (@P is null) Set @P = 0
		Set @P = @P + 1
		
		Insert Into tblPasta(PastaPaiId,SiteId,Descricao,Posicao) 
		Values(@PastaPaiId,@SiteId,@Descricao,@P)
		Select Cast(@@Identity As int) PastaId
	End

End