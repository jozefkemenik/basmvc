CREATE TABLE [dbo].[AlbumType_t]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
	[Type] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	CONSTRAINT [PK_AlbumType_t] PRIMARY KEY CLUSTERED ([Id] ASC))
