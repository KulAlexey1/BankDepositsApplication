/*
Post-Deployment Script Template                            
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.        
 Use SQLCMD syntax to include a file in the post-deployment script.            
 Example:      :r .\myfile.sql                                
 Use SQLCMD syntax to reference a variable in the post-deployment script.        
 Example:      :setvar TableName MyTable                            
               SELECT * FROM [$(TableName)]                    
--------------------------------------------------------------------------------------
*/

insert into dbo.employee_positions
values (1, N'Employee'),
       (5, N'Cashier'),
       (10, N'Manager'),
       (15, N'Admin')
GO
insert into dbo.account_operation_types
values (1, N'Credit'),
       (5, N'Debit')
GO
insert into dbo.citizenships
values (1, N'PB'),
       (5, N'BA'),
       (10, N'BI')
GO
insert into dbo.currencies
values (1, N'BYN'),
       (5, N'USD'),
       (10, N'EUR'),
       (15, N'RUB')
GO
insert into dbo.currency_conversions
values (5, 0, 2.1130),
       (5, 1, 2.0770),
       (10, 0, 2.3530),
       (10, 1, 2.3170),
       (15, 0, 0.03236)
GO

insert into dbo.currency_conversions values (15, 1, 0.03174)
GO
insert into dbo.locality_types
values (1, N'Agro-city'),
       (5, N'City'),
       (10, N'Urban village')
GO
insert into dbo.log_types
values (1, N'INSERT'),
       (5, N'UPDATE'),
       (10, N'DELETE')
GO

declare @sql_command nvarchar(max)
declare command_cursor cursor for
select concat(N'create trigger ', t.table_schema, '.tgr_', t.table_name, '_insert ',
    'on ', t.full_table_name, ' after insert as begin ',
    'declare @log_header nvarchar(MAX) = ''Insert into ', t.full_table_name, '.'';',
    'declare @json nvarchar(max) = (select * from inserted i for json path, without_array_wrapper);',
    'insert into dbo.logs(log_type_id, log_text) values (1, concat(@log_header, '' '', @json, ''.'')); end ')
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
GO
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
GO
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
GO
