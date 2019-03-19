create table [dbo].[passports]
(
    passport_id int not null identity(1,1),
    first_name nvarchar(50) not null,
    middle_name nvarchar(50) not null,
    last_name nvarchar(50) not null,
    birth_date date not null,
    gender bit not null,
    citizenship_id int not null,
    number nvarchar(20) not null,
    identification_number nvarchar(20) not null,
    issuing_authority_id int not null,
    issuing_authority_locality_id int not null,
    issue_date date not null,
    expiration_date date not null,
    constraint pk_passports primary key (passport_id)
)
go