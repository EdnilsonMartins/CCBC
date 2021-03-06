USE [dbCCBC]
GO
/****** Object:  StoredProcedure [dbo].[USP_DEL_Regra]    Script Date: 05/06/2015 10:41:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_DEL_Site													*
* Objetivo  : Exclusão de site												*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	:   /06/2015	                                            *
* Descrição	    :															*
*                                                                           *
****************************************************************************/
Create Procedure [dbo].[USP_DEL_Site](
	@SiteId Int
) As
Begin

	Declare @indErro bit,
			@msgErro VarChar(120)

	Set @indErro = 0
	Set @msgErro = ''

	Delete From tblSite Where SiteId = @SiteId

	Select @indErro indErro, @msgErro msgErro

End