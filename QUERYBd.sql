--CREATE DATABASE MiIngresoHitss
-- Tabla de Productos
CREATE TABLE Productos (
    Id INT PRIMARY KEY  IDENTITY(1,1),
    Nombre VARCHAR(100),
    Descripcion VARCHAR(200),
    Precio DECIMAL(10, 2)
);

-- Tabla de Clientes
CREATE TABLE Clientes (
    Id INT PRIMARY KEY  IDENTITY(1,1),
    Nombre VARCHAR(100),
    Direccion VARCHAR(200),
    Telefono VARCHAR(20)
);

-- Tabla de Compras
	CREATE TABLE Compras (
		Id INT PRIMARY KEY IDENTITY(1,1),
		ClienteId INT,
		ProductoId INT,
		FechaCompra DATE,
		FOREIGN KEY (ClienteId) REFERENCES Clientes(Id),
		FOREIGN KEY (ProductoId) REFERENCES Productos(Id)
	);

