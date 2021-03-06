--prod 21:59

/****** Object:  StoredProcedure [dbo].[USP_INS_Publicacao]    Script Date: 31/05/2015 18:32:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_INS_Publicacao											*
* Objetivo  : Inserção e alteração de dados da publicação.					*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 07/03/2015	                                            *
* Descrição	    : Adicionado parâmetro @Privado								*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 09/03/2015	                                            *
* Descrição	    : Adicionado parâmetro @ListaUsuarioGrupo					*
*                 Gravação dos Grupos de Usuários da tabela de restrições.  *
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 29/03/2015	                                            *
* Descrição	    : Se houver alteração do Privado, verificar regras novamente*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 04/04/2015	                                            *
* Descrição	    : Adicionado parâmetro @Ativo								*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 19/04/2015	                                            *
* Descrição	    : Adicionado parâmetro @EditoriaId							*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	: 31/05/2015	                                            *
* Descrição	    : Adicionado parâmetros @ListaUsuario e @Tags				*
*				  
****************************************************************************/
ALTER Procedure [dbo].[USP_INS_Publicacao](
	@PublicacaoId		Int,
	@SiteId				Int,
	@PublicacaoTipoId	Int = Null,
	@Data				DateTime = Null,
	@DataValidade		DateTime = Null,
	@Posicao			Int = 0,
	@Destaque			Bit = 0,
	@Privado			Bit = Null,
	@ListaUsuarioGrupo	VarChar(80) = Null,
	@ListaUsuario		VarChar(80) = Null,
	@Ativo				Bit = Null,
	@EditoriaId			Int = Null,
	@Tags				VarChar(4000)
)
As
Begin

	Declare @PrivadoAnterior Bit
	Select @PrivadoAnterior = Privado From tblPublicacao Where PublicacaoId = @PublicacaoId

	If @EditoriaId = 0 Set @EditoriaId = Null

	If @PublicacaoId <> 0 Begin
		Update 
			tblPublicacao 
		Set 
			PublicacaoTipoId = @PublicacaoTipoId,
			Data = @Data, 
			DataValidade = @DataValidade, 
			Posicao = @Posicao,
			Destaque = @Destaque,
			Privado = @Privado,
			Ativo = @Ativo,
			EditoriaId = @EditoriaId,
			Tags = @Tags
		Where 
			PublicacaoId = @PublicacaoId
		Select @PublicacaoId PublicacaoId
	End Else Begin

		Insert Into tblPublicacao(SiteId, PublicacaoTipoId, Data, DataValidade, Posicao, Destaque, Liberado, Privado, Ativo, EditoriaId, Tags) 
		Values(@SiteId, @PublicacaoTipoId, @Data, @DataValidade, @Posicao, @Destaque, Null, @Privado, @Ativo, @EditoriaId, @Tags)
		Select @PublicacaoId = Cast(@@Identity As int)
		Select @PublicacaoId PublicacaoId
	End

	-- Restrição: Grupo.
	Delete 
		tblPublicacaoRestricaoUsuarioGrupo
	From
		tblPublicacaoRestricaoUsuarioGrupo C
	Where
		C.PublicacaoId = @PublicacaoId	
		And C.UsuarioGrupoId Not In (Select Item From dbo.fnSplit(@ListaUsuarioGrupo, ','))

	-- Restrição: Usuário.
	Delete
		tblPublicacaoRestricaoUsuario
	From
		tblPublicacaoRestricaoUsuario PRU
	Where
		PRU.PublicacaoId = @PublicacaoId
		And PRU.UsuarioId Not In (Select Item From dbo.fnSplit(@ListaUsuarioGrupo, ','))

	-- Restrição: Grupo.
	Insert Into tblPublicacaoRestricaoUsuarioGrupo(PublicacaoId, UsuarioGrupoId)
	Select
		@PublicacaoId, Item
	From
		dbo.fnSplit(@ListaUsuarioGrupo, ',') C
	Where
		C.Item Not In (Select AAC.UsuarioGrupoId From tblPublicacaoRestricaoUsuarioGrupo AAC Where AAC.PublicacaoId = @PublicacaoId)

	-- Restrição: Usuário.
	Insert Into tblPublicacaoRestricaoUsuario(PublicacaoId, UsuarioId)
	Select
		@PublicacaoId, Item
	From
		dbo.fnSplit(@ListaUsuario, ',') C
	Where
		C.Item Not In (Select AAC.UsuarioId From tblPublicacaoRestricaoUsuario AAC Where AAC.PublicacaoId = @PublicacaoId)

	-- Se houve alteração no controle de Privado, verificar as regras novamente!
	If (@PrivadoAnterior <> @Privado) Begin
		Exec  USP_INS_Publicacao_Liberacao @PublicacaoId
	End

End