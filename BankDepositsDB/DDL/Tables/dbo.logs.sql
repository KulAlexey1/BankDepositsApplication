create table [dbo].[logs]
(
	log_id int not null identity(1, 1),
	log_type_id int not null,
	log_text nvarchar(MAX) not null,
	log_datetime datetime not null,
	constraint pk_logs primary key (log_id)
)
go