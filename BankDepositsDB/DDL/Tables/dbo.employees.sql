create table [dbo].[employees]
(
    employee_id int not null identity(1,1),
    passport_id int not null,
    address_id int not null,
    phone_number_id int not null,
    email nvarchar(254) null,
    position_id int not null,
    [start_date] date not null,
    [end_date] date null,
    [password] nvarchar(20) not null,
    constraint pk_employees primary key (employee_id)
)
go