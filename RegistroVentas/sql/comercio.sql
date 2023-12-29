CREATE DATABASE COMERCIO
GO

USE COMERCIO
GO

CREATE TABLE categorias
(
	idcategoria		INT IDENTITY(1,1) PRIMARY KEY,
	categoria		VARCHAR(50) NOT NULL,
	created_at		DATETIME NOT NULL DEFAULT GETDATE(),
	updated_at		DATETIME NULL
)
GO

INSERT INTO categorias (categoria) values ('galletas')
GO


CREATE TABLE productos
(
	idproducto		INT	IDENTITY(1,1) PRIMARY KEY,
	idcategoria		INT NOT NULL,
	nomproducto		VARCHAR(80) NOT NULL,
	cantidad		INT NOT NULL,
	precio			DECIMAL(5,2) NOT NULL,
	created_at		DATETIME NOT NULL DEFAULT GETDATE(),
	updated_at		DATETIME NULL,
	CONSTRAINT uk_producto UNIQUE(nomproducto),
	CONSTRAINT fk_idcategoria	FOREIGN KEY (idcategoria) REFERENCES categorias (idcategoria)
)
GO
INSERT INTO productos (idcategoria, nomproducto, cantidad, precio) values (1,'sprite', 40, 3.20)

CREATE TABLE boleta
(
	idboleta	INT IDENTITY(1,1) PRIMARY KEY,
	codboleta	CHAR(5) NOT NULL,
	created_at	DATETIME NOT NULL DEFAULT GETDATE()
	CONSTRAINT uk_codboleta	UNIQUE(codboleta)
)
GO



CREATE TABLE productoselegidos
(
	idproductoselegidos		INT IDENTITY(1,1) PRIMARY KEY,
	idboleta				INT NOT NULL,
	idproducto				INT NOT NULL,
	cantidad				INT NOT NULL
	CONSTRAINT fk_idboleta	FOREIGN KEY(idboleta) REFERENCES boleta(idboleta)
)
GO

select * from productoselegidos

CREATE TABLE tipopago
(
	idtipopago				INT IDENTITY(1,1) PRIMARY KEY,
	tipopago				VARCHAR(80) NOT NULL
)
GO

INSERT INTO tipopago(tipopago) values ('efectivo')

CREATE TABLE pago
(
	idpago					INT IDENTITY(1,1) PRIMARY KEY,
	idboleta				INT NOT NULL,
	idtipopago				INT NOT NULL
	CONSTRAINT fk_idcontrato	FOREIGN KEY(idboleta) REFERENCES boleta(idboleta),
	CONSTRAINT fk_idtipopago	FOREIGN KEY(idtipopago) REFERENCES tipopago (idtipopago)
)
GO


