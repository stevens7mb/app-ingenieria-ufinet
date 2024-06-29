﻿IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Prefix')
BEGIN
	CREATE TABLE dbo.Prefix
	(
		IdPrefix INT,
		PrefixDesc VARCHAR(10),	
		CONSTRAINT PK_Prefix PRIMARY KEY(IdPrefix)
	)
END