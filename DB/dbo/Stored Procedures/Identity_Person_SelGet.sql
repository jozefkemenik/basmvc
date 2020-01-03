-- =============================================
-- Author:        Michal Deák
-- Create date: 05.05.2014
-- Description:   Procedure for selecting datasets for autocompleete
/*
exec [dbo].[Identity_Person_GetSel]
*/
-- =============================================
CREATE PROCEDURE [dbo].[Identity_Person_SelGet]
@ID_Identity int = null,
@IsActive bit = null
AS
BEGIN
	SET NOCOUNT ON;

SELECT [ID]
      ,[IdentityType]
      ,[IsActive]
      ,[Title_Prefix]
      ,[Name]
      ,[Surname]
      ,[CompanyName]
      ,[ID_Country]
      ,[ID_City]
      ,[City]
      ,[ZIP]
      ,[Street]
      ,[Email]
      ,[Phone]
      ,[Fax]
      ,[Web]
  FROM [dbo].[Person]
  where ID = isnull(@ID_Identity,ID)
  
 END