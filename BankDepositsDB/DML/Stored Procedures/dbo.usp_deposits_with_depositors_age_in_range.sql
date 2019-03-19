CREATE PROCEDURE [dbo].[usp_any_select_with_subselect]
    @param1 int = 0,
    @param2 int
AS
    SELECT @param1, @param2
RETURN 0
