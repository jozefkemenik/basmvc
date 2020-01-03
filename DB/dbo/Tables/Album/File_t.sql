CREATE TABLE [dbo].[File_t](
	[Id] [int] IDENTITY(1,1) NOT NULL, 
	[CreatedDate] DATETIME2 NOT NULL DEFAULT GetDate(),
	[FileName] [nvarchar](500) NOT NULL,
	[Title] [nvarchar](500) NULL,
	[Description] [nvarchar](1000) NULL,
	[IsOnTopAlbum] [bit] NOT NULL DEFAULT 0,
	[Private] [bit] NOT NULL DEFAULT 0,
	[Deleted] [bit] NOT NULL DEFAULT 0,
	[Album_Id] [int] NULL,
	[UserId] [int] NOT NULL,
    [Sort] INT NOT NULL, 
    [Image] VARBINARY(MAX) NOT NULL, 
 CONSTRAINT [FK_dbo.File_t_dbo.Album_t_Album_Id] FOREIGN KEY([Album_Id]) REFERENCES [dbo].[Album_t] ([Id]),
 CONSTRAINT [FK_dbo.File_t_dbo.UserProfile_UserProfile_UserId] FOREIGN KEY([UserId]) REFERENCES [dbo].[UserProfile] ([UserId]), 
    CONSTRAINT [PK_File_t] PRIMARY KEY ([Id])

);
