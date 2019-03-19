create table [dbo].[streets]
(
    street_id int not null identity(1,1),
    locality_id int not null,
    street nvarchar(255) not null,
    postcode int null,
    constraint pk_streets primary key (street_id)
)
go