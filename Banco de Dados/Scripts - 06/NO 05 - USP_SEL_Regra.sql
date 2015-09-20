

/****** Object:  StoredProcedure [dbo].[USP_DEL_Banner]    Script Date: 07/03/2015 01:54:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_SEL_PublicacaoTipoRegra									*
* Objetivo  : Consulta das regras associadas a publicacao					*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	:   /03/2015	                                            *
* Descrição	    :															*
*                                                                           *
****************************************************************************/
Alter Procedure USP_SEL_PublicacaoTipoRegra(
	@PublicacaoTipoRegraId Int = Null
)
As
Begin

	Select
		PT.PublicacaoTipoId,
		PT.Descricao
		R.Descricaossssssssssssssss ????
	From 
		tblPublicacaoTipo PT
		join tblPublicacaoTipoRegra  PTR On PTR.RegraTipoId = PT.RegraTipoId
		join tblRegra R On PTR.RegraId = R.RegraId
	Where
		(@RegraId Is Null) Or (regraId = @RegraId)

End