alter table dbo.account_operations
add constraint fk_account_operations_account
    foreign key (account_id) references dbo.accounts(account_id)
go

alter table dbo.account_operations
add constraint fk_account_operations_account_operation_types
    foreign key ([type_id]) references dbo.account_operation_types(account_operation_type_id)
go