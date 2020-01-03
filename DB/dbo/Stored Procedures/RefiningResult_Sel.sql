
-- =============================================
-- Author:		Michal Deák
-- Create date: 06/11/2014 08:04:55
-- Description:	Procedure for selecting refining result
/*
SQL Object info:
drop type RefineSearchUdt
CREATE TYPE RefineSearchUdt AS TABLE(
 ID int,
 ID_Type smallint,
 MaxSearchResults int
 )

declare @data RefineSearchUdt ;

insert into @data ( ID,
 ID_Type,
 MaxSearchResults)
 select 746,1,-1
 union all
 select 785,2,-1
select * from @data

exec [RefiningResult_Sel] @data, 1033
*/
-- =============================================
CREATE PROCEDURE [dbo].[RefiningResult_Sel]
@RefineSearch AS RefineSearchUdt READONLY,
@idLanguage int
	AS
BEGIN

--select Title_Prefix,Name,Surname,CompanyName,ID_Country,ID_City,City,ZIP,Street,Email,Phone,Fax,Web,Branche from Doctor a
--inner join DoctorOptions b on a.ID = b.ID_Doctor
--inner join @RefineSearch c on b.ID_Option = c.ID and c.ID_Type = 1
--where id_option = c.ID
--and c.ID_Type = 1
--UNION ALL
--select Title_Prefix = '',Name = '',Surname = '',CompanyName,ID_Country,ID_City,City,ZIP,Street,Email,Phone,Fax,Web,Branche = '' from Provider a
--inner join ProviderOptions b on a.ID = b.ID_Provider
--inner join @RefineSearch c on b.ID_Option = c.ID and c.ID_Type = 2
--where id_option = c.ID
--and c.ID_Type = 2
	
  SELECT 1;
END