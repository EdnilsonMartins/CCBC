
Create Table tblBannerTipo(
	BannerTipoId	Int Not Null,
	Descricao		VarChar(30),
	Constraint PK_tblBannerTipo Primary Key(BannerTipoId)
)
Go

Insert Into tblBannerTipo(BannerTipoId, Descricao) Values(1, 'Página Inicial - Superior')
Insert Into tblBannerTipo(BannerTipoId, Descricao) Values(2, 'Menu Lateral')
Insert Into tblBannerTipo(BannerTipoId, Descricao) Values(3, 'Mantenedores')
Insert Into tblBannerTipo(BannerTipoId, Descricao) Values(4, 'Parceiros')
Insert Into tblBannerTipo(BannerTipoId, Descricao) Values(5, 'Página Inicial - Inferior')
Go

Create Table tblTarget(
	TargetId		Int			Not Null,
	Descricao		VarChar(30)	Not Null,
	Constraint PK_tblTarget Primary Key(TargetId)
)
Go

Insert Into tblTarget(TargetId, Descricao) Values(1, 'Mesma janela')
Insert Into tblTarget(TargetId, Descricao) Values(2, 'Nova janela')
Go

Create Table tblBanner(
	BannerId		Int Identity Not Null,	
	SiteId			Int				Not Null,
	BannerTipoId	Int			Not Null,
	TargetId		Int			Null,
	Data			Datetime	Null,
	DataValidade	Datetime	Null,
	Posicao			Int			Null,
	LinkURL			VarChar(500) Null,
	Privado			Bit			Null,
	Liberado		Bit			Null,
	Constraint PK_tblBanner Primary Key(BannerId),
	Constraint FK_tblBanner_tblSite Foreign Key(SiteId) References tblSite(SiteId),
	Constraint FK_tblBanner_tblBannerTipo Foreign Key(BannerTipoId) References tblBannerTipo(BannerTipoId),
	Constraint FK_tblBanner_tblTarget Foreign Key(TargetId) References tblTarget(TargetId)
)
Go

Create Table tblBannerIdiomaExcecao(
	BannerIdiomaExcecaoId	Int Identity Not Null,
	BannerId				Int Not Null,
	IdiomaId				Int Not Null,
	Titulo					VarChar(80),
	Resumo					VarChar(200),
	Descricao				VarChar(200),
	Constraint PK_tblBannerIdiomaExcecao Primary Key(BannerIdiomaExcecaoId),
	Constraint FK_tblBannerIdiomaExcecao_tblBanner Foreign Key(BannerId) References tblBanner(BannerId),
	Constraint FK_tblBannerIdiomaExcecao_tblIdioma Foreign Key(IdiomaId) References tblIdioma(IdiomaId)
)
Go

Create Table tblBannerRestricaoUsuarioGrupo(
	BannerRestricaoUsuarioGrupoId	Int Identity Not Null,
	BannerId						Int Not Null,
	UsuarioGrupoId					Int,
	Constraint PK_tblBannerRestricaoUsuarioGrupo Primary Key(BannerRestricaoUsuarioGrupoId),
	Constraint FK_tblBannerRestricaoUsuarioGrupo_tblBanner Foreign Key(BannerId) References tblBanner(BannerId),
	Constraint FK_tblBannerRestricaoUsuarioGrupo_tblUsuarioGrupo Foreign Key(UsuarioGrupoId) References tblUsuarioGrupo(UsuarioGrupoId)
)
Go

Create Table tblBannerRestricaoUsuario(
	BannerRestricaoUsuarioId	Int Identity Not Null,
	BannerId						Int Not Null,
	UsuarioId					Int,
	Constraint PK_tblBannerRestricaoUsuario Primary Key(BannerRestricaoUsuarioId),
	Constraint FK_tblBannerRestricaoUsuario_tblBanner Foreign Key(BannerId) References tblBanner(BannerId),
	Constraint FK_tblBannerRestricaoUsuario_tblUsuarioGrupo Foreign Key(UsuarioId) References tblUsuario(UsuarioId)
)
Go


Create Table tblBannerArquivo(
	BannerArquivoId	BigInt Identity Not Null,
	BannerId		Int				Not Null,
	ArquivoId		BigInt			Not Null,
	DataInclusao	DateTime		Not Null,
	Constraint PK_tblBannerArquivo Primary Key(BannerArquivoId),
	Constraint FK_tblBannerArquivo_tblBanner Foreign Key(BannerId) References tblBanner(BannerId),
	Constraint FK_tblBannerArquivo_tblArquivo Foreign Key(ArquivoId) References tblArquivo(ArquivoId)
)
Go

Create Table tblBannerClique(
	BannerCliqueId	Int Identity Not Null,
	BannerId		Int Not Null,
	Data			DateTime Not Null,
	Constraint PK_tblBannerClique Primary Key(BannerCliqueId),
	Constraint FK_tblBannerClique_tblBanner Foreign Key(BannerId) References tblBanner(BannerId)
)
Go

Create Table tblBannerAprovacaoHistorico(
	BannerAprovacaoHistoricoId	Int Identity Not Null,
	BannerId					Int Not Null,
	DataLiberacao				DateTime Not Null,
	Liberado					Bit Not Null,
	Constraint PK_tblBannerAprovacaoHistorico Primary Key(BannerAprovacaoHistoricoId),
	Constraint FK_tblBannerAprovacaoHistorico_tblBanner Foreign Key(BannerId) References tblBanner(BannerId)
)
Go

Create Table tblBannerAprovacaoItem(
	BannerAprovacaoItemId	Int Identity	Not Null,
	BannerId			Int				Not Null,
	UsuarioId			Int				Not Null,
	DataAprovacao		DateTime		Not Null,
	Liberado			Bit				Null,
	Constraint PK_tblBannerAprovacao Primary Key(BannerAprovacaoItemId),
	Constraint FK_tblBannerAprovacao_tblBanner Foreign Key(BannerId) References tblBanner(BannerId),
	Constraint FK_tblBannerAprovacao_tblUsuario Foreign Key(UsuarioId) References tblUsuario(UsuarioId)
)
Go



