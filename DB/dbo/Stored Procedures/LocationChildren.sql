CREATE PROCEDURE [dbo].[LocationChildren]
			@LangCodeId INT,
		    @ParentId INT
	AS
BEGIN

DECLARE @V DECIMAL(18,4) = 0;

SELECT 1 AS Id, 'meno'  as Name, @V as Long, @V as Lat

END