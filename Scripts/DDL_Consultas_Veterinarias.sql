CREATE DATABASE Consultas_Veterinarias;
GO

USE Consultas_Veterinarias;
GO


CREATE TABLE Animais(
	Id INT PRIMARY KEY IDENTITY,
	Nome NVARCHAR(MAX),
	Raca NVARCHAR(MAX),
	NomeDono NVARCHAR(MAX),
	ContatoDono NVARCHAR(MAX),
	Imagem NVARCHAR(MAX)
);

CREATE TABLE Veterinarios(
	Id INT PRIMARY KEY IDENTITY,
	Nome NVARCHAR(MAX),
	Email NVARCHAR(MAX),
	Contato NVARCHAR(MAX),
	Imagem NVARCHAR(MAX)
);

CREATE TABLE Consultas(
	Id INT PRIMARY KEY IDENTITY,
	Descricao NVARCHAR(MAX),
	
	/*FKs*/
	AnimalId INT
	FOREIGN KEY (AnimalId) REFERENCES Animais(Id),

	VeterinarioId INT
	FOREIGN KEY (VeterinarioId) REFERENCES Veterinarios(Id)
);

CREATE TABLE Diagnosticos(
	Id INT PRIMARY KEY IDENTITY,
	Diagnostico NVARCHAR(MAX),

	/* FKs */
	ConsultaId INT
	FOREIGN KEY (ConsultaId) REFERENCES Consultas(Id)
);