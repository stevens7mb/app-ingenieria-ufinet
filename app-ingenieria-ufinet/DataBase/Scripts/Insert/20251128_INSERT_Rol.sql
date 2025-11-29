IF NOT EXISTS (SELECT 1 FROM dbo.Rol WHERE Descripcion = 'Comercial')
BEGIN
    INSERT INTO dbo.Rol (IdRol, Descripcion) 
    VALUES (5, 'Comercial');
END
