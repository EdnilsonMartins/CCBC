

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_DEL_Pasta													*
* Objetivo  : Exclusão de Pasta												*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	:   /04/2015	                                            *
* Descrição	    :															*
*                                                                           *
****************************************************************************/
Create Procedure [dbo].[USP_DEL_Pasta](
	@PastaId Int
) As
Begin

	Declare @indErro bit,
			@msgErro VarChar(120)

	Set @indErro = 0
	Set @msgErro = ''

	Declare @OutraPastaId Int
	Select @OutraPastaId = PastaId From tblPasta Where PastaPaiId = @PastaId
	if (@OutraPastaId is not null) begin
		Exec USP_DEL_Pasta @OutraPastaId
	end

	Delete From tblPasta Where PastaId = @PastaId
	
	Select @indErro indErro, @msgErro msgErro

End