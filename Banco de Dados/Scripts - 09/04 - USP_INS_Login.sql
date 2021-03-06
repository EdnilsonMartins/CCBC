
/****** Object:  StoredProcedure [dbo].[USP_INS_Login]    Script Date: 29/03/2015 14:48:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_INS_Login													*
* Objetivo  : Manutenção do Cadastro de Usuários.							*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 29/03/2015	                                            *
* Descrição	    : Adicionados parâmetros @Email e @ListaUsuarioGrupo.		*
*				  @SiteId                                                   *
****************************************************************************/
ALTER Procedure [dbo].[USP_INS_Login](
	@UsuarioId	Int,
	@Nome		VarChar(80)	= '',
	@Email		VarChar(255)	= '',
	@Login		VarChar(80) = '',
	@Senha		VarChar(32) = '',
	@Ativo		Bit = 0,
	@ListaUsuarioGrupo	VarChar(80) = Null,
	@SiteId		Int
)
As
Begin

	If @UsuarioId <> 0 Begin
		Update tblUsuario Set Nome = @Nome, Email = @Email, Login = @Login, Ativo = @Ativo Where UsuarioId = @UsuarioId
		Select @UsuarioId UsuarioId
	End Else Begin

		Insert Into tblUsuario(Nome, Email, Login, Senha, Ativo, SiteId) Values(@Nome, @Email, @Login, @Senha, @Ativo, @SiteId)
		Set @UsuarioId = Cast(@@Identity As int)
		Select @UsuarioId UsuarioId
	End


	Delete 
		tblUsuarioUsuarioGrupo
	From
		tblUsuarioUsuarioGrupo C
	Where
		C.UsuarioId = @UsuarioId	
		And C.UsuarioGrupoId Not In (Select Item From dbo.fnSplit(@ListaUsuarioGrupo, ','))


	Insert Into tblUsuarioUsuarioGrupo(UsuarioId, UsuarioGrupoId)
	Select
		@UsuarioId, Item
	From
		dbo.fnSplit(@ListaUsuarioGrupo, ',') C
	Where
		C.Item Not In (Select AAC.UsuarioGrupoId From tblUsuarioUsuarioGrupo AAC Where AAC.UsuarioId = @UsuarioId)
End