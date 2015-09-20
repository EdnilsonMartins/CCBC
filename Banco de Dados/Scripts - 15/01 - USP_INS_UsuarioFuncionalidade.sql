


/****************************************************************************
* Banco : dbCCBC														    *
* Procedure : USP_INS_UsuarioFuncionalidade									*
* Objetivo  : Gravação das Funcionalidades do Usuário (Insert/Update)		*
*                                                                           *
* Criada por: Ednilson Martins                                              *
* Data Criação: 10/05/2015                                                  *
*																			*
* Alterada por	:  															*
* Data Alteração:  															*
* Descrição		:															*
*																			*
****************************************************************************/

Create Procedure USP_INS_UsuarioFuncionalidade(
	@UsuarioId			Int,
	@XML				xml
)
As

Begin

	Set Transaction Isolation Level Read Uncommitted
	Set NoCount On


	Declare @indErro As Bit,
			@msgErro As VarChar(1000)
	Set @indErro = 0
	Set @msgErro = ''

/*
    Declare @UsuarioId			Int = 1,
			@XML				xml
	Set @XML = '<UsuarioFuncionalidades>
<Fun><FuncionalidadeId>1</FuncionalidadeId><Ativo>1</Ativo><Parametro>a</Parametro></Fun>
<Fun><FuncionalidadeId>10</FuncionalidadeId><Ativo>1</Ativo><Parametro>b</Parametro></Fun>
<Fun><FuncionalidadeId>20</FuncionalidadeId><Ativo>1</Ativo><Parametro>c</Parametro></Fun>
</UsuarioFuncionalidades>'
*/

      
	/**** Todos os Acessórios ****/
    Create Table #tblUsuarioFuncionalidade_Novo(
		UsuarioId			Int,
		FuncionalidadeId	Int,
		Ativo				Bit,
		Parametro			VarChar(80)
	)

	Insert Into #tblUsuarioFuncionalidade_Novo(UsuarioId, FuncionalidadeId, Ativo, Parametro)
	Select
		@UsuarioId UsuarioId,
		I.value ('FuncionalidadeId[1]', 'Int'),
		Case I.value ('Ativo[1]', 'Char(1)') 
			When '' Then Null 
			Else I.value ('Ativo[1]', 'Bit') 
		End,
		I.value ('Parametro[1]', 'VarChar(80)')
	From  @XML.nodes ('/UsuarioFuncionalidades/Fun') InformacaoTecnica(I)
	/*****************************/




	/**** Acessórios Atuais ****/
	Create Table #tblUsuarioFuncionalidade_Atual(
		UsuarioId			Int,
		FuncionalidadeId	Int,
		Ativo				Bit,
		Parametro			VarChar(80)
	)

	Insert Into #tblUsuarioFuncionalidade_Atual(UsuarioId, FuncionalidadeId, Ativo, Parametro)
	Select UsuarioId, FuncionalidadeId, Ativo, Parametro
	From tblUsuarioFuncionalidade where UsuarioId = @UsuarioId
	/***************************/




	/**** Alterações Necessárias ****/
	Create Table #tblUsuarioFuncionalidade_Final(
		UsuarioId			Int,
		FuncionalidadeId	Int,
		Ativo				Bit,
		Parametro			VarChar(80),
		TipoAlteracao		TinyInt
	)

	-- TipoAlteração (1) Inserido
	Insert	Into #tblUsuarioFuncionalidade_Final(UsuarioId, FuncionalidadeId, Ativo, Parametro, TipoAlteracao)
	Select	B.UsuarioId, B.FuncionalidadeId, B.Ativo, B.Parametro,  1   
	From	#tblUsuarioFuncionalidade_Atual A 
			Right Join #tblUsuarioFuncionalidade_Novo B On A.UsuarioId = B.UsuarioId 
													 And A.FuncionalidadeId = B.FuncionalidadeId 
	Where	A.UsuarioId Is Null
	-- TipoAlteração (2) Atualização
	Insert Into #tblUsuarioFuncionalidade_Final(UsuarioId, FuncionalidadeId, Ativo, Parametro, TipoAlteracao)
	Select	B.UsuarioId, B.FuncionalidadeId, B.Ativo, B.Parametro, 2 
	From	#tblUsuarioFuncionalidade_Atual A 
			join #tblUsuarioFuncionalidade_Novo B On A.UsuarioId = B.UsuarioId 
													 And A.FuncionalidadeId = B.FuncionalidadeId 
	Where	A.Ativo <> B.Ativo
			Or (A.Ativo Is Null And B.Ativo Is Not Null)
			Or (A.Ativo Is Not Null And B.Ativo Is Null)
			Or A.Parametro <> B.Parametro
			Or (A.Parametro Is Null And B.Parametro Is Not Null)
			Or (A.Parametro Is Not Null And B.Parametro Is Null)
	-- TipoAlteração (4) Deleteda
	Insert Into #tblUsuarioFuncionalidade_Final(UsuarioId, FuncionalidadeId, Ativo, Parametro, TipoAlteracao)
	Select	A.UsuarioId, A.FuncionalidadeId, B.Ativo, B.Parametro, 4   
	From	#tblUsuarioFuncionalidade_Atual A 
			Left Join #tblUsuarioFuncionalidade_Novo B On	A.UsuarioId = B.UsuarioId 
													 And A.FuncionalidadeId = B.FuncionalidadeId 
	Where	B.UsuarioId Is Null
	/********************************/





	--- Efetiva as inclusões/alterações na coleta:
	-- Inclusão:
	Insert Into tblUsuarioFuncionalidade(UsuarioId, FuncionalidadeId, Ativo, Parametro)
	Select UsuarioId, FuncionalidadeId, Ativo, Parametro From #tblUsuarioFuncionalidade_Final Where TipoAlteracao = 1

	-- Exclusão:
	Delete	C
	From	tblUsuarioFuncionalidade C
			join #tblUsuarioFuncionalidade_Final F On	F.UsuarioId = C.UsuarioId
												And F.FuncionalidadeId = C.FuncionalidadeId
	Where	F.TipoAlteracao = 4

	-- Atualização/Ativo:
	Update	tblUsuarioFuncionalidade
	Set		Ativo = F.Ativo
	From	tblUsuarioFuncionalidade C
			join #tblUsuarioFuncionalidade_Final F on	F.UsuarioId = C.UsuarioId
												And F.FuncionalidadeId = C.FuncionalidadeId
	Where	TipoAlteracao = 2
			And (C.Ativo <> F.Ativo
				 Or (C.Ativo Is Null And F.Ativo Is Not Null)
				 Or (C.Ativo Is Not Null And F.Ativo Is Null)
				 )

	-- Atualização/Parametro:
	Update	tblUsuarioFuncionalidade
	Set		Parametro = F.Parametro
	From	tblUsuarioFuncionalidade C
			join #tblUsuarioFuncionalidade_Final F on	F.UsuarioId = C.UsuarioId
												And F.FuncionalidadeId = C.FuncionalidadeId
	Where	TipoAlteracao = 2
			And (C.Parametro <> F.Parametro
				 Or (C.Parametro Is Null And F.Parametro Is Not Null)
				 Or (C.Parametro Is Not Null And F.Parametro Is Null)
				 )
	-----------------------------------------------------

	Drop Table #tblUsuarioFuncionalidade_Novo
	Drop Table #tblUsuarioFuncionalidade_Atual
	Drop Table #tblUsuarioFuncionalidade_Final

	Select @indErro As indErro, @msgErro As msgErro

End

