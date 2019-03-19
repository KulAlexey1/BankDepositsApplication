alter table dbo.locality_types
add constraint ak_locality_type unique (locality_type)
go