
/****** Object:  StoredProcedure [dbo].[USP_INS_Banner]    Script Date: 07/03/2015 01:21:18 ******/
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
* Data Criação	: 															*
* Descrição	    : 															*
****************************************************************************/

CREATE Procedure [dbo].[USP_INS_Regra](
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

		Insert Into tblRegra(RegraTipoId, Descricao) 
		Values(@RegraTipoId, @Descricao)
		Select Cast(@@Identity As int) RegraId
	End

End