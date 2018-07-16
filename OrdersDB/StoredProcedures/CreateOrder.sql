CREATE PROCEDURE [dbo].[CreateOrder]
	@Date DATE,
	@Address VARCHAR(50), 
    @CellPhone VARBINARY(50), 
    @Quantity INT, 
    @Product_ID INT, 
    @Customer_ID INT, 
    @TotalAmount MONEY
AS
	insert into Orders([Date], [Address], CellPhone, Quantity, Product_ID, Customer_ID, TotalAmount)
	Values (@Date, @Address, @CellPhone, @Quantity, @Product_ID, @Customer_ID, @TotalAmount)
GO
