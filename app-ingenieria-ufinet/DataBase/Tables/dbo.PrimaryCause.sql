﻿IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PrimaryCause')
BEGIN
	CREATE TABLE dbo.PrimaryCause
	(
		IdPrimaryCause INT IDENTITY(1,1),
		Description VARCHAR(100),
		CONSTRAINT PK_PrimaryCause PRIMARY KEY (IdPrimaryCause)
	)
END