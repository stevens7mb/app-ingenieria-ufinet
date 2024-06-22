CREATE OR ALTER PROCEDURE dbo.lista_factibilidades (
	@usuario VARCHAR(100)
)
AS
BEGIN
	DECLARE 
		@admin_rol INT,
		@sucursal INT

	SELECT 
		@admin_rol = idRol
	FROM dbo.Rol
	WHERE Descripcion = 'Admin'

	IF @admin_rol IN (SELECT IdRol FROM dbo.RolUsuario WHERE Usuario = @usuario)
	BEGIN
		SELECT
			IdFactibilidad, 
			Ticket,
			Estudio, 
			c.Nombre AS Cliente, 
			k.Nombre AS KAM, 
			BW, 
			FechaSolicitud, 
			FechaRespuesta, 
			CASE 
				WHEN f.Estado = -1 THEN 'Activo'
				ELSE 'Inactivo'
			END AS Estado, 
			SitioConCobertura,
			SitioConCoberturaParcial,
			SitioSinCobertura,
			SitioConCobertura + SitioConCoberturaParcial + SitioSinCobertura AS SitiosAnalizados,
			ing.Nombre AS Ingeniero,
			ISNULL(ts.TipoServicio, '') AS 'TipoServicio'
		FROM Factibilidad f
		INNER JOIN Cliente c ON c.IdCliente = f.IdCliente 
		INNER JOIN KAM k ON k.IdKAM = f.IdKAM 
		INNER JOIN Provisioning ing ON ing.IdIngeniero = f.IdIngeniero 
		LEFT JOIN TipoServicio ts ON ts.IdTipoServicio = f.idTipoServicio
		WHERE f.Estado = -1
	END
	ELSE
	BEGIN
		SELECT 
			@sucursal = idSucursal
		FROM dbo.Usuario 
		WHERE Usuario = @usuario

		SELECT
			IdFactibilidad, 
			Ticket,
			Estudio, 
			c.Nombre AS Cliente, 
			k.Nombre AS KAM, 
			BW, 
			FechaSolicitud, 
			FechaRespuesta, 
			CASE 
				WHEN f.Estado = -1 THEN 'Activo'
				ELSE 'Inactivo'
			END AS Estado, 
			SitioConCobertura,
			SitioConCoberturaParcial,
			SitioSinCobertura,
			SitioConCobertura + SitioConCoberturaParcial + SitioSinCobertura AS SitiosAnalizados,
			ing.Nombre AS Ingeniero,
			ISNULL(ts.TipoServicio, '') AS 'TipoServicio'
		FROM Factibilidad f
		INNER JOIN Cliente c ON c.IdCliente = f.IdCliente 
		INNER JOIN KAM k ON k.IdKAM = f.IdKAM 
		INNER JOIN Provisioning ing ON ing.IdIngeniero = f.IdIngeniero 
		LEFT JOIN TipoServicio ts ON ts.IdTipoServicio = f.idTipoServicio
		WHERE f.Estado = -1
		AND f.idSucursal = @sucursal			
	END
END;