alter table dbo.passports
add constraint fk_passports_citizenships
    foreign key (citizenship_id) references dbo.citizenships(citizenship_id)
go

alter table dbo.passports
add constraint fk_passports_issuing_authorities_localities
    foreign key (issuing_authority_id, issuing_authority_locality_id)
    references dbo.issuing_authorities_localities(issuing_authority_id, issuing_authority_locality_id)
go

alter table dbo.passports
add constraint ak_first_name_middle_name_last_name_birth_date_gender_issue_date
    unique (first_name, middle_name, last_name, birth_date, gender, issue_date)
go

alter table dbo.passports
add constraint ak_number unique (number)
go

alter table dbo.passports
add constraint ak_identification_number unique (identification_number)
go

alter table dbo.passports
add constraint check_passport_issue_date_expiration_date
    check (expiration_date > issue_date)
go