create table [dbo].[depositors]
(
    depositor_id int not null identity(1,1),
    passport_id int not null,
    address_id int not null,
    phone_number_id int not null,
    email nvarchar(254) null,
    constraint pk_depositors primary key (depositor_id)
)
go