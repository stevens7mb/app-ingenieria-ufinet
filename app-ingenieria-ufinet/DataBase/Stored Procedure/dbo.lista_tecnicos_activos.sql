IF EXISTS (SELECT 1 FROM sys.objects s WHERE s.object_id = OBJECT_ID('dbo.lista_tecnicos_activos') AND type IN (N'P', N'PC'))
DROP PROCEDURE lista_tecnicos_activos
GO

CREATE PROCEDURE dbo.lista_tecnicos_activos
AS
BEGIN
  	SELECT
		IdTecnico,
		Nombre,
		Usuario
	FROM Tecnico t
	WHERE t.Estado = -1
END