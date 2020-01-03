-- roles
INSERT INTO [dbo].[webpages_Roles]
           ([RoleName])
     VALUES
           ('Administrator')
INSERT INTO [dbo].[webpages_Roles]
           ([RoleName])
     VALUES
           ('User')
INSERT INTO [dbo].[webpages_Roles]
           ([RoleName])
     VALUES
           ('Guess')


-- user lovas@netinfodata.com as Administrator role

INSERT INTO [dbo].[webpages_Membership]
           ([UserId]
           ,[CreateDate]
           ,[ConfirmationToken]
           ,[IsConfirmed]
           ,[LastPasswordFailureDate]
           ,[PasswordFailuresSinceLastSuccess]
           ,[Password]
           ,[PasswordChangedDate]
           ,[PasswordSalt]
           )
     VALUES
           (1
           ,GETDATE ()
           ,NULL
           ,1
           ,NULL
           ,0
           ,'AIdHZz32Bgj56ODpTfh3kNLktODu94l8efH2VJMItXnAcoyw+fBsnFvnFW0novXUng=='
           ,GETDATE ()
           ,''
           )		

/****** [dbo].[UserProfile]  ******/
SET IDENTITY_INSERT [dbo].[UserProfile] ON
INSERT [dbo].[UserProfile] 
([UserId], 
 [UserName], 
 [DefaultIdenityType], 
 [LanguageId], 
 [FirstName], 
 [LastName], 
 [Email]
 ) 
 VALUES 
 (1, 
  N'lovas@netinfodata.com', 
  NULL, 
  1051, 
  'Juraj', 
  'Lovas', 
  'lovas@netinfodata.com' 

)

INSERT INTO [dbo].[webpages_UsersInRoles]
           ([UserId]
           ,[RoleId])
     VALUES (1,1)




-- user kemenik@netinfodata.com as Userrole

INSERT INTO [dbo].[webpages_Membership]
           ([UserId]
           ,[CreateDate]
           ,[ConfirmationToken]
           ,[IsConfirmed]
           ,[LastPasswordFailureDate]
           ,[PasswordFailuresSinceLastSuccess]
           ,[Password]
           ,[PasswordChangedDate]
           ,[PasswordSalt]
           )
     VALUES
           (2
           ,GETDATE ()
           ,NULL
           ,1
           ,NULL
           ,0
           ,'AIdHZz32Bgj56ODpTfh3kNLktODu94l8efH2VJMItXnAcoyw+fBsnFvnFW0novXUng=='
           ,GETDATE ()
           ,''
           )



/****** [dbo].[UserProfile]  ******/
SET IDENTITY_INSERT [dbo].[UserProfile] ON
INSERT [dbo].[UserProfile] 
([UserId], 
 [UserName], 
 [DefaultIdenityType], 
 [LanguageId], 
 [FirstName], 
 [LastName], 
 [Email]
 ) 
 VALUES 
 (2, 
  N'kemenik@netinfodata.com', 
  NULL, 
  1051, 
  'Jozef', 
  'Kemenik', 
  'kemenik@netinfodata.com' 

)
SET IDENTITY_INSERT [dbo].[UserProfile] OFF

INSERT INTO [dbo].[webpages_UsersInRoles]
           ([UserId]
           ,[RoleId])
     VALUES (2,2)
GO