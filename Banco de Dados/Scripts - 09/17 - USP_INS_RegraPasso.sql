
/****** Object:  StoredProcedure [dbo].[USP_INS_Regra]    Script Date: 29/03/2015 22:48:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_INS_RegraPasso											*
* Objetivo  : Manutenção dos Passos.										*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 															*
* Descrição	    : 															*
****************************************************************************/

Create Procedure [dbo].[USP_INS_RegraPasso](
	@RegraPassoId		Int,
	@RegraId			Int,
	@Sequencia			Int,
	@Descricao			VarChar(200) = Null,
	@QuantidadeMinimaUsuariosDoGrupo	Int = Null
)
As
Begin

	If @RegraPassoId <> 0 Begin
		Update 
			tblRegraPasso
		Set 
			Sequencia = @Sequencia,
			Descricao = @Descricao,
			QuantidadeMinimaUsuariosDoGrupo = @QuantidadeMinimaUsuariosDoGrupo
		Where 
			RegraPassoId = @RegraPassoId
		Select @RegraPassoId RegraPassoId
	End Else Begin

		Insert Into tblRegraPasso(RegraId, Sequencia, Descricao, QuantidadeMinimaUsuariosDoGrupo) 
		Values(@RegraId, @Sequencia, @Descricao, @QuantidadeMinimaUsuariosDoGrupo)
		Select Cast(@@Identity As int) RegraPassoId
	End

End