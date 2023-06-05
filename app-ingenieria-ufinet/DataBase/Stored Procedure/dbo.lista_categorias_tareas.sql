IF EXISTS (SELECT 1 FROM sys.objects s WHERE s.object_id = OBJECT_ID('dbo.lista_categorias_tareas') AND type IN (N'P', N'PC'))
DROP PROCEDURE lista_categorias_tareas
GO

CREATE PROCEDURE dbo.lista_categorias_tareas
AS
BEGIN
  	SELECT
		IdCategoriaTarea,
		Categoria 
	FROM CategoriaTarea ct 
END