
-- =============================================
-- Author:        Jozef Kemenik
-- Create date: 02.07.2014
-- Description:   
-- =============================================
CREATE PROCEDURE [dbo].[MainSearch_GetWhere]
@word nvarchar(128) = null,
@idLanguage  int =1033
      AS
BEGIN
      SET NOCOUNT ON;

DECLARE @wordtrim nvarchar(128);
SET @wordtrim = LTRIM(RTRIM(@word));

SELECT  DISTINCT tmp.Name,tmp.idIdent as Category, ID  from (


  SELECT      a.ID_Country as ID,
			  'Cities' as idIdent,
              City as Name
              FROM [dbo].Doctor a
              WHERE [dbo].[fn_PocitanieSlova](@wordtrim,a.City)>0

      UNION ALL
  SELECT      a.ID_Country as ID,
			  'Cities' as idIdent,
              City as Name
              FROM [dbo].Provider a
              WHERE [dbo].[fn_PocitanieSlova](@wordtrim,a.City)>0

      UNION ALL
      
  SELECT ci.[ID] as ID, 
      'Countries' as idIdent,
      ci.name as Name
    
  FROM [dbo].[CountryInfo] ci
  left join [dbo].Doctor d on d.ID_Country = ci.ID
  left join [dbo].Provider p on p.ID_Country = ci.ID
   
   WHERE [dbo].[fn_PocitanieSlova](@wordtrim,ci.name)>0
   and ((d.ID_Country is not NULL) or (p.ID_Country is not NULL))
   
  )
  tmp order by Category,tmp.Name
 
END