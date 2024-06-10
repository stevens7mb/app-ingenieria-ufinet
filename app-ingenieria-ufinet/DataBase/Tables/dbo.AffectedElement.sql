IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AffectedElement')
BEGIN
	CREATE TABLE dbo.AffectedElement
	(
		IdAffectedElement INT IDENTITY(1,1),
		Description VARCHAR(100),	
		CONSTRAINT PK_AffectedElement PRIMARY KEY(IdAffectedElement)
	)
END