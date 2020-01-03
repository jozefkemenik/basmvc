-- =============================================
-- Author:        Michal Deák
-- Create date: 05.05.2014
-- Description:   Procedure for selecting datasets for autocompleete
/*
exec [Identity_GetAutocompleete] 's. carlo',1033
*/
-- =============================================
create PROCEDURE [dbo].[Identity_GetAutocompleete]
@word nvarchar(128) = null,
@identityType TinyInt = null,
@isMapped  bit =null
      AS
BEGIN
      SET NOCOUNT ON;

DECLARE @wordtrim nvarchar(128);
SET @wordtrim = LTRIM(RTRIM(@word));

SELECT top 5  tmp.idIdent as Category, tmp.[ID], tmp.Name from (
  SELECT top 5 a.[ID],
              'Doctor' as idIdent,
              CompanyName as Name,
              CompanyName as Surname,
              a.IdentityType
              
              FROM [dbo].Doctor a
              WHERE [dbo].[fn_PocitanieSlova](@wordtrim,a.CompanyName)>0
				and a.IsActive = 1
				 and a.IsMapped = isnull(@isMapped,a.IsMapped )
				 and a.IdentityType  = isnull(@identityType,a.IdentityType )
      UNION ALL
  SELECT  top 5 a.[ID],
              'Provider'  as idIdent,
              CompanyName as Name,
              CompanyName as Surname,
              a.IdentityType
              FROM [dbo].Provider a
              WHERE [dbo].[fn_PocitanieSlova](@wordtrim,a.CompanyName)>0
              and a.IsActive = 1
			  and a.IsMapped = isnull(@isMapped,a.IsMapped )
			  and a.IdentityType  = isnull(@identityType,a.IdentityType )

      UNION ALL

    SELECT top 5 a.[ID] as ID,
              'Doctor' as idIdent,
              a.[Name]+' '+a.Surname as Name,
              Surname,
              a.IdentityType
              FROM [dbo].Doctor a
              WHERE [dbo].[fn_PocitanieSlova](@wordtrim,a.Surname)>0
              and a.[ID] not in (
              SELECT  a.[ID]
              FROM [dbo].Doctor a
              WHERE [dbo].[fn_PocitanieSlova](@wordtrim,a.CompanyName)>0)
              and a.IsActive = 1
			  and a.IsMapped = isnull(@isMapped,a.IsMapped )
			  and a.IdentityType  = isnull(@identityType,a.IdentityType )
              ) tmp 
              
END