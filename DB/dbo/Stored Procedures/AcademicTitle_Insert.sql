-- =============================================
-- Author:		Michal Deák
-- Create date: 07/03/2014 14:36:16 
-- Description:	Procedure for inserting Academic Title
/*
exec [AcademicTitle_Insert]
*/
-- =============================================
Create PROCEDURE [dbo].[AcademicTitle_Insert]
			@ID int out
		   ,@Title nvarchar(512)
           ,@IsAccepted bit = 0
           ,@ID_Country int = 127           
           ,@ID_Language int = 127
	AS
BEGIN
SET NOCOUNT ON;

INSERT INTO [dbo].[AcademicTitle]
           ([Title]
           ,[IsAccepted]
           ,[ID_Country]
           ,[ID_Language])
     VALUES
           ( @Title 
           ,@IsAccepted
           ,@ID_Country         
           ,@ID_Language)
           set  @ID = SCOPE_IDENTITY()
END