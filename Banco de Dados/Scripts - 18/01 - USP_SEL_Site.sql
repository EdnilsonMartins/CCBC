/****************************************************************************
* Banco		: dbCCBC														*
* Procedure : USP_SEL_Site													*
* Objetivo  : Carga de dados / Listagem dos Sites.							*
*                                                                           *
* Alterada por	: Ednilson Martins	                                        *
* Data Cria��o	:   /06/2015												*
* Descri��o	    : 															*
****************************************************************************/

Create Procedure [dbo].[USP_SEL_Site](
	@SiteId			Int = Null,
	@Descricao		VarChar(80) = Null
) As

Begin
	
	Select
		S.SiteId,
		S.Descricao,
		S.Tags
	From
		tblSite S
	Where
		@SiteId Is Null Or S.SiteId = @SiteId
End
