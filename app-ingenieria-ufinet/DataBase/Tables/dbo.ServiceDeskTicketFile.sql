IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ServiceDeskTicketFile')
BEGIN
	CREATE TABLE dbo.ServiceDeskTicketFile (
		TicketFileId INT IDENTITY(1,1),
		NameFile VARCHAR(500) NOT NULL,
		PathFile VARCHAR(500) NOT NULL,
		FileSize BIGINT NOT NULL,
		IdPrefix INT,
		IdTicket INT NOT NULL,
		CONSTRAINT PK_ServiceDeskTicketFile PRIMARY KEY(TicketFileId),
		CONSTRAINT FK_ServiceDeskTicket FOREIGN KEY(IdPrefix, IdTicket) REFERENCES ServiceDeskTicket(IdPrefix, IdTicket)
	)
END