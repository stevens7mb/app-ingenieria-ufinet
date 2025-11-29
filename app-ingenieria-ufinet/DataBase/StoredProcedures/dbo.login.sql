CREATE OR ALTER PROCEDURE login
(
	@username VARCHAR(100),
	@password VARCHAR(MAX)
) 
AS
BEGIN
	SELECT 
		u.Correlativo  as 'Correlative',
		u.Usuario  as 'User',
		u.Contraseña as 'Contraseña',
		u.Nombre as 'Nombre',
		u.Email as 'Email',
		1 as 'idRol',
		u.IdSucursal as 'idSucursal',
		s.Descripcion as 'Sucursal',
		u.Activo as 'Activo'
	FROM Usuario u 
	INNER JOIN dbo.Sucursal s ON s.idSucursal = u.IdSucursal
	WHERE Usuario = @username
	AND Contraseña = @password
	AND Activo = -1
END;