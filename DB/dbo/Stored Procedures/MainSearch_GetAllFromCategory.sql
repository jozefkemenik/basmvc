CREATE PROCEDURE [dbo].[MainSearch_GetAllFromCategory]
@category nvarchar(128) = null,
@word nvarchar(128) = null,
@idLanguage  int =1033 
      AS
BEGIN
      SET NOCOUNT ON;

DECLARE @wordtrim nvarchar(128);
SET @wordtrim = LTRIM(RTRIM(@word));

If @category = 'name'
    Begin
    	SELECT  'name' as Category, tmp.[ID],tmp.Name from (
			SELECT  a.[ID],
              1 as idIdent,
              CompanyName as Name,
              CompanyName as Surname
              FROM [dbo].Doctor a
              WHERE [dbo].[fn_PocitanieSlova](@wordtrim,a.CompanyName)>0
				and a.IsActive = 1
			UNION ALL
			SELECT   a.[ID],
              1 as idIdent,
              CompanyName as Name,
              CompanyName as Surname
              FROM [dbo].Provider a
              WHERE [dbo].[fn_PocitanieSlova](@wordtrim,a.CompanyName)>0
              and a.IsActive = 1

			UNION ALL

			 SELECT a.[ID] as ID,
              2 as idIdent,
              a.[Name]+' '+a.Surname as Name,
              Surname
              FROM [dbo].Doctor a
              WHERE [dbo].[fn_PocitanieSlova](@wordtrim,a.Surname)>0
              and a.[ID] not in (
              SELECT  a.[ID]
              FROM [dbo].Doctor a
              WHERE [dbo].[fn_PocitanieSlova](@wordtrim,a.CompanyName)>0)
              and a.IsActive = 1
              ) tmp
    End
	Else If @category = 'branch'
    Begin
    	SELECT  'branch' as Category, 
			[ID],
			dbo.fn_GetTranslationWord(@idLanguage,1,a.ID_Translation) as Name  
              FROM Discipline a
              WHERE [dbo].[fn_PocitanieSlova](@wordtrim,dbo.fn_GetTranslationWord(@idLanguage,1,a.ID_Translation))>0 --and ID_Language=1031--@idLanguage
              --ORDER BY dbo.fn_GetTranslationWord(@idLanguage,1,a.ID_Translation) )
    End
    Else If @category = 'method'
    Begin
    	SELECT  'method' as Category, 
			[ID],
			dbo.fn_GetTranslationWord(@idLanguage,1,a.ID_Translation) as Name  
              FROM TreatmentMethods a
              WHERE [dbo].[fn_PocitanieSlova](@wordtrim,dbo.fn_GetTranslationWord(@idLanguage,1,a.ID_Translation))>0 --and ID_Language=1031--@idLanguage
       --       ORDER BY dbo.fn_GetTranslationWord(@idLanguage,1,a.ID_Translation)
    End
    
    Else If @category = 'desease'
    Begin
    	SELECT 'desease' as Category, 
			[ID],
			dbo.fn_GetTranslationWord(@idLanguage,1,a.ID_Translation) as Name
              FROM Disease a
              WHERE [dbo].[fn_PocitanieSlova](@wordtrim, dbo.fn_GetTranslationWord(@idLanguage,1,a.ID_Translation))>0 --and ID_Language=1031--@idLanguage
    End

END
