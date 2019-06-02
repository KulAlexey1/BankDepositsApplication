declare @sql_command nvarchar(max)
declare command_cursor cursor for
select concat(N'create trigger ', t.table_schema, '.tgr_', t.table_name, '_delete ',
    'on ', t.full_table_name, ' after delete as begin ',
    'declare @log_header nvarchar(MAX) = ''Delete from ', t.full_table_name, '.'';',
    'declare @json nvarchar(max) = (select * from deleted d for json path, without_array_wrapper);',
    'insert into dbo.logs(log_type_id, log_text) values (10, concat(@log_header, '' '', @json, ''.'')); end ')
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