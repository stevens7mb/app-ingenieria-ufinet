CREATE OR ALTER PROCEDURE dashboard_lista_fact_kams(
	@mes_desde INT = NULL,
	@mes_hasta INT = NULL,
	@anio INT = NULL,
	@id_sucursal INT,
	@es_admin INT
) AS
BEGIN
	--Factibilidades por Ingeniero
	SELECT
		k.Nombre AS Kam,
		COUNT(f.IdFactibilidad) AS CantidadEstudios
	FROM Factibilidad f
	INNER JOIN KAM k ON k.IdKAM = f.IdKAM
	WHERE f.Estado = -1
	AND (@mes_desde IS NULL OR MONTH(f.FechaRespuesta) >= @mes_desde)
	AND (@mes_hasta IS NULL OR MONTH(f.FechaRespuesta) <= @mes_hasta)
	AND (@anio IS NULL OR YEAR(f.FechaRespuesta) = @anio)
	AND (@es_admin = -1 OR f.idSucursal = @id_sucursal)
	GROUP BY k.Nombre 
	ORDER BY COUNT(f.IdFactibilidad) DESC
END;