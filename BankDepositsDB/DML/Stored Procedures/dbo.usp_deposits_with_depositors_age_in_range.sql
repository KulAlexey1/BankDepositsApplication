create procedure [dbo].[usp_deposits_with_depositors_age_in_range]
    @min_age int,
    @max_age int
as
begin
    select dtr.depositor_id, p.last_name, p.first_name, p.middle_name, p.birth_date,
        dt.deposit_id, dt.deposit, dt.currency_id, dt.deposit_term_id, dt.rate,
        dt.payment_period, dt.account_replenishment, c.conclusion_date, c.termination_date, a.amount
    from dbo.deposits dt
        join dbo.contracts c on dt.deposit_id = c.deposit_id
        join dbo.accounts a on c.account_id = a.account_id        
        join dbo.depositors dtr on c.depositor_id = dtr.depositor_id
        join dbo.passports p on dtr.passport_id = p.passport_id
    where dbo.ufn_get_age(p.birth_date) >= @min_age and dbo.ufn_get_age(p.birth_date) <= @max_age
    order by p.last_name, p.first_name, p.middle_name;
end
go