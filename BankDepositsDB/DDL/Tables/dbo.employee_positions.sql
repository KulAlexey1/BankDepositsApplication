create table [dbo].[employee_positions]
(
    employee_position_id int not null,
    employee_position nvarchar(100) not null,
    constraint pk_employee_positions primary key (employee_position_id)
)
go