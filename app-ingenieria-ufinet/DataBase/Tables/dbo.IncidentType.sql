﻿IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'IncydentType')
BEGIN
	CREATE TABLE dbo.IncidentType (
		IdIncidentType INT IDENTITY(1,1) NOT NULL,
		Description VARCHAR(100) NULL,
		CONSTRAINT PK_IncydentType PRIMARY KEY(IdIncidentType)
	)
END