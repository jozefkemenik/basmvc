GO

CREATE PROCEDURE [dbo].[Search_RefineSearch_stp]
			@LangCodeId INT,
		    @Page INT,
		    @PageSize INT,
		    @Location INT = NULL,
		    @ProviderId INT = NULL,
		    @EnumerationIdCSV NVARCHAR (512) = NULL,
		    @DiseaseIdCSV NVARCHAR (512) = NULL,
		    @DisciplineIdCSV NVARCHAR (512) = NULL,
		    @TreatmentMethodIdCSV NVARCHAR (512) = NULL
	AS
BEGIN

SELECT 1 AS ProviderId,  'Dr.House' AS ProviderName, 'USA' AS Country, 'New Yourk' AS City, 'Broadway 12' AS Street, '554X4' AS Zip, 'Content/Pictures/Provider/1.png' AS Picture, 'Sports Medicine|Orthopedic Surgery' AS Specialities, 4 AS Rating

END