

/****** Object:  StoredProcedure [dbo].[USP_INS_PublicacaoAprovacaoItem]    Script Date: 08/03/2015 11:57:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_DEL_PublicacaoAprovacaoItem								*
* Objetivo  : Processamento da liberação da Publicação						*
*                                                                           *
* Criada por	: Ednilson Martins	                                        *
* Data Criação	: 08/03/2015	                                            *
*                                                                           *
****************************************************************************/

CREATE Procedure [dbo].[USP_DEL_PublicacaoAprovacaoItem](
	@PublicacaoId		Int,
	@UsuarioId			Int
)
As

Begin

	Declare @PublicacaoAprovacaoItemId Int

	Select @PublicacaoAprovacaoItemId = PublicacaoAprovacaoItemId 
	From tblPublicacaoAprovacaoItem Where PublicacaoId = @PublicacaoId And UsuarioId = @UsuarioId

	If (@PublicacaoAprovacaoItemId Is Not Null) Begin

		Delete tblPublicacaoAprovacaoItem Where PublicacaoAprovacaoItemId = @PublicacaoAprovacaoItemId
		
		Insert Into tblPublicacaoAprovacaoHistorico(PublicacaoId, DataLiberacao, Liberado, Descricao, UsuarioId)
		Values(@PublicacaoId, GetDate(), Null, 'Remoção da validação do usuário', @UsuarioId)
		
	End

End