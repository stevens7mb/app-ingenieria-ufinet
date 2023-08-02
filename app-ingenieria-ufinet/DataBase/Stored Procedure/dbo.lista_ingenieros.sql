IF EXISTS (SELECT 1 FROM sys.objects s WHERE s.object_id = OBJECT_ID('dbo.lista_ingenieros') AND type IN (N'P', N'PC'))
DROP PROCEDURE lista_ingenieros
GO

CREATE PROCEDURE dbo.lista_ingenieros
AS
BEGIN
  	SELECT
		IdIngeniero,
		Nombre,
		Usuario,
		Estado
	FROM Provisioning 
END