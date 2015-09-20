--select * from tblPublicacaoTipo

select * from tblArquivo order by arquivoid desc


ArquivoId	Content	Legenda
10057	Quick6
10056	Quick5
10055	Quick4
10054	Quick3
10053	Quick2
10052	Quick1


Select * From tblMenu Where MenuTipoId = 2
Select * From tblMenuTipo

Select * From tblMenu 

/*
Delete From tblMenu Where MenuTipoId = 2
Select
	*
Delete
	tblMenuIdiomaExcecao
From 
	tblMenu M join tblMenuIdiomaExcecao MIE On MIE.MenuId = M.MenuId
Where
	MenuTipoId = 2
*/

Declare @MenuId Int
Insert Into tblMenu(MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL, ImageURL) Values(Null, 2, 2, 2, Null, Null, 'File?Id=10052')
Select @MenuId = @@Identity

Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(@MenuId, 1, 'Leg. de Arbitragem')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(@MenuId, 2, 'Leg. Arbitreto')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(@MenuId, 3, 'Rules Regulation')

Go

Declare @MenuId Int
Insert Into tblMenu(MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL, ImageURL) Values(Null, 2, 2, 2, Null, Null, 'File?Id=10053')
Select @MenuId = @@Identity

Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(@MenuId, 1, 'Modelos de Claúsulas')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(@MenuId, 2, 'Clausuletas')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(@MenuId, 3, 'Clause Models')


Go --

Declare @MenuId Int
Insert Into tblMenu(MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL, ImageURL) Values(Null, 2, 2, 2, Null, Null, 'File?Id=10054')
Select @MenuId = @@Identity

Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(@MenuId, 1, 'Corpo de Arbitro')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(@MenuId, 2, 'Cuerpo Chefia')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(@MenuId, 3, 'Head Tech')

Go

Declare @MenuId Int
Insert Into tblMenu(MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL, ImageURL) Values(Null, 2, 2, 2, Null, Null, 'File?Id=10055')
Select @MenuId = @@Identity

Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(@MenuId, 1, 'Calculadora Tabela Despesas')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(@MenuId, 2, 'Maquinita de Calculo')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(@MenuId, 3, 'Cost Calculator')

Go

Declare @MenuId Int
Insert Into tblMenu(MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL, ImageURL) Values(Null, 2, 2, 2, Null, Null, 'File?Id=10056')
Select @MenuId = @@Identity

Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(@MenuId, 1, 'FAQ')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(@MenuId, 2, 'Questionamentos')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(@MenuId, 3, 'F.A.Q.')

Go

Declare @MenuId Int
Insert Into tblMenu(MenuPaiId, SiteId, MenuTipoId, MenuTipoAcaoId, PublicacaoId, LinkURL, ImageURL) Values(Null, 2, 2, 2, Null, Null, 'File?Id=10057')
Select @MenuId = @@Identity

Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(@MenuId, 1, 'Agenda')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(@MenuId, 2, 'Ponto')
Insert Into tblMenuIdiomaExcecao(MenuId, IdiomaId, Rotulo) Values(@MenuId, 3, 'Schedule')


select * from tblPublicacaoTipo

select * from tblPublicacaoIdiomaExcecao