--prod 21:58

/****** Object:  UserDefinedFunction [dbo].[GetMenuAcessoUsuario]    Script Date: 31/05/2015 16:28:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco			:	dbCCBC													*
* Procedure		:	GetMenuAcessoUsuario									*
* Objetivo		:	Retorna se o usuário possui direito a visualização do	*
*					menu baseado no controle de privacidade.				*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 31/05/2015	                                            *
* Descrição	    : Adicionado join com a tblPublicacaoRestricaoUsuario		*
*                 para permitir restrição a nível de usuário.               *
****************************************************************************/

ALTER Function [dbo].[GetMenuAcessoUsuario](
	@MenuId	Int,
	@UsuarioId		Int = Null
) 
Returns Bit As
Begin

	/*Declare @PublicacaoId	Int,
			@UsuarioId		Int
	Set @PublicacaoId = 3
	Set @UsuarioId = null*/

	Declare @Retorno Bit
	If Exists(
				Select 
					1
				From
					tblMenu M
					left join tblMenuRestricaoUsuarioGrupo MRUG On MRUG.MenuId = M.MenuId
					left join tblMenuRestricaoUsuario MRU On MRU.MenuId = M.MenuId
				Where 
					M.MenuId = @MenuId
					And ((IsNUll(M.Privado, 0) = 0)
						Or
						((M.Privado = 1)
						And
						(
						MRUG.UsuarioGrupoId In (Select UsuarioGrupoId From dbo.GetUsuarioGrupo(@UsuarioId))
						) OR (
						MRU.UsuarioId = @UsuarioId
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

