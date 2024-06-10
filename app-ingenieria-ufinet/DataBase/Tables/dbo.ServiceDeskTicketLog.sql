IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ServiceDeskTicketLog')
BEGIN
	CREATE TABLE dbo.ServiceDeskTicketLog
	(
		IdTicketLog INT IDENTITY(1,1),
		IdPrefix INT,
		IdTicket INT,
		Comment VARCHAR(500) NOT NULL,
		EngineerId INT NOT NULL,
		RegistryDate DATETIME NOT NULL,
		CONSTRAINT PK_ServiceDeskTicketLog PRIMARY KEY(IdTicketLog),
		CONSTRAINT FK_ServiceDeskTicketLogIdTicket FOREIGN KEY(IdPrefix, IdTicket) REFERENCES ServiceDeskTicket(IdPrefix, IdTicket),
		CONSTRAINT FK_ServiceDeskTicketLogEngineerId FOREIGN KEY(EngineerId) REFERENCES Engineer(EngineerId)	
	)
END