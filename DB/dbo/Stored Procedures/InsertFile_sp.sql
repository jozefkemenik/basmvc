CREATE PROCEDURE [dbo].[InsertFile_sp]
		
       @FileName nvarchar(500)
      ,@Title nvarchar(500)
      ,@Description nvarchar(1000)
      ,@IsOnTopAlbum bit
      ,@Private  bit
      ,@Album_Id int = NULL
      ,@UserId int
      ,@Sort int
      ,@Image varbinary(MAX)
 
	AS
	BEGIN
	
	IF @@Error  <> 0
         BEGIN
           SELECT -1
         END	

	INSERT INTO dbo.File_t
	(  [FileName]
      ,[Title]
      ,[Description]
      ,[IsOnTopAlbum]
      ,[Private]
      ,[Album_Id]
      ,[UserId]
      ,[Sort]
      ,[Image])
	 VALUES
	  (@FileName
      ,@Title
      ,@Description
      ,@IsOnTopAlbum
      ,@Private 
      ,@Album_Id
      ,@UserId
      ,@Sort
      ,@Image)

	  DECLARE @id int;
	  SET @id = @@IDENTITY
	  SELECT @id

	  END