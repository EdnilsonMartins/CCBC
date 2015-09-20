Create Procedure USP_SEL_Mailing(
	@MailingId		Int = Null,
	@SiteId			Int = Null
)
As
Begin

	If (@MailingId Is Not Null) Begin
		Select
			MailingId, SiteId, Nome, Email, Segmento, DataInclusao, Ativo
		From
			tblMailing
		Where
			MailingId = @MailingId
	End Else Begin
		Select
			MailingId, SiteId, Nome, Email, Segmento, DataInclusao, Ativo
		From
			tblMailing
		Where
			SiteId = @SiteId
	End

End