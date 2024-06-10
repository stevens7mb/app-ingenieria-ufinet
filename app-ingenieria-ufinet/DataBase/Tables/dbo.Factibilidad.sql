IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Factibilidad')
BEGIN
	CREATE TABLE dbo.Factibilidad (
		IdFactibilidad int IDENTITY(1,1) NOT NULL,
		Ticket int NOT NULL,
		Estudio varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
		IdCliente int NOT NULL,
		IdKAM int NOT NULL,
		BW int NULL,
		FechaSolicitud datetime NULL,
		FechaRespuesta datetime NULL,
		Estado int DEFAULT -1 NULL,
		SitioConCobertura int NULL,
		SitioConCoberturaParcial int NULL,
		SitioSinCobertura int NULL,
		IdIngeniero int NOT NULL,
		IdTipoServicio int NULL,
		CONSTRAINT PK_Factibilidad_IdFactibilidadTicket PRIMARY KEY (IdFactibilidad,Ticket)
	);
	
	ALTER TABLE db_a73217_ufinet.dbo.Factibilidad ADD CONSTRAINT FK__Factibili__IdTip__57A801BA FOREIGN KEY (IdTipoServicio) REFERENCES TipoServicio(IdTipoServicio);
	ALTER TABLE db_a73217_ufinet.dbo.Factibilidad ADD CONSTRAINT fk_Cliente_Factibilidad FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente);
	ALTER TABLE db_a73217_ufinet.dbo.Factibilidad ADD CONSTRAINT fk_KAM_Factibilidad FOREIGN KEY (IdKAM) REFERENCES KAM(IdKAM);
	ALTER TABLE db_a73217_ufinet.dbo.Factibilidad ADD CONSTRAINT fk_Provisioning_Factibilidad FOREIGN KEY (IdIngeniero) REFERENCES Provisioning(IdIngeniero);
END

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Factibilidad' AND COLUMN_NAME = 'idSucursal')
BEGIN
    ALTER TABLE dbo.Factibilidad ADD idSucursal INT;
    
	ALTER TABLE dbo.Factibilidad ADD CONSTRAINT FK_Factibilidad_Sucursal FOREIGN KEY (idSucursal) REFERENCES Sucursal(idSucursal);

	UPDATE dbo.Factibilidad SET idSucursal = 1;
END;