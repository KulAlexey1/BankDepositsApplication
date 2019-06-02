declare @sql_command nvarchar(max)
declare command_cursor cursor for
select concat(N'create trigger ', t.table_schema, '.tgr_', t.table_name, '_update ',
    'on ', t.full_table_name, ' after update as begin ',
    'declare @log_header nvarchar(MAX) = ''Update ', t.full_table_name, '.''; ',
    'select ''false'' as is_new, * into #temp_table from deleted d ',
    'union all select ''true'' as is_new, * from inserted i; ',
    'declare @json nvarchar(max) = (select * from #temp_table for json auto); ',
    'insert into dbo.logs(log_type_id, log_text) values (5, concat(@log_header, '' '', @json, ''.'')); ',
    'drop table #temp_table; end ')
from
(select table_schema, table_name, concat(table_schema, '.', table_name) as full_table_name
from information_schema.tables
where table_name not in ('sysdiagrams', 'logs')) as t;

open command_cursor

fetch next from command_cursor
into @sql_command

while @@FETCH_STATUS = 0
begin
    exec sp_executesql @sql_command

    fetch next from command_cursor
    into @sql_command
end

close command_cursor;  
deallocate command_cursor;
go