
/****** Object:  UserDefinedFunction [dbo].[fnGetMenuIcone]    Script Date: 21/02/2015 22:11:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Function  : fnGetPublicacaoArquivoDestaque								*
* Objetivo  : Retorna 1 arquivo destaque da publicacao.						*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 21/02/2015	                                            *
* Descrição	    : Filtrar pelo campo ArquivoCategoriaId.					*
****************************************************************************/
Create Function [dbo].[fnGetPublicacaoArquivoDestaque](
	@PublicacaoId		Int
) 
Returns BigInt As
Begin

	Declare @Saida BigInt
	
	Select 
		@Saida = A.ArquivoId
	From
		(
		Select
			Top 1 PA.ArquivoId ArquivoId
		From
			tblPublicacao P
			inner join tblPublicacaoArquivo PA On PA.PublicacaoId = P.PublicacaoId 
			inner join tblArquivoArquivoCategoria AAC On AAC.ArquivoId = PA.ArquivoId
		Where
			P.PublicacaoId = @PublicacaoId
			And AAC.ArquivoCategoriaId In (1) --> 1 = Destaque
		) A

	Return @Saida
End