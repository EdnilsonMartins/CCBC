Create Table tblPasta(
	PastaId	int identity not null,
	PastaPaiId	int null,
	SiteId int null,
	Descricao varchar(255) null,
	constraint PK_tblPasta primary key(PastaId),
	constraint FK_tblPasta_tblPasta foreign key(PastaPaiId) references tblPasta(PastaId),
	constraint FK_tblPasta_tblSite foreign key(SiteId) references tblSite(SiteId)
)
go


Alter Table tblPasta
Add Posicao Int
go