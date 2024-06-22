CREATE OR ALTER PROCEDURE dashboard_fact_sitios_totales(
	@mes_desde INT = NULL,
	@mes_hasta INT = NULL,
	@anio INT = NULL,
	@id_sucursal INT,
	@es_admin INT
) AS
BEGIN
	--Ancho de banda
	SELECT 
		SUM((SitioConCobertura + SitioConCoberturaParcial + SitioSinCobertura)) AS SitiosTotales,
		SUM(SitioConCobertura) AS SitiosConCobertura,
		SUM(SitioConCoberturaParcial) AS SitiosConCoberturaParcial,
		SUM(SitioSinCobertura) AS SitiosSinCobertura
	FROM Factibilidad f 
	WHERE f.Estado = -1
	AND (@mes_desde IS NULL OR MONTH(f.FechaRespuesta) >= @mes_desde)
	AND (@mes_hasta IS NULL OR MONTH(f.FechaRespuesta) <= @mes_hasta)
	AND (@anio IS NULL OR YEAR(f.FechaRespuesta) = @anio)
	AND (@es_admin = -1 OR f.idSucursal = @id_sucursal)
END;