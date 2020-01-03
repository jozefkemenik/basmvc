-- =============================================
-- Author:		Mgr. Michal Deák
-- Create date: 20140602
-- Description:	Function to get word from translation
-- select * from fn_GetTranslationWord(1031,1,'CCE3D14B-D86E-4E26-83DA-9ED58EEFDB28')
-- =============================================
CREATE FUNCTION [dbo].[fn_GetDefaultLanguage]
(	
)
RETURNS  int
AS
BEGIN
	declare @ResultVar nvarchar(max)
	select top 1 @ResultVar = ID from [Language] a
	where a.IsDefault = 1	
	return @ResultVar
END
