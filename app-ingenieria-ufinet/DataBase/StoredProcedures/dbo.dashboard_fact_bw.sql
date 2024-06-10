CREATE OR ALTER PROCEDURE dashboard_fact_bw(
	@mes_desde INT = NULL,
	@mes_hasta INT = NULL,
	@anio INT = NULL,
	@id_sucursal INT
) AS
BEGIN
	--Ancho de banda
	SELECT SUM(BW) as AnchoDeBanda
	FROM Factibilidad f 
	WHERE f.Estado = -1
	AND (@mes_desde IS NULL OR MONTH(f.FechaRespuesta) >= @mes_desde)
	AND (@mes_hasta IS NULL OR MONTH(f.FechaRespuesta) <= @mes_hasta)
	AND (@anio IS NULL OR YEAR(f.FechaRespuesta) = @anio)
	AND f.idSucursal = @id_sucursal
END;