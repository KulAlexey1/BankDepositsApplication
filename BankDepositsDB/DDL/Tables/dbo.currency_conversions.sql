create table [dbo].[currency_conversions]
(
    currency_id int not null,
    buy bit not null,
    amount_in_native_currency_per_unit decimal(10, 5) not null,
    constraint pk_currency_conversion primary key (currency_id, buy, amount_in_native_currency_per_unit)
)
go