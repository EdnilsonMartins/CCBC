


Create Table tblBannerTipo(	
	BannerTipoId	Int Identity Not Null,
	Descricao		VarChar(50) Null,
	Posicao			Int Null,
	Constraint PK_tblBannerTipo Primary Key(BannerTipoId)
)
Go

Grant Select On tblBannerTipo To usuCCBC
Go

Set Identity_Insert tblBannerTipo On
Insert Into tblBannerTipo(BannerTipoId, Descricao, Posicao) Values(1, 'Home Principal', 1)
Insert Into tblBannerTipo(BannerTipoId, Descricao, Posicao) Values(2, 'Home Mantenedores', 2)
Insert Into tblBannerTipo(BannerTipoId, Descricao, Posicao) Values(3, 'Home Parceiras', 3)
Insert Into tblBannerTipo(BannerTipoId, Descricao, Posicao) Values(4, 'Home Lateral', 4)
Insert Into tblBannerTipo(BannerTipoId, Descricao, Posicao) Values(5, 'Home Inferior Esquerda', 5)
Insert Into tblBannerTipo(BannerTipoId, Descricao, Posicao) Values(6, 'Home Inferior', 6)
Insert Into tblBannerTipo(BannerTipoId, Descricao, Posicao) Values(7, 'Notícias', 1)
Insert Into tblBannerTipo(BannerTipoId, Descricao, Posicao) Values(8, 'Eventos', 1)
Set Identity_Insert tblBannerTipo Off
Go

Select * From tblBannerTipo
Go

---------------------------------------------------------------------------------------------------


	Banner
	SiteId
	BannerTipo
	Titulo
	Descricao
	TextoAlternativo
	URL
	Target
	Relacionamento ???
	Posicao
	DataValidade


	GaleriaId
	SiteId
	Nome


	ArquivoTipoId
	Descricao
'Video'
'Áudio'
'Imagem'


	GaleriaItemId
	GaleriaId
	ArquivoTipoId
	IdiomaId
	Titulo
	Descricao
	Content


	select * from tblArquivo order by arquivoid desc