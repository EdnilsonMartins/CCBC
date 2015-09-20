
/****** Object:  StoredProcedure [dbo].[USP_INS_Banner]    Script Date: 03/05/2015 09:13:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_INS_BannerClique											*
* Objetivo  : Registro de clique do Banner.									*
*                                                                           *
* Criada por	: Ednilson Martins	                                        *
* Data Criação	: 03/05/2015	                                            *
*                                                                           *
* Alterada por	: 															*
* Data Criação	:															*
* Descrição	    :															*
*																			*
****************************************************************************/

Create Procedure [dbo].[USP_INS_BannerEvento](
	@BannerEventoTipoId Int,
	@BannerId			Int,
	@ArquivoId			BigInt
)
As
Begin

	If @BannerId <> 0 Begin
		Insert Into tblBannerEvento(BannerEventoTipoId, BannerId, ArquivoId, DataEvento) Values(@BannerEventoTipoId, @BannerId, @ArquivoId, GetDate())
	End

End