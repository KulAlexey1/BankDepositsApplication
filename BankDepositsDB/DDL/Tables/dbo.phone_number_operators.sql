create table [dbo].[phone_number_operators]
(
    phone_number_operator_id int not null identity(1, 1),
    phone_number_operator nvarchar(100) not null,
    constraint pk_phone_number_operators primary key (phone_number_operator_id)
)
go