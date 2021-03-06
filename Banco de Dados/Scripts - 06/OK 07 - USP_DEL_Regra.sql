

/****** Object:  StoredProcedure [dbo].[USP_DEL_Banner]    Script Date: 07/03/2015 01:54:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_DEL_Regra													*
* Objetivo  : Exclusão de regra												*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	:   /03/2015	                                            *
* Descrição	    :															*
*                                                                           *
****************************************************************************/
Alter Procedure [dbo].[USP_DEL_Regra](
	@RegraId Int
) As
Begin

	Declare @indErro bit,
			@msgErro VarChar(120)

	Set @indErro = 0
	Set @msgErro = ''

	Delete From tblPublicacaoTipoRegra Where RegraId = @RegraId
	Delete From tblRegra Where RegraId = @RegraId

	Select @indErro indErro, @msgErro msgErro

End