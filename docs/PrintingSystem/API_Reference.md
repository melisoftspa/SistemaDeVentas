# Referencia de APIs

## IThermalPrinterService

Interfaz principal del servicio de impresión térmica.

### Métodos

#### ConnectAsync
```csharp
Task<Result<bool>> ConnectAsync(string printerName)
```
Conecta a la impresora térmica especificada.

**Parámetros:**
- `printerName`: Nombre de la impresora configurada en appsettings.json

**Retorna:** Resultado de la operación de conexión

#### DisconnectAsync
```csharp
Task<Result<bool>> DisconnectAsync()
```
Desconecta la impresora térmica actual.

**Retorna:** Resultado de la operación de desconexión

#### PrintAsync
```csharp
Task<Result<Guid>> PrintAsync(string data, PrintJobPriority priority = PrintJobPriority.Normal)
```
Imprime los datos especificados de forma encolada.

**Parámetros:**
- `data`: Datos de texto a imprimir
- `priority`: Prioridad del trabajo (Normal, High)

**Retorna:** ID del trabajo de impresión encolado

#### PrintRawAsync
```csharp
Task<Result<Guid>> PrintRawAsync(byte[] data, PrintJobPriority priority = PrintJobPriority.Normal)
```
Imprime datos binarios (comandos ESC/POS) de forma encolada.

**Parámetros:**
- `data`: Datos binarios a imprimir
- `priority`: Prioridad del trabajo

**Retorna:** ID del trabajo de impresión encolado

#### GetStatusAsync
```csharp
Task<Result<string>> GetStatusAsync()
```
Obtiene el estado actual de la impresora térmica.

**Retorna:** Estado de la impresora como cadena

#### IsCompatibleAsync
```csharp
Task<Result<bool>> IsCompatibleAsync(string printerModel)
```
Verifica si el modelo de impresora es compatible.

**Parámetros:**
- `printerModel`: Modelo de la impresora

**Retorna:** Resultado de la verificación de compatibilidad

#### FormatReceiptDataAsync
```csharp
Task<Result<byte[]>> FormatReceiptDataAsync(Sale sale)
```
Formatea los datos de una boleta de venta para impresión térmica.

**Parámetros:**
- `sale`: Venta a formatear

**Retorna:** Datos formateados para impresión

#### FormatInvoiceDataAsync
```csharp
Task<Result<byte[]>> FormatInvoiceDataAsync(DteDocument dteDocument, string qrCodeData)
```
Formatea los datos de una factura con DTE para impresión térmica.

**Parámetros:**
- `dteDocument`: Documento DTE a formatear
- `qrCodeData`: Datos para el código QR

**Retorna:** Datos formateados para impresión

#### PrintReceiptAsync
```csharp
Task<Result<Guid>> PrintReceiptAsync(Sale sale)
```
Imprime una boleta de venta (encolado con prioridad normal).

**Parámetros:**
- `sale`: Venta a imprimir

**Retorna:** ID del trabajo de impresión encolado

#### PrintInvoiceAsync
```csharp
Task<Result<Guid>> PrintInvoiceAsync(DteDocument dteDocument, string qrCodeData)
```
Imprime una factura con DTE (encolado con prioridad alta).

**Parámetros:**
- `dteDocument`: Documento DTE a imprimir
- `qrCodeData`: Datos para el código QR

**Retorna:** ID del trabajo de impresión encolado

#### CutPaperAsync
```csharp
Task<Result<bool>> CutPaperAsync()
```
Ejecuta un corte de papel.

**Retorna:** Resultado de la operación de corte

#### InitializeAsync
```csharp
Task<Result<bool>> InitializeAsync()
```
Inicializa la impresora con comandos ESC/POS.

**Retorna:** Resultado de la inicialización

## IPrinterConfiguration

Interfaz para la gestión de configuración de impresoras.

### Métodos

#### GetConfigurationAsync
```csharp
Task<Result<PrinterConfig>> GetConfigurationAsync(string printerId)
```
Obtiene la configuración de la impresora especificada por ID.

**Parámetros:**
- `printerId`: ID de la impresora

**Retorna:** Configuración de la impresora

#### SaveConfigurationAsync
```csharp
Task<Result<bool>> SaveConfigurationAsync(PrinterConfig config)
```
Guarda la configuración de la impresora.

**Parámetros:**
- `config`: Configuración a guardar

**Retorna:** Resultado de la operación de guardado

#### GetAllConfigurationsAsync
```csharp
Task<Result<List<PrinterConfig>>> GetAllConfigurationsAsync()
```
Obtiene todas las configuraciones de impresoras.

**Retorna:** Lista de configuraciones de impresoras

#### ListAvailablePrintersAsync
```csharp
Task<Result<List<string>>> ListAvailablePrintersAsync()
```
Lista las impresoras disponibles en el sistema.

**Retorna:** Lista de nombres de impresoras disponibles

#### ValidateConfigurationAsync
```csharp
Task<Result<bool>> ValidateConfigurationAsync(PrinterConfig config)
```
Valida una configuración de impresora.

**Parámetros:**
- `config`: Configuración a validar

**Retorna:** Resultado de la validación

## IPrintJobQueue

Interfaz para la gestión de la cola de trabajos de impresión.

### Métodos

#### EnqueueAsync
```csharp
Task<Result<Guid>> EnqueueAsync(byte[] data, PrintJobPriority priority = PrintJobPriority.Normal)
```
Encola un trabajo de impresión con la prioridad especificada.

**Parámetros:**
- `data`: Datos a imprimir
- `priority`: Prioridad del trabajo

**Retorna:** ID del trabajo encolado

#### DequeueAsync
```csharp
Task<PrintJob?> DequeueAsync()
```
Desencola el siguiente trabajo de impresión según prioridad y timestamp.

**Retorna:** Trabajo de impresión o null si la cola está vacía

#### CancelAsync
```csharp
Task<Result<bool>> CancelAsync(Guid jobId)
```
Cancela un trabajo de impresión pendiente.

**Parámetros:**
- `jobId`: ID del trabajo a cancelar

**Retorna:** Resultado de la operación

#### GetStatusAsync
```csharp
Task<Result<PrintJobStatus>> GetStatusAsync(Guid jobId)
```
Obtiene el estado de un trabajo de impresión.

**Parámetros:**
- `jobId`: ID del trabajo

**Retorna:** Estado del trabajo

#### GetPendingJobsAsync
```csharp
Task<Result<IEnumerable<PrintJob>>> GetPendingJobsAsync()
```
Obtiene todos los trabajos pendientes.

**Retorna:** Lista de trabajos pendientes

#### GetMetricsAsync
```csharp
Task<Result<PrintQueueMetrics>> GetMetricsAsync()
```
Obtiene métricas de rendimiento de la cola.

**Retorna:** Métricas de la cola

#### CleanupAsync
```csharp
Task<Result<int>> CleanupAsync(DateTime olderThan)
```
Limpia trabajos completados o fallidos antiguos.

**Parámetros:**
- `olderThan`: Trabajos más antiguos que esta fecha

**Retorna:** Número de trabajos limpiados

## PrintViewModel

ViewModel para la interfaz de usuario de impresión.

### Propiedades

#### IsPrinting
```csharp
bool IsPrinting { get; set; }
```
Indica si hay una operación de impresión en curso.

#### SelectedPrinter
```csharp
string SelectedPrinter { get; set; }
```
Impresora seleccionada actualmente.

#### AvailablePrinters
```csharp
ObservableCollection<string> AvailablePrinters { get; set; }
```
Lista de impresoras disponibles.

#### PrintJobs
```csharp
ObservableCollection<PrintJob> PrintJobs { get; set; }
```
Lista de trabajos de impresión.

#### PrintStatus
```csharp
string PrintStatus { get; set; }
```
Estado actual de la impresión.

#### SuccessMessage
```csharp
string SuccessMessage { get; set; }
```
Mensaje de éxito de la última operación.

#### CurrentSale
```csharp
Sale? CurrentSale { get; set; }
```
Venta actual para impresión.

#### CurrentDteDocument
```csharp
DteDocument? CurrentDteDocument { get; set; }
```
Documento DTE actual para impresión.

#### QrCodeData
```csharp
string QrCodeData { get; set; }
```
Datos del código QR para facturas DTE.

### Comandos

#### PrintReceiptCommand
```csharp
ICommand PrintReceiptCommand { get; }
```
Comando para imprimir boleta de venta.

#### PrintInvoiceCommand
```csharp
ICommand PrintInvoiceCommand { get; }
```
Comando para imprimir factura DTE.

#### TestPrintCommand
```csharp
ICommand TestPrintCommand { get; }
```
Comando para impresión de prueba.

#### CancelPrintJobCommand
```csharp
ICommand CancelPrintJobCommand { get; }
```
Comando para cancelar trabajo de impresión.

#### RefreshPrintersCommand
```csharp
ICommand RefreshPrintersCommand { get; }
```
Comando para refrescar lista de impresoras.

## Clases de Datos

### PrinterConfig
Configuración de una impresora térmica.

```csharp
public class PrinterConfig
{
    public string Id { get; set; }
    public ThermalPrinterSettings Settings { get; set; }
}
```

### ThermalPrinterSettings
Configuración específica de la impresora térmica.

```csharp
public class ThermalPrinterSettings
{
    public PrinterModel Model { get; set; }
    public ConnectionType ConnectionType { get; set; }
    public string? Port { get; set; }
    public int BaudRate { get; set; }
    public int TimeoutMilliseconds { get; set; }
    public int PaperWidth { get; set; }
    public string? Name { get; set; }
}
```

### PrintJob
Trabajo de impresión en la cola.

```csharp
public class PrintJob
{
    public Guid Id { get; set; }
    public byte[] Data { get; set; }
    public PrintJobPriority Priority { get; set; }
    public PrintJobStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public TimeSpan? ProcessingTime { get; set; }
    public string? ErrorMessage { get; set; }
}
```

### PrintQueueMetrics
Métricas de rendimiento de la cola de impresión.

```csharp
public class PrintQueueMetrics
{
    public int TotalJobs { get; set; }
    public int PendingJobs { get; set; }
    public int ProcessingJobs { get; set; }
    public int CompletedJobs { get; set; }
    public int FailedJobs { get; set; }
    public int CancelledJobs { get; set; }
    public double AverageProcessingTime { get; set; }
    public double SuccessRate { get; set; }
}
```

## Enumeraciones

### PrintJobPriority
Prioridad de los trabajos de impresión.

```csharp
public enum PrintJobPriority
{
    Low = 0,
    Normal = 1,
    High = 2,
    Critical = 3
}
```

### PrintJobStatus
Estado de un trabajo de impresión.

```csharp
public enum PrintJobStatus
{
    Pending = 0,
    Processing = 1,
    Completed = 2,
    Failed = 3,
    Cancelled = 4
}
```

### PrinterModel
Modelos de impresoras soportadas.

```csharp
public enum PrinterModel
{
    Generic = 0,
    RPT008 = 1,
    // Futuros modelos...
}
```

### ConnectionType
Tipos de conexión soportados.

```csharp
public enum ConnectionType
{
    Serial = 0,
    USB = 1,
    LAN = 2,
    Bluetooth = 3
}
```

## Comandos ESC/POS

### Comandos Implementados

| Comando | Descripción | Código |
|---------|-------------|--------|
| ESC @ | Inicializar impresora | `\x1B\x40` |
| ESC E | Negrita ON/OFF | `\x1B\x45\x01` / `\x1B\x45\x00` |
| ESC ! | Tamaño de fuente | `\x1B\x21\x00` (normal) |
| ESC a | Alineación | `\x1B\x61\x00` (izq), `\x1B\x61\x01` (centro), `\x1B\x61\x02` (der) |
| GS V B | Corte de papel | `\x1D\x56\x42\x00` |
| DLE EOT | Estado de impresora | `\x10\x04\x01` |

### Estructura de Datos de Impresión

Los datos de impresión siguen esta estructura típica:

1. **Inicialización**: `ESC @`
2. **Formato**: Alineación, negrita, tamaño
3. **Contenido**: Texto a imprimir
4. **Corte**: `GS V B 0`

### Ejemplo de Datos Binarios

```csharp
// Inicializar + Centrar + Negrita + Texto + Corte
byte[] receiptData = new byte[]
{
    0x1B, 0x40,              // ESC @
    0x1B, 0x61, 0x01,        // ESC a 1 (centrado)
    0x1B, 0x45, 0x01,        // ESC E 1 (negrita)
    // ... texto en UTF-8 ...
    0x1D, 0x56, 0x42, 0x00   // GS V B 0 (corte)
};
```

## Registro de Servicios

### Configuración en Program.cs

```csharp
builder.Services.AddScoped<IPrinterConfiguration, PrinterConfiguration>();
builder.Services.AddScoped<IThermalPrinterService, ThermalPrinterService>();
builder.Services.AddScoped<IPrintJobQueue, PrintJobQueue>();
builder.Services.AddHostedService<PrintJobProcessor>();
```

### Configuración en appsettings.json

```json
{
  "ThermalPrinters": {
    "PrinterId": {
      "Model": "RPT008",
      "ConnectionType": "Serial",
      "Port": "COM1",
      "BaudRate": 9600,
      "TimeoutMilliseconds": 5000,
      "PaperWidth": 32,
      "Name": "Mi Impresora"
    }
  }
}
```

## Manejo de Errores

Todos los métodos retornan `Result<T>` de FluentResults:

- **Success**: `result.IsSuccess == true`
- **Failure**: `result.IsFailed == true`
- **Errores**: `result.Errors` (lista de errores)
- **Valor**: `result.Value` (valor retornado en caso de éxito)

### Tipos de Error Comunes

- `ValidationError`: Datos inválidos
- `NotFoundError`: Recurso no encontrado
- `ConnectionError`: Problemas de conexión
- `TimeoutError`: Operación expiró
- `PrinterError`: Error específico de impresora

## Logging

El sistema registra operaciones importantes:

- Conexión/desconexión de impresoras
- Encolado y procesamiento de trabajos
- Errores de impresión
- Métricas de rendimiento

### Niveles de Log

- **Information**: Operaciones exitosas
- **Warning**: Problemas recuperables
- **Error**: Errores que requieren atención
- **Debug**: Información detallada para diagnóstico

### Configuración de Logging

```json
{
  "Logging": {
    "LogLevel": {
      "SistemaDeVentas.Infrastructure.Services.Printer": "Information"
    }
  }
}