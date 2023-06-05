IF EXISTS (SELECT 1 FROM sys.objects s WHERE s.object_id = OBJECT_ID('dbo.detalle_individual_inspeccion_por_id') AND type IN (N'P', N'PC'))
DROP PROCEDURE detalle_individual_inspeccion_por_id
GO

CREATE PROCEDURE dbo.detalle_individual_inspeccion_por_id(
	@id_inspeccion_trabajo INT
)
AS
BEGIN
  	SELECT
		IdInspeccionTrabajo,
		IdServicioActivo,
		IdServicioNuevo,
		c.NombreContrata AS Contrata,
		t.Nombre AS Tecnico,
		p.Nombre AS Ingeniero,
		it.FechaInspeccion,
		u.Nombre AS UsuarioInspeccion,
		ite.Descripcion AS Estado
	FROM InspeccionTrabajo it
	INNER JOIN Contrata c ON c.IdContrata = it.IdContrata 
	INNER JOIN  Tecnico t ON t.IdTecnico = it.IdTecnico
	INNER JOIN Provisioning p ON p.IdIngeniero = it.IdIngeniero
	INNER JOIN InspeccionTrabajoEstado ite ON ite.IdInspeccionTrabajoEstado = it.IdInspeccionTrabajoEstado
	INNER JOIN Usuario u ON u.Usuario = it.UsuarioInspeccion
	WHERE 
		it.IdInspeccionTrabajo = @id_inspeccion_trabajo
END