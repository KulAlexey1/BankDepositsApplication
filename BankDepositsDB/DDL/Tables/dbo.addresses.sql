create table [dbo].[addresses]
(
    address_id int not null identity,
    street_id int not null,
    house int not null,
    housing nvarchar(25) null,
    apartment int not null,
    constraint pk_addresses primary key (address_id)
)
go