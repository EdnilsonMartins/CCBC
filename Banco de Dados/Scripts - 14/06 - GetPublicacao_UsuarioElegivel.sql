

/****************************************************************************
* Banco			:	dbCCBC													*
* Procedure		:	GetPublicacao_UsuarioElegivel							*
* Objetivo		:	Retorna se o usuário é elegível ou não para uma 		*
*					determinada Publicação.									*
*																			*
* Criada por	: Ednilson Martins	                                        *
* Data Criação	: 09/05/2015	                                            *
*                                                                           *
Select * From dbo.GetPublicacao_UsuarioElegivel(3111, 1)
****************************************************************************/
Create Function [dbo].[GetPublicacao_UsuarioElegivel](
    @PublicacaoId Int,
	@UsuarioId Int
) RETURNS @result TABLE (Ok_Usuario Bit, Ok_UsuarioGrupo Bit, UsuarioElegivel Bit, Liberado Bit, DataAprovacao DateTime)

BEGIN

	--Declare @PublicacaoId Int = 3112
	--Declare @UsuarioId Int = 1

	Insert Into @result
	Select 
		Cast(S.Ok_Usuario As Bit) As Ok_Usuario,
		Cast(S.Ok_UsuarioGrupo As Bit) As Ok_UsuarioGrupo,
		Cast(Case When (S.Ok_Usuario = 1) Or (S.Ok_UsuarioGrupo = 1) Then 1 Else 0 End As Bit) UsuarioElegivel,
		L.Liberado As Liberado,
		L.DataAprovacao As DataAprovacao
	From
		(
		Select
			top 1
			F.Sequencia,
			F.Liberado,
			F.Resultado, 
		
			RPC.UsuarioGrupoId,
			RPC.UsuarioId,

			Case When RPC.UsuarioId = @UsuarioId Then 1 Else 0 End Ok_Usuario,
			Case When Exists(select 1 from dbo.GetUsuarioGrupo(@UsuarioId) where UsuarioGrupoId = RPC.UsuarioGrupoId) Then 1 Else 0 End Ok_UsuarioGrupo

		From
			dbo.GetRegraPassoResultadoFinal(@PublicacaoId) F
			join tblRegraPassoCondicao RPC On RPC.RegraPassoId = F.RegraPassoId
		Order By 
			F.Resultado Asc, F.Sequencia Asc
		) S
		outer apply (Select UsuarioId, Liberado, DataAprovacao From tblPublicacaoAprovacaoItem Where PublicacaoId = @PublicacaoId And UsuarioId = @UsuarioId) L

	Return

End