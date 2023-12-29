USE COMERCIO
GO

-- PROCEDIMIENTO ALMACENADOS
	
CREATE PROCEDURE spu_registrar_productoselegidos
@idboleta				INT,
@idproducto				INT,
@cantidad				INT
AS BEGIN
	INSERT INTO productoselegidos (idboleta, idproducto, cantidad) values (@idboleta, @idproducto, @cantidad)
END
GO


CREATE PROCEDURE spu_listar_productoselegidos
@idboleta	INT
AS BEGIN
	SELECT 
		prod.nomproducto,
		prodele.cantidad
	FROM
		productoselegidos prodele
	INNER JOIN productos prod ON prod.idproducto = prodele.idproducto
	WHERE idboleta = @idboleta
END
GO


CREATE PROCEDURE spu_listar_productos
AS BEGIN
	SELECT 
		p.idproducto,
		c.idcategoria,
		c.categoria,
		p.nomproducto,
		p.cantidad,
		p.precio
	FROM
		productos p
		INNER JOIN categorias c ON p.idcategoria = c.idcategoria
END
GO

CREATE PROCEDURE spu_registrar_producto
@idcategoria	INT,
@nomproducto	VARCHAR(80),
@cantidad		INT,
@precio			DECIMAL(5,2)
AS BEGIN
	INSERT INTO productos
				(idcategoria, nomproducto, cantidad, precio)
				VALUES
				(@idcategoria, @nomproducto, @cantidad, @precio)
END
GO

CREATE PROCEDURE spu_listar_categorias
AS BEGIN
	SELECT
		idcategoria,
		categoria
	FROM
		categorias
END
GO

CREATE PROCEDURE spu_actualizar_producto
@idproducto		INT,
@idcategoria	INT,
@nomproducto	varchar(80),
@cantidad		INT,
@precio			DECIMAL(5,2)
AS BEGIN
	UPDATE productos SET
					idcategoria = @idcategoria,
					nomproducto = @nomproducto,
					cantidad = @cantidad,
					precio = @precio,
					updated_at = GETDATE()
					WHERE 
					idproducto = @idproducto

END
GO

CREATE PROCEDURE spu_listar_tipopago
AS BEGIN
	SELECT
		idtipopago,
		tipopago
	FROM
		tipopago
END
GO

CREATE PROCEDURE spu_registrar_boleta
@codboleta	CHAR(5)
AS BEGIN
	INSERT INTO boleta (codboleta) values (@codboleta)
END
GO

CREATE PROCEDURE spu_registrar_pago
@idboleta	INT,
@idtipopago	INT
AS BEGIN
	INSERT INTO pago (idboleta, idtipopago) values (@idboleta, @idtipopago)
END
GO

CREATE PROCEDURE spu_listar_registros
@idboleta	INT
AS BEGIN
	SELECT 
		bol.codboleta,
		bol.created_at,
		prod.nomproducto,
		prod.precio,
		prdele.cantidad,
		tp.tipopago
	FROM 
		productoselegidos prdele
	INNER JOIN productos prod ON prod.idproducto = prdele.idproducto
	INNER JOIN boleta bol ON bol.idboleta = prdele.idboleta
	INNER JOIN pago pag ON pag.idboleta = prdele.idboleta
	INNER JOIN tipopago tp ON tp.idtipopago = pag.idtipopago
	WHERE bol.idboleta = @idboleta
END
GO
