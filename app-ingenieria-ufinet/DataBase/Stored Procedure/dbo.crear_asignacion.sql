IF EXISTS (SELECT 1 FROM sys.objects s WHERE s.object_id = OBJECT_ID('dbo.crear_asignacion') AND type IN (N'P', N'PC'))
DROP PROCEDURE crear_asignacion
GO

CREATE PROCEDURE dbo.crear_asignacion(
	@id_servicio_nuevo VARCHAR(100),
	@id_cliente INT,
	@id_tecnico INT,
	@id_contrata INT,
	@id_ingeniero INT
)
AS
BEGIN
  	BEGIN TRY
		BEGIN TRAN
		
		INSERT INTO AsignacionInspeccion(
			IdServicioNuevo,
			IdCliente,
			IdTecnico,
			IdContrata,
			Fecha,
			IdIngeniero
		)
		VALUES(
			@id_servicio_nuevo,
			@id_cliente,
			@id_tecnico,
			@id_contrata,
			GETDATE(),
			@id_ingeniero
		)
		
	    SELECT 
	    @@ROWCOUNT as 'FilasAfectadas',
	  	'Asignación insertada exitosamente' AS 'Mensaje',
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
END