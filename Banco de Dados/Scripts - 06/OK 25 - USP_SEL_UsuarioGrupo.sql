

/****** Object:  StoredProcedure [dbo].[USP_INS_Publicacao]    Script Date: 07/03/2015 22:28:34 ******/
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
****************************************************************************/

Create Procedure [dbo].[USP_SEL_UsuarioGrupo](
	@UsuarioGrupoId	Int = Null
)
As

Begin

	--Declare @PublicacaoId Int
	--Set @PublicacaoId = 3077

	--Declare @UsuarioId Int
	--Set @UsuarioId = 30

	Select
		UsuarioGrupoId,
		Nome
	From
		tblUsuarioGrupo
	Where
		(@UsuarioGrupoId  Is Null) Or (UsuarioGrupoId = @UsuarioGrupoId)


End