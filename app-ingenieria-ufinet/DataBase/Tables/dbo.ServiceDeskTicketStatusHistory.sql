IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ServiceDeskTicketStatusHistory')
BEGIN
	CREATE TABLE dbo.ServiceDeskTicketStatusHistory (
		IdTicketStatusHistory INT IDENTITY(1,1),
		IdPrefix INT,
		IdTicket INT,
		IdTicketState INT NOT NULL,
		ChangeDateTime DATETIME NOT NULL,
		ChangedByEngineerId INT NOT NULL,
		ChangedByUser VARCHAR(100),
		CONSTRAINT PK_ServiceDeskTicketStatusHistory PRIMARY KEY(IdTicketStatusHistory),
		CONSTRAINT FK_ServiceDeskTicketStatusHistoryIdTicket FOREIGN KEY(IdPrefix, IdTicket) REFERENCES ServiceDeskTicket(IdPrefix, IdTicket),
		CONSTRAINT FK_ServiceDeskTicketStatusHistoryIdTicketState FOREIGN KEY(IdTicketState) REFERENCES TicketState(IdTicketState),
		CONSTRAINT FK_ServiceDeskTicketStatusHistoryChangedByEngineerId FOREIGN KEY(ChangedByEngineerId) REFERENCES Engineer(EngineerId),
		CONSTRAINT FK_ServiceDeskTicketStatusHistoryChangedByUser FOREIGN KEY(ChangedByUser) REFERENCES Usuario(Usuario),
	)
END