-- =============================================
-- Author:        Michal Deák
-- Create date: 05.05.2014
-- Description:   Procedure for selecting datasets for autocompleete
/*



declare @p1 int
set @p1=null
exec [Identity_Create] @ID=@p1 output,@IdentityType=5,@IsActive=0,@Title_Prefix=NULL,@Name=N'test21@netinfodata.com',@Surname=NULL,@CompanyName=N'test21@netinfodata.com',@CompanyName2=NULL,@ID_Country=NULL,@ID_City=NULL,@City=NULL,@ZIP=NULL,@Street=NULL,@Email=N'test21@netinfodata.com',@Phone=NULL,@Fax=NULL,@Web=NULL,@Branche =NULL
select @p1


*/
-- =============================================
CREATE PROCEDURE [dbo].[Identity_Create]
		@ID int = null out,
       @IdentityType tinyint,
       @IsActive bit = 0,
       @Title_Prefix nvarchar(64) = null,
       @Name nvarchar(256) = null,
       @Surname nvarchar(256) = null,
       @CompanyName nvarchar(512) = null,
       @CompanyName2 nvarchar(512) = null,
       @ID_Country int = null,
       @ID_City bigint = null,
       @City nvarchar(256) = null,
       @ZIP nvarchar(50) = null,
       @Street nvarchar(256) = null,
       @Email nvarchar(256) = null,
       @Phone nvarchar(64) = null,
       @Fax nvarchar(64) = null,
       @Web nvarchar(512) = null,
       @Branche nvarchar(max) = null
AS
BEGIN
      SET NOCOUNT ON;


if @IdentityType = 1 /*Doctor*/
BEGIN
INSERT INTO [dbo].[Doctor]
           ([IdentityType]
           ,[IsActive]
           ,[Title_Prefix]
           ,[Name]
           ,[Surname]
           ,[CompanyName]
           ,[ID_Country]
           ,[ID_City]
           ,[City]
           ,[ZIP]
           ,[Street]
           ,[Email]
           ,[Phone]
           ,[Fax]
           ,[Web]
           ,[Branche])
     VALUES (
       @IdentityType,
       @IsActive,
       @Title_Prefix ,
       @Name,
       @Surname,
       @CompanyName,
       @ID_Country,
       @ID_City,
       @City,
       @ZIP,
       @Street,
       @Email,
       @Phone,
       @Fax,
       @Web,
       @Branche)
       set @ID = @@IDENTITY;
 END        
 /**/
 if @IdentityType = 5 /*patient*/
BEGIN
INSERT INTO [dbo].[Person]
           ([IdentityType]
           ,[IsActive]
           ,[Title_Prefix]
           ,[Name]
           ,[Surname]
           ,[CompanyName]
           ,[ID_Country]
           ,[ID_City]
           ,[City]
           ,[ZIP]
           ,[Street]
           ,[Email]
           ,[Phone]
           ,[Fax]
           ,[Web])
     VALUES (
       @IdentityType,
       @IsActive,
       @Title_Prefix ,
       @Name,
       @Surname,
       @CompanyName,
       @ID_Country,
       @ID_City,
       @City,
       @ZIP,
       @Street,
       @Email,
       @Phone,
       @Fax,
       @Web)
       set @ID = @@IDENTITY;
 END    
 
 if @IdentityType not in ( 5,1)  /*Hospital*/
 BEGIN    
           INSERT INTO [dbo].[Provider]
           ([IdentityType]
           ,[IsActive]
           ,[CompanyName]
           ,[ID_Country]
           ,[ID_City]
           ,[City]
           ,[ZIP]
           ,[Street]
           ,[Email]
           ,[Phone]
           ,[Fax]
           ,[Web])
     VALUES
           (
       @IdentityType,
       @IsActive,
       @CompanyName,
       @ID_Country,
       @ID_City,
       @City,
       @ZIP,
       @Street,
       @Email,
       @Phone,
       @Fax,
       @Web)
       set @ID = @@IDENTITY;
END
END

