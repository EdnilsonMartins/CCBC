--prod 21:57

/****** Object:  StoredProcedure [dbo].[USP_SEL_UsuarioGrupo]    Script Date: 31/05/2015 11:30:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_SEL_UsuarioGrupo											*
* Objetivo  : Retorna os Grupos de Usuários do sistema.						*
*                                                                           *
* Criada por	: Ednilson Martins	                                        *
* Data Criação	: 09/03/2015	                                            *
*                                                                           *
* Alterada por	: Ednilson Martins											*
* Data Alteração: 29/03/2015												*
* Descricao		: Adicionado o parâmetro @SiteId e a coluna SiteId.			*
*                                                                           *
* Alterada por	: Ednilson Martins											*
* Data Alteração: 31/05/2015												*
* Descricao		: Retornar nova coluna UsuarioGrupoId para recursividade.	*
****************************************************************************/

ALTER Procedure [dbo].[USP_SEL_UsuarioGrupo](
	@UsuarioGrupoId	Int = Null,
	@SiteId			Int = Null
)
As

Begin

	--Declare @UsuarioGrupoId Int = Null
	--Declare @SiteId Int = 1

	Select
		UsuarioGrupoId, 
		Nome,
		SiteId,
		UsuarioGrupoPaiId
	From
		tblUsuarioGrupo
	Where
		((@UsuarioGrupoId  Is Null) Or (UsuarioGrupoId = @UsuarioGrupoId))
		And ((@SiteId Is Null) Or (SiteId = @SiteId))

End