﻿CREATE PROCEDURE [dbo].[UpdateSeller]

	@Name varchar(25),
	@Address varchar(60),
	@CellPhone varchar(50),
	@Login varchar(30),
	@Password varchar(25)
AS
	Update Sellers
	set [Name]= @Name, [Address]=@Address, [CellPhone]=@CellPhone, [Login]=@Login, [Password]=@Password
	where [Login]=@Login
Go
