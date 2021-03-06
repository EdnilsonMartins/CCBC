

/****** Object:  UserDefinedFunction [dbo].[fnRetornaArquivoCategoria]    Script Date: 09/03/2015 01:15:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : fnRetornaArquivoCategoria										*
* Objetivo  : Retorna os grupos de usuários restritos de uma publicação		*
*                                                                           *
* Criada por	: Ednilson Martins	                                        *
* Data Criação	: 09/03/2015	                                            *
*                                                                           *
****************************************************************************/

Create Function [dbo].[fnRetornaUsuarioGrupo](
	@PublicacaoId	Int
) 
Returns VarChar(80) As

Begin

	--Declare @PublicacaoId BigInt
	--Set @PublicacaoId = 3078

	Declare @Total Int,
			@i Int
	Select @Total = Count(1) From tblPublicacaoRestricaoUsuarioGrupo Where PublicacaoId = @PublicacaoId
	Declare @Saida VarChar(20)
	Set @Saida = ''
	Set @i = 0
	While @i < @Total Begin
		Set @i = @i + 1
		Select 
			@Saida = @Saida + Cast(A.UsuarioGrupoId As VarChar(20)) + ','
		From
			(
			Select 
				UsuarioGrupoId, ROW_NUMBER() OVER(ORDER BY PublicacaoRestricaoUsuarioGrupoId DESC) Row
			From 
				tblPublicacaoRestricaoUsuarioGrupo
			Where 
				PublicacaoId = @PublicacaoId
			) A
		Where
			A.Row = @i
	
	End
	If Len(@Saida) > 0
		Set @Saida = Left(@Saida, Len(@Saida) - 1)

	--Select @Saida
	Return @Saida

End