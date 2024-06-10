CREATE OR ALTER PROCEDURE dashboard_lista_fact_ingeniero(
	@mes_desde INT = NULL,
	@mes_hasta INT = NULL,
	@anio INT = NULL,
	@id_sucursal INT
) AS
BEGIN
	--Factibilidades por Ingeniero
	SELECT
		p.Nombre AS Ingeniero,
		COUNT(f.IdFactibilidad) AS CantidadEstudios
	FROM Factibilidad f
	INNER JOIN Provisioning p ON p.IdIngeniero = f.IdIngeniero 
	WHERE f.Estado = -1
	AND (@mes_desde IS NULL OR MONTH(f.FechaRespuesta) >= @mes_desde)
	AND (@mes_hasta IS NULL OR MONTH(f.FechaRespuesta) <= @mes_hasta)
	AND (@anio IS NULL OR YEAR(f.FechaRespuesta) = @anio)
	AND f.idSucursal = @id_sucursal
	GROUP BY p.Nombre 
	ORDER BY COUNT(f.IdFactibilidad) DESC
END;