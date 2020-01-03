-- =============================================
-- Author:        Michal Deák
-- Create date: 07/02/2014 13:53:12
-- Description:   Returns Identity by type and id
/*
exec [Identity_GetByIDAndIdentityType] @MembershipID='E7CB946A-7549-471B-8025-E6D56326E0E5'
		
*/
-- =============================================
CREATE PROCEDURE [dbo].[Identity_GetByIDAndIdentityType]
       @Id_Identity int,
       @IdentityType tinyint
AS
BEGIN
	
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
			where a.ID = @Id_Identity
			
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
			where a.ID = @Id_Identity
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
			where a.ID = @Id_Identity
		 END
		
		--END

END