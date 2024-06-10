IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Municipality')
BEGIN
    CREATE TABLE dbo.Municipality(
        IdMunicipality INT IDENTITY(1,1) NOT NULL,
        Name VARCHAR(45) NOT NULL,
        IdState INT NOT NULL,
        CONSTRAINT PK_Municipality PRIMARY KEY(IdMunicipality),
        CONSTRAINT FK_State FOREIGN KEY (IdState) REFERENCES State (IdState)
    )
END