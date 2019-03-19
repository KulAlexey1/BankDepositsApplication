create table [dbo].[issuing_authorities]
(
    issuing_authority_id int not null,
    issuing_authority nvarchar(100) not null,
    constraint pk_issuing_authorities primary key (issuing_authority_id)
)