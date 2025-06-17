IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'geometry_columns')
BEGIN
	CREATE TABLE geometry_columns (
		f_table_catalog VARCHAR(128) NULL,
		f_table_schema VARCHAR(128) NULL,
		f_table_name VARCHAR(256) NULL,
		f_geometry_column VARCHAR(256) NULL,
		coord_dimension INT NOT NULL,
		srid INT NOT NULL,
		geometry_type VARCHAR(30) NULL
	);
END