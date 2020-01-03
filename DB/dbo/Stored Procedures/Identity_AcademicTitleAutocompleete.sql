-- =============================================
-- Author:        Michal Deák
-- Create date:  06/23/2014 13:19:05
-- Description:   Procedure for selecting AcademicTitle for autocompleete
/*
exec [Identity_AcademicTitleAutocompleete] 'd',1031
*/
-- =============================================
CREATE PROCEDURE [dbo].[Identity_AcademicTitleAutocompleete]
@word nvarchar(128) = null,
@idLanguage  int =1033
 AS
BEGIN
      SET NOCOUNT ON;

DECLARE @wordtrim nvarchar(128);
SET @wordtrim = LTRIM(RTRIM(@word));

  SELECT TOP 10 a.[ID],
              a.Title,
              a.ID_Language
              FROM AcademicTitle a
              WHERE [dbo].[fn_PocitanieSlova](@wordtrim,a.Title)>0
				and a.ID_Language in( @idLanguage,127)
END 