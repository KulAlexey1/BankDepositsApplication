create procedure [dbo].[usp_depositors_with_account_replenishment_deposits_for_year]
    @year int
as
begin
    select dtr.depositor_id, p.last_name, p.first_name, p.middle_name
    from dbo.depositors dtr join dbo.passports p on dtr.passport_id = p.passport_id
    where exists (select 1
           from dbo.contracts c join dbo.deposits d on c.deposit_id = d.deposit_id
           where c.depositor_id = dtr.depositor_id
               and year(c.conclusion_date) = @year and d.account_replenishment = 1)    
    order by p.last_name, p.first_name, p.middle_name;
end
go