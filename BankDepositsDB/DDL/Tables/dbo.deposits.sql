create table [dbo].[deposits]
(
    deposit_id int not null identity(1,1),
    deposit nvarchar(255) not null,
    payment_period decimal(5, 2) not null,
    currency_id int not null,
    term decimal(5, 2) not null,
    rate decimal(10, 7) not null,
    constraint pk_deposits primary key (deposit_id)
)
go