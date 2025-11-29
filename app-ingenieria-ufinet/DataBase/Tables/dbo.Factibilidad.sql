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

------------------------------------------------------------
-- 1. Eliminar PK si existe
------------------------------------------------------------
IF EXISTS (
    SELECT 1 
    FROM sys.key_constraints
    WHERE name = 'PK_Factibilidad_IdFactibilidadTicket'
      AND parent_object_id = OBJECT_ID('dbo.Factibilidad')
)
BEGIN
    ALTER TABLE dbo.Factibilidad
    DROP CONSTRAINT PK_Factibilidad_IdFactibilidadTicket;
END;

------------------------------------------------------------
-- 2. Alterar la columna Ticket a VARCHAR(200)
------------------------------------------------------------
IF EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE name = 'Ticket'
      AND object_id = OBJECT_ID('dbo.Factibilidad')
)
BEGIN
    ALTER TABLE dbo.Factibilidad
    ALTER COLUMN Ticket VARCHAR(200) NOT NULL;
END;

------------------------------------------------------------
-- 3. Crear nuevamente la Primary Key
------------------------------------------------------------
IF NOT EXISTS (
    SELECT 1
    FROM sys.key_constraints
    WHERE name = 'PK_Factibilidad_IdFactibilidadTicket'
      AND parent_object_id = OBJECT_ID('dbo.Factibilidad')
)
BEGIN
    ALTER TABLE dbo.Factibilidad
    ADD CONSTRAINT PK_Factibilidad_IdFactibilidadTicket
    PRIMARY KEY (IdFactibilidad, Ticket);
END;

------------------------------------------------------------
-- 4. Crear CHECK para Sucursales y formato de Ticket
------------------------------------------------------------
IF NOT EXISTS (
	SELECT 1 
	FROM sys.check_constraints 
	WHERE name = 'CK_Ticket_Sucursal'
	  AND parent_object_id = OBJECT_ID('dbo.Factibilidad')
)
BEGIN
	ALTER TABLE dbo.Factibilidad
	ADD CONSTRAINT CK_Ticket_Sucursal
	CHECK (
		-- Guatemala: solo números
		(idSucursal = 1 AND Ticket NOT LIKE '%[^0-9]%')
        -- El Salvador: solo números
        OR (idSucursal = 2 AND Ticket NOT LIKE '%[^0-9]%')
		-- GUatemala - Infinitum: alfanumérico
		OR (idSucursal = 3 AND Ticket LIKE '%[A-Za-z0-9]%')
	);
END;

------------------------------------------------------------
-- Agregar columna Coordenada
------------------------------------------------------------
IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Factibilidad' AND COLUMN_NAME = 'Coordenada'
)
BEGIN
    ALTER TABLE dbo.Factibilidad ADD Coordenada VARCHAR(200) NULL;
END;


------------------------------------------------------------
-- Agregar columna IdMunicipio + FK
------------------------------------------------------------
IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Factibilidad' AND COLUMN_NAME = 'IdMunicipio'
)
BEGIN
    ALTER TABLE dbo.Factibilidad ADD IdMunicipio INT NULL;

    ALTER TABLE dbo.Factibilidad
    ADD CONSTRAINT FK_Factibilidad_Municipio 
    FOREIGN KEY (IdMunicipio) REFERENCES Municipality(IdMunicipality);
END;


------------------------------------------------------------
-- Agregar columna IdDepartamento + FK
------------------------------------------------------------
IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Factibilidad' AND COLUMN_NAME = 'IdDepartamento'
)
BEGIN
    ALTER TABLE dbo.Factibilidad ADD IdDepartamento INT NULL;

    ALTER TABLE dbo.Factibilidad
    ADD CONSTRAINT FK_Factibilidad_Departamento 
    FOREIGN KEY (IdDepartamento) REFERENCES State(IdState);
END;


------------------------------------------------------------
-- Agregar columna Capex
------------------------------------------------------------
IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Factibilidad' AND COLUMN_NAME = 'Capex'
)
BEGIN
    ALTER TABLE dbo.Factibilidad ADD Capex DECIMAL(18,2) NULL;
END;


------------------------------------------------------------
-- Agregar columna Opex
------------------------------------------------------------
IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Factibilidad' AND COLUMN_NAME = 'Opex'
)
BEGIN
    ALTER TABLE dbo.Factibilidad ADD Opex DECIMAL(18,2) NULL;
END;

------------------------------------------------------------
-- Agregar columna IdEstado + FK
------------------------------------------------------------
IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Factibilidad' AND COLUMN_NAME = 'IdEstado'
)
BEGIN
    ALTER TABLE dbo.Factibilidad ADD IdEstado INT NULL DEFAULT 1;

    ALTER TABLE dbo.Factibilidad ADD CONSTRAINT FK_Factibilidad_EstadoFactibilidad
    FOREIGN KEY (IdEstado) REFERENCES EstadoFactibilidad(IdEstado);
END;


------------------------------------------------------------
-- Datos de control extra
------------------------------------------------------------
------------------------------------------------------------
-- Agregar columna UsuarioRegistro + FK
------------------------------------------------------------
IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Factibilidad' AND COLUMN_NAME = 'UsuarioRegistro'
)
BEGIN
    ALTER TABLE dbo.Factibilidad 
    ADD UsuarioRegistro VARCHAR(100) NULL;

    ALTER TABLE dbo.Factibilidad 
    ADD CONSTRAINT FK_Factibilidad_UsuarioRegistro
    FOREIGN KEY (UsuarioRegistro) REFERENCES dbo.Usuario(Usuario);
END;

------------------------------------------------------------
-- Agregar columna UsuarioModifica + FK
------------------------------------------------------------
IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Factibilidad' AND COLUMN_NAME = 'UsuarioModifica'
)
BEGIN
    ALTER TABLE dbo.Factibilidad 
    ADD UsuarioModifica VARCHAR(100) NULL;

    ALTER TABLE dbo.Factibilidad 
    ADD CONSTRAINT FK_Factibilidad_UsuarioModifica
    FOREIGN KEY (UsuarioModifica) REFERENCES dbo.Usuario(Usuario);
END;

------------------------------------------------------------
-- Agregar columna FechaRegistro
------------------------------------------------------------
IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Factibilidad' AND COLUMN_NAME = 'FechaRegistro'
)
BEGIN
    ALTER TABLE dbo.Factibilidad 
    ADD FechaRegistro DATETIME NULL DEFAULT GETDATE();
END;

------------------------------------------------------------
-- Agregar columna FechaModificacion
------------------------------------------------------------
IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Factibilidad' AND COLUMN_NAME = 'FechaModificacion'
)
BEGIN
    ALTER TABLE dbo.Factibilidad 
    ADD FechaModificacion DATETIME NULL;
END;