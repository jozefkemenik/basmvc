CREATE TABLE [dbo].[Message_t]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,

    [New] INT NOT NULL DEFAULT 1, 
	[Deleted] INT NOT NULL DEFAULT 0,   
    [FromUserId] INT NULL, 
	[UserId] INT NULL,    
    [Phone] VARCHAR(100) NULL, 
    [Email] VARCHAR(100) NULL,   
    [ParentId] INT NULL, 
	[Ip] CHAR(15) NULL, 
    [CreatedDate] DATETIME2 NOT NULL DEFAULT GETDATE(), 
    [Name] NVARCHAR(200) NULL, 
    CONSTRAINT [PK_Message_t] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Message_t_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [Message_t] ([ID]),
  
    CONSTRAINT [FK_Message_t_UserId] FOREIGN KEY ([UserId]) REFERENCES [UserProfile]([UserId]), 
	CONSTRAINT [FK_Message_t_FromUserId] FOREIGN KEY ([FromUserId]) REFERENCES [UserProfile]([UserId])
   --CONSTRAINT [FK_Advertisement_t_Translation_t_TextTranslationKey] FOREIGN KEY ([TextTranslationKey]) REFERENCES [Translation_t]([Key]),
   --CONSTRAINT [FK_Advertisement_t_Language_t_DefaultLanguageId] FOREIGN KEY ([TextTranslationKey]) REFERENCES [Translation_t]([Key]),
	--CONSTRAINT [FK_Advertisement_t_Translation_t_UserId] FOREIGN KEY ([TextTranslationKey]) REFERENCES [Translation_t]([Key]),
)