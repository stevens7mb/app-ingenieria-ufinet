IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Engineer')
BEGIN
    CREATE TABLE dbo.Engineer (
        EngineerId INT NOT NULL,
        Name VARCHAR(100) NULL,
        UserName VARCHAR(100) NOT NULL,
        ActiveStatus SiNo DEFAULT -1 NULL,
        IdRegion INT NOT NULL,
        EngineerTypeId INT NOT NULL,
        CONSTRAINT PK_Engineer PRIMARY KEY (EngineerId),
        CONSTRAINT FK_EngineerEngineerType FOREIGN KEY (EngineerTypeId) REFERENCES EngineerType (EngineerTypeId),
        CONSTRAINT fk_EngineerUsuario FOREIGN KEY(UserName) REFERENCES Usuario(Usuario),
        CONSTRAINT fk_EngineerRegion FOREIGN KEY(IdRegion) REFERENCES Region(IdRegion)
    )
END