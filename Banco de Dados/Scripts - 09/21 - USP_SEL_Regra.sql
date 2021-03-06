
/****** Object:  StoredProcedure [dbo].[USP_SEL_Regra]    Script Date: 30/03/2015 02:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_SEL_Regra													*
* Objetivo  : Retorna as Regras.											*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 30/03/2015							                    *
* Descrição	    : Filtrar por site.											*
*                                                                           *
****************************************************************************/
ALTER Procedure [dbo].[USP_SEL_Regra](
	@SiteId		Int = Null,
	@RegraId	Int = Null
)
As
Begin

	Select
		R.RegraId,
		R.Descricao Regra,
		R.DataCadastro,
		RT.RegraTipoId,
		RT.Descricao RegraTipo
	From 
		tblRegra R
		join tblRegraTipo RT On RT.RegraTipoId = R.RegraTipoId
	Where
		SiteId = @SiteId
		And ((@RegraId Is Null) Or (regraId = @RegraId))

End