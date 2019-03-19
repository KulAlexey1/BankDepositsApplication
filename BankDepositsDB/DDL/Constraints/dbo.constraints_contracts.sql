alter table dbo.contracts
add constraint fk_contracts_deposits
    foreign key (deposit_id) references dbo.deposits(deposit_id)
go

alter table dbo.contracts
add constraint fk_contracts_depositors
    foreign key (depositor_id) references dbo.depositors(depositor_id)
go

alter table dbo.contracts
add constraint fk_contracts_employees
    foreign key (employee_id) references dbo.employees(employee_id)
go

alter table dbo.contracts
add constraint fk_contracts_accounts
    foreign key (account_id) references dbo.accounts(account_id)
go

alter table dbo.contracts
add constraint ak_contracts_account_id unique (account_id)
go

alter table dbo.contracts
add constraint check_contracts_conclusion_date_termination_date
    check (termination_date > conclusion_date)
go