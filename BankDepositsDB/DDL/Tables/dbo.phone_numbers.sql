create table [dbo].[phone_numbers]
(
    phone_number_id int not null identity(1,1),
    operator_id int not null,
    operator_code int not null,
    phone_number int not null,
    constraint pk_phone_numbers primary key (phone_number_id)
)
go