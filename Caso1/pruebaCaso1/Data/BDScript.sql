-- Crear la base de datos
CREATE DATABASE caso1;
GO

-- Usar la base de datos
USE caso1;
GO

-- Tabla Habitaciones
CREATE TABLE Habitaciones (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    CodigoDeHabitacion VARCHAR(7) NOT NULL,
    NombreDeHabitacion VARCHAR(30) NOT NULL,
    CantidadDeHuespedesPermitidos INT NOT NULL CHECK (CantidadDeHuespedesPermitidos > 0),
    CantidadDeCamas INT NOT NULL,
    CantidadDeBanos INT NOT NULL,
    Ubicacion VARCHAR(10) NOT NULL,
    EncargadoDeLimpieza VARCHAR(100) NOT NULL,
    TipoDeHabitacion INT NOT NULL CHECK (TipoDeHabitacion IN (1, 2, 3)),
    CostoDeLimpieza DECIMAL(18,2) NOT NULL CHECK (CostoDeLimpieza > 0),
    CostoDeReserva DECIMAL(18,2) NOT NULL CHECK (CostoDeReserva > 0),
    FechaDeRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    FechaDeModificacion DATETIME NULL,
    Estado BIT NOT NULL DEFAULT 1
);
GO

-- Tabla Reservaciones
CREATE TABLE Reservaciones (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    NombreDeLaPersona VARCHAR(150) NOT NULL,
    Identificacion VARCHAR(30) NOT NULL,
    Telefono VARCHAR(10) NOT NULL,
    Correo VARCHAR(50) NOT NULL,
    FechaNacimiento DATETIME NOT NULL,
    Direccion VARCHAR(200) NOT NULL,
    MontoTotal DECIMAL(18,2) NOT NULL CHECK (MontoTotal >= 0),
    FechaInicioReserva DATETIME NOT NULL,
    FechaFinReserva DATETIME NOT NULL,
    FechaDeRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    IdHabitacion INT NOT NULL REFERENCES Habitaciones(Id)  -- Llave foránea inline
);
GO

