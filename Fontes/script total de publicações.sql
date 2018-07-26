

Use cdbc_prod

-- 1. Total
Select
count(1) Total
From 
	tblPublicacao p
	join tblSite s on s.siteid = p.siteid
	join tblPublicacaoTipo pt on pt.publicacaotipoid = p.publicacaotipoid
where
	p.siteid in (1,2)



-- 2. Total por Site
Select
p.siteid,
s.descricao,
count(1) Total
From 
	tblPublicacao p
	join tblSite s on s.siteid = p.siteid
	join tblPublicacaoTipo pt on pt.publicacaotipoid = p.publicacaotipoid
where
	p.siteid in (1,2)
group by
	p.SiteId,
s.descricao

order by SiteId
-- 3. Por Tipo de Publicaçao
Select
p.SiteId,
s.descricao,
--pt.publicacaotipoid,
pt.descricao,
count(1) Total_Publicacoes
From 
	tblPublicacao p
	join tblSite s on s.siteid = p.siteid
	join tblPublicacaoTipo pt on pt.publicacaotipoid = p.publicacaotipoid
where
	p.siteid in (1,2)
group by
	p.SiteId,
s.descricao,
--pt.publicacaotipoid,
pt.descricao

order by SiteId