
--- Nombre de la BD EscuelaXYZ

-- Tablas alumnos, aulas y docentes 


	CREATE DATABASE EscuelaXYZZ;
	GO
	USE EscuelaXYZZ;
	GO

	CREATE TABLE Docente(
		DocenteId INT IDENTITY PRIMARY KEY,
		Nombre VARCHAR(50) NOT NULL,
		GradoAcademico VARCHAR(50) NULL
	);

	CREATE TABLE Aula (
		AulaId INT IDENTITY PRIMARY KEY,
		Nombre VARCHAR(10) NOT NULL,
		Ubicacion VARCHAR(50),
		DocenteId INT NOT NULL,
		FOREIGN KEY (DocenteId) REFERENCES Docente(DocenteId)
	);

	CREATE TABLE Alumnos (
		AlumnoId INT IDENTITY PRIMARY KEY,
		Nombre VARCHAR(50) NOT NULL,
		Edad INT,
		AulaId INT NOT NULL,
		FOREIGN KEY (AulaId) REFERENCES Aula(AulaId)
	);

--crear mínimo 2 aulas para el ejercicio 

--crear dos docentes como mínimo para el ejercicio 

-- Insercion 

	INSERT INTO Docente (Nombre, GradoAcademico)
	VALUES ('Juan Perez', 'Lic.'), ('Carlos Quispe', 'Mg.');

	INSERT INTO Aula (Nombre, Ubicacion, DocenteId)
	VALUES ('Aula A', 'Piso 1', 1), ('Aula B', 'Piso 2', 2);

-- Procedimientos 

--1.	El sistema debe ver la lista de alumnos por cada aula.

		CREATE OR ALTER PROCEDURE sp_Alumno_ListarPorAula
		@AulaId INT
		AS
		BEGIN
			SET NOCOUNT ON;

			IF @AulaId IS NULL OR @AulaId <= 0
			BEGIN
				SELECT 'El AulaId es inválido' AS Msg, 1 AS Rpta;
				RETURN;
			END

			SELECT AlumnoId, Nombre, Edad
			FROM Alumnos
			WHERE AulaId = @AulaId
			ORDER BY Nombre;

			SELECT 'OK' AS Msg, 0 AS Rpta;
		END

GO

--2.	Un docente solo puede ver la lista de alumnos si es docente de esa aula.

		CREATE OR ALTER PROCEDURE sp_Alumno_ListarPorDocente
		@DocenteId INT,
		@AulaId INT
		AS
		BEGIN
			SET NOCOUNT ON;

			IF NOT EXISTS (SELECT 1 FROM Aula WHERE AulaId = @AulaId AND DocenteId = @DocenteId)
			BEGIN
				SELECT 'El docente no pertenece a esta aula' AS Msg, 1 AS Rpta;
				RETURN;
			END

			SELECT al.AlumnoId, al.Nombre, al.Edad
			FROM Alumnos al
			WHERE al.AulaId = @AulaId;

			SELECT 'OK' AS Msg, 0 AS Rpta;
		END
GO
		
--3.	El sistema debe permitir asignar alumnos en aulas (Max de alumnos por aula debe ser de 5 en caso supere la cantidad mostrar mensaje correspondiente.)

		CREATE OR ALTER PROCEDURE sp_Alumno_Asignar
		@Nombre VARCHAR(50),
		@Edad INT,
		@AulaId INT
		AS
		BEGIN
			SET NOCOUNT ON;

			IF @AulaId IS NULL OR @AulaId <= 0
			BEGIN
				SELECT 'El AulaId es inválido' AS Msg, 1 AS Rpta;
				RETURN;
			END

			IF (SELECT COUNT(*) FROM Alumnos WHERE AulaId = @AulaId) >= 5
			BEGIN
				SELECT 'El aula ya tiene el máximo de 5 alumnos' AS Msg, 1 AS Rpta;
				RETURN;
			END

			INSERT INTO Alumnos (Nombre, Edad, AulaId)
			VALUES (@Nombre, @Edad, @AulaId);

			SELECT 'Alumno Creado Correctamente' AS Msg, 0 AS Rpta, SCOPE_IDENTITY() AS AlumnoId;
		END
GO

--4.	El sistema debe permitir actualizar la información de sus alumnos

		CREATE OR ALTER PROCEDURE sp_Alumno_Actualizar
		@AlumnoId INT,
		@Nombre VARCHAR(50),
		@Edad INT
		AS
		BEGIN
			SET NOCOUNT ON;

			IF NOT EXISTS (SELECT 1 FROM Alumnos WHERE AlumnoId = @AlumnoId)
			BEGIN
				SELECT 'El alumno no existe' AS Msg, 1 AS Rpta;
				RETURN;
			END

			UPDATE Alumnos
			SET Nombre = @Nombre,
				Edad = @Edad
			WHERE AlumnoId = @AlumnoId;

			SELECT 'Alumno Actualizado Correctamente' AS Msg, 0 AS Rpta;
		END
GO
--5.	El sistema debe permitir eliminar un alumno de un aula

		CREATE OR ALTER PROCEDURE sp_Alumno_Eliminar
		@AlumnoId INT
		AS
		BEGIN
			SET NOCOUNT ON;

			IF NOT EXISTS (SELECT 1 FROM Alumnos WHERE AlumnoId = @AlumnoId)
			BEGIN
				SELECT 'El alumno no existe' AS Msg, 1 AS Rpta;
				RETURN;
			END

			DELETE FROM Alumnos WHERE AlumnoId = @AlumnoId;

			SELECT 'Alumno Eliminado Correctamente' AS Msg, 0 AS Rpta;
		END
GO
