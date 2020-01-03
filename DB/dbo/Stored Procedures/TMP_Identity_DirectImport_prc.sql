-- =============================================
-- Author:		Michal Deák
-- Create date: 05.05.2014
-- Description:	Procedure for selecting Language
/* Tests
-----------------------------------------
exec [dbo].[TMP_Identity_DirectImport_prc]
*/
-- =============================================
CREATE PROCEDURE [dbo].[TMP_Identity_DirectImport_prc]
	AS
BEGIN
	SET NOCOUNT ON;

declare
@schemaId int,
@ID_Trans uniqueidentifier,
@ttID int,
@type nvarchar,
@ID int,
@IdentityType tinyint,
@IsActive bit,
@Name nvarchar(512), 
@Surname nvarchar(512),
@Title_Prefix [nvarchar](128) ,
@CompanyName nvarchar(512) ,
@Country nvarchar(128),	
@Region nvarchar(128) ,
@City nvarchar(128) ,
@ZIP numeric(20, 0) ,
@Street nvarchar(128) ,
@Phone nvarchar(128) ,
@Email nvarchar(128) ,
@Url nvarchar(512) ,
@Fax nvarchar(128) ,
@Branche nvarchar(512),
@titleID int;

set @type = 'Identity';

DECLARE ImpCurs CURSOR LOCAL  READ_ONLY
FOR SELECT [ID]
      ,[IdentityType]
      ,[IsActive]
      ,[Name]
      ,[Surname]
      ,LTRIM(RTRIM([Title_Prefix]))
      ,[CompanyName]
      ,[Country]
      ,[Region]
      ,[City]
      ,[ZIP]
      ,[Street]
      ,[Phone]
      ,[Email]
      ,[Url]
      ,[Fax]
      ,[Branche]
  FROM [dbo].[TMP_Identity_DirectImport]
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
INTO 
@ID,      
@IdentityType,
@IsActive,
@Name ,    
@Surname,
@Title_Prefix  ,
@CompanyName  ,
@Country ,	
@Region  ,
@City  ,
@ZIP  ,
@Street  ,
@Phone  ,
@Email  ,
@Url  ,
@Fax ,
@Branche

WHILE @@FETCH_STATUS = 0
BEGIN	
	
	if exists (Select 1 from AcademicTitle where [Title] = @Title_Prefix)
	BEGIN
		Select @titleID = ID from AcademicTitle where [Title] = @Title_Prefix
		IF(@@ERROR > 0 or @titleID is null)
		GOTO Fail;
	END
	else
	BEGIN
		INSERT INTO AcademicTitle(Title,ID_Language)
		SELECT @Title_Prefix,127 
		SET @titleID = SCOPE_IDENTITY()
		IF(@@ERROR > 0 or @titleID is null)
		GOTO Fail;
	END
	
	

	if(@IdentityType=1)
	BEGIN
		INSERT INTO [dbo].[Doctor]
           ([IdentityType]
           ,[IsActive]
           ,Title_Prefix
           ,[Name]
           ,[Surname]
           ,[CompanyName]
           ,[ID_Country]
           ,[City]
           ,ZIP
           ,[Street]
           ,[Email]
           ,[Phone]
           ,[Fax]
           ,[Web]
           ,[Branche]
           ,ID_TitlePrefix
           )  
		select     
		1,
		1,
		@Title_Prefix  ,
		@Name ,    
		@Surname,
		@CompanyName  ,
		(select ID from CountryInfo where Iso = @Country) ,	
		@City  ,
		@ZIP  ,
		@Street  ,
		@Email  ,
		@Phone  ,
		@Fax ,
		@Url  ,
		@Branche,
		@titleID
		SET @ttID = SCOPE_IDENTITY()
		IF(@@ERROR > 0 or @ttID is null)
		GOTO Fail;
	END
	
	if(@IdentityType>1)
	BEGIN
	INSERT INTO [dbo].Provider
           ([IdentityType]
           ,[IsActive]
           ,[CompanyName]
           ,[ID_Country]
           ,[City]
           ,ZIP
           ,[Street]
           ,[Email]
           ,[Phone]
           ,[Fax]
           ,[Web])  
		select     
		2,
		1,
		@CompanyName  ,
		(select ID from CountryInfo where Iso = @Country),
		@City  ,
		@ZIP  ,
		@Street  ,
		@Email  ,
		@Phone  ,
		@Fax ,
		@Url
		SET @ttID = SCOPE_IDENTITY()
	END
	
		update [TMP_Identity_DirectImport]
	set IsAdded =0,
		ID_Imported = @ttID
	where @ID = ID
	IF(@@ERROR > 0)
		GOTO Fail;
	
	
	set @ttID = null;
	set @ID_Trans = null;
	
    FETCH NEXT FROM ImpCurs 
	INTO 
@ID,      
@IdentityType,
@IsActive,
@Name ,    
@Surname,
@Title_Prefix  ,
@CompanyName  ,
@Country ,	
@Region  ,
@City  ,
@ZIP  ,
@Street  ,
@Phone  ,
@Email  ,
@Url  ,
@Fax ,
@Branche
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