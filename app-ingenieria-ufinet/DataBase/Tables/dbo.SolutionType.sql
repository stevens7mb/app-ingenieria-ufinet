﻿IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ConfirmedArea')
BEGIN
	CREATE TABLE dbo.ConfirmedArea (
		IdConfirmedArea INT NOT NULL,
		Description VARCHAR(100) NOT NULL,
		CONSTRAINT PK_ConfirmedArea PRIMARY KEY (IdConfirmedArea)
	)
END