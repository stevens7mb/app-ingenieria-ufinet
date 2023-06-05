IF EXISTS (SELECT 1 FROM sys.objects s WHERE s.object_id = OBJECT_ID('dbo.detalle_preguntas_inspeccion_por_id') AND type IN (N'P', N'PC'))
DROP PROCEDURE detalle_preguntas_inspeccion_por_id
GO

CREATE PROCEDURE dbo.detalle_preguntas_inspeccion_por_id(
	@id_inspeccion_trabajo INT
)
AS
BEGIN
	SELECT
		itt.IdInspeccionTrabajoTarea,
		itt.IdTarea,
		tt.Tarea,
		ct.IdCategoriaTarea,
		ct.Categoria,
		rt.IdRespuesta,
		rt.Respuesta
	FROM InspeccionTrabajoTarea itt 
	INNER JOIN TrabajoTarea tt ON tt.IdTarea = itt.IdTarea 
	INNER JOIN CategoriaTarea ct ON ct.IdCategoriaTarea = tt.IdCategoriaTarea 
	INNER JOIN RespuestaTarea rt ON rt.IdRespuesta = itt.IdRespuesta 
	WHERE 
		itt.IdInspeccionTrabajo = @id_inspeccion_trabajo
END