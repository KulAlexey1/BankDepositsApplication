create table [dbo].[currencies]
(
    currency_id int not null,
    currency nvarchar(100) not null,
    constraint pk_currencies primary key (currency_id)
)
go