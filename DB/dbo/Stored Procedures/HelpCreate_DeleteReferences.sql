-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
/*
exec HelpCreate_DeleteReferences 'dbo.Doctor'
*/
CREATE procedure HelpCreate_DeleteReferences
@TableToDelete nvarchar(1024) --'dbo.Doctor'
as
Declare @sql1 varchar(max)
      , @ptn1 varchar(200)
      , @ctn1 varchar(200)
      , @ptn2 varchar(200)
      , @ctn2 varchar(200)
--
SET @ptn1 = ''
--
SET @ctn1 = ''
--
SET @ptn2 = ''
--
SET @ctn2 = ''
--
SELECT @sql1 = case when (@ptn1 <> OBJECT_NAME (f.referenced_object_id)) then
                         COALESCE( @sql1 + char(10), '') + 'DELETE' + char(10) + ' ' + OBJECT_NAME (f.referenced_object_id) + ' FROM ' + OBJECT_NAME(f.parent_object_id) + ', '+OBJECT_NAME (f.referenced_object_id) + char(10) +' WHERE ' + OBJECT_NAME(f.parent_object_id) + '.' + COL_NAME(fc.parent_object_id, fc.parent_column_id) +'='+OBJECT_NAME (f.referenced_object_id)+'.'+COL_NAME(fc.referenced_object_id, fc.referenced_column_id)
                    else
                         @sql1 + ' AND ' + OBJECT_NAME(f.parent_object_id) + '.' + COL_NAME(fc.parent_object_id, fc.parent_column_id) +'='+OBJECT_NAME (f.referenced_object_id)+'.'+COL_NAME(fc.referenced_object_id, fc.referenced_column_id)
                    end + char(10)
     , @ptn1 = OBJECT_NAME (f.referenced_object_id)
     , @ptn2  = object_name(f.parent_object_id)
FROM   sys.foreign_keys AS f
       INNER JOIN
       sys.foreign_key_columns AS fc ON f.object_id = fc.constraint_object_id
WHERE  f.parent_object_id = OBJECT_ID(@TableToDelete); -- CHANGE here schema.table_name
--
print  '--Table Depended on ' + @ptn2 + char(10) + @sql1