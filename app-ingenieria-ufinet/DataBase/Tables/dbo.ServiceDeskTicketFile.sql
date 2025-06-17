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

IF NOT EXISTS (
	SELECT 1 
	FROM INFORMATION_SCHEMA.COLUMNS 
	WHERE TABLE_NAME = 'ServiceDeskTicketFile' 
	  AND COLUMN_NAME = 'TypeStatusId'
)
BEGIN
	ALTER TABLE dbo.ServiceDeskTicketFile ADD TypeStatusId INT NOT NULL DEFAULT 1; -- O el valor por defecto que uses
END

IF NOT EXISTS (
	SELECT 1 
	FROM sys.foreign_keys 
	WHERE name = 'FK_ServiceDeskTicketFileTypeStatus'
)
BEGIN
	ALTER TABLE dbo.ServiceDeskTicketFile ADD CONSTRAINT FK_ServiceDeskTicketFileTypeStatus FOREIGN KEY (TypeStatusId) REFERENCES ServiceDeskTicketFileTypeStatus(TypeStatusId);
END
