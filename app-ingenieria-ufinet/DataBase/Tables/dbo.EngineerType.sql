IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'EngineerType')
BEGIN
    CREATE TABLE dbo.EngineerType (
        EngineerTypeId INT,
        TypeName VARCHAR(50) NOT NULL,
        CONSTRAINT PK_EngineerType PRIMARY KEY(EngineerTypeId)
    )
END