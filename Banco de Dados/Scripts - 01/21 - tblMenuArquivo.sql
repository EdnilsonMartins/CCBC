Create Table tblMenuArquivo(
	MenuArquivoId	BigInt Identity Not Null,
	MenuId			Int				Not Null,
	ArquivoId		BigInt			Not Null,
	DataInclusao	DateTime		Default GetDate(),
	Constraint PK_tblMenuArquivo Primary Key(MenuArquivoId)
)
Go
