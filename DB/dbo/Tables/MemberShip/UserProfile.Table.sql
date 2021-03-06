
CREATE TABLE [dbo].[UserProfile](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NULL,
	-- to do constrain
	[DefaultIdenityType] [int] NULL,
	[LanguageId] [int]  NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
    [Phone] NVARCHAR(50) NULL, 
    [BirthDayYear] INT NULL, 
    [Gender] BIT NULL, 
    [ProfileType] BIT NULL, 
    PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)
,

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO