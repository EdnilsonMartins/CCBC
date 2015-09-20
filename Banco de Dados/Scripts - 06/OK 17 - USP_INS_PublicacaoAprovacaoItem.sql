

/****** Object:  StoredProcedure [dbo].[USP_INS_Publicacao]    Script Date: 07/03/2015 22:28:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_INS_PublicacaoAprovacaoItem								*
* Objetivo  : Processamento da libera��o da Publica��o						*
*                                                                           *
* Criada por	: Ednilson Martins	                                        *
* Data Cria��o	: 08/03/2015	                                            *
*                                                                           *
****************************************************************************/

CREATE Procedure [dbo].[USP_INS_PublicacaoAprovacaoItem](
	@PublicacaoId		Int,
	@UsuarioId			Int,
	@Liberado			Bit
)
As

Begin

	Declare @PublicacaoAprovacaoItemId Int

	Select @PublicacaoAprovacaoItemId = PublicacaoAprovacaoItemId 
	From tblPublicacaoAprovacaoItem Where PublicacaoId = @PublicacaoId And UsuarioId = @UsuarioId

	If (@PublicacaoAprovacaoItemId Is Not Null) Begin

		If Exists(Select 1 From tblPublicacaoAprovacaoItem Where PublicacaoAprovacaoItemId = @PublicacaoAprovacaoItemId And Liberado <> @Liberado) Begin
		
			Update tblPublicacaoAprovacaoItem Set Liberado = @Liberado Where PublicacaoAprovacaoItemId = @PublicacaoAprovacaoItemId
	
			Insert Into tblPublicacaoAprovacaoHistorico(PublicacaoId, DataLiberacao, Liberado, Descricao, UsuarioId)
			Values(@PublicacaoId, GetDate(), @Liberado, (Case When @Liberado = 1 Then 'Aprovou a libera��o' When @Liberado = 0 Then 'Reprovou a libera��o'  Else Null End), @UsuarioId)
		
		End

	End Else Begin
	
		Insert Into tblPublicacaoAprovacaoItem(PublicacaoId, UsuarioId, DataAprovacao, Liberado)
		Values(@PublicacaoId, @UsuarioId, GetDate(), @Liberado)

		Insert Into tblPublicacaoAprovacaoHistorico(PublicacaoId, DataLiberacao, Liberado, Descricao, UsuarioId)
		Values(@PublicacaoId, GetDate(), @Liberado, (Case When @Liberado = 1 Then 'Aprovou a libera��o' When @Liberado = 0 Then 'Reprovou a libera��o'  Else Null End), @UsuarioId)

	End

End