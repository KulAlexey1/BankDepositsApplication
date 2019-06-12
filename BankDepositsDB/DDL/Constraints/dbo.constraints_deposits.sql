alter table dbo.deposits
add constraint fk_deposits_currencies
    foreign key (currency_id) references dbo.currencies(currency_id)
go

alter table dbo.deposits
add constraint fk_deposits_deposit_terms
    foreign key (deposit_term_id) references dbo.deposit_terms(deposit_term_id)
go

alter table dbo.deposits
add constraint ak_deposit_payment_period_currency_id_term_rate
    unique (deposit, payment_period, currency_id, deposit_term_id, rate, account_replenishment)
go