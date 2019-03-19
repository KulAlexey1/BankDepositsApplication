alter table dbo.phone_numbers
add constraint fk_phone_numbers_phone_number_operators
    foreign key (operator_id) references dbo.phone_number_operators(phone_number_operator_id)
go

alter table dbo.phone_numbers
add constraint ak_operator_operator_code_phone_number unique (operator_id, operator_code, phone_number)
go