alter table dbo.issuing_authorities
add constraint ak_issuing_authority unique (issuing_authority)
go