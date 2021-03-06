
/****** Object:  StoredProcedure [dbo].[USP_DEL_Banner]    Script Date: 26/07/2015 17:22:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_DEL_Associado												*
* Objetivo  : Exclusão de Pessoa (Associado/Árbitro/Mediadores)				*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	:   /07/2015	                                            *
* Descrição	    :															*
*                                                                           *
****************************************************************************/
Create Procedure [dbo].[USP_DEL_Associado](
	@AssociadoId Int
) As
Begin

	Declare @indErro bit,
			@msgErro VarChar(120)

	Set @indErro = 0
	Set @msgErro = ''

	Delete From tblAssociado Where AssociadoId = @AssociadoId

	Select @indErro indErro, @msgErro msgErro

End