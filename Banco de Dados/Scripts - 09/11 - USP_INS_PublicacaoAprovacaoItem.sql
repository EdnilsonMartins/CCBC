
/****** Object:  StoredProcedure [dbo].[USP_INS_PublicacaoAprovacaoItem]    Script Date: 29/03/2015 19:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_INS_PublicacaoAprovacaoItem								*
* Objetivo  : Processamento da liberação da Publicação						*
*                                                                           *
* Criada por	: Ednilson Martins	                                        *
* Data Criação	: 08/03/2015	                                            *
*                                                                           *
* Alterada por	: Ednilson Martins											*
* Data Alteração: 29/03/2015												*
* Descrição		: Ajustade para registrar Aprovação/Repovação quando		*
*				  não existir regra para a publicação.						*
****************************************************************************/

ALTER Procedure [dbo].[USP_INS_PublicacaoAprovacaoItem](
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
			Values(@PublicacaoId, GetDate(), @Liberado, (Case When @Liberado = 1 Then 'Aprovou a liberação' When @Liberado = 0 Then 'Reprovou a liberação'  Else Null End), @UsuarioId)
		
		End

	End Else Begin
	
		Insert Into tblPublicacaoAprovacaoItem(PublicacaoId, UsuarioId, DataAprovacao, Liberado)
		Values(@PublicacaoId, @UsuarioId, GetDate(), @Liberado)

		Insert Into tblPublicacaoAprovacaoHistorico(PublicacaoId, DataLiberacao, Liberado, Descricao, UsuarioId)
		Values(@PublicacaoId, GetDate(), @Liberado, (Case When @Liberado = 1 Then 'Aprovou a liberação' When @Liberado = 0 Then 'Reprovou a liberação'  Else Null End), @UsuarioId)

	End

	-- Atualizar direto a flag de Liberação se não existir regra!
	If ((Select count(1) from dbo.GetRegraPassoResultadoFinal(@PublicacaoId)) = 0) Begin
		Update tblPublicacao Set Liberado = @Liberado, DataLiberado = GetDate() Where PublicacaoId = @PublicacaoId
	End

End