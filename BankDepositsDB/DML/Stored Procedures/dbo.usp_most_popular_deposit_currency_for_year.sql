create procedure [dbo].[usp_most_popular_deposit_currency_for_year]  
    @year int,
    @result int output
as
begin
    select @result = r.currency_id
    from (select top 1 d.currency_id, count(d.currency_id) as deposit_currency_count
          from dbo.contracts cts
              join dbo.deposits d on cts.deposit_id = d.deposit_id
          where year(cts.conclusion_date) = @year
          group by d.currency_id
          order by deposit_currency_count DESC) r;
end
go