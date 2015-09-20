
-- Tabelas de dom�nio:
Select * From tblRegraTipo
1	Banner
2	Evento
3	Mat�ria

Select * From tblRegraAprovacaoCondicao
1	Uma das condi��es
2	Todas das condi��es
---------------------------------------

Select * From tblRegraAprovacao
Select * From tblRegraAprovacaoItem

-- ETAPA 1 ------------------------------------------------------
-- 1. Cadastrar a Regra:
-- O campo privado define se a regra ser� aplicada a conte�do privado ou p�blico.
-- Exemplo: A regra abaixo � do tipo Banner, onde todas as condi��es dever�o ser satizfeitas e ser� aplicada para conte�do privado.
--		    Regra 1 - Aprova��o de Banner da Home


-- ID#1
Insert Into tblRegra(RegraTipoId, Descricao)
Values(1, 'Aprova��o de Banner da Home (Privado)')
	 
-- ID#2
Insert Into tblRegra(RegraTipoId, Descricao)
Values(1, 'Aprova��o de Banner da Home (P�blico)')
	 
-- ID#3
Insert Into tblRegra(RegraTipoId, Descricao)
Values(1, 'Aprova��o de uma Not�cia (Privado)')


-- 2. Cadastrar os Passos:
select * from tblRegraPasso
-- Regra #1  |  Passo #1
Insert Into tblRegraPasso(RegraId, Sequencia, Descricao, QuantidadeMinimaUsuariosDoGrupo)
Values(1, 1, 'Supervisor', 1)
--				Passo #2
Insert Into tblRegraPasso(RegraId, Sequencia, Descricao, QuantidadeMinimaUsuariosDoGrupo)
Values(1, 2, 'Gerente ou Diretor', 1)

-- Regra #5 --
Select * From tblUsuarioGrupo

Select * From tblRegraPasso
Insert Into tblRegraPasso(RegraId, Sequencia, Descricao, QuantidadeMinimaUsuariosDoGrupo) Values(5, 1, 'Aprova��o do Coordenador', 1)
Insert Into tblRegraPasso(RegraId, Sequencia, Descricao, QuantidadeMinimaUsuariosDoGrupo) Values(5, 2, 'Aprova��o do Gerente', 1)


Select * From tblRegraPassoCondicao
Insert Into tblRegraPassoCondicao(RegraPassoId, UsuarioGrupoId, UsuarioId, QuantidadeMinimaUsuarios) Values(4, 5, null, null)
Insert Into tblRegraPassoCondicao(RegraPassoId, UsuarioGrupoId, UsuarioId, QuantidadeMinimaUsuarios) Values(5, 2, null, null)
Insert Into tblRegraPassoCondicao(RegraPassoId, UsuarioGrupoId, UsuarioId, QuantidadeMinimaUsuarios) Values(5, null, 29, null)
--------------


Passo 1) ID#3  2, 1, 'Supervisor', 1

Passo 1) ID#4  3, 1, 'Coordenador e Supervidor', 0
Passo 2) ID#5  3, 2, 'Gerente', 0

Passo 1) ID#6  3, 1, 'Coordenador ou Supervidor por�m Gerente', 2

-- 3. Cadastrar as Condi��es da Regra:
-- A condi��o exige apenas que 1 usu�rio do grupo 1 aprove a libera��o do conte�do.

Insert Into tblRegraPassoCondicao(RegraPassoId, UsuarioGrupoId, UsuarioId, QuantidadeMinimaUsuarios)
Values(2, 4, null, null)
Insert Into tblRegraPassoCondicao(RegraPassoId, UsuarioGrupoId, UsuarioId, QuantidadeMinimaUsuarios)
Values(3, 2, null, null)
Insert Into tblRegraPassoCondicao(RegraPassoId, UsuarioGrupoId, UsuarioId, QuantidadeMinimaUsuarios)
Values(3, 1, null, null)



Select * From tblUsuarioGrupo
-- insert into tblUsuarioGrupo(Nome) values('COORDENADOR')


Insert Into tblRegraAprovacaoItem(RegraAprovacaoPassoId, UsuarioId, UsuarioGrupoId, QuantidadeMinimaUsuario, Obrigatorio)
Regra #1 / Passo #1
	1, null, 2 (supervisor), 1
Regra #1 / Passo #2
	2, null, 3 (gerente), 0
	2, null, 4 (diretor), 0 


Regra #2 / Passo #1
	3, null, 2 (supervisor), 1


Regra #3 / Passo #1
    4, null, 2 (supervisor), 1
	4, null, 5 (coordenador), 1
Regra #3 / Passo #2
	5, null, 3 (gerente), 1


Regra #4 / Passo #1
    4, null, 2 (supervisor), 0
	4, null, 5 (coordenador), 0
	4, null, 3 (gerente), 1


-- ETAPA 2 ------------------------------------------------------
-- 3. Configura��o da Regra conforme Tipo de Banner:
Select * From tblBannerTipo
Select * From tblBannerRegra
Insert tblBannerRegra(BannerTipoId, RegraAprovacaoId) Values(1, 1)
-----------------------------------------------------------------


-- Relacionar os Banner Pendentes de Liberacao.
Select 
	*
From
	tblBanner
Where
	Liberado Is Null


-- Verificar quais as condi��es exigidas para a aprova��o do banner:
Select 
	RA.Privado,
	RAI.*
From 
	tblRegraAprovacao RA 
	join tblBannerRegra BR On RA.RegraAprovacaoId = BR.RegraAprovacaoId
	join tblRegraAprovacaoItem RAI On RAI.RegraAprovacaoId = RA.RegraAprovacaoId
Order By RAI.Sequencia



/********************************
Processamento das Regras
*********************************/
select * from tblRegra
select * from tblRegraPasso
select * from tblRegraPassoCondicao

select * from tblPublicacaoTipoRegra
-- Delete From tblPublicacaoTipoRegra Where PublicacaoTipoRegraId = 1

-- Insert Into tblPublicacaoTipoRegra(PublicacaoTipoId, RegraId, Privado) Values(3, 5, 1)


-- Exibir os passos de aprova��o da publica��o:
Select 
	RP.Descricao,
	RP.Sequencia,
	null 'Status'
From
	tblPublicacao P
	join tblPublicacaoTipoRegra PTR On PTR.PublicacaoTipoId = P.PublicacaoTipoId
	join tblRegraPasso RP On RP.RegraId = PTR.RegraId
Where
	P.PublicacaoId = 3074

select * from tblPublicacao where PublicacaoId = 3074


Select
	RP.RegraPassoId,
	RP.Descricao,
	--RPC.*,

	RPC.UsuarioGrupoId,
	RPC.UsuarioId,
	RPC.QuantidadeMinimaUsuarios

From	
	tblRegraPassoCondicao RPC join tblRegraPasso RP On RP.RegraPassoId = RPC.RegraPassoId

		
Where
	RP.RegraPassoId = 4



			select PAI.UsuarioId from tblPublicacaoAprovacaoItem PAI where PAI.PublicacaoId = 3074
					

					Select 
						S.*
						, RPC.QuantidadeMinimaUsuarios
						, Case When (S.TotalUsuario >= IsNull(RPC.QuantidadeMinimaUsuarios, 0)) Then 1 Else 0 End Aprovado
					From
						(
						Select 
							L.PublicacaoId PublicacaoId,
							L.RegraPassoId RegraPassoId,
							L.RegraPassoCondicaoId RegraPassoCondicaoId,
							Count(L.UsuarioId) TotalUsuario
							--,RP.QuantidadeMinimaUsuariosDoGrupo
							--,Case When L.TotalUsuario >= RP.QuantidadeMinimaUsuariosDoGrupo Then 1 Else 0 End PassoAprovado
						From
							dbo.GetRegraPublicacaoAprovacaoItem(3074) L
							join tblRegraPassoCondicao RPC On RPC.RegraPassoCondicaoId = L.RegraPassoCondicaoId
						Group By
							L.PublicacaoId,
							L.RegraPassoId,
							L.RegraPassoCondicaoId,
							RPC.QuantidadeMinimaUsuarios 
						) S
						join tblRegraPassoCondicao RPC On RPC.RegraPassoCondicaoId = S.RegraPassoCondicaoId
					




RPC.UsuarioGrupoId In (Select UsuarioGrupoId From dbo.GetUsuarioGrupo(PAI.UsuarioId))

-- Aprova��o
Insert into tblPublicacaoAprovacaoItem(PublicacaoId, UsuarioId, DataAprovacao, Liberado) Values(3074, 3, GetDate(), 1)
Insert into tblPublicacaoAprovacaoItem(PublicacaoId, UsuarioId, DataAprovacao, Liberado) Values(3074, 30, GetDate(), 1)
Insert into tblPublicacaoAprovacaoItem(PublicacaoId, UsuarioId, DataAprovacao, Liberado) Values(3074, 29, GetDate(), 1)

select * from tblUsuarioUsuarioGrupo
Insert into tblUsuarioUsuarioGrupo(UsuarioId, UsuarioGrupoId) values(30, 5)

select * from tblUsuario

-- Dom�nio de tblPublicacaoTipo --
1	Evento
2	Not�cia
3	Mat�ria
4	Artigo
5	P�gina
----------------------------------

/**************************************************
 Configura��o manual de privacidade na publica��o.
 This is just for a test. Does not necessary to put
 into a new proc. ;)
***************************************************/
select * from tblPublicacao Where PublicacaoId = 3074
Select * from tblPublicacaoRestricaoUsuarioGrupo Where PublicacaoId = 3074

Update tblPublicacao Set Privado = 1  Where PublicacaoId = 3074

Insert Into tblPublicacaoRestricaoUsuarioGrupo(PublicacaoId, UsuarioGrupoId) Values( 3074, 1)
delete from tblPublicacaoRestricaoUsuarioGrupo where publicacaoId = 3074

Update tblPublicacao Set Privado = 0 Where PublicacaoId = 3074
