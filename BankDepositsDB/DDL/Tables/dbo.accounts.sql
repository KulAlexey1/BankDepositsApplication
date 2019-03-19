create table [dbo].[accounts]
(
    account_id int not null identity(1, 1),
    currency_id int not null,
    opening_date date not null,
    closing_date date null,
    amount decimal(30, 2) not null,
    constraint pk_accounts primary key (account_id)
)
go