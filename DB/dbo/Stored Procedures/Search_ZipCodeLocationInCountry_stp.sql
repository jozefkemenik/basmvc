CREATE PROCEDURE [dbo].[Search_ZipCodeLocationInCountry_stp]
			@LangCodeId INT,
			@CountryId INT,
		    @Term NVARCHAR(512)
	AS
BEGIN


--DECLARE @searchTerm nvarchar(550) = '"' + @Term +'*"';
DECLARE @searchTerm nvarchar(550) = @Term +'%';


SELECT				main_t.Id, 
					main_t.Name ,  
					c_w.CountryId, 
					c_w.CountryName,
					c_w.ContinentId,
					c_w.ContinentName,
					c_w.Id as CityId, 
					c_w.Name AS CityName 
	  FROM  ZipCodeList_t   main_t
			 INNER JOIN	Cities_w c_w ON c_w.Id = main_t.CityId
			
			WHERE	c_w.LangCodeId = @LangCodeId 
					AND main_t.Active = 1 
					AND c_w.CountryId = @CountryId
				--	AND main_t.LangCodeId = @LangCodeId
				--	AND CONTAINS(main_t.Name, @searchTerm)
					AND main_t.Name like @searchTerm


END