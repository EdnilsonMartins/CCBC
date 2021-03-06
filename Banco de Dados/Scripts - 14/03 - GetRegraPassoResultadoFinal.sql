

/****** Object:  UserDefinedFunction [dbo].[GetRegraPassoResultadoFinal]    Script Date: 09/05/2015 10:04:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco			:	dbCCBC													*
* Procedure		:	GetRegraPassoResultadoFinal								*
* Objetivo		:	Retorna resultado Final de aprovação para cada			*
*					passo de uma regra da publicação.						*
*																			*
* Criada por	: Ednilson Martins	                                        *
* Data Criação	: 08/03/2015	                                            *
*                                                                           *
* Alterada por	: Ednilson Martins											*
* Data Alteração: 09/05/2015												*
* Descrição		: Criada mais uma condição para o resultado, enquato estiver*
*				  Null no AprovadoPasso o Resultado será Null.				*
****************************************************************************/

ALTER FUNCTION [dbo].[GetRegraPassoResultadoFinal](
    @PublicacaoId Int
) RETURNS @result TABLE (RegraId Int, RegraPassoId Int, Sequencia Int, Descricao VarChar(200), TotalUsuarios Int, CondicaoResultado Bit, AprovadoPasso Bit, Liberado Bit, Resultado Bit)

BEGIN

	--Declare @PublicacaoId Int = 3112

	Insert Into @result
	Select
		RegraId,
		RegraPassoId,
		Sequencia,
		Descricao,
		TotalUsuarios,
		CondicaoResultado,
		AprovadoPasso,
		Liberado,
		Cast(
		Case When AprovadoPasso = 1 And Liberado = 1 Then 1
			 When AprovadoPasso = 0 And Liberado = 1 Then Null
			 When AprovadoPasso = 0 And Liberado = 0 Then 0
			 When AprovadoPasso = 1 And Liberado = 0 Then 0
			 When AprovadoPasso Is Null Then Null
			 Else Null
			 End As Bit) Resultado
	From
		dbo.GetRegraPassoResultado(@PublicacaoId)

	RETURN

END

