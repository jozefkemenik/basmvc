
CREATE FUNCTION [dbo].[fn_PocitanieSlova]
(
@Slovo NVARCHAR(25),
@Veta NVARCHAR(1000)

 )
RETURNS SMALLINT
AS
BEGIN

IF @Slovo IS NULL OR @Veta IS NULL RETURN 0

SET @Veta = ' '+@Veta
SET @Slovo = ' '+@Slovo
DECLARE @SlovoPlus NVARCHAR(26)
SELECT @SlovoPlus = @Slovo + 'x'

DECLARE @VetaPlus NVARCHAR(1300)
SELECT @VetaPlus = REPLACE (@Veta , @Slovo , @SlovoPlus)

RETURN LEN(@VetaPlus) - LEN(@Veta)

END
