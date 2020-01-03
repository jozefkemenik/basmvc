CREATE FUNCTION [dbo].[fn_GetFullPathsPicture](@FileId int, @AlbumId int  = -1)
RETURNS 
@result TABLE ( ThumPath VARCHAR(300) , StandPath VARCHAR(300) , OrigPath VARCHAR(300)) 
AS 

BEGIN
	DECLARE @TmpALbumId INT;
	IF (@AlbumId = -1)
	BEGIN
		SELECT @TmpALbumId  = Album_Id From dbo.File_t WHERE Id = @FileId
	END
	ELSE
	BEGIN
		SET @TmpALbumId = @AlbumId;
	END
	IF (@TmpALbumId IS NOT NULL)
	BEGIN
	INSERT INTO @result 
	VALUES(
	'/Uploads/Albums/Album' + CAST(@AlbumId  AS NVARCHAR(10)) +'/thum_album' + CAST(@AlbumId AS NVARCHAR(10))+ '_file' + CAST(@FileId  AS NVARCHAR(15)) + '.jpg',
	'/Uploads/Albums/Album' + CAST(@AlbumId  AS NVARCHAR(10)) +'/stand_album' + CAST(@AlbumId AS NVARCHAR(10))+ '_file' + CAST(@FileId  AS NVARCHAR(15)) + '.jpg',
	'/Uploads/Albums/Album' + CAST(@AlbumId  AS NVARCHAR(10)) +'/or_album' + CAST(@AlbumId AS NVARCHAR(10))+ '_file' + CAST(@FileId  AS NVARCHAR(15)) + '.jpg'
	)
	END
	RETURN

END