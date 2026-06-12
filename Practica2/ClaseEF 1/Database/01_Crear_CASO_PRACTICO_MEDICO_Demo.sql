/*
    Script demo para recrear la base usada por ClaseEF
    Ejecutar en SQL Server con permisos para crear base de datos.

    Si ya existe la base, el script la elimina y la vuelve a crear.
*/

USE master;
GO

IF DB_ID(N'CASO_PRACTICO_MEDICO') IS NOT NULL
BEGIN
    ALTER DATABASE CASO_PRACTICO_MEDICO SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE CASO_PRACTICO_MEDICO;
END
GO

CREATE DATABASE CASO_PRACTICO_MEDICO;
GO

USE CASO_PRACTICO_MEDICO;
GO

CREATE TABLE dbo.Clinicas
(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Direccion NVARCHAR(200) NULL
);
GO

CREATE TABLE dbo.Especialidades
(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE dbo.Servicios
(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(200) NOT NULL,
    Monto DECIMAL(18,2) NOT NULL,
    IVA DECIMAL(18,2) NOT NULL,
    Especialidad INT NOT NULL,
    Especialista NVARCHAR(200) NOT NULL,
    Clinica NVARCHAR(200) NOT NULL,
    FechaDeRegistro DATETIME NOT NULL,
    FechaDeModificacion DATETIME NULL,
    ESTADO BIT NOT NULL
);
GO

CREATE TABLE dbo.Pacientes
(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    NombreDeLaPersona NVARCHAR(150) NOT NULL,
    Identificacion NVARCHAR(30) NOT NULL,
    Telefono NVARCHAR(10) NOT NULL,
    Correo NVARCHAR(50) NOT NULL,
    FechaNacimiento DATETIME NOT NULL,
    Direccion NVARCHAR(200) NOT NULL
);
GO

CREATE TABLE dbo.CITAS
(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    MontoTotal DECIMAL(18,2) NOT NULL,
    FechaDeLaCita DATETIME NOT NULL,
    FechaDeRegistro DATETIME NOT NULL,
    IdServicio INT NOT NULL,
    IdPaciente INT NOT NULL
);
GO

INSERT INTO dbo.Clinicas (Nombre, Direccion)
VALUES
    (N'Clinica Biblica', N'San Jose, Costa Rica'),
    (N'Nuevo Amanecer', N'Heredia, Costa Rica'),
    (N'Centro Medico del Este', N'Cartago, Costa Rica');
GO

INSERT INTO dbo.Especialidades (Nombre)
VALUES
    (N'Odontologia'),
    (N'Radiologia'),
    (N'Laboratorio Clinico'),
    (N'Pediatria');
GO

INSERT INTO dbo.Servicios
(
    Nombre,
    Descripcion,
    Monto,
    IVA,
    Especialidad,
    Especialista,
    Clinica,
    FechaDeRegistro,
    FechaDeModificacion,
    ESTADO
)
VALUES
    (N'Limpieza dental', N'Profilaxis dental para adultos', 25000.00, 13.00, 1, N'Dra. Maria Solis', N'Clinica Biblica', GETDATE(), NULL, 1),
    (N'Extraccion de cordal', N'Procedimiento odontologico ambulatorio', 60000.00, 13.00, 1, N'Dr. Juan Ramos', N'Nuevo Amanecer', GETDATE(), NULL, 1),
    (N'Rayos X de craneo', N'Estudio radiologico de cabeza', 130000.00, 2.00, 2, N'Dr. Francisco Gutierrez', N'Clinica Biblica', GETDATE(), NULL, 1),
    (N'Examen general de orina', N'Prueba de laboratorio para poblacion general', 21000.00, 13.00, 3, N'Dra. Josseline Arguedas', N'Centro Medico del Este', GETDATE(), NULL, 1),
    (N'Consulta pediatrica', N'Valoracion general para ninos', 35000.00, 12.00, 4, N'Dra. Jennifer Campos', N'Nuevo Amanecer', GETDATE(), NULL, 1);
GO

INSERT INTO dbo.Pacientes
(
    NombreDeLaPersona,
    Identificacion,
    Telefono,
    Correo,
    FechaNacimiento,
    Direccion
)
VALUES
    (N'Juan Perez', N'304940356', N'88888888', N'juan@mail.com', '1998-04-12', N'San Jose centro'),
    (N'Ana Rodriguez', N'208760145', N'87878787', N'ana@mail.com', '2001-09-25', N'Heredia'),
    (N'Carlos Mena', N'112340987', N'89999999', N'carlos@mail.com', '1990-01-10', N'Cartago');
GO

INSERT INTO dbo.CITAS
(
    MontoTotal,
    FechaDeLaCita,
    FechaDeRegistro,
    IdServicio,
    IdPaciente
)
VALUES
    (67800.00, DATEADD(DAY, 1, GETDATE()), GETDATE(), 2, 1),
    (25000.00, DATEADD(DAY, 2, GETDATE()), GETDATE(), 1, 2),
    (35000.00, DATEADD(DAY, 3, GETDATE()), GETDATE(), 5, 3);
GO

SELECT 'Clinicas' AS Tabla, COUNT(*) AS Total FROM dbo.Clinicas
UNION ALL
SELECT 'Especialidades', COUNT(*) FROM dbo.Especialidades
UNION ALL
SELECT 'Servicios', COUNT(*) FROM dbo.Servicios
UNION ALL
SELECT 'Pacientes', COUNT(*) FROM dbo.Pacientes
UNION ALL
SELECT 'CITAS', COUNT(*) FROM dbo.CITAS;
GO