CREATE PROCEDURE [dbo].[GetOrdersByTimeSpan]
	@start date,
	@end date
AS
	select * from Orders where [Date]>@start and [Date]<@end
GO
