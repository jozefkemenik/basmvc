-- =============================================
-- Author:        Michal Deák
-- Create date: 05.05.2014
-- Description:   Procedure for selecting datasets for autocompleete
/*
exec identity_delete 347,1,null,1
*/
-- =============================================
CREATE PROCEDURE [dbo].[Identity_Delete]
		@ID int,
		@IdentityType tinyint,
		@IsActive bit = 0,
		@SuperAdminDelete bit = 0       
AS
BEGIN
	SET NOCOUNT ON;
      
	if   @SuperAdminDelete is not null and @SuperAdminDelete = 1
	BEGIN
		if @IdentityType = 1 /*Doctor*/
		BEGIN
			DELETE  FROM Doctor
			WHERE IdentityType=@IdentityType
			and ID = @ID
		END
		if @IdentityType = 5 /*patient*/	
		BEGIN
			DELETE  FROM Person
			WHERE IdentityType=@IdentityType
			and ID = @ID
		END
		
		 if @IdentityType not in ( 5,1)  /*Hospital, Medical centre*/
		 BEGIN
			DELETE  FROM Provider
			WHERE IdentityType=@IdentityType
			and ID = @ID
		END
	END
	ELSE
	BEGIN

		if @IdentityType = 1 /*Doctor*/
		BEGIN
			update Doctor
			set IsActive = 0		
			WHERE IdentityType=@IdentityType
			and ID = @ID
		END
		if @IdentityType = 5 /*patient*/	
		BEGIN	
			update Person
			set IsActive = 0		
			WHERE IdentityType=@IdentityType
			and ID = @ID
		END
		
		 if @IdentityType not in ( 5,1)  /*Hospital, Medical centre*/
		 BEGIN
			update Provider
			set IsActive = 0		
			WHERE IdentityType=@IdentityType
			and ID = @ID	
		END
	END
END

