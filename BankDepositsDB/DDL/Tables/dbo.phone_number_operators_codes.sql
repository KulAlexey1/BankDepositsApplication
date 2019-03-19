create table [dbo].[phone_number_operators_codes]
(
    phone_number_operator_id int not null,
    phone_number_operator_code int not null,
    constraint pk_phone_number_operators_codes primary key (phone_number_operator_id, phone_number_operator_code)
)
go