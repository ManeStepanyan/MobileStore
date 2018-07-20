CREATE PROCEDURE [dbo].[CreateOrder]
	@ProductId int,
	@Date DATE,
	@Address VARCHAR(50), 
    @CellPhone VARBINARY(50), 
    @Quantity INT, 
    @TotalAmount MONEY
AS
	insert into Orders(Product_Id, [Date], [Address], CellPhone, Quantity, TotalAmount)
	Values (@ProductId, @Date, @Address, @CellPhone, @Quantity, @TotalAmount)
GO
