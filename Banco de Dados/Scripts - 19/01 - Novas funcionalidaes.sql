 
-- Novo grupo de funcionalidade
Insert Into tblFuncionalidadeGrupo(FuncionalidadeGrupoId, SistemaId, Nome) Values(14, 1, 'Pessoas')

-- Novas funcionalidades
Insert Into tblFuncionalidade(FuncionalidadeId, FuncionalidadeGrupoId, Nome, Descricao) Values(380, 14, 'Listar', Null)
Insert Into tblFuncionalidade(FuncionalidadeId, FuncionalidadeGrupoId, Nome, Descricao) Values(390, 14, 'Adicionar', Null)
Insert Into tblFuncionalidade(FuncionalidadeId, FuncionalidadeGrupoId, Nome, Descricao) Values(400, 14, 'Editar', Null)
Insert Into tblFuncionalidade(FuncionalidadeId, FuncionalidadeGrupoId, Nome, Descricao) Values(410, 14, 'Excluir', Null)


-- Resumo dos registros inseridos
Select * from tblFuncionalidadeGrupo
Select * from tblFuncionalidade