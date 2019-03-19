create table [dbo].[account_operation_types]
(
    account_operation_type_id int not null identity(1, 1),
    account_operation_type nvarchar(100) not null,
    constraint pk_account_operation_types primary key (account_operation_type_id)
)
go