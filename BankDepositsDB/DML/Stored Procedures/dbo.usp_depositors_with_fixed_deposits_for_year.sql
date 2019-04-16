create procedure [dbo].[usp_depositors_with_fixed_deposits_for_year]
    @year int
as
begin
    select dtr.depositor_id, p.last_name, p.first_name, p.middle_name
    from dbo.depositors dtr join dbo.passports p on dtr.passport_id = p.passport_id
    where dtr.depositor_id in (select c.depositor_id
           from dbo.contracts c join dbo.deposits d on c.deposit_id = d.deposit_id
               and year(c.conclusion_date) = @year and d.payment_period = 0
           group by c.depositor_id)    
    order by p.last_name, p.first_name, p.middle_name;
end
go