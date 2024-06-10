CREATE OR ALTER PROCEDURE dashboard_indicadores_desemp_anio_unif(
	@anio INT = NULL,
	@id_sucursal INT
) AS
BEGIN
	DECLARE @sitios_totales DECIMAL(18,2)
	
	DROP TABLE IF EXISTS #tmp_indicadores_desemp
	DROP TABLE IF EXISTS #tmp_indicadores_desemp_return
	
	CREATE TABLE #tmp_indicadores_desemp(
		Correlativo INT,
		SitiosTotales INT,
		SitiosConCoberturaP VARCHAR(31),
		SitiosConCoberturaParcialP VARCHAR(31),
		SitiosSinCoberturaP VARCHAR(31),
		EstudiosTotales INT,
		TiempoPromedioEntrega INT,
		Mes Varchar(30)
	)
	
	CREATE TABLE #tmp_indicadores_desemp_return(
		Enero VARCHAR(100),
		Febrero VARCHAR(100),
		Marzo VARCHAR(100),
		Abril VARCHAR(100),
		Mayo VARCHAR(100),
		Junio VARCHAR(100),
		Julio VARCHAR(100),
		Agosto VARCHAR(100),
		Septiembre VARCHAR(100),
		Octubre VARCHAR(100),
		Noviembre VARCHAR(100),
		Diciembre VARCHAR(100),
	)
	
	IF @anio IS NULL
	BEGIN
		SET @anio = YEAR(GETDATE())
	END
	
	DECLARE @intFlag INT
	SET @intFlag = 1
	WHILE (@intFlag <=12)
	BEGIN
		
		SELECT @sitios_totales = SUM((SitioConCobertura + SitioConCoberturaParcial + SitioSinCobertura)) 
		FROM Factibilidad f 
		WHERE f.Estado = -1
		AND YEAR(f.FechaRespuesta) = @anio
		AND MONTH(f.FechaRespuesta) = @intFlag
		AND f.idSucursal = @id_sucursal
		
		SET LANGUAGE Spanish
		
		INSERT INTO #tmp_indicadores_desemp
		SELECT 
			@intFlag AS Correlativo,
			ISNULL(CONVERT(INT, @sitios_totales), 0) AS SitiosTotales,
			CONVERT(VARCHAR, CONVERT(INT, ROUND((SUM(SitioConCobertura) / @sitios_totales) * 100, 0))) + '%' AS SitiosConCoberturaP,
			CONVERT(VARCHAR, CONVERT(INT, ROUND((SUM(SitioConCoberturaParcial) / @sitios_totales) * 100, 0))) + '%'  AS SitiosConCoberturaParcialP,
			CONVERT(VARCHAR, CONVERT(INT, ROUND((SUM(SitioSinCobertura) / @sitios_totales) * 100, 0))) + '%' AS SitiosSinCoberturaP,
			ISNULL(COUNT(F.IdFactibilidad), 0) AS EstudiosTotales,
			ISNULL(0, 0) AS TiempoPromedioEntrega,
			DATENAME(MONTH, CONVERT(DATETIME, CONCAT(@anio, RIGHT('0' + Ltrim(Rtrim(@intFlag)),2),'01'), 102))
		FROM Factibilidad f 
		WHERE f.Estado = -1
		AND YEAR(f.FechaRespuesta) = @anio
		AND MONTH(f.FechaRespuesta) = @intFlag
		AND f.idSucursal = @id_sucursal

		SET @intFlag = @intFlag + 1
	END
	
	--INSERT INTO #tmp_indicadores_desemp_return
	SELECT
	*
	FROM   #tmp_indicadores_desemp

	DROP TABLE IF EXISTS #tmp_indicadores_desemp
	DROP TABLE IF EXISTS #tmp_indicadores_desemp_return
	
END;