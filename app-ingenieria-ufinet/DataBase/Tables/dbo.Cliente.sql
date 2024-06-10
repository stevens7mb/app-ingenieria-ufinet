IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Cliente')
BEGIN
	CREATE TABLE Cliente (
		IdCliente int NOT NULL,
		Nombre varchar(100) NULL,
		AreaEstudio varchar(100) NULL,
		Estado int DEFAULT -1 NULL,
		CONSTRAINT PK_Cliente_IdCliente PRIMARY KEY (IdCliente)
	);
END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Cliente' AND COLUMN_NAME = 'idSucursal')
BEGIN
    ALTER TABLE dbo.Cliente ADD idSucursal INT;
    
	ALTER TABLE dbo.Cliente ADD CONSTRAINT FK_Cliente_Sucursal FOREIGN KEY (idSucursal) REFERENCES Sucursal(idSucursal);

	UPDATE dbo.Cliente SET idSucursal = 1 WHERE AreaEstudio = 'Guatemala'
END;