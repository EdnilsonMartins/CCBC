--prod 21:58

/****** Object:  UserDefinedFunction [dbo].[GetPublicacaoAcessoUsuario]    Script Date: 31/05/2015 16:19:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco			:	dbCCBC													*
* Procedure		:	GetPublicacaoAcessoUsuario								*
* Objetivo		:	Retorna se o usuário possui direito a visualização da	*
*					publicidade baseado no controle de privacidade.			*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 07/03/2015	                                            *
* Descrição	    : O atributo "privado" foi movido para a tblPublicacao.		*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 31/05/2015	                                            *
* Descrição	    : Adicionado join com a tblPublicacaoRestricaoUsuario		*
*                 para permitir restrição a nível de usuário.               *
****************************************************************************/

ALTER Function [dbo].[GetPublicacaoAcessoUsuario](
	@PublicacaoId	Int,
	@UsuarioId		Int = Null
) 
Returns Bit As

Begin

	/*
	Declare @PublicacaoId	Int,
			@UsuarioId		Int
	Set @PublicacaoId = 3074
	Set @UsuarioId = 1
	*/

	Declare @Retorno Bit
	If Exists(
				Select 
					1
				From
					tblPublicacao P
					left join tblPublicacaoRestricaoUsuarioGrupo PRUG On PRUG.PublicacaoId = P.PublicacaoId
					left join tblPublicacaoRestricaoUsuario PRU On PRU.PublicacaoId = P.PublicacaoId
				Where 
					P.PublicacaoId = @PublicacaoId
					And ((IsNUll(P.Privado, 0) = 0)
						Or
						((P.Privado = 1)
						And
						(
						PRUG.UsuarioGrupoId In (Select UsuarioGrupoId From dbo.GetUsuarioGrupo(@UsuarioId))
						) OR (
						PRU.UsuarioId = @UsuarioId
						))
						)
						) Begin
		Set @Retorno = 1
	End Else Begin
		Set @Retorno = 0
	End
	
	--Select @Retorno

	Return @Retorno

End

