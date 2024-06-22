CREATE OR ALTER PROCEDURE dashboard_lista_fact_clientes(
	@mes_desde INT = NULL,
	@mes_hasta INT = NULL,
	@anio INT = NULL,
	@id_sucursal INT,
	@es_admin INT
) AS
BEGIN
	---Factibilidades por Cliente
	DECLARE @total_estudios DECIMAL(18,2) 
	
	SELECT @total_estudios = COUNT(IdFactibilidad) 
	FROM Factibilidad f 
	WHERE Estado = -1 
	AND (@mes_desde IS NULL OR MONTH(f.FechaRespuesta) >= @mes_desde)
	AND (@mes_hasta IS NULL OR MONTH(f.FechaRespuesta) <= @mes_hasta)
	AND (@anio IS NULL OR YEAR(f.FechaRespuesta) = @anio) 
	AND (@es_admin = -1 OR f.idSucursal = @id_sucursal)
	
	SELECT 
		c.Nombre as Cliente,
		COUNT(f.IdCliente) AS CantidadEstudios,
		ROUND((CAST(COUNT(f.IdCliente) AS DECIMAL) / @total_estudios) * 100, 2 ) AS Porcentaje
	FROM Factibilidad f 
	INNER JOIN Cliente c ON c.IdCliente = f.IdCliente 
	WHERE f.Estado = -1
	AND (@mes_desde IS NULL OR MONTH(f.FechaRespuesta) >= @mes_desde)
	AND (@mes_hasta IS NULL OR MONTH(f.FechaRespuesta) <= @mes_hasta)
	AND (@anio IS NULL OR YEAR(f.FechaRespuesta) = @anio)
	AND (@es_admin = -1 OR f.idSucursal = @id_sucursal)
	GROUP BY c.Nombre 
	ORDER BY COUNT(f.IdCliente) DESC
END;