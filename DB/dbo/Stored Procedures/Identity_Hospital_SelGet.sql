-- =============================================
-- Author:        Michal Deák
-- Create date: 05.05.2014
-- Description:   Procedure for selecting datasets for autocompleete
/*
exec [dbo].[Identity_Hospital_SelGet] 7

*/
-- =============================================
CREATE PROCEDURE [dbo].[Identity_Hospital_SelGet]
@ID_Identity int = null,
@IsActive bit = null
AS
BEGIN
	SET NOCOUNT ON;


  SELECT [ID]
      ,[IdentityType]
      ,[IsActive]
      ,[CompanyName]
      ,[ID_Country]
      ,[ID_City]
      ,[City]
      ,[ZIP]
      ,[Phone]
      ,[Fax]
      ,[Street]
      ,[Email]
      ,[Web]
  FROM [PremiumHealth].[dbo].[Provider]
  where ID = isnull(@ID_Identity,ID)
	and IsActive =isnull(@IsActive,IsActive)
  
 END