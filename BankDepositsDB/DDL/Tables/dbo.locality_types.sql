create table [dbo].[locality_types]
(
    locality_type_id int not null,
    locality_type nvarchar(100) not null,
    constraint pk_locality_types primary key (locality_type_id)
)
go