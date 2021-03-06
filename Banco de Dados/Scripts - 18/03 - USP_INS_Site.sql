USE [dbCCBC]
GO
/****** Object:  StoredProcedure [dbo].[USP_INS_Regra]    Script Date: 05/06/2015 10:44:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_INS_Site													*
* Objetivo  : Manutenção dos sites.											*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Criação	:   /06/2015												*
* Descrição	    :															*
****************************************************************************/

Create Procedure [dbo].[USP_INS_Site](
	@SiteId				Int,
	@Descricao			VarChar(80) = Null,
	@Tags				VarChar(4000) = Null
)
As
Begin

	If @SiteId <> 0 Begin
		Update 
			tblSite
		Set 
			Descricao = @Descricao,
			Tags = @Tags
		Where 
			SiteId = @SiteId
		Select @SiteId SiteId
	End Else Begin
		Insert Into tblSite(Descricao, Tags) 
		Values(@Descricao, @Tags)
		Select Cast(@@Identity As int) SiteId
	End

End