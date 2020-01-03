-- =============================================
-- Author:		Michal Deák
-- Create date: 05.05.2014
-- Description:	Procedure for deleting ListsOptions
/*
exec [ListsOptions_Del] 18
*/
-- =============================================
CREATE PROCEDURE [dbo].[ListsOptions_Del]
@ID_List int = null,
@ID_Option int = null,
@ID_Lang int = null,
@IsActive bit = null
	AS
BEGIN
SET NOCOUNT ON;

delete from Options
where ID_Translation in (select b.ID from Options a 
	inner join	TranslationGroup b on b.ID = a.ID_Translation
	inner join TranslationValue c on b.ID =c.ID_TranslationGroup group by b.ID )

declare  @ID_Delete table (ID uniqueidentifier)
insert into @ID_Delete
select ID_Translation from Options

delete from Options 
where isnull(@ID_Option, ID) = ID

declare  @ID_DeleteList table (ID uniqueidentifier)
insert into @ID_Delete
select ID_Translation from Lists

delete from Lists 
where isnull(@ID_List, ID) = ID

delete from TranslationGroup 
where ID in (select b.ID from @ID_Delete a 
	inner join	TranslationGroup b on b.ID = a.ID
	group by b.ID )  

delete from TranslationGroup 
where ID in (select b.ID from @ID_DeleteList a 
	inner join	TranslationGroup b on b.ID = a.ID
	group by b.ID )  
END
