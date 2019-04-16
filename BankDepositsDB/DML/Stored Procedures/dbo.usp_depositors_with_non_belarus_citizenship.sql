create procedure [dbo].[usp_depositors_with_non_belarus_citizenship]    
as
begin
    select dtr.depositor_id, p.last_name, p.first_name, p.middle_name, p.citizenship_id
    from dbo.depositors dtr join dbo.passports p on dtr.passport_id = p.passport_id
    where p.citizenship_id != 1
    order by p.last_name, p.first_name, p.middle_name;
end
go