-- =============================================
-- Author:		Michal Deák
-- Create date: 05.05.2014
-- Description:	Procedure for selecting discipline
/*
exec [TreatmentMethods_Del]
*/
-- =============================================
CREATE PROCEDURE [dbo].[TreatmentMethods_Del]
@ID_TreatmentMethod int = null,
@ID_Lang int = null,
@IsActive bit = null
	AS
BEGIN
	SET NOCOUNT ON;

delete from TranslationValue
where ID_TranslationGroup in (select b.ID from TreatmentMethods a 
	inner join	TranslationGroup b on b.ID = a.ID_Translation
	inner join TranslationValue c on b.ID =c.ID_TranslationGroup group by b.ID )

declare  @ID_Delete table (ID uniqueidentifier)
insert into @ID_Delete
select ID_Translation from TreatmentMethods

delete from TreatmentMethods 
where isnull(@ID_TreatmentMethod, ID) = ID

delete from TranslationGroup 
where ID in (select b.ID from @ID_Delete a 
	inner join	TranslationGroup b on b.ID = a.ID
	group by b.ID )  
END