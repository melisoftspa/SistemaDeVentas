# Configuración del Sistema de Impresión Térmica

## Configuración en appsettings.json

El sistema de impresión térmica se configura a través de la sección `ThermalPrinters` en el archivo `appsettings.json`. Cada impresora se configura con un identificador único.

### Ejemplo de Configuración Básica

```json
{
  "ThermalPrinters": {
    "RPT008_Default": {
      "Model": "RPT008",
      "ConnectionType": "Serial",
      "Port": "COM1",
      "BaudRate": 9600,
      "TimeoutMilliseconds": 5000,
      "PaperWidth": 32,
      "Name": "Impresora Térmica RPT008"
    }
  }
}
```

### Configuración Múltiple de Impresoras

```json
{
  "ThermalPrinters": {
    "RPT008_Principal": {
      "Model": "RPT008",
      "ConnectionType": "Serial",
      "Port": "COM1",
      "BaudRate": 9600,
      "TimeoutMilliseconds": 5000,
      "PaperWidth": 32,
      "Name": "Impresora Principal"
    },
    "RPT008_Backup": {
      "Model": "RPT008",
      "ConnectionType": "LAN",
      "Port": "192.168.1.100:9100",
      "BaudRate": 9600,
      "TimeoutMilliseconds": 3000,
      "PaperWidth": 32,
      "Name": "Impresora Backup"
    }
  }
}
```

## Parámetros de Configuración

### Model
- **Tipo**: `string` (enum)
- **Valores**: `"RPT008"`, `"Generic"`
- **Descripción**: Modelo de la impresora térmica
- **Por defecto**: `"Generic"`

### ConnectionType
- **Tipo**: `string` (enum)
- **Valores**: `"Serial"`, `"USB"`, `"LAN"`
- **Descripción**: Tipo de conexión a usar
- **Por defecto**: `"USB"`

### Port
- **Tipo**: `string`
- **Descripción**:
  - Para `Serial`: Nombre del puerto (ej: `"COM1"`, `"COM2"`)
  - Para `USB`: Nombre del puerto virtual (ej: `"COM3"`)
  - Para `LAN`: Dirección IP y puerto (ej: `"192.168.1.100:9100"`)
- **Por defecto**: `null`

### BaudRate
- **Tipo**: `int`
- **Descripción**: Velocidad de transmisión para conexiones seriales (bps)
- **Valores comunes**: `9600`, `19200`, `38400`, `115200`
- **Por defecto**: `9600`

### TimeoutMilliseconds
- **Tipo**: `int`
- **Descripción**: Tiempo de espera para operaciones de conexión e impresión (ms)
- **Rango recomendado**: `1000` - `10000`
- **Por defecto**: `5000`

### PaperWidth
- **Tipo**: `int`
- **Descripción**: Ancho del papel en caracteres (normalmente 32 o 48)
- **Valores comunes**: `32` (80mm), `48` (110mm)
- **Por defecto**: `32`

### Name
- **Tipo**: `string`
- **Descripción**: Nombre descriptivo de la impresora para la UI
- **Por defecto**: Igual al ID de configuración

## Configuración por Tipo de Conexión

### Conexión Serial
```json
{
  "Model": "RPT008",
  "ConnectionType": "Serial",
  "Port": "COM1",
  "BaudRate": 9600,
  "TimeoutMilliseconds": 5000
}
```

**Requisitos**:
- Puerto serial físico disponible
- Configuración correcta de velocidad (baud rate)
- Cable serial apropiado

### Conexión USB
```json
{
  "Model": "RPT008",
  "ConnectionType": "USB",
  "Port": "COM3",
  "BaudRate": 9600,
  "TimeoutMilliseconds": 5000
}
```

**Notas**:
- USB aparece como puerto COM virtual
- Verificar el número de puerto en el Administrador de Dispositivos

### Conexión LAN
```json
{
  "Model": "RPT008",
  "ConnectionType": "LAN",
  "Port": "192.168.1.100:9100",
  "TimeoutMilliseconds": 3000
}
```

**Requisitos**:
- Impresora con interfaz Ethernet
- Dirección IP estática o DHCP configurada
- Puerto estándar ESC/POS (normalmente 9100)

## Validación de Configuración

El sistema valida automáticamente la configuración al cargar:

1. **Campos requeridos**: Model, ConnectionType, Port
2. **Rangos válidos**: Timeout entre 1000-30000ms, PaperWidth 16-64
3. **Compatibilidad**: Verificación de modelo soportado
4. **Conectividad**: Prueba de conexión básica (opcional)

### Errores Comunes de Configuración

- **Puerto no encontrado**: Verificar nombre del puerto COM
- **Timeout demasiado corto**: Aumentar `TimeoutMilliseconds`
- **BaudRate incorrecto**: Verificar especificaciones de la impresora
- **Dirección IP inválida**: Verificar conectividad de red

## Configuración en Runtime

### Cambio de Impresora Activa

```csharp
// Cambiar impresora usando el servicio
var result = await _thermalPrinterService.ConnectAsync("RPT008_Backup");
if (result.IsSuccess)
{
    // Impresora cambiada exitosamente
}
```

### Recarga de Configuración

La configuración se recarga automáticamente al reiniciar la aplicación. Para cambios en runtime, implementar recarga manual:

```csharp
// Recargar configuración (requiere implementación personalizada)
await _printerConfiguration.LoadConfigurationAsync();
```

## Configuración Avanzada

### Configuración por Entorno

```json
// appsettings.Development.json
{
  "ThermalPrinters": {
    "Test_Printer": {
      "Model": "Generic",
      "ConnectionType": "Serial",
      "Port": "COM1",
      "Name": "Impresora de Desarrollo"
    }
  }
}

// appsettings.Production.json
{
  "ThermalPrinters": {
    "Production_Printer": {
      "Model": "RPT008",
      "ConnectionType": "LAN",
      "Port": "192.168.1.100:9100",
      "Name": "Impresora de Producción"
    }
  }
}
```

### Configuración de Cola de Impresión

La cola de impresión se configura en el contenedor de DI:

```csharp
services.AddScoped<IPrintJobQueue, PrintJobQueue>();
services.AddHostedService<PrintJobProcessor>();
```

### Logging de Impresión

Configurar logging específico para operaciones de impresión:

```json
{
  "Logging": {
    "LogLevel": {
      "SistemaDeVentas.Infrastructure.Services.Printer": "Information"
    }
  }
}
```

## Verificación de Configuración

### Prueba de Conexión

```csharp
// Verificar conexión
var statusResult = await _thermalPrinterService.GetStatusAsync();
if (statusResult.IsSuccess)
{
    Console.WriteLine($"Estado: {statusResult.Value}");
}
```

### Impresión de Prueba

```csharp
// Imprimir datos de prueba
var testData = "PRUEBA DE IMPRESION\nFecha: " + DateTime.Now;
var result = await _thermalPrinterService.PrintAsync(testData);
```

## Solución de Problemas de Configuración

Ver [Troubleshooting.md](Troubleshooting.md) para problemas específicos de configuración.