IF EXISTS (SELECT 1 FROM sys.objects s WHERE s.object_id = OBJECT_ID('dbo.eliminar_asignacion') AND type IN (N'P', N'PC'))
DROP PROCEDURE eliminar_asignacion
GO

CREATE PROCEDURE dbo.eliminar_asignacion(
	@id_servicio_nuevo VARCHAR(100)
)
AS
BEGIN
  	BEGIN TRY
		BEGIN TRAN

		--Validación si hay inspeccion en proceso
		IF EXISTS(SELECT 1 FROM Inspecciontrabajo WHERE IdServicioNuevo = @id_servicio_nuevo)
		BEGIN
			SELECT
				0 AS 'FilasAfectadas',
	  			'Existe una inspección en proceso, no se puede eliminar' AS 'Mensaje',
	  			'error' AS 'Tipo'
		END
		
		--Elimina asignación
		DELETE FROM AsignacionInspeccion
		WHERE IdServicioNuevo = @id_servicio_nuevo
		
	    SELECT 
	    @@ROWCOUNT as 'FilasAfectadas',
	  	'Asignación eliminada exitosamente' AS 'Mensaje',
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