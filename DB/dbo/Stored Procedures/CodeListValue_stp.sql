CREATE PROCEDURE [dbo].[CodeListValue_stp]
			@UserId					INT = NULL,
			@LangCodeId				INT,
			@CodeListId				INT,
			@Published				INT = 1
	AS
BEGIN

-- UserId is skipped yet - dont have information how to use it

	SELECT main_t.Id, main_t.ParentId, main_t.NewEntry, trans_t.Id AS TransId, ISNULL(trans_t.Name, 'Not Provided')
	FROM dbo.CodeListValue_t main_t
	LEFT JOIN dbo.CodeListTranslation_t trans_t ON main_t.Id = trans_t.ListValueId
	WHERE trans_t.LangCodeId = @LangCodeId 
	AND main_t.ListId = @CodeListId
	AND main_t.Published = @Published
	AND main_t.Active = 1;


END