GO
CREATE PROCEDURE [dbo].[Search_AutoSuggestCategory_stp]
			@LangCodeId INT,
			@Category INT,
		    @Term NVARCHAR(512)
	AS
BEGIN

IF(@Category = 1) -- Provider
BEGIN
			(SELECT 1 AS Id, 'Provider1'  as Name, 1 as Type Union
			SELECT 2 AS Id, 'Provider2'  as Name, 1 as Type Union
			SELECT 3 AS Id, 'Provider3'  as Name, 1 as Type Union
			SELECT 4 AS Id, 'Provider4'  as Name, 1 as Type Union
			SELECT 5 AS Id, 'Provider5'  as Name, 1 as Type Union
			SELECT 6 AS Id, 'Provider6'  as Name, 1 as Type)
END
IF(@Category = 2) -- Discipline
BEGIN
			(SELECT 7 AS Id, 'Discipline1'  as Name, 2 as Type Union
			SELECT 8 AS Id, 'Discipline2'  as Name, 2 as Type Union
			SELECT 9 AS Id, 'Discipline3'  as Name, 2 as Type Union
			SELECT 10 AS Id, 'Discipline4'  as Name, 2 as Type)
END
IF(@Category = 3) -- Method
BEGIN
			(SELECT 11 AS Id, 'Method1'  as Name, 3 as Type Union
			SELECT 12 AS Id, 'Method2'  as Name, 3 as Type Union
			SELECT 13 AS Id, 'Method3'  as Name, 3 as Type Union
			SELECT 14 AS Id, 'Method4'  as Name, 3 as Type)
END
IF(@Category = 4) -- Disease
BEGIN
			(SELECT 15 AS Id, 'Disease1'  as Name, 4 as Type Union
			SELECT 16 AS Id, 'Disease2'  as Name, 4 as Type Union
			SELECT 17 AS Id, 'Disease3'  as Name, 4 as Type Union
			SELECT 18 AS Id, 'Disease4'  as Name, 4 as Type)
END
END