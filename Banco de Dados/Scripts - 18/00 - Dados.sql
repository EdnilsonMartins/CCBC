select * from tblSite
select * from tblConfiguracao
select * from tblFuncionalidadeGrupo

Insert Into tblFuncionalidade(FuncionalidadeId, FuncionalidadeGrupoId, Nome, Descricao) Values(51, 3, 'Adicionar', null)
Insert Into tblFuncionalidade(FuncionalidadeId, FuncionalidadeGrupoId, Nome, Descricao) Values(52, 3, 'Editar', null)
Insert Into tblFuncionalidade(FuncionalidadeId, FuncionalidadeGrupoId, Nome, Descricao) Values(53, 3, 'Excluir', null)

Alter Table tblSite
Add Tags VarChar(4000)
Go