

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_SEL_Funcionalidade										*
* Objetivo  : Retorna as funcionalidades do Sistema							*
*                                                                           *
* Creiada por	: Ednilson Martins	                                        *
* Data Criação	: 10/05/2015	                                            *
*                                                                           *
USP_SEL_Funcionalidade 1
****************************************************************************/
Create Procedure USP_SEL_Funcionalidade(
	@SistemaId	Int
)
As
Begin


	Select 
		F.FuncionalidadeId,
		F.Nome Funcionalidade,
		FG.FuncionalidadeGrupoId,
		FG.Nome FuncionalidadeGrupo
	From
		tblFuncionalidade F
		join tblFuncionalidadeGrupo FG On F.FuncionalidadeGrupoId = FG.FuncionalidadeGrupoId
		join tblSistema S On FG.SistemaId = S.SistemaId
	Where
		S.SistemaId = @SistemaId

End