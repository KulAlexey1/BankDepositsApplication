create procedure [dbo].[usp_largest_deposit_for_year]
    @year int,
    @result decimal(30, 2) output
as
begin
    select @result = max(dbo.ufn_convert_to_currency(a.currency_id, ao.amount, 1))
    from dbo.contracts cts
        join dbo.accounts a
            on cts.account_id = a.account_id
        join dbo.account_operations ao
            on a.account_id = ao.account_id
    where year(cts.conclusion_date) = @year and year(ao.date_time) = @year and ao.[type_id] = 1;
end
go