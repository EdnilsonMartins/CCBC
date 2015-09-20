
/****** Object:  StoredProcedure [dbo].[USP_DEL_Menu]    Script Date: 29/03/2015 11:31:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_DEL_RegraPassoCondicao									*
* Objetivo  : Exclus�o das condi��es dos passos da regra					*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Cria��o	:				                                            *
* Descri��o	    :															*
*                                                                           *
****************************************************************************/
CREATE Procedure [dbo].[USP_DEL_RegraPassoCondicao](
	@RegraPassoCondicaoId Int
) As
Begin

	Declare @indErro bit,
			@msgErro VarChar(120)


	Set @indErro = 0
	Set @msgErro = ''

	Delete From tblRegraPassoCondicao Where RegraPassoCondicaoId = @RegraPassoCondicaoId

	Select @indErro indErro, @msgErro msgErro

End