--Limpiar tablas
DROP TABLE dbo.ServiceDeskTicketLog
DROP TABLE dbo.ServiceDeskTicketFile 
DROP TABLE dbo.ServiceDeskTicketStatusHistory
DROP TABLE dbo.ServiceDeskTicket 
DROP TABLE dbo.Municipality
DROP TABLE dbo.State 
DROP TABLE dbo.Engineer 
DROP TABLE dbo.Region


--Incident Type
INSERT INTO dbo.IncidentType (Description) VALUES(N'Avería en FO')
INSERT INTO dbo.IncidentType (Description) VALUES(N'Corte en FO')
INSERT INTO dbo.IncidentType (Description) VALUES(N'Desconexión de equipo')
INSERT INTO dbo.IncidentType (Description) VALUES(N'Mufa en mal estado')
INSERT INTO dbo.IncidentType (Description) VALUES(N'Corte en mufa')
INSERT INTO dbo.IncidentType (Description) VALUES(N'etc.')

--Primary Cause
INSERT INTO dbo.PrimaryCause (Description) VALUES(N'Trabajos Propios')
INSERT INTO dbo.PrimaryCause (Description) VALUES(N'No aplica')
INSERT INTO dbo.PrimaryCause (Description) VALUES(N'Cliente')
INSERT INTO dbo.PrimaryCause (Description) VALUES(N'Desastre natural')
INSERT INTO dbo.PrimaryCause (Description) VALUES(N'etc.')

--Affected Element
INSERT INTO dbo.AffectedElement (Description) VALUES(N'FO')
INSERT INTO dbo.AffectedElement (Description) VALUES(N'Mufa')
INSERT INTO dbo.AffectedElement (Description) VALUES(N'Cliente')

--Solution Type
INSERT INTO dbo.SolutionType (Description) VALUES(N'Definitiva')
INSERT INTO dbo.SolutionType (Description) VALUES(N'Parcial')
INSERT INTO dbo.SolutionType (Description) VALUES(N'Sin Solución')

--Confirmed Area
INSERT INTO dbo.ConfirmedArea(IdConfirmedArea, Description) VALUES(1, 'Cliente')
INSERT INTO dbo.ConfirmedArea(IdConfirmedArea, Description) VALUES(2, 'Área de Mantenimiento')
INSERT INTO dbo.ConfirmedArea(IdConfirmedArea, Description) VALUES(3, 'Área de Ingeniería')

INSERT INTO dbo.Region (IdRegion, Description) VALUES
(1, 'Región Metropolitana'),
(2, 'Región Norte'),
(3, 'Región Nororiental'),
(4, 'Región Suroriental'),
(5, 'Región Central'),
(6, 'Región Sur Occidental'),
(7, 'Región Noroccidental'),
(8, 'Región Petén');

--State
INSERT INTO state(IdState, IdRegion, Name) VALUES (1, 1, 'Guatemala');
INSERT INTO state(IdState,IdRegion, Name) VALUES (2, 2, 'Baja Verapaz');
INSERT INTO state(IdState,IdRegion, Name) VALUES (3, 2, 'Alta Verapaz');
INSERT INTO state(IdState,IdRegion, Name) VALUES (4, 3, 'El Progreso');
INSERT INTO state(IdState,IdRegion, Name) VALUES (5, 3, 'Izabal');
INSERT INTO state(IdState,IdRegion, Name) VALUES (6, 3, 'Zacapa');
INSERT INTO state(IdState,IdRegion, Name) VALUES (7, 3, 'Chiquimula');
INSERT INTO state(IdState,IdRegion, Name) VALUES (8, 4, 'Santa Rosa');
INSERT INTO state(IdState,IdRegion, Name) VALUES (9, 4, 'Jalapa');
INSERT INTO state(IdState,IdRegion, Name) VALUES (10, 4, 'Jutiapa');
INSERT INTO state(IdState,IdRegion, Name) VALUES (11, 5, 'Sacatepéquez');
INSERT INTO state(IdState,IdRegion, Name) VALUES (12, 5, 'Chimaltenango');
INSERT INTO state(IdState,IdRegion, Name) VALUES (13, 5, 'Escuintla');
INSERT INTO state(IdState,IdRegion, Name) VALUES (14, 6, 'Solola');
INSERT INTO state(IdState,IdRegion, Name) VALUES (15, 6, 'Totonicapán');
INSERT INTO state(IdState,IdRegion, Name) VALUES (16, 6, 'Quetzaltenango');
INSERT INTO state(IdState,IdRegion, Name) VALUES (17, 6, 'Suchitepéquez');
INSERT INTO state(IdState,IdRegion, Name) VALUES (18, 6, 'Retalhuleu');
INSERT INTO state(IdState,IdRegion, Name) VALUES (19, 6, 'San Marcos');
INSERT INTO state(IdState,IdRegion, Name) VALUES (20, 7, 'Huehuetenango');
INSERT INTO state(IdState,IdRegion, Name) VALUES (21, 7, 'Quiché');
INSERT INTO state(IdState,IdRegion, Name) VALUES (22, 8, 'Petén');

--Municipality
INSERT INTO municipality(Name, IdState) VALUES
-- Alta Verapaz
('Chahal', 3),
('Chisec', 3),
('Cobán', 3),
('Fray Bartolomé de las Casas', 3),
('La Tinta', 3),
('Lanquín', 3),
('Panzós', 3),
('Raxruhá', 3),
('San Cristóbal Verapaz', 3),
('San Juan Chamelco', 3),
('San Pedro Carchá', 3),
('Santa Cruz Verapaz', 3),
('Santa María Cahabón', 3),
('Senahú', 3),
('Tamahú', 3),
('Tactic', 3),
('Tucurú', 3),

    -- Baja Verapaz
('Cubulco', 2),
('Granados', 2),
('Purulhá', 2),
('Rabinal', 2),
('Salamá', 2),
('San Jerónimo', 2),
('San Miguel Chicaj', 2),
('Santa Cruz el Chol', 2),

-- Chimaltenango
('Acatenango', 12),
('Chimaltenango', 12),
('El Tejar', 12),
('Parramos', 12),
('Patzicía', 12),
('Patzún', 12),
('Pochuta', 12),
('San Andrés Itzapa', 12),
('San José Poaquíl', 12),
('San Juan Comalapa', 12),
('San Martín Jilotepeque', 12),
('Santa Apolonia', 12),
('Santa Cruz Balanyá', 12),
('Tecpán', 12),
('Yepocapa', 12),
('Zaragoza', 12),

    -- Chiquimula
('Camotán', 7),
('Chiquimula', 7),
('Concepción Las Minas', 7),
('Esquipulas', 7),
('Ipala', 7),
('Jocotán', 7),
('Olopa', 7),
('Quezaltepeque', 7),
('San Jacinto', 7),
('San José la Arada', 7),
('San Juan Ermita', 7),

-- El Progreso
('El Jícaro', 4),
('Guastatoya', 4),
('Morazán', 4),
('San Agustín Acasaguastlán', 4),
('San Antonio La Paz', 4),
('San Cristóbal Acasaguastlán', 4),
('Sanarate', 4),
('Sansare', 4),

-- Escuintla
('Escuintla', 13),
('Guanagazapa', 13),
('Iztapa', 13),
('La Democracia', 13),
('La Gomera', 13),
('Masagua', 13),
('Nueva Concepción', 13),
('Palín', 13),
('San José', 13),
('San Vicente Pacaya', 13),
('Santa Lucía Cotzumalguapa', 13),
('Siquinalá', 13),
('Tiquisate', 13),

-- Guatemala
('Amatitlán', 1),
('Chinautla', 1),
('Chuarrancho', 1),
('Guatemala', 1),
('Fraijanes', 1),
('Mixco', 1),
('Palencia', 1),
('San José del Golfo', 1),
('San José Pinula', 1),
('San Juan Sacatepéquez', 1),
('San Miguel Petapa', 1),
('San Pedro Ayampuc', 1),
('San Pedro Sacatepéquez', 1),
('San Raymundo', 1),
('Santa Catarina Pinula', 1),
('Villa Canales', 1),
('Villa Nueva', 1),

-- Huehuetenango
('Aguacatán', 20),
('Chiantla', 20),
('Colotenango', 20),
('Concepción Huista', 20),
('Cuilco', 20),
('Huehuetenango', 20),
('Jacaltenango', 20),
('La Democracia', 20),
('La Libertad', 20),
('Malacatancito', 20),
('Nentón', 20),
('San Antonio Huista', 20),
('San Gaspar Ixchil', 20),
('San Ildefonso Ixtahuacán', 20),
('San Juan Atitán', 20),
('San Juan Ixcoy', 20),
('San Mateo Ixtatán', 20),
('San Miguel Acatán', 20),
('San Pedro Nécta', 20),
('San Pedro Soloma', 20),
('San Rafael La Independencia', 20),
('San Rafael Pétzal', 20),
('San Sebastián Coatán', 20),
('San Sebastián Huehuetenango', 20),
('Santa Ana Huista', 20),
('Santa Bárbara', 20),
('Santa Cruz Barillas', 20),
('Santa Eulalia', 20),
('Santiago Chimaltenango', 20),
('Tectitán', 20),
('Todos Santos Cuchumatán', 20),
('Unión Cantinil', 20),

-- Izabal
('El Estor', 5),
('Livingston', 5),
('Los Amates', 5),
('Morales', 5),
('Puerto Barrios', 5),

-- Jalapa
('Jalapa', 9),
('Mataquescuintla', 9),
('Monjas', 9),
('San Carlos Alzatate', 9),
('San Luis Jilotepeque', 9),
('San Manuel Chaparrón', 9),
('San Pedro Pinula', 9),

-- Jutiapa
('Agua Blanca', 10),
('Asunción Mita', 10),
('Atescatempa', 10),
('Comapa', 10),
('Conguaco', 10),
('El Adelanto', 10),
('El Progreso', 10),
('Jalpatagua', 10),
('Jerez', 10),
('Jutiapa', 10),
('Moyuta', 10),
('Pasaco', 10),
('Quesada', 10),
('San José Acatempa', 10),
('Santa Catarina Mita', 10),
('Yupiltepeque', 10),
('Zapotitlán', 10),

-- Petén
('Dolores', 22),
('El Chal', 22),
('Ciudad Flores', 22),
('La Libertad', 22),
('Las Cruces', 22),
('Melchor de Mencos', 22),
('Poptún', 22),
('San Andrés', 22),
('San Benito', 22),
('San Francisco', 22),
('San José', 22),
('San Luis', 22),
('Santa Ana', 22),
('Sayaxché', 22),

-- Quetzaltenango
('Almolonga', 16),
('Cabricán', 16),
('Cajolá', 16),
('Cantel', 16),
('Coatepeque', 16),
('Colomba Costa Cuca', 16),
('Concepción Chiquirichapa', 16),
('El Palmar', 16),
('Flores Costa Cuca', 16),
('Génova', 16),
('Huitán', 16),
('La Esperanza', 16),
('Olintepeque', 16),
('Palestina de Los Altos', 16),
('Quetzaltenango', 16),
('Salcajá', 16),
('San Carlos Sija', 16),
('San Francisco La Unión', 16),
('San Juan Ostuncalco', 16),
('San Martín Sacatepéquez', 16),
('San Mateo', 16),
('San Miguel Sigüilá', 16),
('Sibilia', 16),
('Zunil', 16),

-- Quiché
('Canillá', 21),
('Chajul', 21),
('Chicamán', 21),
('Chiché', 21),
('Chichicastenango', 21),
('Chinique', 21),
('Cunén', 21),
('Ixcán Playa Grande', 21),
('Joyabaj', 21),
('Nebaj', 21),
('Pachalum', 21),
('Patzité', 21),
('Sacapulas', 21),
('San Andrés Sajcabajá', 21),
('San Antonio Ilotenango', 21),
('San Bartolomé Jocotenango', 21),
('San Juan Cotzal', 21),
('San Pedro Jocopilas', 21),
('Santa Cruz del Quiché', 21),
('Uspantán', 21),
('Zacualpa', 21),

-- Retalhuleu
('Champerico', 18),
('El Asintal', 18),
('Nuevo San Carlos', 18),
('Retalhuleu', 18),
('San Andrés Villa Seca', 18),
('San Felipe Reu', 18),
('San Martín Zapotitlán', 18),
('San Sebastián', 18),
('Santa Cruz Muluá', 18),

-- Sacatepéquez
('Alotenango', 11),
('Ciudad Vieja', 11),
('Jocotenango', 11),
('Antigua Guatemala', 11),
('Magdalena Milpas Altas', 11),
('Pastores', 11),
('San Antonio Aguas Calientes', 11),
('San Bartolomé Milpas Altas', 11),
('San Lucas Sacatepéquez', 11),
('San Miguel Dueñas', 11),
('Santa Catarina Barahona', 11),
('Santa Lucía Milpas Altas', 11),
('Santa María de Jesús', 11),
('Santiago Sacatepéquez', 11),
('Santo Domingo Xenacoj', 11),
('Sumpango', 11),

-- San Marcos
('Ayutla', 19),
('Catarina', 19),
('Comitancillo', 19),
('Concepción Tutuapa', 19),
('El Quetzal', 19),
('El Tumbador', 19),
('Esquipulas Palo Gordo', 19),
('Ixchiguán', 19),
('La Blanca', 19),
('La Reforma', 19),
('Malacatán', 19),
('Nuevo Progreso', 19),
('Ocós', 19),
('Pajapita', 19),
('Río Blanco', 19),
('San Antonio Sacatepéquez', 19),
('San Cristóbal Cucho', 19),
('San José El Rodeo', 19),
('San José Ojetenam', 19),
('San Lorenzo', 19),
('San Marcos', 19),
('San Miguel Ixtahuacán', 19),
('San Pablo', 19),
('San Pedro Sacatepéquez', 19),
('San Rafael Pie de la Cuesta', 19),
('Sibinal', 19),
('Sipacapa', 19),
('Tacaná', 19),
('Tajumulco', 19),
('Tejutla', 19),

-- Santa Rosa
('Barberena', 8),
('Casillas', 8),
('Chiquimulilla', 8),
('Cuilapa', 8),
('Guazacapán', 8),
('Nueva Santa Rosa', 8),
('Oratorio', 8),
('Pueblo Nuevo Viñas', 8),
('San Juan Tecuaco', 8),
('San Rafael las Flores', 8),
('Santa Cruz Naranjo', 8),
('Santa María Ixhuatán', 8),
('Santa Rosa de Lima', 8),
('Taxisco', 8),

-- Sololá
('Concepción', 14),
('Nahualá', 14),
('Panajachel', 14),
('San Andrés Semetabaj', 14),
('San Antonio Palopó', 14),
('San José Chacayá', 14),
('San Juan La Laguna', 14),
('San Lucas Tolimán', 14),
('San Marcos La Laguna', 14),
('San Pablo La Laguna', 14),
('San Pedro La Laguna', 14),
('Santa Catarina Ixtahuacán', 14),
('Santa Catarina Palopó', 14),
('Santa Clara La Laguna', 14),
('Santa Cruz La Laguna', 14),
('Santa Lucía Utatlán', 14),
('Santa María Visitación', 14),
('Santiago Atitlán', 14),
('Sololá', 14),

-- Suchitepéquez
('Chicacao', 17),
('Cuyotenango', 17),
('Mazatenango', 17),
('Patulul', 17),
('Pueblo Nuevo', 17),
('Río Bravo', 17),
('Samayac', 17),
('San Antonio Suchitepéquez', 17),
('San Bernardino', 17),
('San Francisco Zapotitlán', 17),
('San Gabriel', 17),
('San José El Idolo', 17),
('San José La Maquina', 17),
('San Juan Bautista', 17), 
('San Lorenzo', 17),
('San Miguel Panán', 17),
('San Pablo Jocopilas', 17),
('Santa Bárbara', 17),
('Santo Domingo Suchitepéquez', 17),
('Santo Tomás La Unión', 17),
('Zunilito', 17),

-- Totonicapán
('Momostenango', 15),
('San Andrés Xecul', 15),
('San Bartolo', 15),
('San Cristóbal Totonicapán', 15),
('San Francisco El Alto', 15),
('Santa Lucía La Reforma', 15),
('Santa María Chiquimula', 15),
('Totonicapán', 15),

-- Zacapa
('Cabañas', 6),
('Estanzuela', 6),
('Gualán', 6),
('Huité', 6),
('La Unión', 6),
('Río Hondo', 6),
('San Diego', 6),
('San Jorge', 6),
('Teculután', 6),
('Usumatlán', 6),
('Zacapa', 6)

--New Role
--IF NOT EXISTS(SELECT * FROM dbo.Rol WHERE Descripcion = 'IngenieroMantenimiento')
--BEGIN 
--	INSERT INTO dbo.Rol(
--		IdRol,
--		Descripcion,
--		SNAppContrata,
--		SNAppInspeccion,
--		SNAppSupervision,
--		SNMaintanceEngineer
--	)
--	VALUES(
--		5,
--		'IngenieroMantenimiento',
--		0,
--		0,
--		0,
--		-1
--	)
--END

--Ticket State
IF NOT EXISTS(SELECT 1 FROM dbo.TicketState WHERE Description = 'Creado')
BEGIN 
	INSERT INTO dbo.TicketState(
		IdTicketState,
		Description
	)
	VALUES(
		1,
		'Creado'
	)
END

IF NOT EXISTS(SELECT 1 FROM dbo.TicketState WHERE Description = 'Asignado')
BEGIN 
	INSERT INTO dbo.TicketState(
		IdTicketState,
		Description
	)
	VALUES(
		2,
		'Asignado'
	)
END

IF NOT EXISTS(SELECT 1 FROM dbo.TicketState WHERE Description = 'Finalizado')
BEGIN 
	INSERT INTO dbo.TicketState(
		IdTicketState,
		Description
	)
	VALUES(
		3,
		'Finalizado'
	)
END

--EngineerType
INSERT INTO dbo.EngineerType (
	EngineerTypeId,
	TypeName 
)
VALUES(
	1,
	'Aprovisionamiento'
),
(
	2,
	'Mantenimiento'
)

--Priority Level
INSERT INTO dbo.PriorityLevel(IdPriorityLevel, Description)
VALUES(1, 'Bajo'),
(2, 'Medio'),
(3, 'Alto')

--Engineers
INSERT INTO dbo.Engineer(EngineerId, Name, UserName, ActiveStatus, IdRegion, EngineerTypeId)
VALUES(1, 'Ricardo Conde Teret', 'rconde', -1, NULL, 1 )

INSERT INTO dbo.Engineer(EngineerId, Name, UserName, ActiveStatus, IdRegion, EngineerTypeId)
VALUES(2, 'Ingeniero de prueba mantenimiento', 'prueba', -1, 5, 2 )

--Prefix
INSERT INTO dbo.Prefix(IdPrefix, Prefix)
VALUES(1, 'GMT-')