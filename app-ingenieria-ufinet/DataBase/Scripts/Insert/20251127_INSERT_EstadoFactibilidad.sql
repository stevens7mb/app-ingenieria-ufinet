IF NOT EXISTS (SELECT 1 FROM dbo.EstadoFactibilidad WHERE Estado = 'Creado')
    BEGIN
        INSERT INTO dbo.EstadoFactibilidad (IdEstado, Estado) 
        VALUES (1, 'Creado');
    END

    -- Insertar 'Aprobado' si no existe
    IF NOT EXISTS (SELECT 1 FROM dbo.EstadoFactibilidad WHERE Estado = 'Aprobado')
    BEGIN
        INSERT INTO dbo.EstadoFactibilidad (IdEstado, Estado) 
        VALUES (2, 'Aprobado');
    END

    -- Insertar 'Rechazado' si no existe
    IF NOT EXISTS (SELECT 1 FROM dbo.EstadoFactibilidad WHERE Estado = 'Rechazado')
    BEGIN
        INSERT INTO dbo.EstadoFactibilidad (IdEstado, Estado) 
        VALUES (3, 'Rechazado');
    END