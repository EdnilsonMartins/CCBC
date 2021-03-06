
/****** Object:  StoredProcedure [dbo].[USP_INS_Regra]    Script Date: 30/03/2015 02:04:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_INS_Regra													*
* Objetivo  : Manutenção das Regras.										*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 30/03/2015												*
* Descrição	    : Gravação do SiteId na Regra.								*
****************************************************************************/

Alter Procedure [dbo].[USP_INS_Regra](
	@RegraId			Int,
	@SiteId				Int,
	@Descricao			VarChar(200) = Null,
	@RegraTipoId			Int = Null
)
As
Begin

	If @RegraId <> 0 Begin
		Update 
			tblRegra
		Set 
			Descricao = @Descricao,
			RegraTipoId = @RegraTipoId
		Where 
			RegraId = @RegraId
		Select @RegraId RegraId
	End Else Begin

		Insert Into tblRegra(RegraTipoId, Descricao, SiteId) 
		Values(@RegraTipoId, @Descricao, @SiteId)
		Select Cast(@@Identity As int) RegraId
	End

End