alter table dbo.addresses
add constraint fk_addresses_streets
    foreign key (street_id) references dbo.streets(street_id)
go

alter table dbo.addresses
add constraint ak_street_house_housing_apartment
    unique (street_id, house, housing, apartment)
go