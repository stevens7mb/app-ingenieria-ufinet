﻿IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TicketState')
BEGIN
	CREATE TABLE dbo.TicketState (
		IdTicketState INT NOT NULL,
		Description VARCHAR(100),
		CONSTRAINT PK_TicketState PRIMARY KEY(IdTicketState)
	)
END