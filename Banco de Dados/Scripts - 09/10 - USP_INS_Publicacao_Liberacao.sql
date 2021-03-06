
/****** Object:  StoredProcedure [dbo].[USP_INS_Publicacao_Liberacao]    Script Date: 29/03/2015 18:39:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_INS_Publicacao_Liberacao									*
* Objetivo  : Processamento da liberação da Publicação						*
*                                                                           *
* Criada por	: Ednilson Martins	                                        *
* Data Criação	: 07/03/2015	                                            *
*                                                                           *
* Alterada por	: Ednilson Martins											*
* Data Alteração: 29/03/2015												*
* Descrição		: Ajustade para registrar a Reprova no Histórico quando		*
*				  não existir regra para a publicação.						*
USP_INS_Publicacao_Liberacao 3095
****************************************************************************/

ALTER Procedure [dbo].[USP_INS_Publicacao_Liberacao](
	@PublicacaoId		Int
)
As

Begin

	--Declare @PublicacaoId Int
	--Set @PublicacaoId = 3093

	--Select * From dbo.GetRegraPassoResultadoFinal(@PublicacaoId)
	--Select count(1) From dbo.GetRegraPassoResultadoFinal(@PublicacaoId)

	If Exists(Select 1 from dbo.GetRegraPassoResultadoFinal(@PublicacaoId) Where Resultado = 0) Begin
		If Exists(Select 1 From tblPublicacao Where PublicacaoId = @PublicacaoId And IsNull(Liberado, 1) = 1) Begin
			Update tblPublicacao Set Liberado = 0, DataLiberado = GetDate() Where PublicacaoId = @PublicacaoId
			Insert into tblPublicacaoAprovacaoHistorico(PublicacaoId, DataLiberacao, Liberado, Descricao)
			Values(@PublicacaoId, GetDate(), 0, 'Liberação da publicação REPROVADA')
			print 'Liberação da publicação REPROVADA'
		End
	End Else Begin
		If ((Select count(1) from dbo.GetRegraPassoResultadoFinal(@PublicacaoId)) = 0) Begin
			If Exists(Select 1 From tblPublicacao Where PublicacaoId = @PublicacaoId And Liberado = 0) Begin
				Insert into tblPublicacaoAprovacaoHistorico(PublicacaoId, DataLiberacao, Liberado, Descricao)
				Values(@PublicacaoId, GetDate(), 0, 'Liberação da publicação REPROVADA')
				print 'Liberação da publicação REPROVADA (sem regra)'
			End 
			If Exists(Select 1 From tblPublicacao Where PublicacaoId = @PublicacaoId And Liberado = 1) Begin
				Insert into tblPublicacaoAprovacaoHistorico(PublicacaoId, DataLiberacao, Liberado, Descricao)
				Values(@PublicacaoId, GetDate(), 1, 'Liberação da publicação APROVADA')
				print 'Liberação da publicação APROVADA (sem regra)'
			End 
		End Else Begin
			If Not Exists(Select 1 from dbo.GetRegraPassoResultadoFinal(@PublicacaoId) Where Resultado Is Null) Begin
				If Exists(Select 1 From tblPublicacao Where PublicacaoId = @PublicacaoId And IsNull(Liberado, 0) = 0) Begin
					--If Exists(Select 1 From tblPublicacao Where PublicacaoId = @PublicacaoId And IsNull(Liberado, 1) = 1) Begin
						Update tblPublicacao Set Liberado = 1, DataLiberado = GetDate() Where PublicacaoId = @PublicacaoId
						Insert into tblPublicacaoAprovacaoHistorico(PublicacaoId, DataLiberacao, Liberado, Descricao)
						Values(@PublicacaoId, GetDate(), 1, 'Liberação da publicação APROVADA')
						print 'Liberação da publicação APROVADA'
					--End
				End
			End Else Begin
				If Exists(Select 1 From tblPublicacao Where PublicacaoId = @PublicacaoId And Liberado Is Not Null) Begin
					Update tblPublicacao Set Liberado = Null, DataLiberado = null Where PublicacaoId = @PublicacaoId
					Insert into tblPublicacaoAprovacaoHistorico(PublicacaoId, DataLiberacao, Liberado, Descricao)
					Values(@PublicacaoId, GetDate(), Null, 'Em processo de liberação')
					print 'Em processo de liberação'
				End
			End
		End
	End

	-- Select Liberado, * From tblPublicacao Where PublicacaoId = @PublicacaoId
	-- Select * From tblPublicacaoAprovacaoHistorico Where PublicacaoId = @PublicacaoId

End