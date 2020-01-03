-- =============================================
-- Author:		Mgr. Michal Deák
-- Create date: 20140602
-- Description:	Function to get word from translation
-- select * from fn_GetTranslationWord(1031,1,'CCE3D14B-D86E-4E26-83DA-9ED58EEFDB28')
-- =============================================
CREATE FUNCTION fn_GetTranslationWord
(
	@LangID  int, -- language id
	@TransValType tinyint, --type of the value (Name, Description i.e.)
	@TranslGroup uniqueidentifier -- row group id
)
RETURNS  nvarchar(max)
AS
BEGIN
	declare @ResultVar nvarchar(max)
	select @ResultVar = a.Value from TranslationValue a
	where a.ID_Language = @LangID
	and a.ID_TranslationGroup = @TranslGroup
	and a.ID_Type = @TransValType
	return @ResultVar
END
