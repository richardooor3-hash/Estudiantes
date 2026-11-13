create database DB_Estudiantes
use DB_Estudiantes

IF OBJECT_ID('dbo.Nota', 'U') IS NOT NULL
BEGIN
	drop table Nota
END
BEGIN
    PRINT 'La tabla Nota NO existe.';
END;

IF OBJECT_ID('dbo.Estudiante', 'U') IS NOT NULL
BEGIN
	drop table Estudiante
END
BEGIN
    PRINT 'La tabla NO existe.';
END;

IF OBJECT_ID('dbo.Profesor', 'U') IS NOT NULL
BEGIN
	drop table Profesor
END
BEGIN
    PRINT 'La tabla Profesor NO existe.';
END;

create table Estudiante
(
  id int IDENTITY(1,1) primary key, 
  nombre nvarchar(200) 
)

create table Profesor
(
	id int identity(1,1) primary key, 
	nombre nvarchar(200)
)

create table Nota
(
	 id int identity(1,1) primary key, 
	 nombre nvarchar(200), 
	 idProfesor int NOT NULL, 
	 idEstudiante int NOT NULL, 
	 valor numeric(5, 2) NOT NULL
)


ALTER TABLE Nota
ADD CONSTRAINT FK_Nota_Profesor
FOREIGN KEY (idProfesor) REFERENCES Profesor(id)
ON DELETE CASCADE
ON UPDATE CASCADE;


ALTER TABLE Nota
ADD CONSTRAINT FK_Nota_Estudiante
FOREIGN KEY (idEstudiante) REFERENCES Estudiante(id)
ON DELETE CASCADE
ON UPDATE CASCADE;
