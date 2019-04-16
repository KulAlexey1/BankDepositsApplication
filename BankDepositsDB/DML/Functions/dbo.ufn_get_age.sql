create function [dbo].[ufn_get_age](@birth_date date)
returns int
as
begin
    declare @current_date date = getdate();

    declare @birth_year int = year(@birth_date);
    declare @current_year int = year(@current_date); 

    declare @age int = @current_year - @birth_year;

    declare @birth_month int = month(@birth_date);
    declare @current_month int = month(@current_date);
    declare @month_diff int = @current_month - @birth_month; 
    
    if @month_diff < 0
        return @age - 1;
    else if @month_diff > 0
        return @age;
    else if @month_diff = 0 and day(@current_date) - day(@birth_date) < 0    
        return @age - 1;
    return @age;
end
go