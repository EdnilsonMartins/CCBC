USE [dbCCBC]
GO
/****** Object:  UserDefinedFunction [dbo].[GetPublicacaoAcessoUsuario]    Script Date: 18/02/2015 22:54:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Function [dbo].[GetBannerAcessoUsuario](
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
				Where 
					B.BannerId = @BannerId
					And ((B.Privado = 0)
						Or
						((B.Privado = 1)
						And
						(
						BRUG.UsuarioGrupoId In (Select UsuarioGrupoId From dbo.GetUsuarioGrupo(@UsuarioId))
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

