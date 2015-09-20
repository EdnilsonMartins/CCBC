select * from tblMenu
select * from tblBanner
select * from tblConfiguracao

update tblMenu Set LinkURL = Replace(LinkURL, 'http://177.70.120.244:8080/', 'http://177.70.120.244/')
update tblBanner Set LinkURL = Replace(LinkURL, 'http://177.70.120.244:8080/', 'http://177.70.120.244/')
update tblConfiguracao Set HostBase = Replace(HostBase, 'http://177.70.120.244:8080/', 'http://177.70.120.244/')
