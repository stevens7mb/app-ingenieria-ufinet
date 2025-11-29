CREATE OR ALTER PROCEDURE lista_factibilidades_paginate
(  
	@SearchValue NVARCHAR(255) = NULL,  
	@PageNo INT = 0,
	@PageSize INT = 10,
	@SortColumn INT = 0,
	@SortDirection NVARCHAR(10) = 'ASC',
	@Usuario VARCHAR(100),
	@tipo_servicio_filter VARCHAR(200) = NULL,
	@cliente_filter VARCHAR(200) = NULL
)
AS
BEGIN
  	SET NOCOUNT ON;
 

	DECLARE 
		@TotalCount AS INT = (SELECT COUNT(*) FROM Factibilidad)

	DECLARE 
		@FirstRec INT,
		@LastRec INT,
		@IdSucursal INT,
		@admin_rol INT,
		@es_admin INT = 0

	SELECT 
		@admin_rol = idRol
	FROM dbo.Rol
	WHERE Descripcion = 'Admin'

	IF EXISTS(SELECT 1 FROM dbo.RolUsuario WHERE Usuario = @Usuario AND idRol = @admin_rol)
	BEGIN
		SET @es_admin = -1;
	END

	SELECT 
		@IdSucursal = idSucursal
	FROM dbo.Usuario
	WHERE Usuario = @Usuario
	
  	SET @FirstRec = @PageNo * @PageSize + 1;
  	SET @LastRec = (@PageNo + 1) * @PageSize;
 
    SET @SearchValue = LTRIM(RTRIM(@SearchValue))

  	--Opcion mostrar todo para exportar a excel
  	IF @PageSize = -1
  	BEGIN 
	  	SELECT
	  		@TotalCount AS FilteredCount,
	    	@TotalCount AS TotalCount,
			IdFactibilidad, 
			Ticket,
			Estudio, 
			ISNULL(ts.TipoServicio, '') AS 'TipoServicio',
			c.Nombre AS Cliente, 
			k.Nombre AS KAM, 
			BW, 
			FechaSolicitud, 
			FechaRespuesta, 
			CASE 
				WHEN f.Estado = -1 THEN 'Activo'
				ELSE 'Inactivo'
			END AS Estado, 
			SitioConCobertura,
			SitioConCoberturaParcial,
			SitioSinCobertura,
			SitioConCobertura + SitioConCoberturaParcial + SitioSinCobertura AS SitiosAnalizados,
			ing.Nombre AS Ingeniero,
			Capex,
			Opex,
			ef.Estado AS EstadoFactibilidad
		FROM Factibilidad f
		INNER JOIN Cliente c ON c.IdCliente = f.IdCliente 
		INNER JOIN KAM k ON k.IdKAM = f.IdKAM 
		INNER JOIN Provisioning ing ON ing.IdIngeniero = f.IdIngeniero 
		LEFT JOIN TipoServicio ts ON ts.IdTipoServicio = f.idTipoServicio
		INNER JOIN Usuario u ON u.Usuario = ing.Usuario
		LEFT JOIN dbo.EstadoFactibilidad ef ON ef.IdEstado = f.IdEstado
		WHERE f.Estado = -1
		AND u.idSucursal = @IdSucursal
		AND (@tipo_servicio_filter IS NULL OR ts.TipoServicio = @tipo_servicio_filter)
		AND (@cliente_filter IS NULL OR c.Nombre = @cliente_filter)
	END
    ELSE 
	--EMPIEZA PAGINACION
    BEGIN 
    	;WITH CTE_Results AS  
		(  
			SELECT ROW_NUMBER() OVER (ORDER BY 
	 
	      	CASE WHEN (@SortColumn = 0 AND @SortDirection='asc') THEN IdFactibilidad END ASC,  
	      	CASE WHEN (@SortColumn = 0 AND @SortDirection='desc') THEN IdFactibilidad END DESC, 
	      	CASE WHEN (@SortColumn = 1 AND @SortDirection='asc') THEN 
			CASE WHEN TRY_CAST(Ticket AS INT) IS NOT NULL THEN TRY_CAST(Ticket AS INT) ELSE NULL END END ASC,
			CASE WHEN (@SortColumn = 1 AND @SortDirection='asc') THEN Ticket END ASC,
			CASE WHEN (@SortColumn = 1 AND @SortDirection='desc') THEN 
			CASE WHEN TRY_CAST(Ticket AS INT) IS NOT NULL THEN TRY_CAST(Ticket AS INT) ELSE NULL END END DESC,
			CASE WHEN (@SortColumn = 1 AND @SortDirection='desc') THEN Ticket END DESC,
	      	CASE WHEN (@SortColumn = 2 AND @SortDirection='asc') THEN Estudio END ASC,  
	      	CASE WHEN (@SortColumn = 2 AND @SortDirection='desc') THEN Estudio END DESC,
			CASE WHEN (@SortColumn = 3 AND @SortDirection='asc') THEN TipoServicio END ASC,  
	      	CASE WHEN (@SortColumn = 3 AND @SortDirection='desc') THEN TipoServicio END DESC,
	      	CASE WHEN (@SortColumn = 4 AND @SortDirection='asc') THEN c.Nombre END ASC,  
	      	CASE WHEN (@SortColumn = 4 AND @SortDirection='desc') THEN c.Nombre END DESC,
	      	CASE WHEN (@SortColumn = 5 AND @SortDirection='asc') THEN k.Nombre END ASC,  
	      	CASE WHEN (@SortColumn = 5 AND @SortDirection='desc') THEN k.Nombre END DESC,
	      	CASE WHEN (@SortColumn = 6 AND @SortDirection='asc') THEN BW END ASC,  
	      	CASE WHEN (@SortColumn = 6 AND @SortDirection='desc') THEN BW END DESC,
	      	CASE WHEN (@SortColumn = 7 AND @SortDirection='asc') THEN FechaSolicitud END ASC,  
	      	CASE WHEN (@SortColumn = 7 AND @SortDirection='desc') THEN FechaSolicitud END DESC,
	      	CASE WHEN (@SortColumn = 8 AND @SortDirection='asc') THEN FechaRespuesta END ASC,  
	      	CASE WHEN (@SortColumn = 8 AND @SortDirection='desc') THEN FechaRespuesta END DESC,
	      	CASE WHEN (@SortColumn = 9 AND @SortDirection='asc') THEN (CASE WHEN f.Estado = -1 THEN 'Activo'ELSE 'Inactivo' END) END ASC,  
	      	CASE WHEN (@SortColumn = 9 AND @SortDirection='desc') THEN (CASE WHEN f.Estado = -1 THEN 'Activo'ELSE 'Inactivo'END) END DESC,
	      	CASE WHEN (@SortColumn = 10 AND @SortDirection='asc') THEN SitioConCobertura END ASC,  
	      	CASE WHEN (@SortColumn = 10 AND @SortDirection='desc') THEN SitioConCobertura END DESC,
	      	CASE WHEN (@SortColumn = 11 AND @SortDirection='asc') THEN SitioConCoberturaParcial END ASC,  
	      	CASE WHEN (@SortColumn = 11 AND @SortDirection='desc') THEN SitioConCoberturaParcial END DESC,
	      	CASE WHEN (@SortColumn = 12 AND @SortDirection='asc') THEN SitioSinCobertura END ASC,  
	      	CASE WHEN (@SortColumn = 12 AND @SortDirection='desc') THEN SitioSinCobertura END DESC,
	      	CASE WHEN (@SortColumn = 13 AND @SortDirection='asc') THEN (SitioConCobertura + SitioConCoberturaParcial + SitioSinCobertura) END ASC,  
	      	CASE WHEN (@SortColumn = 13 AND @SortDirection='desc') THEN (SitioConCobertura + SitioConCoberturaParcial + SitioSinCobertura) END DESC,
	      	CASE WHEN (@SortColumn = 14 AND @SortDirection='asc') THEN ing.Nombre END ASC,  
	      	CASE WHEN (@SortColumn = 14 AND @SortDirection='desc') THEN ing.Nombre END DESC,
			CASE WHEN (@SortColumn = 15 AND @SortDirection='asc') THEN Capex END ASC, 
			CASE WHEN (@SortColumn = 15 AND @SortDirection='desc') THEN Capex END DESC,
			CASE WHEN (@SortColumn = 16 AND @SortDirection='asc') THEN Opex END ASC, 
			CASE WHEN (@SortColumn = 16 AND @SortDirection='desc') THEN Opex END DESC,
		    CASE WHEN (@SortColumn = 17) AND @SortDirection='asc' THEN ef.Estado END ASC,  
	      	CASE WHEN (@SortColumn = 17) AND @SortDirection='desc' THEN ef.Estado END DESC
	    )
	    	AS RowNum,
	    	COUNT(*) OVER() as FilteredCount,
	    	
			IdFactibilidad, 
			Ticket,
			Estudio, 
			c.Nombre AS Cliente, 
			k.Nombre AS KAM, 
			BW, 
			FechaSolicitud, 
			FechaRespuesta, 
			CASE 
				WHEN f.Estado = -1 THEN 'Activo'
				ELSE 'Inactivo'
			END AS Estado, 
			SitioConCobertura,
			SitioConCoberturaParcial,
			SitioSinCobertura,
			SitioConCobertura + SitioConCoberturaParcial + SitioSinCobertura AS SitiosAnalizados,
			ing.Nombre AS Ingeniero,
			ISNULL(ts.TipoServicio, '') AS 'TipoServicio',
			Capex,
			Opex,
			ef.Estado AS EstadoFactibilidad
			FROM Factibilidad f
			INNER JOIN Cliente c ON c.IdCliente = f.IdCliente 
			INNER JOIN KAM k ON k.IdKAM = f.IdKAM 
			INNER JOIN Provisioning ing ON ing.IdIngeniero = f.IdIngeniero 
			LEFT JOIN TipoServicio ts ON ts.IdTipoServicio = f.idTipoServicio
			INNER JOIN dbo.Usuario u ON u.Usuario = ing.Usuario
			LEFT JOIN dbo.EstadoFactibilidad ef ON ef.IdEstado = f.IdEstado
		    WHERE 
		    (ISNULL(@SearchValue, '') = ''
				OR IdFactibilidad LIKE '%' + @SearchValue + '%'
				OR Ticket LIKE '%' + @SearchValue + '%'
				OR Estudio LIKE '%' + @SearchValue + '%'
				OR TipoServicio LIKE '%' + @SearchValue + '%'
				OR c.Nombre LIKE '%' + @SearchValue + '%'
				OR k.Nombre LIKE '%' + @SearchValue + '%'
				OR BW LIKE '%' + @SearchValue + '%'
				OR CONVERT(VARCHAR, FechaSolicitud, 126) LIKE '%' + @SearchValue + '%'
				OR CONVERT(VARCHAR, FechaRespuesta, 126) LIKE '%' + @SearchValue + '%'
				OR (CASE 
						WHEN f.Estado = -1 THEN 'Activo'
						ELSE 'Inactivo'
					END) LIKE '%' + @SearchValue + '%'
				OR SitioConCobertura LIKE '%' + @SearchValue + '%'
				OR SitioConCoberturaParcial LIKE '%' + @SearchValue + '%'
				OR SitioSinCobertura LIKE '%' + @SearchValue + '%'
				OR SitioConCobertura + SitioConCoberturaParcial + SitioSinCobertura LIKE '%' + @SearchValue + '%'
				OR ing.Nombre LIKE '%' + @SearchValue + '%'
				OR Capex LIKE '%' + @SearchValue + '%'	
				OR Opex LIKE '%' + @SearchValue + '%' 
				OR ef.Estado LIKE '%' + @SearchValue + '%'
			)
		    AND f.Estado = -1
			AND (@es_admin = -1 OR u.idSucursal = @IdSucursal)
			AND (@tipo_servicio_filter IS NULL OR ts.TipoServicio = @tipo_servicio_filter)
			AND (@cliente_filter IS NULL OR c.Nombre = @cliente_filter)
		) 

		SELECT
		    IdFactibilidad, 
			Ticket,
			Estudio, 
			ISNULL(TipoServicio, '') AS 'TipoServicio', 
			Cliente AS Cliente, 
			KAM AS KAM, 
			BW, 
			FechaSolicitud, 
			FechaRespuesta, 
			Estado, 
			SitioConCobertura,
			SitioConCoberturaParcial,
			SitioSinCobertura,
			SitioConCobertura + SitioConCoberturaParcial + SitioSinCobertura AS SitiosAnalizados,
			Ingeniero,
			Capex,
			Opex,
			EstadoFactibilidad,
	    	FilteredCount,
	    	@TotalCount AS TotalCount
	  	FROM CTE_Results
		WHERE RowNum BETWEEN @FirstRec AND @LastRec  
	END
END;