USE [dbCCBC]
GO
/****** Object:  StoredProcedure [dbo].[USP_SEL_Arquivo]    Script Date: 11/03/2015 10:59:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER Procedure [dbo].[USP_SEL_Arquivo](
	@ArquivoId	Int = Null
) As
Begin

	Select
		A.ArquivoId,
		A.Content,
		A.Legenda,
		dbo.fnRetornaArquivoCategoria(A.ArquivoId) ListaCategoria,
		A.Tipo
		--Convert(VarChar(max), content) Content64
	From
		tblArquivo A
	Where
		((@ArquivoId Is Null) Or (ArquivoId = @ArquivoId))

End
