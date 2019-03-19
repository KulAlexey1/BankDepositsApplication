create table [dbo].[localities]
(
    locality_id int not null identity(1,1),
    locality_type_id int not null,
    region nvarchar(255) not null,
    locality nvarchar(255) not null,
    postcode int null,
    constraint pk_localities primary key (locality_id)
)
go