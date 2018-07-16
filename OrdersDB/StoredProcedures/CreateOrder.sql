CREATE PROCEDURE [dbo].[CreateOrder]
	@Date DATE,
	@Address VARCHAR(50), 
    @CellPhone VARBINARY(50), 
    @Quantity INT, 
    @TotalAmount MONEY
AS
	insert into Orders([Date], [Address], CellPhone, Quantity, TotalAmount)
	Values (@Date, @Address, @CellPhone, @Quantity, @TotalAmount)
GO
