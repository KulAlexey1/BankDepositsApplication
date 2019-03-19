create table [dbo].[contracts]
(
    contract_id int not null identity(1, 1),
    deposit_id int not null,
    depositor_id int not null,
    employee_id int not null,
    account_id int not null,
    conclusion_date date not null,
    termination_date date null,
    constraint pk_contracts primary key (contract_id)
)
go