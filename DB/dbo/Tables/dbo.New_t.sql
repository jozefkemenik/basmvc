CREATE TABLE [dbo].[New_t](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IsVisible] [bit] NOT NULL,
	[Text] [nvarchar](max) NULL,
	[Title] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IssueDate] [datetime] NOT NULL,
	[UserId] [int] NULL,
	[AlbumId] [int] NULL,
 CONSTRAINT [PK_dbo.New_t] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[New_t]  WITH CHECK ADD  CONSTRAINT [FK_dbo.New_t_dbo.Album_t] FOREIGN KEY([AlbumId])
REFERENCES [dbo].[Album_t] ([Id])
GO

ALTER TABLE [dbo].[New_t] CHECK CONSTRAINT [FK_dbo.New_t_dbo.Album_t]
GO
