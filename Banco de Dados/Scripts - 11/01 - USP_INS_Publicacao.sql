
/****** Object:  StoredProcedure [dbo].[USP_INS_Publicacao]    Script Date: 04/04/2015 13:23:32 ******/
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
	@Ativo				Bit = Null
)
As
Begin

	Declare @PrivadoAnterior Bit
	Select @PrivadoAnterior = Privado From tblPublicacao Where PublicacaoId = @PublicacaoId

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
			Ativo = @Ativo
		Where 
			PublicacaoId = @PublicacaoId
		Select @PublicacaoId PublicacaoId
	End Else Begin

		Insert Into tblPublicacao(SiteId, PublicacaoTipoId, Data, DataValidade, Posicao, Destaque, Liberado, Privado, Ativo) 
		Values(@SiteId, @PublicacaoTipoId, @Data, @DataValidade, @Posicao, @Destaque, Null, @Privado, @Ativo)
		Select Cast(@@Identity As int) PublicacaoId
	End

	Delete 
		tblPublicacaoRestricaoUsuarioGrupo
	From
		tblPublicacaoRestricaoUsuarioGrupo C
	Where
		C.PublicacaoId = @PublicacaoId	
		And C.UsuarioGrupoId Not In (Select Item From dbo.fnSplit(@ListaUsuarioGrupo, ','))


	Insert Into tblPublicacaoRestricaoUsuarioGrupo(PublicacaoId, UsuarioGrupoId)
	Select
		@PublicacaoId, Item
	From
		dbo.fnSplit(@ListaUsuarioGrupo, ',') C
	Where
		C.Item Not In (Select AAC.UsuarioGrupoId From tblPublicacaoRestricaoUsuarioGrupo AAC Where AAC.PublicacaoId = @PublicacaoId)

	-- Se houve alteração no controle de Privado, verificar as regras novamente!
	If (@PrivadoAnterior <> @Privado) Begin
		Exec  USP_INS_Publicacao_Liberacao @PublicacaoId
	End

End