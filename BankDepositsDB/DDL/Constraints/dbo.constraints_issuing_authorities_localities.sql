alter table dbo.issuing_authorities_localities
add constraint fk_issuing_authorities_localities_issuing_authorities
    foreign key (issuing_authority_id) references dbo.issuing_authorities(issuing_authority_id)
go

alter table dbo.issuing_authorities_localities
add constraint fk_issuing_authorities_localities_localities
    foreign key (issuing_authority_locality_id) references dbo.localities(locality_id)
go