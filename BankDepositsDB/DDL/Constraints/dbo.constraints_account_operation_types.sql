﻿alter table dbo.account_operation_types
add constraint ak_account_operation_type unique (account_operation_type)
go