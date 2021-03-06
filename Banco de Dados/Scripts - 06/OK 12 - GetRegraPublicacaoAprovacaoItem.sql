

/****** Object:  UserDefinedFunction [dbo].[GetUsuarioGrupo]    Script Date: 08/03/2015 00:00:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco			:	dbCCBC													*
* Procedure		:	GetRegraPublicacaoAprovacaoItem							*
* Objetivo		:	Retorna uma tabela contendo todos os passos e condições	*
*                   com base na regra que deverá ser aplicada na publicação.*
*					Esta tabela apresenta também as aprovações relacionadas *
*					com cada passo/condicao.								*
*																			*
* Criada por	: Ednilson Martins	                                        *
* Data Criação	: 07/03/2015	                                            *
*                                                                           *
****************************************************************************/
cREATE FUNCTION [dbo].[GetRegraPublicacaoAprovacaoItem](
    @PublicacaoId Int
) RETURNS @result TABLE (PublicacaoId Int, RegraPassoId Int, Descricao VarChar(200), Sequencia Int, RegraPassoCondicaoId Int, UsuarioId Int, Liberado Bit)

BEGIN

	Insert Into @result
	Select 
		P.PublicacaoId,
		RP.RegraPassoId,
		RP.Descricao,
		RP.Sequencia
		,RPC.RegraPassoCondicaoId
		,PAI.UsuarioId
		,PAI.Liberado					
	From 
		tblPublicacao P --On P.PublicacaoId = PAI.PublicacaoId
		join tblPublicacaoTipoRegra PTR On PTR.PublicacaoTipoId = P.PublicacaoTipoId
		join tblRegra R On R.RegraId = PTR.RegraId
		join tblRegraPasso RP On RP.RegraId = R.RegraId
		join tblRegraPassoCondicao RPC On RPC.RegraPassoId = RP.RegraPassoId
							
		left join tblPublicacaoAprovacaoItem PAI On PAI.PublicacaoId = P.PublicacaoId 
			And (RPC.UsuarioGrupoId In (Select UsuarioGrupoId From dbo.GetUsuarioGrupo(PAI.UsuarioId))
					Or
					RPC.UsuarioId = PAI.UsuarioId
				)

	Where
		P.PublicacaoId = @PublicacaoId

	RETURN

END

