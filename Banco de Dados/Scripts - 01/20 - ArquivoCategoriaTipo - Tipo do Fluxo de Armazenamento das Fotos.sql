Create Table tblArquivoCategoriaTipo(
	ArquivoCategoriaTipoId	Int Not Null,
	Descricao VarChar(50),
	Constraint PK_tblArquivoCategoriaTipo Primary Key(ArquivoCategoriaTipoId)
)
Go

Insert Into tblArquivoCategoriaTipo(ArquivoCategoriaTipoId, Descricao) Values(1, 'Publica��o')
Insert Into tblArquivoCategoriaTipo(ArquivoCategoriaTipoId, Descricao) Values(2, 'Menu')
Insert Into tblArquivoCategoriaTipo(ArquivoCategoriaTipoId, Descricao) Values(3, 'Banner')
Insert Into tblArquivoCategoriaTipo(ArquivoCategoriaTipoId, Descricao) Values(4, 'Media Global')
Go

Alter Table tblArquivoCategoria
Add ArquivoCategoriaTipoId Int Null
Go

-- Publica��o: Categorias das Fotos.
Update tblArquivoCategoria Set ArquivoCategoriaTipoId = 1 Where ArquivoCategoriaId = 1
Update tblArquivoCategoria Set ArquivoCategoriaTipoId = 1 Where ArquivoCategoriaId = 2
Update tblArquivoCategoria Set ArquivoCategoriaTipoId = 1 Where ArquivoCategoriaId = 3

-- Publica��o: �cones do Menu. --- !!!!Verificar insert antes de execut�-lo!!!!!

Insert Into tblArquivoCategoria(ArquivoCategoriaId, Descricao) Values(4, '�cone de Menu')
Update tblArquivoCategoria Set ArquivoCategoriaTipoId = 2 Where ArquivoCategoriaId = 4