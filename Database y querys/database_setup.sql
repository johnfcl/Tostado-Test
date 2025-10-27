/*
=========================================================
 SISTEMA DE GESTIÓN DOCUMENTAL - SCRIPT DE CREACIÓN
 Base de datos: GetionDocumentalTostaoBD
 Autor: John Fredy Culma Leal
 Descripción: Script con manejo de transacciones y rollback
 Fecha: 2025-10-25
=========================================================
*/

-- Crear base de datos si no existe
IF DB_ID('GetionDocumentalTostaoBD') IS NULL
BEGIN
    CREATE DATABASE GetionDocumentalTostaoBD;
    PRINT 'Base de datos GetionDocumentalTostaoBD creada.';
END
ELSE
    PRINT 'Base de datos GetionDocumentalTostaoBD ya existe.';
GO

USE GetionDocumentalTostaoBD;
GO

BEGIN TRY
    BEGIN TRANSACTION;

    PRINT '--- Eliminando tablas si existen ---';
    IF OBJECT_ID('dbo.MovimientosDocumento','U') IS NOT NULL DROP TABLE dbo.MovimientosDocumento;
    IF OBJECT_ID('dbo.Documentos','U') IS NOT NULL DROP TABLE dbo.Documentos;
    IF OBJECT_ID('dbo.LogCambios','U') IS NOT NULL DROP TABLE dbo.LogCambios;

    PRINT '--- Creando tablas ---';

    CREATE TABLE dbo.Documentos (
        Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
        Titulo NVARCHAR(200) NOT NULL,
        Autor NVARCHAR(150) NOT NULL,
        Tipo NVARCHAR(10) NOT NULL CHECK (Tipo IN ('PDF','DOC','XLS','IMG','TXT')),
        Estado NVARCHAR(20) NOT NULL CHECK (Estado IN ('Registrado','Pendiente','Validado','Archivado')),
        FechaRegistro DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
        FechaValidacion DATETIME2 NULL,
        RutaArchivo NVARCHAR(500) NULL
    );
    PRINT 'Tabla Documentos creada correctamente.';

    CREATE TABLE dbo.MovimientosDocumento (
        Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
        DocumentoId UNIQUEIDENTIFIER NOT NULL,
        FechaMovimiento DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
        Usuario NVARCHAR(150) NOT NULL,
        Accion NVARCHAR(200) NOT NULL,
        Observaciones NVARCHAR(500) NULL,
        CONSTRAINT FK_MovimientosDocumento_Documentos FOREIGN KEY (DocumentoId) REFERENCES dbo.Documentos(Id)
    );
    PRINT 'Tabla MovimientosDocumento creada correctamente.';

    CREATE TABLE dbo.LogCambios (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Entidad NVARCHAR(100),
        Descripcion NVARCHAR(1000),
        Fecha DATETIME2 DEFAULT SYSUTCDATETIME()
    );
    PRINT 'Tabla LogCambios creada correctamente.';

    PRINT '--- Creando índices ---';
    CREATE INDEX IX_Documentos_Autor ON dbo.Documentos(Autor);
    CREATE INDEX IX_Documentos_Tipo ON dbo.Documentos(Tipo);
    CREATE INDEX IX_Documentos_Estado_Fecha ON dbo.Documentos(Estado, FechaRegistro);
    CREATE INDEX IX_MovimientosDocumento_DocumentoId ON dbo.MovimientosDocumento(DocumentoId);
    PRINT 'Índices creados correctamente.';

    PRINT '--- Insertando registros de prueba en Documentos ---';

    INSERT INTO dbo.Documentos (Titulo, Autor, Tipo, Estado, FechaRegistro, FechaValidacion, RutaArchivo)
    VALUES
    ('Informe de Ventas Q1', 'Carlos Pérez', 'PDF', 'Pendiente', DATEADD(DAY,-10,SYSUTCDATETIME()), NULL, '/files/informe_q1.pdf'),
    ('Contrato Servicios 001', 'Ana Gómez', 'DOC', 'Validado', DATEADD(DAY,-200,SYSUTCDATETIME()), DATEADD(DAY,-180,SYSUTCDATETIME()), '/files/contrato_001.docx'),
    ('Reporte Producción', 'Laura Ruiz', 'XLS', 'Registrado', DATEADD(DAY,-95,SYSUTCDATETIME()), NULL, '/files/reporte_prod.xlsx'),
    ('Imagen Diseño Logo', 'Carlos Pérez', 'IMG', 'Validado', DATEADD(DAY,-400,SYSUTCDATETIME()), DATEADD(DAY,-380,SYSUTCDATETIME()), '/files/logo.png'),
    ('Plan Financiero 2024', 'Luis Torres', 'XLS', 'Validado', DATEADD(DAY,-120,SYSUTCDATETIME()), DATEADD(DAY,-110,SYSUTCDATETIME()), '/files/plan_fin_2024.xlsx'),
    ('Contrato Arrendamiento', 'Ana Gómez', 'DOC', 'Pendiente', DATEADD(DAY,-100,SYSUTCDATETIME()), NULL, '/files/contrato_arr.docx'),
    ('Memo Interno 07', 'Laura Ruiz', 'TXT', 'Archivado', DATEADD(DAY,-5,SYSUTCDATETIME()), NULL, '/files/memo_07.txt'),
    ('Informe Auditoría', 'Diego Mendoza', 'PDF', 'Validado', DATEADD(DAY,-250,SYSUTCDATETIME()), DATEADD(DAY,-245,SYSUTCDATETIME()), '/files/informe_auditoria.pdf'),
    ('Carta RRHH', 'Camila Rojas', 'DOC', 'Pendiente', DATEADD(DAY,-3,SYSUTCDATETIME()), NULL, '/files/carta_rrhh.docx'),
    ('Gráfico Anual', 'Sofía Hernández', 'IMG', 'Validado', DATEADD(DAY,-300,SYSUTCDATETIME()), DATEADD(DAY,-295,SYSUTCDATETIME()), '/files/grafico_anual.png'),
    ('Reporte Proyecto X', 'Felipe González', 'PDF', 'Registrado', DATEADD(DAY,-91,SYSUTCDATETIME()), NULL, '/files/reporte_proy_x.pdf'),
    ('Contrato Confidencial', 'Laura Ruiz', 'DOC', 'Validado', DATEADD(DAY,-60,SYSUTCDATETIME()), DATEADD(DAY,-55,SYSUTCDATETIME()), '/files/contrato_conf.docx'),
    ('Foto Evento', 'Luis Torres', 'IMG', 'Pendiente', DATEADD(DAY,-500,SYSUTCDATETIME()), NULL, '/files/foto_evento.png'),
    ('Memorando Legal', 'Valentina Castro', 'TXT', 'Archivado', DATEADD(DAY,-45,SYSUTCDATETIME()), NULL, '/files/memo_legal.txt'),
    ('Informe Financiero Anual', 'Carlos Pérez', 'XLS', 'Validado', DATEADD(DAY,-2,SYSUTCDATETIME()), NULL, '/files/informe_fin_anual.xlsx'),
    ('Contrato Consultoría', 'Andrés Ramírez', 'DOC', 'Pendiente', DATEADD(DAY,-200,SYSUTCDATETIME()), NULL, '/files/contrato_cons.docx'),
    ('Acta Cierre Proyecto', 'Santiago Mejía', 'PDF', 'Pendiente', DATEADD(DAY,-1,SYSUTCDATETIME()), NULL, '/files/acta_cierre.pdf'),
    ('Registro Fotográfico', 'Valeria Ortiz', 'IMG', 'Validado', DATEADD(DAY,-600,SYSUTCDATETIME()), DATEADD(DAY,-590,SYSUTCDATETIME()), '/files/registro_foto.png'),
    ('Reporte Riesgos Q2', 'Esteban López', 'XLS', 'Validado', DATEADD(DAY,-180,SYSUTCDATETIME()), DATEADD(DAY,-175,SYSUTCDATETIME()), '/files/reporte_riesgos.xlsx'),
    ('Contrato Compra 11', 'Manuela Díaz', 'DOC', 'Registrado', DATEADD(DAY,-4,SYSUTCDATETIME()), NULL, '/files/contrato_compra.docx');

    PRINT '20 registros de prueba insertados correctamente.';

    INSERT INTO dbo.MovimientosDocumento (DocumentoId, Usuario, Accion, Observaciones)
    SELECT TOP 5 Id, 'system', 'Creación', 'Documento creado automáticamente' FROM dbo.Documentos;

    INSERT INTO dbo.MovimientosDocumento (DocumentoId, Usuario, Accion, Observaciones)
    SELECT TOP 5 Id, 'validador', 'Validación', 'Documento validado automáticamente' FROM dbo.Documentos WHERE Estado = 'Validado';

    PRINT 'Movimientos insertados correctamente.';

    PRINT '--- Creando procedimiento almacenado ---';
    IF OBJECT_ID('dbo.SP_ReportePorAutorYTipo','P') IS NOT NULL
        DROP PROCEDURE dbo.SP_ReportePorAutorYTipo;

    EXEC('
    CREATE PROCEDURE dbo.SP_ReportePorAutorYTipo
    AS
    BEGIN
        SET NOCOUNT ON;

        SELECT Autor, Tipo, COUNT(*) AS CantidadDocumentos
        FROM dbo.Documentos
        GROUP BY Autor, Tipo
        ORDER BY Autor, Tipo;

        SELECT ''TOTAL'' AS Autor, NULL AS Tipo, COUNT(*) AS CantidadTotal FROM dbo.Documentos;
    END;
    ');

    COMMIT TRANSACTION;
    PRINT 'Transacción completada correctamente (COMMIT).';
END TRY
BEGIN CATCH
    PRINT 'ERROR detectado: aplicando ROLLBACK...';
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;

    DECLARE @Error NVARCHAR(4000) = ERROR_MESSAGE();
    PRINT 'ERROR: ' + @Error;

    INSERT INTO dbo.LogCambios (Entidad, Descripcion)
    VALUES ('DB_CREATION', @Error);

    THROW;
END CATCH;
GO
