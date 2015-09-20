
-- Tabelas de domínio:
Select * From tblRegraTipo
1	Banner
2	Evento
3	Matéria

Select * From tblRegraAprovacaoCondicao
1	Uma das condições
2	Todas das condições
---------------------------------------

Select * From tblRegraAprovacao
Select * From tblRegraAprovacaoItem

-- ETAPA 1 ------------------------------------------------------
-- 1. Cadastrar a Regra:
-- O campo privado define se a regra será aplicada a conteúdo privado ou público.
-- Exemplo: A regra abaixo é do tipo Banner, onde todas as condições deverão ser satizfeitas e será aplicada para conteúdo privado.
--		    Regra 1 - Aprovação de Banner da Home


-- ID#1
Insert Into tblRegra(RegraTipoId, Descricao)
Values(1, 'Aprovação de Banner da Home (Privado)')
	 
-- ID#2
Insert Into tblRegra(RegraTipoId, Descricao)
Values(1, 'Aprovação de Banner da Home (Público)')
	 
-- ID#3
Insert Into tblRegra(RegraTipoId, Descricao)
Values(1, 'Aprovação de uma Notícia (Privado)')


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
Insert Into tblRegraPasso(RegraId, Sequencia, Descricao, QuantidadeMinimaUsuariosDoGrupo) Values(5, 1, 'Aprovação do Coordenador', 1)
Insert Into tblRegraPasso(RegraId, Sequencia, Descricao, QuantidadeMinimaUsuariosDoGrupo) Values(5, 2, 'Aprovação do Gerente', 1)


Select * From tblRegraPassoCondicao
Insert Into tblRegraPassoCondicao(RegraPassoId, UsuarioGrupoId, UsuarioId, QuantidadeMinimaUsuarios) Values(4, 5, null, null)
Insert Into tblRegraPassoCondicao(RegraPassoId, UsuarioGrupoId, UsuarioId, QuantidadeMinimaUsuarios) Values(5, 2, null, null)
Insert Into tblRegraPassoCondicao(RegraPassoId, UsuarioGrupoId, UsuarioId, QuantidadeMinimaUsuarios) Values(5, null, 29, null)
--------------


Passo 1) ID#3  2, 1, 'Supervisor', 1

Passo 1) ID#4  3, 1, 'Coordenador e Supervidor', 0
Passo 2) ID#5  3, 2, 'Gerente', 0

Passo 1) ID#6  3, 1, 'Coordenador ou Supervidor porém Gerente', 2

-- 3. Cadastrar as Condições da Regra:
-- A condição exige apenas que 1 usuário do grupo 1 aprove a liberação do conteúdo.

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
-- 3. Configuração da Regra conforme Tipo de Banner:
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


-- Verificar quais as condições exigidas para a aprovação do banner:
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


-- Exibir os passos de aprovação da publicação:
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

-- Aprovação
Insert into tblPublicacaoAprovacaoItem(PublicacaoId, UsuarioId, DataAprovacao, Liberado) Values(3074, 3, GetDate(), 1)
Insert into tblPublicacaoAprovacaoItem(PublicacaoId, UsuarioId, DataAprovacao, Liberado) Values(3074, 30, GetDate(), 1)
Insert into tblPublicacaoAprovacaoItem(PublicacaoId, UsuarioId, DataAprovacao, Liberado) Values(3074, 29, GetDate(), 1)

select * from tblUsuarioUsuarioGrupo
Insert into tblUsuarioUsuarioGrupo(UsuarioId, UsuarioGrupoId) values(30, 5)

select * from tblUsuario

-- Domínio de tblPublicacaoTipo --
1	Evento
2	Notícia
3	Matéria
4	Artigo
5	Página
----------------------------------

/**************************************************
 Configuração manual de privacidade na publicação.
 This is just for a test. Does not necessary to put
 into a new proc. ;)
***************************************************/
select * from tblPublicacao Where PublicacaoId = 3074
Select * from tblPublicacaoRestricaoUsuarioGrupo Where PublicacaoId = 3074

Update tblPublicacao Set Privado = 1  Where PublicacaoId = 3074

Insert Into tblPublicacaoRestricaoUsuarioGrupo(PublicacaoId, UsuarioGrupoId) Values( 3074, 1)
delete from tblPublicacaoRestricaoUsuarioGrupo where publicacaoId = 3074

Update tblPublicacao Set Privado = 0 Where PublicacaoId = 3074
