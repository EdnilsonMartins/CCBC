--prod 21:59

/****** Object:  UserDefinedFunction [dbo].[fnRetornaUsuarioGrupo]    Script Date: 31/05/2015 18:39:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : fnRetornaUsuarioPublicacao									*
* Objetivo  : Retorna os usuários restritos de uma publicação				*
*                                                                           *
* Criada por	: Ednilson Martins	                                        *
* Data Criação	: 31/05/2015	                                            *
*                                                                           *
****************************************************************************/

Create Function [dbo].[fnRetornaUsuarioPublicacao](
	@PublicacaoId	Int
) 
Returns VarChar(80) As

Begin

	--Declare @PublicacaoId BigInt
	--Set @PublicacaoId = 3078

	Declare @Total Int,
			@i Int
	Select @Total = Count(1) From tblPublicacaoRestricaoUsuario Where PublicacaoId = @PublicacaoId
	Declare @Saida VarChar(20)
	Set @Saida = ''
	Set @i = 0
	While @i < @Total Begin
		Set @i = @i + 1
		Select 
			@Saida = @Saida + Cast(A.UsuarioId As VarChar(20)) + ','
		From
			(
			Select 
				UsuarioId, ROW_NUMBER() OVER(ORDER BY PublicacaoRestricaoUsuarioId DESC) Row
			From 
				tblPublicacaoRestricaoUsuario
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