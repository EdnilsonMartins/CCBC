
/****** Object:  StoredProcedure [dbo].[USP_DEL_Publicacao]    Script Date: 04/03/2015 22:08:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_DEL_Publicacao											*
* Objetivo  : Exclusão de publicações										*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 04/03/2015	                                            *
* Descrição	    : Remover os vínculos de arquivos da publicação.			*
*                                                                           *
****************************************************************************/
ALTER Procedure [dbo].[USP_DEL_Publicacao](
	@PublicacaoId Int
) As
Begin

	Declare @indErro bit,
			@msgErro VarChar(120)


	Set @indErro = 0
	Set @msgErro = ''

	Update tblMenu Set PublicacaoId = Null Where PublicacaoId = @PublicacaoId
	Delete From tblPublicacaoArquivo Where PublicacaoId = @PublicacaoId
	Delete From tblPublicacaoRestricao Where PublicacaoId = @PublicacaoId 
	Delete From tblPublicacaoIdiomaExcecao Where PublicacaoId = @PublicacaoId 
	Delete From tblPublicacao Where PublicacaoId = @PublicacaoId

	Select @indErro indErro, @msgErro msgErro

End