CREATE PROCEDURE [dbo].[CreateOrder]
	@Product_Id int,
	@Date DATE,
	@Address VARCHAR(50), 
    @CellPhone VARBINARY(50), 
    @Quantity INT, 
    @TotalAmount MONEY
AS
	insert into Orders(Product_Id, [Date], [Address], CellPhone, Quantity, TotalAmount)
	Values (@Product_Id, @Date, @Address, @CellPhone, @Quantity, @TotalAmount)
GO
