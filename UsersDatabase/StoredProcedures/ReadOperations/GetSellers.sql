﻿CREATE PROCEDURE [dbo].[GetSellers]
	AS
	SELECT *
	from Sellers inner join Users
	on Sellers.[User_Id]=Users.Id
