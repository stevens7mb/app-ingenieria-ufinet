CREATE OR ALTER PROCEDURE editar_factibilidad(
	@id_factibilidad INT,
	@ticket VARCHAR(200),
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
	@tipo_servicio INT,
	@coordenada VARCHAR(200) = NULL,
	@municipio INT = NULL,	
	@departamento INT = NULL,
	@capex DECIMAL(18,2) = NULL,
	@opex DECIMAL(18,2) = NULL
) AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
		DECLARE @idIngeniero INT

		SELECT TOP 1 @idIngeniero = IdIngeniero FROM Provisioning WHERE Usuario = @usuario
		
		UPDATE Factibilidad
		SET 
			Estudio=@estudio, 
			IdCliente=@id_cliente, 
			IdKAM=@id_kam, 
			BW=@bw,
			FechaSolicitud=@fecha_solicitud,
			FechaRespuesta=@fecha_respuesta, 
			SitioConCobertura=@sitio_con_cobertura,
			SitioConCoberturaParcial=@sitio_con_cobertura_parcial,
			SitioSinCobertura=@sitio_sin_cobertura,
			IdIngeniero=@idIngeniero,
			IdTipoServicio=@tipo_servicio,
			Coordenada = @coordenada,
			IdMunicipio = @municipio,
			IdDepartamento = @departamento,
			Capex = @capex,
			Opex = @opex,
			UsuarioModifica = @usuario,
			FechaModificacion = GETDATE()
		WHERE 
			IdFactibilidad= @id_factibilidad AND Ticket = @ticket
		
	    SELECT 
	    @@ROWCOUNT as 'FilasAfectadas',
	  	'Factibilidad editada exitosamente' AS 'Mensaje',
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