
CREATE DATABASE CASO_PRACTICO_RESERVACIONES

GO

USE CASO_PRACTICO_RESERVACIONES

GO

CREATE TABLE HABITACIONES (
    Id int identity (1,1) not null,
    CodigoDeHabitacion varchar(7) not null,
    NombreDeHabitacion varchar(30) not null,
	CantidadDeHuespedesPermitidos int not null,
	CantidadDeCamas int not null,
	CantidadDeBanos int not null,
	Ubicacion varchar(10) not null,
	EncargadoDeLimpieza varchar(100) not null,
	TipoDeHabitacion int not null,
	CostoDeLimpieza decimal(18,2) not null,
	CostoDeReserva decimal(18,2) not null,
	FechaDeRegistro datetime not null,
	FechaDeModificacion datetime,
	Estado bit,
);

go

CREATE TABLE RESERVACIONES (
    Id int identity (1,1) not null,
    NombreDeLaPersona varchar(150) not null,
    Identificacion varchar(30) not null,
	Telefono varchar(10) not null,
	Correo varchar(50) not null,
	FechaNacimiento datetime not null,
	Direccion varchar(200) not null,
	MontoTotal decimal(18,2) not null,
	FechaInicioReserva datetime not null,
	FechaFinReserva datetime not null,
	FechaDeRegistro datetime not null,
	IdHabitacion int not null,
);