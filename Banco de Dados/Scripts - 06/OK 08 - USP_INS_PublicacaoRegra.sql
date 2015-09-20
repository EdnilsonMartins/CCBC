
/****** Object:  StoredProcedure [dbo].[USP_INS_Banner]    Script Date: 07/03/2015 01:21:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_INS_PublicacaoRegra										*
* Objetivo  : Associação das Regras na Publicacao							*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 															*
* Descrição	    : 															*
****************************************************************************/

CREATE Procedure [dbo].[USP_INS_PublicacaoTipoRegra](
	@PublicacaoTipoRegraId	Int = null,
	@PublicacaoTipoId		Int,
	@RegraId				Int,
	@Privado				Bit
)
As
Begin

	If @RegraId <> 0 Begin
		Update 
			tblPublicacaoTipoRegra
		Set 
			Privado = @Privado
		Where 
			PublicacaoTipoRegraId = @PublicacaoTipoRegraId
		Select @PublicacaoTipoRegraId PublicacaoTipoRegraId
	End Else Begin

		Insert Into tblPublicacaoTipoRegra(PublicacaoTipoId, RegraId, Privado) 
		Values(@PublicacaoTipoId, @RegraId, @Privado)
		Select Cast(@@Identity As int) PublicacaoTipoRegraId
	End

End