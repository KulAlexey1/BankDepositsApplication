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

:r .\DML\Inserts\dbo.insert_employee_positions.sql
:r .\DML\Inserts\dbo.insert_account_operation_types.sql
:r .\DML\Inserts\dbo.insert_citizenships.sql
:r .\DML\Inserts\dbo.insert_currencies.sql
:r .\DML\Inserts\dbo.insert_currency_conversions.sql
:r .\DML\Inserts\dbo.insert_locality_types.sql
:r .\DML\Inserts\dbo.insert_log_types.sql

:r .\DML\Triggers\dbo.tgr_insert.sql
:r .\DML\Triggers\dbo.tgr_update.sql
:r .\DML\Triggers\dbo.tgr_delete.sql