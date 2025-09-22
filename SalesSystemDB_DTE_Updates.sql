-- Script SQL para agregar soporte completo DTE a SalesSystemDB
-- Ejecutar después de respaldar la base de datos

USE SalesSystemDB;
GO

-- Agregar campos DTE a la tabla sale
ALTER TABLE sale ADD dte_generated BIT NOT NULL DEFAULT 0;
ALTER TABLE sale ADD folio INT NULL;
ALTER TABLE sale ADD dte_xml NVARCHAR(MAX) NULL;
ALTER TABLE sale ADD dte_folio INT NULL;
ALTER TABLE sale ADD dte_status NVARCHAR(100) NULL;
ALTER TABLE sale ADD dte_type NVARCHAR(10) NULL;
ALTER TABLE sale ADD caf_id UNIQUEIDENTIFIER NULL;
ALTER TABLE sale ADD dte_sent_date DATETIME NULL;
ALTER TABLE sale ADD payment_method NVARCHAR(100) NULL;
ALTER TABLE sale ADD payment_transaction_id NVARCHAR(500) NULL;
ALTER TABLE sale ADD payment_date DATETIME NULL;

-- Crear tabla caf
CREATE TABLE caf (
    id UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    tipo_documento INT NOT NULL,
    folio_desde INT NOT NULL,
    folio_hasta INT NOT NULL,
    fecha_autorizacion DATETIME NOT NULL,
    fecha_vencimiento DATETIME NOT NULL,
    xml_content NVARCHAR(MAX) NOT NULL,
    ambiente INT NOT NULL,
    rut_emisor NVARCHAR(20) NOT NULL,
    folio_actual INT NOT NULL DEFAULT 0,
    activo BIT NOT NULL DEFAULT 1
);

-- Crear tabla dte_log
CREATE TABLE dte_log (
    id INT IDENTITY(1,1) PRIMARY KEY,
    sale_id UNIQUEIDENTIFIER NOT NULL,
    status NVARCHAR(50) NOT NULL,
    message NVARCHAR(1000) NOT NULL,
    error_code NVARCHAR(100) NULL,
    created_at DATETIME NOT NULL DEFAULT GETDATE(),
    updated_at DATETIME NULL,
    dte_folio INT NULL,
    dte_type NVARCHAR(10) NULL,
    FOREIGN KEY (sale_id) REFERENCES sale(id)
);

-- Crear tabla certificate_data
CREATE TABLE certificate_data (
    id INT IDENTITY(1,1) PRIMARY KEY,
    certificate_name NVARCHAR(255) NOT NULL,
    certificate_content NVARCHAR(MAX) NOT NULL,
    private_key_content NVARCHAR(MAX) NOT NULL,
    password NVARCHAR(255) NULL,
    is_active BIT NOT NULL DEFAULT 1,
    created_at DATETIME NOT NULL DEFAULT GETDATE(),
    expires_at DATETIME NULL
);

-- Crear índices para mejor rendimiento
CREATE INDEX IX_sale_dte_generated ON sale(dte_generated);
CREATE INDEX IX_sale_dte_folio ON sale(dte_folio);
CREATE INDEX IX_sale_caf_id ON sale(caf_id);
CREATE INDEX IX_caf_tipo_documento ON caf(tipo_documento);
CREATE INDEX IX_caf_activo ON caf(activo);
CREATE INDEX IX_dte_log_sale_id ON dte_log(sale_id);
CREATE INDEX IX_dte_log_status ON dte_log(status);
CREATE INDEX IX_certificate_data_is_active ON certificate_data(is_active);

PRINT 'Script DTE completado exitosamente.';