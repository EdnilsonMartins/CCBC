

Select * From tblConfiguracao



Update tblConfiguracao Set HostBase = 'http://177.70.120.244:8080/'




Update tblBannerTipo Set Descricao = 'Home' Where BannerTipoId = 1 
Update tblBannerTipo Set Descricao = 'Lateral' Where BannerTipoId = 2 
Update tblBannerTipo Set Descricao = 'Home Mantenedores' Where BannerTipoId = 3 
Update tblBannerTipo Set Descricao = 'Home Parceiros' Where BannerTipoId = 4 
Update tblBannerTipo Set Descricao = 'Home Inferior' Where BannerTipoId = 5 
Update tblBannerTipo Set Descricao = 'Home Inferior Lateral' Where BannerTipoId = 6 

Insert Into tblBannerTipo(BannerTipoId, Descricao) Values(7, 'Interna Superior')

