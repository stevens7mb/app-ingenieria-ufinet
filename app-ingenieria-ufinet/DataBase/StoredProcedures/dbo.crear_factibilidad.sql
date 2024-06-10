CREATE OR ALTER PROCEDURE crear_factibilidad(
	@ticket INT,
	@estudio VARCHAR(100),
	@id_cliente INT,
	@id_kam INT,
	@bw INT,
	@fecha_solicitud DATETIME,
	@fecha_respuesta DATETIME,
	@sitio_con_cobertura INT,
	@sitio_con_cobertura_parcial INT,
	@sitio_sin_cobertura INT,
	@usuario VARCHAR(100),
	@tipo_servicio INT
) AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
		DECLARE 
			@idIngeniero INT,
			@idSucursal INT

		SELECT TOP 1 @idIngeniero = IdIngeniero FROM Provisioning WHERE Usuario = @usuario

		SELECT TOP 1 @idSucursal = idSucursal FROM dbo.Usuario WHERE Usuario = @usuario
		
		INSERT INTO Factibilidad(
			Ticket,
			Estudio, 
			IdCliente,
			IdKAM,
			BW,
			FechaSolicitud,
			FechaRespuesta,
			Estado,
			SitioConCobertura,
			SitioConCoberturaParcial,
			SitioSinCobertura,
			IdIngeniero,
			IdTipoServicio,
			idSucursal
		)
		VALUES(
			@ticket, 
			@estudio,
			@id_cliente,
			@id_kam,
			@bw,
			@fecha_solicitud,
			@fecha_respuesta,
			-1,
			@sitio_con_cobertura,
			@sitio_con_cobertura_parcial,
			@sitio_sin_cobertura,
			@idIngeniero,
			@tipo_servicio,
			@idSucursal
		)
		
	    SELECT 
	    @@ROWCOUNT as 'FilasAfectadas',
	  	'Factibilidad insertada exitosamente' AS 'Mensaje',
	  	'success' AS 'Tipo'
	  	
	  	COMMIT TRAN
	END TRY
	BEGIN CATCH
	  SELECT
	  	@@ROWCOUNT as 'FilasAfectadas',
	  	ERROR_MESSAGE() AS 'Mensaje',
	  	'error' AS 'Tipo'
	  ROLLBACK TRAN
	END CATCH
END;