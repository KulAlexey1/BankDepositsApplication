alter table dbo.deposit_terms
add constraint ak_deposit_term_days_months_years unique (days_amount, months_amount, years_amount)
go

alter table dbo.deposit_terms
add constraint check_deposit_term_days_months_years
    check ((days_amount + months_amount + years_amount != 0) 
        and (days_amount + months_amount + years_amount) in (days_amount, months_amount, years_amount))
go