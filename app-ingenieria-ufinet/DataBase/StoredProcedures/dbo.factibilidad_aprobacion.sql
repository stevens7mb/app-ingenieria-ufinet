CREATE OR ALTER PROCEDURE factibilidad_aprobacion(
	@id_factibilidad INT,
	@ticket VARCHAR(200),
	@aprueba BIT,
	@usuario VARCHAR(100),
	@comentario VARCHAR(500)
) AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
		DECLARE 
			@estado_factibilidad INT,
			@estado_aprobado INT,
			@estado_rechazado INT

		SELECT
			@estado_aprobado = IdEstado
		FROM dbo.EstadoFactibilidad WHERE Estado = 'Aprobado';

		SELECT
			@estado_rechazado = IdEstado
		FROM dbo.EstadoFactibilidad WHERE Estado = 'Rechazado';

		IF @aprueba = 1
		BEGIN
			SET @estado_factibilidad = @estado_aprobado --Aprobado
		END
		ELSE
		BEGIN
			SET @estado_factibilidad = @estado_rechazado --Rechazado
		END

		-- Validación si ya se aprobo o rechazo
		IF EXISTS (
			SELECT 1 
			FROM dbo.FactibilidadHistorialEstado
			WHERE IdFactibilidad = @id_factibilidad
			AND Ticket = @ticket
			AND IdEstado IN (
				SELECT IdEstado 
				FROM dbo.EstadoFactibilidad 
				WHERE Estado IN ('Aprobado', 'Rechazado')
			)
		)
		BEGIN
			SELECT 
				0 AS 'FilasAfectadas',
				'Esta factibilidad ya fue aprobada o rechazada previamente.' AS 'Mensaje',
				'warning' AS 'Tipo';

			ROLLBACK TRAN;
			RETURN;
		END

		UPDATE Factibilidad
		SET 
			IdEstado = @estado_factibilidad --2=Aprobado, 3=Rechazado
		WHERE IdFactibilidad = @id_factibilidad 
		AND Ticket = @ticket

		INSERT INTO dbo.FactibilidadHistorialEstado(
			IdFactibilidad,
			Ticket,
			IdEstado,
			Usuario,
			Fecha,
			Comentario	
		)
		VALUES(
			@id_factibilidad,
			@ticket,
			@estado_factibilidad,
			@usuario,	
			GETDATE(),
			@comentario	
		)
		
	    SELECT 
	    @@ROWCOUNT as 'FilasAfectadas',
	  	'Factibilidad' + IIF(@estado_factibilidad = @estado_aprobado, ' aprobada ', ' rechazada ' ) + 'exitosamente' AS 'Mensaje',
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