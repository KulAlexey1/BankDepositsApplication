create table [dbo].[issuing_authorities_localities]
(
    issuing_authority_id int not null,
    issuing_authority_locality_id int not null,
    constraint pk_issuing_authorities_localities primary key (issuing_authority_id, issuing_authority_locality_id)
)
go