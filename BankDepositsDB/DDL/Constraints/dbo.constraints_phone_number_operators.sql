﻿alter table dbo.phone_number_operators
add constraint ak_phone_number_operator unique (phone_number_operator)
go