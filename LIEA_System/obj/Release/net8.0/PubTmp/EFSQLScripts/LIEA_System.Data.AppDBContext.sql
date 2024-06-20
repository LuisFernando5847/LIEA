IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616220657_migracionbase'
)
BEGIN
    CREATE TABLE [Clientes] (
        [Id_Cliente] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NOT NULL,
        [Apellido] nvarchar(max) NOT NULL,
        [Direccion] nvarchar(max) NOT NULL,
        [Correo_Electronico] nvarchar(max) NOT NULL,
        [Telefono] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Clientes] PRIMARY KEY ([Id_Cliente])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616220657_migracionbase'
)
BEGIN
    CREATE TABLE [Proveedores] (
        [Id_Proveedor] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NOT NULL,
        [Apellido] nvarchar(max) NOT NULL,
        [Domicilio] nvarchar(max) NOT NULL,
        [Telefono] nvarchar(max) NOT NULL,
        [CorreoElectronico] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Proveedores] PRIMARY KEY ([Id_Proveedor])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616220657_migracionbase'
)
BEGIN
    CREATE TABLE [Ventas] (
        [Id_Venta] int NOT NULL IDENTITY,
        [Fecha_Venta] datetime2 NOT NULL,
        [Hora_Venta] time NOT NULL,
        [Id_Cliente] int NOT NULL,
        [Total] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_Ventas] PRIMARY KEY ([Id_Venta]),
        CONSTRAINT [FK_Ventas_Clientes_Id_Cliente] FOREIGN KEY ([Id_Cliente]) REFERENCES [Clientes] ([Id_Cliente]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616220657_migracionbase'
)
BEGIN
    CREATE TABLE [Compras] (
        [Id_Compra] int NOT NULL IDENTITY,
        [Fecha_Compra] datetime2 NOT NULL,
        [Hora_Compra] time NOT NULL,
        [Id_Proveedor] int NOT NULL,
        [Total] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_Compras] PRIMARY KEY ([Id_Compra]),
        CONSTRAINT [FK_Compras_Proveedores_Id_Proveedor] FOREIGN KEY ([Id_Proveedor]) REFERENCES [Proveedores] ([Id_Proveedor]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616220657_migracionbase'
)
BEGIN
    CREATE TABLE [Productos] (
        [Id_Producto] int NOT NULL IDENTITY,
        [Nombre] nvarchar(max) NOT NULL,
        [Tipo] nvarchar(max) NOT NULL,
        [Genero] nvarchar(max) NOT NULL,
        [Talla] nvarchar(max) NOT NULL,
        [Color] nvarchar(max) NOT NULL,
        [Modelo] nvarchar(max) NOT NULL,
        [Precio] decimal(18,2) NOT NULL,
        [Existencias] int NOT NULL,
        [Imagen] varbinary(max) NOT NULL,
        [Id_Proveedor] int NOT NULL,
        CONSTRAINT [PK_Productos] PRIMARY KEY ([Id_Producto]),
        CONSTRAINT [FK_Productos_Proveedores_Id_Proveedor] FOREIGN KEY ([Id_Proveedor]) REFERENCES [Proveedores] ([Id_Proveedor]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616220657_migracionbase'
)
BEGIN
    CREATE TABLE [DetallesCompra] (
        [Id_DetalleCompra] int NOT NULL IDENTITY,
        [Id_Producto] int NOT NULL,
        [Id_Compra] int NOT NULL,
        [Cantidad] int NOT NULL,
        [P_Unitario] decimal(18,2) NOT NULL,
        [Total] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_DetallesCompra] PRIMARY KEY ([Id_DetalleCompra]),
        CONSTRAINT [FK_DetallesCompra_Compras_Id_Compra] FOREIGN KEY ([Id_Compra]) REFERENCES [Compras] ([Id_Compra]) ON DELETE NO ACTION,
        CONSTRAINT [FK_DetallesCompra_Productos_Id_Producto] FOREIGN KEY ([Id_Producto]) REFERENCES [Productos] ([Id_Producto]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616220657_migracionbase'
)
BEGIN
    CREATE TABLE [DetallesVenta] (
        [Id_DetalleVenta] int NOT NULL IDENTITY,
        [Id_Producto] int NOT NULL,
        [Id_Venta] int NOT NULL,
        [Cantidad] int NOT NULL,
        [P_Unitario] decimal(18,2) NOT NULL,
        [Total] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_DetallesVenta] PRIMARY KEY ([Id_DetalleVenta]),
        CONSTRAINT [FK_DetallesVenta_Productos_Id_Producto] FOREIGN KEY ([Id_Producto]) REFERENCES [Productos] ([Id_Producto]) ON DELETE NO ACTION,
        CONSTRAINT [FK_DetallesVenta_Ventas_Id_Venta] FOREIGN KEY ([Id_Venta]) REFERENCES [Ventas] ([Id_Venta]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616220657_migracionbase'
)
BEGIN
    CREATE INDEX [IX_Compras_Id_Proveedor] ON [Compras] ([Id_Proveedor]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616220657_migracionbase'
)
BEGIN
    CREATE INDEX [IX_DetallesCompra_Id_Compra] ON [DetallesCompra] ([Id_Compra]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616220657_migracionbase'
)
BEGIN
    CREATE INDEX [IX_DetallesCompra_Id_Producto] ON [DetallesCompra] ([Id_Producto]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616220657_migracionbase'
)
BEGIN
    CREATE INDEX [IX_DetallesVenta_Id_Producto] ON [DetallesVenta] ([Id_Producto]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616220657_migracionbase'
)
BEGIN
    CREATE INDEX [IX_DetallesVenta_Id_Venta] ON [DetallesVenta] ([Id_Venta]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616220657_migracionbase'
)
BEGIN
    CREATE INDEX [IX_Productos_Id_Proveedor] ON [Productos] ([Id_Proveedor]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616220657_migracionbase'
)
BEGIN
    CREATE INDEX [IX_Ventas_Id_Cliente] ON [Ventas] ([Id_Cliente]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616220657_migracionbase'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240616220657_migracionbase', N'8.0.6');
END;
GO

COMMIT;
GO

