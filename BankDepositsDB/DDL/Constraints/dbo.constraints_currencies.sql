alter table dbo.currencies
add constraint ak_currency unique (currency)
go