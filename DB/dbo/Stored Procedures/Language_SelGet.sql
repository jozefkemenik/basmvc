-- =============================================
-- Author:		Michal Deák
-- Create date: 05.05.2014
-- Description:	Procedure for selecting Language
/*
exec [Language_SelGet] 1033,0
*/
-- =============================================
CREATE PROCEDURE [dbo].[Language_SelGet]
@ID_Lang int = null,
@IsActive bit = null
	AS
BEGIN
	SET NOCOUNT ON;


SELECT [Id]
      ,[Name]
      ,[ShortCut]
      ,[IconPath]
      ,[IsActive]
      ,[IsDefault]
  FROM [dbo].[Language_t]
  where IsActive = ISNULL(@IsActive,IsActive)
  and [Id] = ISNULL(@ID_Lang,Id)
  
END