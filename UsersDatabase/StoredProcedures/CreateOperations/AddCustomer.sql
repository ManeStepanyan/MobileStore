﻿CREATE PROCEDURE [dbo].[AddCustomer]
	@Name varchar(30),
	@Surname varchar(30),
	@Login varchar(40),
	@Password varchar(50),
	@Email varchar(40)
AS
	begin
    declare @userId int
    execute @userId = AddUser @Login, @Password, @Email, 3
	Insert into dbo.Customers([User_Id],[Name],[Surname],[Status])
	Values(@userId,@Name,@Surname,0)
	end
