-- =============================================
-- Author:		Michal Deák
-- Create date: 05.05.2014
-- Description:	Procedure for selecting discipline
/*
exec [Discipline_SelGet] 1033,1
*/
-- =============================================
CREATE PROCEDURE [dbo].[Discipline_SelGet]
@ID_Discipline int = null,
@ID_Lang int,
@IsActive bit = null
	AS
BEGIN
	SET NOCOUNT ON;


SELECT a.[ID]
      ,Name = dbo.fn_GetTranslationWord(1031,1,a.ID_Translation)
      ,Description = dbo.fn_GetTranslationWord(1031,2,a.ID_Translation)
      ,a.IsActive
  FROM [dbo].[Discipline] a
  where IsActive = ISNULL(@IsActive,IsActive)
  and isnull(@ID_Discipline,ID) = ID
  
END