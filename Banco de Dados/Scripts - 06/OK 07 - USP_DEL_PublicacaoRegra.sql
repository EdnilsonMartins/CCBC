
/****** Object:  StoredProcedure [dbo].[USP_DEL_PublicacaoRegra]    Script Date: 09/03/2015 08:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_DEL_PublicacaoRegra										*
* Objetivo  : Remove uma regra associada a Publicacao						*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	:					                                        *
* Descrição	    :															*
*                                                                           *
****************************************************************************/
Create Procedure [dbo].[USP_DEL_PublicacaoRegra](
	@PublicacaoRegraId	Int
) As
Begin

	Declare @indErro bit,
			@msgErro VarChar(120)

	Set @indErro = 0
	Set @msgErro = ''

	Delete From tblPublicacaoRegra Where PublicacaoRegraId = @PublicacaoRegraId

	Select @indErro indErro, @msgErro msgErro

End