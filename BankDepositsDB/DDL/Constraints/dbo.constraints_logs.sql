alter table dbo.logs
add constraint fk_logs_log_types
	foreign key (log_type_id) references dbo.log_types(log_type_id)
go

alter table dbo.logs
add constraint df_logs_log_datetime
	default getutcdate() for log_datetime
go