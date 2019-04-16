create procedure [dbo].[usp_employees_with_deposit_contract_for_month]
    @year int,
    @month int
as
begin
    select e.employee_id, p.last_name, p.first_name, p.middle_name,
    d.deposit_id, d.deposit, c.conclusion_date
    from dbo.contracts c
        right join dbo.employees e on c.employee_id = e.employee_id
        join dbo.passports p on e.passport_id = e.passport_id
        join dbo.deposits d on c.deposit_id = d.deposit_id
    where year(c.conclusion_date) = @year and month(c.conclusion_date) = @month
        and c.employee_id is not null
    order by p.last_name, p.first_name, p.middle_name, c.conclusion_date desc;
end
go
