alter table dbo.deposits
add constraint fk_deposits_currencies
    foreign key (currency_id) references dbo.currencies(currency_id)
go

alter table dbo.deposits
add constraint ak_deposit_payment_period_currency_id_term_rate
    unique (deposit, payment_period, currency_id, term, rate)
go