CREATE TABLE [dbo].[Album_t](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDate] DATETIME2 NOT NULL DEFAULT GetDate(),
	[Title] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[AlbumTypeId] [int] NULL,
    [UserId] INT NOT NULL, 
    [AlbumPathName] VARCHAR(1000) NULL, 
    CONSTRAINT [PK_dbo.Album_t] PRIMARY KEY CLUSTERED ([Id] ASC),
 CONSTRAINT [FK_dbo.Album_t_dbo.AlbumType_t_AlbumType_Id] FOREIGN KEY([AlbumTypeId]) REFERENCES [dbo].[AlbumType_t] ([Id]),

 )

