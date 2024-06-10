﻿IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Region')
BEGIN
	CREATE TABLE dbo.Region (
		IdRegion INT,
		Description VARCHAR(100),
		CONSTRAINT PK_Region PRIMARY KEY(IdRegion)
	)
END