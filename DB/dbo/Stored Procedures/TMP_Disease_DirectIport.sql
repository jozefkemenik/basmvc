-- =============================================
-- Author:		Michal Deák
-- Create date: 05.05.2014
-- Description:	Procedure for selecting Language
/* Tests
-----------------------------------------
exec [dbo].[TMP_Disease_DirectIport]
*/
-- =============================================
CREATE PROCEDURE [dbo].[TMP_Disease_DirectIport]
	AS
BEGIN
	SET NOCOUNT ON;

declare 
@ID int, 
@Name nvarchar(512), 
@Description nvarchar(max), 
@ID_Language int,
@schemaId int,
@ID_Trans uniqueidentifier,
@ttID int,
@type nvarchar;

set @type = 'Disease';

DECLARE ImpCurs CURSOR LOCAL  READ_ONLY
FOR SELECT [ID]
      ,[Name]
      ,[Description]
      ,[ID_Language]     
FROM [dbo].[TMP_Disease_DirectImport] 
where IsAdded = 0;

begin transaction import 

if not exists (select 1 from TranslationSchema where Name = @type)
BEGIN
insert into TranslationSchema (Name)
select @type
set @schemaId = SCOPE_IDENTITY()
END
ELSE
BEGIN
	Select @schemaId = ID from TranslationSchema where Name =@type
END
--select @schemaId
IF(@@ERROR > 0 or @schemaId is null)
GOTO Fail;
	
OPEN ImpCurs

FETCH NEXT FROM ImpCurs 
INTO @ID, @Name, @Description, @ID_Language
--select @ID, @Name, @Description, @ID_Language
WHILE @@FETCH_STATUS = 0
BEGIN	
	set @ID_Trans = NEWID()
	--select @ID, @Name, @Description, @ID_Language,@ID_Trans
	
	if exists (Select 1 from [dbo].[TMP_Disease_DirectImport] 
	where @ID = ID and IsAdded = 1 and ID_Imported is not null)
	BEGIN
		set @ttID = (Select ID_Imported from [dbo].[TMP_Disease_DirectImport] 
	where ID = @ID )

	END	
	BEGIN
		if(@ttID is null)
		BEGIN		
			insert into TranslationGroup (ID,ID_TranslationSchema)
			select @ID_Trans, @schemaId
			IF(@@ERROR > 0)
			GOTO Fail;		
		
			insert into Disease (IsActive,ID_Translation)
			select 1, @ID_Trans
			set @ttID = SCOPE_IDENTITY();
			IF(@@ERROR > 0 or @ttID is null)
			GOTO Fail;
		END	
		ELSE
		BEGIN
			Select @ID_Trans = a.ID_Translation from Disease a where a.ID = @ttID
			IF(@@ERROR > 0 or @ID_Trans is null)
			GOTO Fail;		
		END
		
		
		insert into TranslationValue (ID_Type,ID_TranslationGroup, ID_Language,Value)
		select 1,@ID_Trans,@ID_Language, @Name
		IF(@@ERROR > 0)
		GOTO Fail;
		
		if(@Description is not null)
		BEGIN
		
		insert into TranslationValue (ID_Type,ID_TranslationGroup, ID_Language,Value)
		select 2, @ID_Trans,@ID_Language, @Description
		IF(@@ERROR > 0)
		GOTO Fail;
		
		END
	END	
	
	update [TMP_Disease_DirectImport]
	set IsAdded =1,
		ID_Imported = @ttID
	where @ID = ID
	IF(@@ERROR > 0)
		GOTO Fail;
	
	
	set @ttID = null;
	set @ID_Trans = null;
	
    FETCH NEXT FROM ImpCurs 
	INTO @ID, @Name, @Description, @ID_Language
	IF(@@ERROR > 0)
		GOTO Fail;
END 
CLOSE ImpCurs;
DEALLOCATE ImpCurs;

Commit Transaction import;
return 0;

Fail:
rollback Transaction import;
return @@error;
END