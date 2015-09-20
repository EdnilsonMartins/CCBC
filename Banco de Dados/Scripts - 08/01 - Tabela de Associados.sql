

Create Table tblAssociadoCategoria(
	AssociadoCategoriaId Int Not Null,
	Descricao	VarChar(30),
	Constraint PK_tblAssociadoCategoria Primary Key(AssociadoCategoriaId)
)
Go

Insert Into tblAssociadoCategoria(AssociadoCategoriaId, Descricao) Values(1, 'Associados')
Insert Into tblAssociadoCategoria(AssociadoCategoriaId, Descricao) Values(2, 'Árbitros')
Insert Into tblAssociadoCategoria(AssociadoCategoriaId, Descricao) Values(3, 'Mediadores')
Go

Create Table tblTipoPessoa(
	TipoPessoaId Int Not Null,
	Descricao VarChar(30),
	Constraint PK_tblTipoPessoa Primary Key(TipoPessoaId)
)
Go
Insert Into tblTipoPessoa(TipoPessoaId, Descricao) Values(1, 'Pessoa Física')
Insert Into tblTipoPessoa(TipoPessoaId, Descricao) Values(2, 'Pessoa Jurídica')
Go

Create Table tblPais(
	PaisId	Int Not Null,
	Nome	VarChar(50),
	Constraint PK_tblPais Primary Key(PaisId)
)
Go
Insert Into tblPais(PaisId, Nome) Values(1, 'Brasil')
Insert Into tblPais(PaisId, Nome) Values(2, 'Canadá')
Insert Into tblPais(PaisId, Nome) Values(3, 'Estados Unidos')
Go

Alter Table tblAssociado
Add AssociadoCategoriaId Int Null,
	TipoPessoaId Int Null,
	PaisId Int Null
Go

Alter Table tblAssociado
Alter Column Resumo VarChar(Max) Null
Go

Alter Table tblAssociado
Add Constraint FK_tblAssociado_tblAssociadoCategoria Foreign Key(AssociadoCategoriaId) References tblAssociadoCategoria(AssociadoCategoriaId),
	Constraint FK_tblAssociado_tblTipoPessoa Foreign Key(TipoPessoaId) References tblTipoPessoa(TipoPessoaId),
	Constraint FK_tblAssociado_tblPais Foreign Key(PaisId) References tblPais(PaisId)

Go

Create Table tblAssociadoArquivo(
	AssociadoArquivoId Int Identity Not Null,
	AssociadoId	Int Not Null,
	ArquivoId BigInt Not Null,
	DataInclusao DateTime Default GetDate(),
	Constraint PK_tblAssociadoArquivo Primary Key(AssociadoArquivoId),
	Constraint FK_tblAssociadoArquivo_tblAssociado Foreign Key (AssociadoId) References tblAssociado(AssociadoId),
	Constraint FK_tblAssociadoArquivo_tblArquivo Foreign Key (ArquivoId) References tblArquivo(ArquivoId)
)
Go