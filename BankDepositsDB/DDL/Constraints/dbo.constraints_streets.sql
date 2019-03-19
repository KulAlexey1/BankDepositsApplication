alter table dbo.streets
add constraint fk_streets_locality
    foreign key (locality_id) references dbo.localities(locality_id)
go

alter table dbo.streets
add constraint ak_locality_street_postcode unique (locality_id, street, postcode)
go