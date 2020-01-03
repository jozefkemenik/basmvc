-- =============================================
-- Author:		Michal Deák
-- Create date: 06.05.2014
-- Description:	Procedure for selecting DiseaseTreatmentType by language
/* Tests
-----------------------------------------
exec [dbo].[DiseaseTreatmentType_SelByLang] 1033
*/
-- =============================================
CREATE PROCEDURE [dbo].[DiseaseTreatmentType_SelByLang]
@ID_Language int
	AS
BEGIN
	SET NOCOUNT ON;

	SELECT a.[ID]
		  ,a.[IsActive]
		  ,b.[Name]
		  ,b.[Description]
		  ,b.[ID_Language]     
	FROM [dbo].[DiseaseTreatmentType]  a
	inner join [dbo].[DiseaseTreatmentTypeLanguage] b 
	ON a.ID = b.ID_DiseaseType
	where ID_Language = @ID_Language;

END