CREATE OR ALTER PROCEDURE detalles_editar_factibilidad(
	@id_factibilidad INT
) AS
BEGIN
	SELECT 
		IdFactibilidad, 
		Ticket, 
		Estudio, 
		IdCliente,
		IdKAM, 
		BW, 
		FechaSolicitud,
		FechaRespuesta,
		Estado,
		SitioConCobertura,
		SitioConCoberturaParcial,
		SitioSinCobertura,
		IdIngeniero,
		ISNULL(IdTipoServicio, 0) AS IdTipoServicio,
		Coordenada,
		IdMunicipio,
		IdDepartamento,
		Capex,
		Opex,
		EsSubcontratado
	FROM Factibilidad
	WHERE IdFactibilidad = @id_factibilidad
END;