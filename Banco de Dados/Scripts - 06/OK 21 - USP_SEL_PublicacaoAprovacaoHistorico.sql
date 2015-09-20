

/****** Object:  StoredProcedure [dbo].[USP_INS_Publicacao]    Script Date: 07/03/2015 22:28:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_SEL_PublicacaoAprovacaoHistorico							*
* Objetivo  : Retorna o histórico de aprovações da publicação.				*
*                                                                           *
* Criada por	: Ednilson Martins	                                        *
* Data Criação	: 08/03/2015	                                            *
*                                                                           *
****************************************************************************/

CREATE Procedure [dbo].[USP_SEL_PublicacaoAprovacaoHistorico](
	@PublicacaoId		Int
)
As

Begin

	--Declare @PublicacaoId Int
	--Set @PublicacaoId = 3074

	Select 
		H.DataLiberacao,
		H.Liberado,
		H.Descricao,
		H.UsuarioId, 
		IsNull(U.Nome, '') Nome
	From 
		tblPublicacaoAprovacaoHistorico H
		left join tblUsuario U On H.UsuarioId = U.UsuarioId
	Where
		H.PublicacaoId = @PublicacaoId


End