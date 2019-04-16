create table [dbo].[deposits]
(
    deposit_id int not null identity(1,1),
    deposit nvarchar(255) not null,     
    payment_period int not null default 0,
    currency_id int not null,
    deposit_term_id int not null,
    rate decimal(4, 2) not null,
    account_replenishment bit not null,
    constraint pk_deposits primary key (deposit_id)
)
go