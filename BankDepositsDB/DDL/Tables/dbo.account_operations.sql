create table [dbo].[account_operations]
(
    account_operation_id int not null identity(1, 1),
    account_id int not null,
    [type_id] int not null,
    date_time datetime not null,
    amount decimal(30, 2) not null,
    constraint pk_account_operations primary key (account_operation_id)
)
go