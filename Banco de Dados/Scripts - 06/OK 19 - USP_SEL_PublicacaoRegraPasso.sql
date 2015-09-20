

/****** Object:  StoredProcedure [dbo].[USP_INS_Publicacao]    Script Date: 07/03/2015 22:28:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_SEL_PublicacaoRegraPasso									*
* Objetivo  : Retorna os passos e o status de aprovação de cada um.			*
*                                                                           *
* Criada por	: Ednilson Martins	                                        *
* Data Criação	: 08/03/2015	                                            *
*                                                                           *
****************************************************************************/

Create Procedure [dbo].[USP_SEL_PublicacaoRegraPasso](
	@PublicacaoId		Int
)
As

Begin

	--Declare @PublicacaoId Int
	--Set @PublicacaoId = 3074

	Select
		RegraId,
		RegraPassoId,
		Sequencia,
		Descricao,
		TotalUsuarios,
		CondicaoResultado,
		AprovadoPasso,
		Liberado,
		Resultado
	From
		dbo.GetRegraPassoResultadoFinal(@PublicacaoId)


End