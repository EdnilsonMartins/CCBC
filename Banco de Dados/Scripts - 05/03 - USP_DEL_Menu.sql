USE [dbCCBC]
GO
/****** Object:  StoredProcedure [dbo].[USP_DEL_Menu]    Script Date: 04/03/2015 22:34:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_DEL_Menu													*
* Objetivo  : Exclusão de menu												*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 04/03/2015	                                            *
* Descrição	    : Remover os vínculos do menu.								*
*                                                                           *
****************************************************************************/
ALTER Procedure [dbo].[USP_DEL_Menu](
	@MenuId Int
) As
Begin

	Declare @indErro bit,
			@msgErro VarChar(120)


	Set @indErro = 0
	Set @msgErro = ''

	Declare @OutroMenuId Int
	Select @OutroMenuId = MenuId From tblMenu Where MenuPaiId = @MenuId
	if (@OutroMenuId is not null) begin
		Exec USP_DEL_Menu @OutroMenuId
	end

	--Delete From tblMenuRestricao Where MenuId = @MenuId 
	Delete From tblMenuIdiomaExcecao Where MenuId = @MenuId 
	Delete From tblMenu Where MenuId = @MenuId

	




	Select @indErro indErro, @msgErro msgErro

End