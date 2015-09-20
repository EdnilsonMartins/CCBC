
/****** Object:  StoredProcedure [dbo].[USP_INS_Banner]    Script Date: 26/07/2015 17:16:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_INS_Associado												*
* Objetivo  : Manutenção das Pessoas (Associados/Árbitros/Mediadores).		*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	:   /07/2015	                                            *
* Descrição	    :															*
*                                                                           *
****************************************************************************/

Create Procedure [dbo].[USP_INS_Associado](
	@AssociadoId			Int,
	@SiteId					Int, 
	@Nome					VarChar(120) = Null,
	@Resumo					VarChar(Max) = Null,
	@AssociadoCategoriaId	Int = Null, 
	@TipoPessoaId			Int = Null,
	@PaisId					Int = Null
)
As
Begin

	If @AssociadoId <> 0 Begin

		Update 
			tblAssociado
		Set 
			Nome = @Nome,
			Resumo = @Resumo,
			AssociadoCategoriaId = @AssociadoCategoriaId,
			TipoPessoaId = @TipoPessoaId,
			PaisId = @PaisId
		Where 
			AssociadoId = @AssociadoId
		Select @AssociadoId AssociadoId

	End Else Begin

		Insert Into tblAssociado(SiteId, Nome, Resumo, AssociadoCategoriaId, TipoPessoaId, PaisId) 
		Values(@SiteId, @Nome, @Resumo, @AssociadoCategoriaId, @TipoPessoaId, @PaisId)
		Select Cast(@@Identity As int) AssociadoId

	End

End