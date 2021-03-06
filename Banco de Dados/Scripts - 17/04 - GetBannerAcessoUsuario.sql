--prod 21:58

/****** Object:  UserDefinedFunction [dbo].[GetBannerAcessoUsuario]    Script Date: 31/05/2015 16:30:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco			:	dbCCBC													*
* Procedure		:	GetBannerAcessoUsuario									*
* Objetivo		:	Retorna se o usuário possui direito a visualização do	*
*					banner baseado no controle de privacidade.				*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 31/05/2015	                                            *
* Descrição	    : Adicionado join com a tblPublicacaoRestricaoUsuario		*
*                 para permitir restrição a nível de usuário.               *
****************************************************************************/
ALTER Function [dbo].[GetBannerAcessoUsuario](
	@BannerId	Int,
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
					tblBanner B
					left join tblBannerRestricaoUsuarioGrupo BRUG On BRUG.BannerId = B.BannerId
					left join tblBannerRestricaoUsuario BRU On BRU.BannerId = B.BannerId
				Where 
					B.BannerId = @BannerId
					And ((IsNUll(B.Privado, 0) = 0)
						Or
						((B.Privado = 1)
						And
						(
						BRUG.UsuarioGrupoId In (Select UsuarioGrupoId From dbo.GetUsuarioGrupo(@UsuarioId))
						) OR (
						BRU.UsuarioId = @UsuarioId
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

