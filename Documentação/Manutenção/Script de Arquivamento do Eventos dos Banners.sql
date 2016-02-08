


select count(1) from tblBannerevento Where DataEvento < '2016-01-15' order by dataevento desc
361884

select top 10 * from tblBannerevento_antigos order by dataevento desc

select count(1) from tblBannerevento_antigos order by dataevento desc
3104707
3466591


-- Insere os eventos na tabela de Backup
Insert Into tblBannerEvento_Antigos(BannerEventoId, BannerEventoTipoId,BannerId,ArquivoId,DataEvento)
Select BannerEventoId, BannerEventoTipoId,BannerId,ArquivoId,DataEvento From tblBannerevento Where DataEvento < '2016-01-15'

-- Exclui os eventos da tabela de Producao
Delete From tblBannerevento Where DataEvento < '2016-01-15' 