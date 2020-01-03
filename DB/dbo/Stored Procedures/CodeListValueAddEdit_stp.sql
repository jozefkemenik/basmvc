CREATE PROCEDURE [dbo].[CodeListValueAddEdit_stp]
			@UserId					INT,
			@LangCodeId				INT,
			@Name					NVARCHAR(255),
			@CodeListId				INT,
			@ParentId				INT = NULL,
			@Id						INT
	AS
BEGIN

BEGIN TRANSACTION

IF(@Id < 1)
BEGIN
	INSERT INTO dbo.CodeListValue_t ( Published,  CreatedBy, NewEntry, ListId, ParentId, Name )
	SELECT 0, @UserId, 1, @CodeListId, @ParentId,  @Name;

	SELECT @Id = SCOPE_IDENTITY();
END

DECLARE @TranslatedId int = (SELECT TOP 1 Id FROM CodeListTranslation_t t WHERE t.LangCodeId = @LangCodeId AND t.ListValueId = @Id);

IF(@TranslatedId is NULL)
BEGIN
	INSERT INTO  CodeListTranslation_t( Published, CreatedBy, NewEntry, ListValueId, LangCodeId, Name)
	SELECT	 0, @UserId, 1, @Id, @LangCodeId, @Name
END
ELSE
BEGIN
	UPDATE CodeListTranslation_t SET Name = @Name WHERE Id = @Id;
END 

SELECT @Id as Id;

IF(@@ERROR > 0)
		ROLLBACK TRANSACTION
	ELSE
		COMMIT TRANSACTION


END