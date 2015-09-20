

/****** Object:  StoredProcedure [dbo].[USP_INS_Publicacao]    Script Date: 07/03/2015 22:28:34 ******/
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
****************************************************************************/

cREATE Procedure [dbo].[USP_INS_Publicacao_Liberacao](
	@PublicacaoId		Int
)
As

Begin

	--Declare @PublicacaoId Int
	--Set @PublicacaoId = 3074

	--Select * From dbo.GetRegraPassoResultadoFinal(@PublicacaoId)

	If Exists(Select 1 from dbo.GetRegraPassoResultadoFinal(@PublicacaoId) Where Resultado = 0) Begin
		If Exists(Select 1 From tblPublicacao Where PublicacaoId = @PublicacaoId And IsNull(Liberado, 1) = 1) Begin
			Update tblPublicacao Set Liberado = 0, DataLiberado = GetDate() Where PublicacaoId = @PublicacaoId
			Insert into tblPublicacaoAprovacaoHistorico(PublicacaoId, DataLiberacao, Liberado, Descricao)
			Values(@PublicacaoId, GetDate(), 0, 'Liberação da publicação REPROVADA')
		End
	End Else Begin
		If Not Exists(Select 1 from dbo.GetRegraPassoResultadoFinal(@PublicacaoId) Where Resultado Is Null) Begin
			If Exists(Select 1 From tblPublicacao Where PublicacaoId = @PublicacaoId And IsNull(Liberado, 0) = 0) Begin
				--If Exists(Select 1 From tblPublicacao Where PublicacaoId = @PublicacaoId And IsNull(Liberado, 1) = 1) Begin
					Update tblPublicacao Set Liberado = 1, DataLiberado = GetDate() Where PublicacaoId = @PublicacaoId
					Insert into tblPublicacaoAprovacaoHistorico(PublicacaoId, DataLiberacao, Liberado, Descricao)
					Values(@PublicacaoId, GetDate(), 1, 'Liberação da publicação APROVADA')
				--End
			End
		End Else Begin
			If Exists(Select 1 From tblPublicacao Where PublicacaoId = @PublicacaoId And Liberado Is Not Null) Begin
				Update tblPublicacao Set Liberado = Null, DataLiberado = null Where PublicacaoId = @PublicacaoId
				Insert into tblPublicacaoAprovacaoHistorico(PublicacaoId, DataLiberacao, Liberado, Descricao)
				Values(@PublicacaoId, GetDate(), Null, 'Em processo de liberação')
			End
		End
	End

	-- Select Liberado, * From tblPublicacao Where PublicacaoId = @PublicacaoId
	-- Select * From tblPublicacaoAprovacaoHistorico Where PublicacaoId = @PublicacaoId

End