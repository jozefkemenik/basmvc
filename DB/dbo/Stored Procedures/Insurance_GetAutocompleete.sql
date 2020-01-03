-- =============================================
-- Author:        Michal Deák
-- Create date: 05.05.2014
-- Description:   Procedure for selecting datasets for autocompleete
/*
exec [Insurance_GetAutocompleete] 's. carlo',1033
*/
-- =============================================
CREATE PROCEDURE [dbo].[Insurance_GetAutocompleete]
@word nvarchar(128) = null,
@ID_Language  int =1033
      AS
BEGIN
      SET NOCOUNT ON;

DECLARE @wordtrim nvarchar(128);
SET @wordtrim = LTRIM(RTRIM(@word));


SELECT 	TOP 10	[ID],
				CompanyName  
              FROM Insurance (nolock) a
              WHERE [dbo].[fn_PocitanieSlova](@wordtrim,CompanyName)>0 --and ID_Language=1031--@idLanguage 
END

