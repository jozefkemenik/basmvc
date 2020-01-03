/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/



:r ..\Data\Membership\dbo.UserProfileAndMembership.sql



INSERT INTO [dbo].[AlbumType_t]
           ([Type]
           ,[Description])
     VALUES
           ('new'
           ,'new')
INSERT INTO [dbo].[AlbumType_t]
           ([Type]
           ,[Description])
     VALUES
           ('event'
           ,'event')



SET IDENTITY_INSERT [BAS].[dbo].[Album_t] ON;

delete from  [BAS].[dbo].[Album_t]

insert into [BAS].[dbo].[Album_t]
      (ID,[CreatedDate]
      ,[Title]
      ,[Description]
      ,[AlbumTypeId]
      ,[UserId]
    )
 


SELECT 
       ID,[CreatedDate]
      ,[Title]
      ,[Description]
      ,[FolderType_Id]
	  ,1
  FROM [W:\DOMAINS\WWW.BESTARTSCHOOL.EU\PUBLIC\WWW_ROOT\APP_DATA\BESTARTSCHOOLDB.MDF].[dbo].[Folder]

SET IDENTITY_INSERT [BAS].[dbo].[Album_t] OFF;


GO







