﻿IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'KAM')
BEGIN
	CREATE TABLE KAM (
		IdKAM int NOT NULL,
		Nombre varchar(100) NULL,
		Usuario varchar(100) NULL,
		Estado int DEFAULT -1 NULL,
		CONSTRAINT PK_KAM_IdKAM PRIMARY KEY (IdKAM)
	);
END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'KAM' AND COLUMN_NAME = 'idSucursal')
BEGIN
    ALTER TABLE dbo.KAM ADD idSucursal INT;
    
	ALTER TABLE dbo.KAM ADD CONSTRAINT FK_KAM_Sucursal FOREIGN KEY (idSucursal) REFERENCES Sucursal(idSucursal);

	UPDATE dbo.KAM SET idSucursal = 1
END;