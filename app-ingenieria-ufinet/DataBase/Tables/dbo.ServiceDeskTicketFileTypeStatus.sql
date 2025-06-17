IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ServiceDeskTicketFileTypeStatus')
BEGIN
	CREATE TABLE dbo.ServiceDeskTicketFileTypeStatus (
		TypeStatusId INT,
		Description VARCHAR(500) NOT NULL,
		CONSTRAINT PK_ServiceDeskTicketFileTypeStatus PRIMARY KEY(TypeStatusId)
	)
END