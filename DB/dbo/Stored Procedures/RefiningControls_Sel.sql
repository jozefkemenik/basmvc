-- =============================================
-- Author:		Michal Deák
-- Create date: 06/11/2014 08:04:55
-- Description:	Procedure for selecting refininf contrels
/*
exec [RefiningControls_Sel] 1033,1
*/
-- =============================================
CREATE PROCEDURE [dbo].[RefiningControls_Sel]
@eRefiningType smallint, --TODO create enum in the BL for this enum
@KeyID nvarchar(512),
@ID_Lang int = null,
@Level int = null
	AS
BEGIN
	SET NOCOUNT ON;

if(@eRefiningType = 1)
BEGIN
	SELECT a.[ID]
		  ,a.[ID_List] as Category --ToDO probably change to string
		  ,a.[ID_Parent]
		  ,Name = dbo.fn_GetTranslationWord(@ID_Lang,1,a.ID_Translation)
		  ,a.[Level]
	  FROM [dbo].[Options] a
	  inner join Lists b on a.ID_List = b.ID
	  where Level = ISNULL(@Level,Level)
	  and b.KeyName = @KeyID
END
	  ---TODO write statements for other sourcess regarding  @eRefiningType....
END