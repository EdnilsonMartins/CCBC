
/****** Object:  StoredProcedure [dbo].[USP_INS_Regra]    Script Date: 29/03/2015 22:48:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_INS_PublicacaoTipoRegra									*
* Objetivo  : Manutenção das PublicacaoTipo x Regra.						*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 															*
* Descrição	    : 															*
****************************************************************************/

Alter Procedure [dbo].[USP_INS_PublicacaoTipoRegra](
	@PublicacaoTipoRegraId		Int,
	@PublicacaoTipoId			Int,
	@RegraId					Int,
	@Privado					bit = Null
)
As
Begin

	If @PublicacaoTipoRegraId <> 0 Begin
		Update 
			tblPublicacaoTipoRegra
		Set 
			PublicacaoTipoId = @PublicacaoTipoId,
			RegraId = @RegraId,
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