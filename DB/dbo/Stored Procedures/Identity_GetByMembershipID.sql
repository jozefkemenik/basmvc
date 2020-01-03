-- =============================================
-- Author:        Michal Deák
-- Create date: 06/19/2014 13:15:42
-- Description:   Procedure for Identity selecetion by membership id
/*
exec [Identity_GetByMembershipID] @MembershipID='E7CB946A-7549-471B-8025-E6D56326E0E5'


select * from PersonAccessRights (nolock) 
		
		where ID_Membership = 'E7CB946A-7549-471B-8025-E6D56326E0E5'
		
		select * from 
			ProviderAccessRights (nolock)
			where ID_Membership = 'E7CB946A-7549-471B-8025-E6D56326E0E5'
		
*/
-- =============================================
CREATE PROCEDURE [dbo].[Identity_GetByMembershipID]
       @MembershipID int,
       @IdentityType tinyint
AS
BEGIN
		/*Declare @IdentityType tinyint,@IdentityId int;
		
		select TOP 1 @IdentityType = 1,@IdentityId = ID_Doctor from DoctorAccessRights (nolock) 
		where ID_Membership = @MembershipID
      
		if @IdentityId is null or @IdentityType is null
		BEGIN
			select TOP 1 @IdentityType = 2, @IdentityId = ID_Provider from 
			ProviderAccessRights (nolock)
			where ID_Membership = @MembershipID
		END
		
		if @IdentityId is null or @IdentityType is null
		BEGIN
			select TOP 1 @IdentityType =5, @IdentityId = ID_Person
			from PersonAccessRights (nolock)
			where ID_Membership = @MembershipID
		END
		
		select @IdentityType
		 
		if @IdentityId is not null AND @IdentityType is not null
		BEGIN
		
		*/
		
		if @IdentityType = 1
		BEGIN
			select
			a.ID,
			a.IdentityType, 
			a.Title_Prefix,
			a.Name,
			a.Surname,
			a.CompanyName,
			a.ID_Country,
			a.ID_City,
			a.City,
			a.ZIP,
			a.Street,
			a.Email,
			a.Phone,
			a.Fax,
			a.Web,
			a.Branche from Doctor (nolock) a
			where a.ID = (select TOP 1 ID_Doctor from 
			DoctorAccessRights (nolock)
			where ID_Membership = @MembershipID group by ID_Doctor)
		END		
		
		if @IdentityType = 5 /*patient*/
		BEGIN
			select 
			a.ID,
			a.IdentityType, 
			a.Title_Prefix,
			a.Name ,
			a.Surname ,
			a.CompanyName,
			a.ID_Country,
			a.ID_City,
			a.City,ZIP,
			a.Street,
			a.Email,
			a.Phone,
			a.Fax,
			a.Web,
			Branche = '' 
			from Person (nolock) a
			where a.ID = (select TOP 1 ID_Person from 
			PersonAccessRights (nolock)
			where ID_Membership = @MembershipID group by ID_Person )
		END
		
		 if @IdentityType not in ( 5,1)  /*Hospital*/
		 BEGIN
		 select 
			a.ID,
			a.IdentityType, 
			Title_Prefix='',
			Name ='' ,
			Surname ='',
			a.CompanyName,
			a.ID_Country,
			a.ID_City,
			a.City,ZIP,
			a.Street,
			a.Email,
			a.Phone,
			a.Fax,
			a.Web,
			Branche = '' 
			from Provider (nolock) a
			where a.ID = (select TOP 1 ID_Provider from 
			ProviderAccessRights (nolock)
			where ID_Membership = @MembershipID group by ID_Provider)
		 END
		
		--END

END
