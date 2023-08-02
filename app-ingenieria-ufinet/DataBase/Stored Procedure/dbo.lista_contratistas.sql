IF EXISTS (SELECT 1 FROM sys.objects s WHERE s.object_id = OBJECT_ID('dbo.lista_contratistas') AND type IN (N'P', N'PC'))
DROP PROCEDURE lista_contratistas
GO

CREATE PROCEDURE dbo.lista_contratistas
AS
BEGIN
  	SELECT
		IdContrata,
		NombreContrata,
		Estado
	FROM Contrata c
END