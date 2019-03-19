alter table dbo.depositors
add constraint fk_depositors_passport
    foreign key (passport_id) references dbo.passports(passport_id)
go

alter table dbo.depositors
add constraint fk_depositors_addresses
    foreign key (address_id) references dbo.addresses(address_id)
go

alter table dbo.depositors
add constraint fk_depositors_phone_numbers
    foreign key (phone_number_id) references dbo.phone_numbers(phone_number_id)
go

alter table dbo.depositors
add constraint ak_depositor_passport_id unique (passport_id)
go