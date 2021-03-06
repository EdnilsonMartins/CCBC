
/****** Object:  StoredProcedure [dbo].[USP_SEL_Regra]    Script Date: 29/03/2015 20:18:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_SEL_RegraPasso											*
* Objetivo  : Retorna os passos de uma regra								*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 										                    *
* Descrição	    :															*

USP_SEL_RegraPasso null, 5
USP_SEL_RegraPasso 4, 5
****************************************************************************/
Create Procedure [dbo].[USP_SEL_RegraPasso](
	@RegraPassoId	Int = Null,
	@RegraId		Int = Null
)
As
Begin

	Select
		RP.RegraPassoId,
		RP.RegraId,
		RP.Sequencia,
		RP.Descricao,
		RP.QuantidadeMinimaUsuariosDoGrupo
	From 
		tblRegraPasso RP
	Where
		RegraId = @RegraId
		And ((@RegraPassoId Is Null) Or (RegraPassoId = @RegraPassoId))

End