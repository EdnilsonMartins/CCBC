
/****** Object:  StoredProcedure [dbo].[USP_DEL_Menu]    Script Date: 29/03/2015 11:31:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_DEL_PublicacaoTipoRegra									*
* Objetivo  : Exclusão da associação entre PublicidadeTipo e Regra			*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	:				                                            *
* Descrição	    :															*
*                                                                           *
****************************************************************************/
Create Procedure [dbo].[USP_DEL_PublicacaoTipoRegra](
	@PublicacaoTipoRegraId Int
) As
Begin

	Declare @indErro bit,
			@msgErro VarChar(120)


	Set @indErro = 0
	Set @msgErro = ''

	Delete From tblPublicacaoTipoRegra Where PublicacaoTipoRegraId = @PublicacaoTipoRegraId

	Select @indErro indErro, @msgErro msgErro

End