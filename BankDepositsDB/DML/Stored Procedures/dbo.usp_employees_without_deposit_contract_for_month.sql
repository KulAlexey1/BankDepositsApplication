create procedure [dbo].[usp_employees_without_deposit_contract_for_month]
    @year int,
    @month int
as
begin
    select e.employee_id, p.last_name, p.first_name, p.middle_name
    from dbo.employees e
        left join dbo.contracts c on e.employee_id = c.employee_id
        join dbo.passports p on e.passport_id = e.passport_id
    where year(c.conclusion_date) = @year and month(c.conclusion_date) = @month
        and c.employee_id is null
    order by p.last_name, p.first_name, p.middle_name;
end
go