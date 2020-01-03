-- =============================================
-- Author:        Michal Deák
-- Create date: 05.05.2014
-- Description:   Procedure for selecting datasets for autocompleete
/*
exec [MainSearch_GetAutocompleete] 's. carlo',1033
*/
-- =============================================
CREATE PROCEDURE [dbo].[MainSearch_GetAutocompleete]
@word nvarchar(128) = null,
@idLanguage  int =1033
      AS
BEGIN
      SET NOCOUNT ON;

DECLARE @wordtrim nvarchar(128);
SET @wordtrim = LTRIM(RTRIM(@word));

SELECT top 5 'name' as Category, tmp.[ID],tmp.Name from (
  SELECT top 5 a.[ID],
              1 as idIdent,
              CompanyName as Name,
              CompanyName as Surname
              FROM [dbo].Doctor a
              WHERE [dbo].[fn_PocitanieSlova](@wordtrim,a.CompanyName)>0
				and a.IsActive = 1
      UNION ALL
  SELECT  top 5 a.[ID],
              1 as idIdent,
              CompanyName as Name,
              CompanyName as Surname
              FROM [dbo].Provider a
              WHERE [dbo].[fn_PocitanieSlova](@wordtrim,a.CompanyName)>0
              and a.IsActive = 1

      UNION ALL

    SELECT top 5 a.[ID] as ID,
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
UNION ALL
SELECT top 5 'branch' as Category, 
			[ID],
			dbo.fn_GetTranslationWord(@idLanguage,1,a.ID_Translation) as Name  
              FROM Discipline a
              WHERE [dbo].[fn_PocitanieSlova](@wordtrim,dbo.fn_GetTranslationWord(@idLanguage,1,a.ID_Translation))>0 --and ID_Language=1031--@idLanguage
              --ORDER BY dbo.fn_GetTranslationWord(@idLanguage,1,a.ID_Translation) )
UNION ALL
SELECT top 5 'method' as Category, 
			[ID],
			dbo.fn_GetTranslationWord(@idLanguage,1,a.ID_Translation) as Name  
              FROM TreatmentMethods a
              WHERE [dbo].[fn_PocitanieSlova](@wordtrim,dbo.fn_GetTranslationWord(@idLanguage,1,a.ID_Translation))>0 --and ID_Language=1031--@idLanguage
       --       ORDER BY dbo.fn_GetTranslationWord(@idLanguage,1,a.ID_Translation)
   UNION ALL
SELECT top 5 'desease' as Category, 
			[ID],
			dbo.fn_GetTranslationWord(@idLanguage,1,a.ID_Translation) as Name
              FROM Disease a
              WHERE [dbo].[fn_PocitanieSlova](@wordtrim, dbo.fn_GetTranslationWord(@idLanguage,1,a.ID_Translation))>0 --and ID_Language=1031--@idLanguage
              --ORDER BY dbo.fn_GetTranslationWord(@idLanguage,1,a.ID_Translation)
 
END

