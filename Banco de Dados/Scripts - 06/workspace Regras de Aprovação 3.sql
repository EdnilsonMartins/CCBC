
select * from tblRegra
select * from tblRegraPasso
select * from tblRegraPassoCondicao

Update tblRegraPasso Set QuantidadeMinimaUsuariosDoGrupo = 2 Where RegraPassoId = 4


Select * From dbo.GetRegraPassoResultado(3074)



Select * From tblUsuarioUsuarioGrupo where UsuarioGrupoId In (2, 5)
U / G
1	2
2	2
3	5
30	5



-- Adicionar APROVAÇÃO
[dbo].[USP_INS_PublicacaoAprovacaoItem] 3074, 29, 1
Exec [dbo].[USP_INS_Publicacao_Liberacao] 3074

-- Remover APROVAÇÃO
[dbo].[USP_DEL_PublicacaoAprovacaoItem] 3074, 29
Exec [dbo].[USP_INS_Publicacao_Liberacao] 3077

-- Passos e Resultados
Select Liberado, * From tblPublicacao Where PublicacaoId = 3077
Exec [dbo].[USP_SEL_PublicacaoRegraPasso] 3077


USP_SEL_PublicacaoAprovacaoHistorico 3074


select * from tblPublicacaoAprovacaoItem
select * from tblRegraPassoCondicao

select * from tblUsuarioUsuarioGrupo
Update tblUsuario Set Senha = '8D38D5005D4CE7B8018ADDF1552C03E7' 

Select * from tblPublicacaoAprovacaoItem Where PublicacaoId = 3077

--Delete from tblPublicacaoAprovacaoItem Where PublicacaoId = 3077
--Delete From tblPublicacaoAprovacaoHistorico

Select * From tblUsuario

Select * From tblPublicacaoRestricaoUsuarioGrupo Where PublicacaoId = 3078

select dbo.fnRetornaUsuarioGrupo(3078)