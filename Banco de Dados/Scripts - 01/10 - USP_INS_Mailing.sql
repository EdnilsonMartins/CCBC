Create Procedure USP_INS_Mailing(
	@MailingId		Int = 0,
	@SiteId			Int, 
	@Nome			VarChar(100), 
	@Email			VarChar(100), 
	@Segmento		VarChar(50) = null, 
	@Ativo			Bit = null
)
As
Begin

	If (@MailingId = 0) Begin
		
		declare @id int
		Select @id = MailingId From tblMailing Where Email = @Email And SiteId = @SiteId
		if IsNull(@id, 0) > 0 Begin
			Set @MailingId = @id

			Update tblMailing
			Set Ativo = @Ativo,
				Nome = @Nome
			Where MailingId = @MailingId

		End Else Begin
			Insert Into tblMailing(SiteId, Nome, Email, Segmento, Ativo)
			Values(@SiteId, @Nome, @Email, @Segmento, 1)

			Select @MailingId = @@IDENTITY
		End
	End Else Begin

		

		Update tblMailing
		Set Ativo = @Ativo
		Where MailingId = @MailingId

	End

	Select @MailingId MailingId

End