-- =============================================
-- Author:		Michal Deák
-- Create date: 06.06.2014
-- Description:	Insert binding to Identity
/*
SQL Object info:
drop type AccessRightsUdt
CREATE TYPE AccessRightsUdt AS TABLE(
 ID_Identity int,
 ID_IdentityType smallint,
 Allow bit,
 ID_User uniqueidentifier
 )
GO
*/

/*
exec [AccessRight_Insert] 1033,0
*/
-- =============================================
CREATE PROCEDURE [dbo].[AccessRight_Insert]
 @AccessRight AS AccessRightsUdt READONLY
	AS
BEGIN
	SET NOCOUNT ON;
 
	declare @type smallint;
	select @type = ID_IdentityType from @AccessRight
	
	
	if (@type = 1) /*Doctor*/
	begin
		MERGE	DoctorAccessRights b
		USING	@AccessRight  a
		ON	a.ID_User =b.ID_Membership
		and a.ID_Identity =b.ID_Doctor
		WHEN MATCHED THEN
			UPDATE SET	
			b.Allow= a.Allow
		WHEN NOT MATCHED BY TARGET THEN			
			INSERT (ID_Doctor ,Allow ,ID_Membership)
     		VALUES	(a.ID_Identity,a.Allow,a.ID_User)
		WHEN NOT MATCHED   BY SOURCE AND b.ID_Membership IN(SELECT ID_User FROM @AccessRight)AND b.ID_Doctor IN(SELECT ID_Identity FROM @AccessRight) THEN
			DELETE;
     end
     if (@type = 5)  /*Patient*/
	begin
		MERGE	PersonAccessRights b
		USING	@AccessRight  a
		ON	a.ID_User =b.ID_Membership
		and a.ID_Identity =b.ID_Person
		WHEN MATCHED THEN
			UPDATE SET	
			b.Allow= a.Allow
		WHEN NOT MATCHED BY TARGET THEN			
			INSERT (ID_Person ,Allow ,ID_Membership)
     		VALUES	(a.ID_Identity,a.Allow,a.ID_User)
		WHEN NOT MATCHED BY SOURCE  AND b.ID_Membership IN(SELECT ID_User FROM @AccessRight)AND b.ID_Person IN(SELECT ID_Identity FROM @AccessRight) THEN
			DELETE;
     end
     
	if (@type <>  1 or @type <> 5)
     begin
     	MERGE	ProviderAccessRights b
		USING	@AccessRight  a
		ON	a.ID_User =b.ID_Membership
		and a.ID_Identity =b.ID_Provider
		WHEN MATCHED THEN
			UPDATE SET	
			b.Allow= a.Allow
		WHEN NOT MATCHED BY TARGET THEN			
			INSERT (ID_Provider ,Allow ,ID_Membership)
     		VALUES	(a.ID_Identity,a.Allow,a.ID_User)
		WHEN NOT MATCHED BY SOURCE  AND b.ID_Membership IN(SELECT ID_User FROM @AccessRight) AND b.ID_Provider IN(SELECT ID_Identity FROM @AccessRight)THEN
			DELETE;
     end 
 END
