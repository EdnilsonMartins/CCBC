
/****** Object:  StoredProcedure [dbo].[USP_SEL_Regra]    Script Date: 29/03/2015 20:18:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_SEL_PublicacaoTipoRegra									*
* Objetivo  : Retorna as associações PublicacaoTipo x Regra					*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 										                    *
* Descrição	    :															*

USP_SEL_RegraPasso null, 5
USP_SEL_RegraPasso 4, 5
****************************************************************************/
Create Procedure [dbo].[USP_SEL_PublicacaoTipoRegra](
	@PublicacaoTipoRegraId	Int = Null,
	@SiteId		Int = Null
)
As
Begin

	Select
		PTR.PublicacaoTipoRegraId, 
		PTR.PublicacaoTipoId, 
		PTR.RegraId, 
		PTR.Privado,
		PT.Descricao PublicacaoTipo,
		R.Descricao Regra
	From 
		tblPublicacaoTipoRegra PTR
		join tblRegra R On R.RegraId = PTR.RegraId
		join tblPublicacaoTipo PT On PT.PublicacaoTipoId = PTR.PublicacaoTipoId
	Where
		R.SiteId = @SiteId
		And ((@PublicacaoTipoRegraId Is Null) Or (@PublicacaoTipoRegraId = @PublicacaoTipoRegraId))

End