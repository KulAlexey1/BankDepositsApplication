create function [dbo].[ufn_convert_to_currency]
(
    @convertible_currency_id int,
    @convertible_amount decimal(30, 4),
    @currency_id_to_convert int
)
returns decimal(30, 4)
as
begin
    declare @result decimal(30, 4)

    select @result = cc.amount_in_native_currency_per_unit * @convertible_amount
    from dbo.currency_conversion cc
    where cc.buy = 1 and cc.currency_id = @convertible_currency_id;

    if @currency_id_to_convert = 1
        return @result;
    else if @convertible_currency_id = 1
        set @result = @convertible_amount;

    select @result = @result / cc.amount_in_native_currency_per_unit
    from dbo.currency_conversion cc
    where cc.buy = 0 and cc.currency_id = @currency_id_to_convert;

    return @result;
end
go