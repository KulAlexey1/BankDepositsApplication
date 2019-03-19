alter table dbo.employees
add constraint fk_employees_passports
    foreign key (passport_id) references dbo.passports(passport_id)
go

alter table dbo.employees
add constraint fk_employees_addresses
    foreign key (address_id) references dbo.addresses(address_id)
go

alter table dbo.employees
add constraint fk_employees_phone_numbers
    foreign key (phone_number_id) references dbo.phone_numbers(phone_number_id)
go

alter table dbo.employees
add constraint fk_employees_employee_positions
    foreign key (position_id) references dbo.employee_positions(employee_position_id)
go

alter table dbo.employees
add constraint ak_employees_passport_id unique (passport_id)
go

alter table dbo.employees
add constraint check_employees_start_date_end_date check ([end_date] > [start_date])
go