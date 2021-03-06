
/****** Object:  UserDefinedFunction [dbo].[fnRetornaUsuarioGrupo]    Script Date: 29/03/2015 15:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : fnRetornaUsuarioUsuarioGrupo									*
* Objetivo  : Retorna os grupos de usuários do usuário.						*
*                                                                           *
* Criada por	: Ednilson Martins	                                        *
* Data Criação	: 29/03/2015	                                            *
*                                                                           *
****************************************************************************/

Create Function [dbo].[fnRetornaUsuarioUsuarioGrupo](
	@UsuarioId	Int
) 
Returns VarChar(80) As

Begin

	--Declare @PublicacaoId BigInt
	--Set @PublicacaoId = 3078

	Declare @Total Int,
			@i Int
	Select @Total = Count(1) From tblUsuarioUsuarioGrupo Where UsuarioId = @UsuarioId
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
				UsuarioGrupoId, ROW_NUMBER() OVER(ORDER BY UsuarioGrupoId Asc) Row
			From 
				tblUsuarioUsuarioGrupo
			Where 
				UsuarioId = @UsuarioId
			) A
		Where
			A.Row = @i
	
	End
	If Len(@Saida) > 0
		Set @Saida = Left(@Saida, Len(@Saida) - 1)

	--Select @Saida
	Return @Saida

End