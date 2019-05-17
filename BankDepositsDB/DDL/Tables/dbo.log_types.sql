create table [dbo].[log_types]
(
	log_type_id int not null,
	log_type nvarchar(100) not null,
	constraint pk_log_types primary key (log_type_id)
)
go