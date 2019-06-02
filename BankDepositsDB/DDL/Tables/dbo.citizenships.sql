create table [dbo].[citizenships]
(
    citizenship_id int not null,
    citizenship nvarchar(100) not null,
    constraint pk_citizenships primary key (citizenship_id)
)
go