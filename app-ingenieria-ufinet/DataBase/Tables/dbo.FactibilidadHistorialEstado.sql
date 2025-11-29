IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FactibilidadHistorialEstado')
BEGIN
    CREATE TABLE dbo.FactibilidadHistorialEstado
    (
        IdHistorial INT IDENTITY(1,1) NOT NULL,
        IdFactibilidad INT NOT NULL,
        Ticket VARCHAR(200) NOT NULL,
        IdEstado INT NOT NULL,
        Usuario VARCHAR(100) NOT NULL,
        Fecha DATETIME NOT NULL DEFAULT GETDATE(),
        Comentario VARCHAR(500) NULL,
        CONSTRAINT PK_FactibilidadHistorialEstado PRIMARY KEY (IdHistorial),
        CONSTRAINT FK_FactibilidadHistorial_Factibilidad FOREIGN KEY (IdFactibilidad, Ticket) REFERENCES dbo.Factibilidad(IdFactibilidad, Ticket),
        CONSTRAINT FK_FactibilidadHistorial_Estado FOREIGN KEY (IdEstado) REFERENCES dbo.EstadoFactibilidad(IdEstado),
        CONSTRAINT FK_FactibilidadHistorial_Usuario FOREIGN KEY (Usuario) REFERENCES dbo.Usuario(Usuario)
    );
END