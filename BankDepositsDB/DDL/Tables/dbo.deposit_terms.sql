create table [dbo].[deposit_terms]
(
    deposit_term_id int not null,
    days_amount int not null default 0,
    months_amount int not null default 0,
    years_amount int not null default 0,
    constraint pk_deposit_terms primary key (deposit_term_id)
)
go