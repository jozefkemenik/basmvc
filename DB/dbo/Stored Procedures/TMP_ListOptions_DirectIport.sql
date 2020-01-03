-- =============================================
-- Author:		Michal Deák
-- Create date: 05.05.2014
-- Description:	Procedure for selecting Language
/* Tests
-----------------------------------------
exec [dbo].[TMP_ListOptions_DirectIport] 'test1'
*/
-- =============================================
CREATE PROCEDURE [dbo].[TMP_ListOptions_DirectIport]
@FileName nvarchar(max)= null
	AS
BEGIN
	SET NOCOUNT ON;

declare 
@ID int, 
@ID_Parent int,
@ID_ParentAdd int,
@ID_List int,
@Name nvarchar(512), 
@ListName nvarchar(max), 
@Text nvarchar(max),
@ID_Language int,
@schemaId int,
@ID_Trans uniqueidentifier,
@ttID int,
@type nvarchar,
@ID_ImportFile uniqueidentifier;

if @FileName is not null
BEGIN
	set @ID_ImportFile = NEWID();
	Insert  into ImportedFile (ID,FileName,ID_ImportType)
	Select @ID_ImportFile,@FileName,4  /*ID_ImportType 4=Lists*/
END

set @type = 'ListsOptions';


with TMP (ListName) as (select ListName from TMP_ListsOptions_DirectImport group by ListName)
MERGE	Lists b
		USING	TMP  a
		ON	a.ListName =b.KeyName			
		WHEN NOT MATCHED BY TARGET THEN						
			INSERT (KeyName ,IsActive)
     		VALUES	(a.ListName,1);


DECLARE ImpCurs CURSOR LOCAL  READ_ONLY
FOR SELECT a.ID 
      ,a.ID_Parent
       ,a.ID_Lang  
     ,a.ListName,
     a.Text      
FROM [dbo].[TMP_ListsOptions_DirectImport] a
where IsAdded = 0;

begin transaction import 

if not exists (select 1 from TranslationSchema (nolock) where Name = @type)
BEGIN
insert into TranslationSchema (Name)
select @type
set @schemaId = SCOPE_IDENTITY()
END
ELSE
BEGIN
	Select @schemaId = ID from TranslationSchema  (nolock) where Name =@type
END
--select @schemaId
IF(@@ERROR > 0 or @schemaId is null)
GOTO Fail;
	
OPEN ImpCurs

FETCH NEXT FROM ImpCurs 
INTO @ID, @ID_Parent, @ID_Language, @ListName, @Name

select @ID, @ID_Parent, @ID_Language,@ListName, @Name
WHILE @@FETCH_STATUS = 0
BEGIN	
	set @ID_Trans = NEWID()	
	select @ID_List = ID from Lists  (nolock) where KeyName = @ListName
	
	select @ID_ParentAdd = ID_Imported from [TMP_ListsOptions_DirectImport] 	
	where ID = isnull(@ID_Parent,null)
	
	--Select 'Is Find :',* from [dbo].[TMP_ListsOptions_DirectImport] (nolock) 
	--where  ID = @ID and IsAdded = 1 and ID_Imported is not null
	
	if exists (Select 1 from [dbo].[TMP_ListsOptions_DirectImport] (nolock) 
	where  ID = @ID and IsAdded = 1 and ID_Imported is not null)
	BEGIN
		Select  @ttID = ID_Imported from [dbo].[TMP_ListsOptions_DirectImport] 
		where ID = @ID and IsAdded = 1 and ID_Imported is not null
		IF(@ID_List is null)
				GOTO Fail;	
	END	

	BEGIN
		if(@ttID is null)
		BEGIN		
			insert into TranslationGroup (ID,ID_TranslationSchema)
			select @ID_Trans, @schemaId
			IF(@@ERROR > 0)
			GOTO Fail;		
		
			insert into Options (ID_Translation,ID_Parent,ID_List)
			select @ID_Trans,@ID_ParentAdd,@ID_List
			set @ttID = SCOPE_IDENTITY();
			IF(@@ERROR > 0 or @ttID is null)
			GOTO Fail;
			
			--select 'Selected --- ', @ttID,@ID_Trans,@ID_Language
		END	
		ELSE
		BEGIN
			select @ttID
			Select @ID_Trans = a.ID_Translation from Options a where a.ID = @ttID
			--select 'Selected --- ', @ttID,@ID_Trans,@ID_Language
			IF(@@ERROR > 0 or @ID_Trans is null)
			GOTO Fail;		
		END
		--select @ID,ID_Parent= @ID_Parent,ID_ParentAdd= @ID_ParentAdd, @ID_Language,@ListName, @Name,ID_List = @ID_List,ttID =@ttID
		
		insert into TranslationValue (ID_Type, ID_TranslationGroup, ID_Language,Value)
		select 1,@ID_Trans,@ID_Language, @Name
		IF(@@ERROR > 0)
		GOTO Fail;
		
		/*if(@Description is not null)
		BEGIN
		
		insert into TranslationValue (ID_Type,ID_TranslationGroup, ID_Language,Value)
		select 2, @ID_Trans,@ID_Language, @Description
		IF(@@ERROR > 0)
		GOTO Fail;
		
		END*/
	END	
	
	update [TMP_ListsOptions_DirectImport]
	set IsAdded = 1,
	--ID_Imported = null,
	ID_Imported = @ttID	
	where @ID = ID
	and ID_Lang = @ID_Language
	IF(@@ERROR > 0)
		GOTO Fail;
	if (@ID_ImportFile is not null 
	and not exists (select 1 from ImportedFileBinding 
	where ID_ImportedFile = @ID_ImportFile 
	and	ID_DestinationTable = @ID
	and	ID_SourceTable = @ttID
	and ID_Language = @ID_Language))
	BEGIN
		insert into ImportedFileBinding (ID_ImportedFile,ID_SourceTable,ID_DestinationTable, ID_Language)
		select @ID_ImportFile,@ID,@ttID, @ID_Language
		IF(@@ERROR > 0)
			GOTO Fail;	
	END
	
	set @ttID = null;
	set @ID_Trans = null;
	set @ID_Parent= null;
	set @ID_ParentAdd= null;
	set @ID_Language= null;
	set @ListName= null;
	set @Name= null;
	
    FETCH NEXT FROM ImpCurs 
	INTO @ID, @ID_Parent, @ID_Language,@ListName, @Name
	IF(@@ERROR > 0)
		GOTO Fail;
END 
CLOSE ImpCurs;
DEALLOCATE ImpCurs;
Commit Transaction import;
return 0;

Fail:
CLOSE ImpCurs;
DEALLOCATE ImpCurs;
rollback Transaction import;
return @@error;
END
