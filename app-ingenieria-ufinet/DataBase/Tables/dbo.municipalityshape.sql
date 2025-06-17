IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'municipalityshape')
BEGIN
	CREATE TABLE dbo.municipalityshape (
		ogr_fid INT NOT NULL,
		ogr_geometry GEOMETRY NULL,
		gid_2 NVARCHAR(MAX) NULL,
		gid_0 NVARCHAR(MAX) NULL,
		country NVARCHAR(MAX) NULL,
		gid_1 NVARCHAR(MAX) NULL,
		name_1 NVARCHAR(MAX) NULL,
		nl_name_1 NVARCHAR(MAX) NULL,
		name_2 NVARCHAR(MAX) NULL,
		varname_2 NVARCHAR(MAX) NULL,
		nl_name_2 NVARCHAR(MAX) NULL,
		type_2 NVARCHAR(MAX) NULL,
		engtype_2 NVARCHAR(MAX) NULL,
		cc_2 NVARCHAR(MAX) NULL,
		hasc_2 NVARCHAR(MAX) NULL
	);
END

-- 1. Agregar columna si no existe
IF NOT EXISTS (
    SELECT 1 
    FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Municipalityshape' 
    AND COLUMN_NAME = 'IdMunicipality'
)
BEGIN
    ALTER TABLE dbo.Municipalityshape ADD IdMunicipality INT NULL;
END