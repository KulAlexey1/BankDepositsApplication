alter table dbo.accounts
add constraint fk_accounts_currencies
    foreign key (currency_id) references dbo.currencies(currency_id)
go

alter table dbo.accounts
add constraint check_accounts_opening_date_closing_date
    check (closing_date > opening_date)
go