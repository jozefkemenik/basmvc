-- =============================================
-- Author:        Michal Deák
-- Create date: 05.05.2014
-- Description:   Procedure for selecting datasets for autocompleete
/*
exec [Discipline_GetAutocompleete] @Word=N't',@ID_Language=1033
*/
-- =============================================
CREATE PROCEDURE [dbo].[Discipline_GetAutocompleete]
@Word nvarchar(128) = null,
@ID_Language  int =1033
      AS
BEGIN
      SET NOCOUNT ON;

DECLARE @wordtrim nvarchar(128);
SET @wordtrim = LTRIM(RTRIM(@word));


SELECT 	TOP 10	[ID],
			dbo.fn_GetTranslationWord(@ID_Language,1,a.ID_Translation) as Name  
              FROM Discipline (nolock) a
              WHERE [dbo].[fn_PocitanieSlova](@wordtrim,dbo.fn_GetTranslationWord(@ID_Language,1,a.ID_Translation))>0 --and ID_Language=1031--@idLanguage


END
