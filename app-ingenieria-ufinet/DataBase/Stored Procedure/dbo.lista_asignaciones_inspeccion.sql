IF EXISTS (SELECT 1 FROM sys.objects s WHERE s.object_id = OBJECT_ID('dbo.lista_asignaciones_inspeccion') AND type IN (N'P', N'PC'))
DROP PROCEDURE lista_asignaciones_inspeccion
GO

CREATE PROCEDURE dbo.lista_asignaciones_inspeccion
AS
BEGIN
	SELECT
		ai.IdServicioNuevo,
		c.Nombre AS Cliente,
		t.Nombre AS Tecnico,
		ct.NombreContrata AS Contrata,
		Fecha,
		p.Nombre AS Ingeniero
	FROM AsignacionInspeccion ai
	INNER JOIN Cliente c ON c.IdCliente = ai.IdCliente
	INNER JOIN Tecnico t ON t.IdTecnico = ai.IdTecnico
	INNER JOIN Contrata ct ON ct.IdContrata = ai.IdContrata
	INNER JOIN Provisioning p ON p.IdIngeniero = ai.IdIngeniero
	LEFT JOIN InspeccionTrabajo i ON i.IdServicioNuevo = ai.IdServicioNuevo
	WHERE i.IdServicioNuevo IS NULL
END