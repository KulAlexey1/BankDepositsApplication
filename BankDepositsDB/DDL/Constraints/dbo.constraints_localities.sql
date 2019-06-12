alter table dbo.localities
add constraint fk_localities_locality_types
    foreign key (locality_type_id) references dbo.locality_types(locality_type_id)
go

alter table dbo.localities
add constraint ak_locality_type_region_locality
    unique (locality_type_id, region, locality)
go