CREATE TABLE [dbo].[Event_t](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IsVisible] [bit] NOT NULL,
	[Text] [nvarchar](max) NULL,
	[Title] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IssueDate] [datetime] NOT NULL,
	[UserId] [int] NULL,
	[AlbumId] [int] NULL,
	[NewId] [int] NULL,
 CONSTRAINT [PK_dbo.Event_t] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Event_t]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Event_t_dbo.Album_t] FOREIGN KEY([AlbumId])
REFERENCES [dbo].[Album_t] ([Id])
GO

ALTER TABLE [dbo].[Event_t] CHECK CONSTRAINT [FK_dbo.Event_t_dbo.Album_t]
GO

ALTER TABLE [dbo].[Event_t]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Event_t_dbo.New_t] FOREIGN KEY([NewId])
REFERENCES [dbo].[New_t] ([Id])
GO

ALTER TABLE [dbo].[Event_t] CHECK CONSTRAINT [FK_dbo.Event_t_dbo.New_t]
GO