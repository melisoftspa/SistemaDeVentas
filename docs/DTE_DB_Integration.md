# Integración DTE en SalesSystemDB - Cambios Mínimos

## Campos Opcionales Agregados a Tabla 'sale'

```sql
-- Agregar campos opcionales para DTE en tabla 'sale'
ALTER TABLE [dbo].[sale] ADD
    [dte_folio] [int] NULL,
    [dte_status] [nvarchar](50) NULL,
    [dte_type] [nvarchar](10) NULL,
    [dte_xml] [nvarchar](max) NULL,
    [caf_id] [uniqueidentifier] NULL,
    [dte_sent_date] [datetime] NULL;
```

## Nueva Tabla 'caf' para Códigos de Autorización de Folios

```sql
-- Crear tabla para CAF (opcional, si no existe)
CREATE TABLE [dbo].[caf](
    [id] [uniqueidentifier] NOT NULL,
    [rut_emisor] [nvarchar](12) NULL,
    [tipo_dte] [nvarchar](10) NULL,
    [folio_desde] [int] NULL,
    [folio_hasta] [int] NULL,
    [fecha_autorizacion] [datetime] NULL,
    [xml_caf] [nvarchar](max) NULL,
    [estado] [bit] NULL,
    [fecha_creacion] [datetime] NULL,
 CONSTRAINT [PK_caf] PRIMARY KEY CLUSTERED
(
    [id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

-- Agregar default para id
ALTER TABLE [dbo].[caf] ADD CONSTRAINT [DF_caf_id] DEFAULT (newid()) FOR [id];
ALTER TABLE [dbo].[caf] ADD CONSTRAINT [DF_caf_fecha_creacion] DEFAULT (getdate()) FOR [fecha_creacion];
ALTER TABLE [dbo].[caf] ADD CONSTRAINT [DF_caf_estado] DEFAULT ((1)) FOR [estado];
```

## Campos Opcionales en Tabla 'parameter' para Configuraciones DTE

Los parámetros DTE se pueden almacenar usando la tabla 'parameter' existente con module = 'DTE':

- certificate_path: Ruta al certificado digital
- certificate_password: Contraseña del certificado
- rut_emisor: RUT del emisor
- razon_social: Razón social del emisor
- giro: Giro del emisor
- direccion: Dirección del emisor
- comuna: Comuna del emisor
- ciudad: Ciudad del emisor
- ambiente_sii: 'certificacion' o 'produccion'

## Consideraciones

- Todos los campos agregados son NULL para mantener compatibilidad con datos existentes
- La tabla 'caf' es opcional si se maneja CAF en memoria o archivos
- Los cambios preservan la estructura existente de la base de datos
- Se recomienda backup antes de aplicar cambios