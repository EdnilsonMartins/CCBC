
Select * From tblSistema
Select * From tblFuncionalidadeGrupo
Select * From tblFuncionalidade Where FuncionalidadeGrupoId = 10
Select * From tblFuncionalidade Where FuncionalidadeId = 295


--Delete tblFuncionalidade Where FuncionalidadeGrupoId = 15
--Delete tblFuncionalidadeGrupo Where FuncionalidadeGrupoId = 15

Insert Into tblFuncionalidadeGrupo(FuncionalidadeGrupoId, SistemaId, Nome) Values(15, 1, 'Template de E-mail Administrativo')


Insert Into tblFuncionalidade(FuncionalidadeId, FuncionalidadeGrupoId, Nome, Descricao) Values(420, 15, 'Listar', Null)
Insert Into tblFuncionalidade(FuncionalidadeId, FuncionalidadeGrupoId, Nome, Descricao) Values(430, 15, 'Editar', Null)



Insert Into tblFuncionalidade(FuncionalidadeId, FuncionalidadeGrupoId, Nome, Descricao) Values(295, 10, 'Pré-Cadastro WebFull', Null)