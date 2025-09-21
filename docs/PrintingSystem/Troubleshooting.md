# Solución de Problemas

## Problemas de Conexión

### Error: "Puerto COM no encontrado"

**Síntomas:**
- Error al conectar: "The port 'COM1' does not exist"
- Impresora no responde

**Soluciones:**
1. Verificar que el puerto COM esté disponible en el Administrador de Dispositivos
2. Para USB: Verificar que el driver de la impresora esté instalado
3. Cambiar el número de puerto en la configuración
4. Reiniciar el servicio de spooler de Windows

```bash
# Reiniciar spooler de impresión
net stop spooler
net start spooler
```

### Error: "Timeout al conectar"

**Síntomas:**
- Conexión tarda mucho tiempo y falla
- Mensaje: "Timeout al conectar por red/serial"

**Soluciones:**
1. Verificar configuración de timeout (aumentar si es necesario)
2. Para red: Verificar conectividad IP y puerto
3. Para serial: Verificar velocidad (baud rate) correcta
4. Verificar cableado y alimentación de la impresora

### Error: "Acceso denegado al puerto"

**Síntomas:**
- Error de permisos al abrir puerto COM
- Aplicación requiere permisos de administrador

**Soluciones:**
1. Ejecutar aplicación como administrador
2. Verificar que ningún otro programa esté usando el puerto
3. Configurar permisos del puerto COM

## Problemas de Impresión

### Impresión no funciona

**Síntomas:**
- Comando de impresión no produce salida
- Cola de impresión se llena pero no se procesa

**Diagnóstico:**
```csharp
// Verificar estado de la impresora
var status = await _printerService.GetStatusAsync();
Console.WriteLine($"Estado: {status.Value}");

// Verificar trabajos pendientes
var jobs = await _printJobQueue.GetPendingJobsAsync();
Console.WriteLine($"Trabajos pendientes: {jobs.Value.Count}");
```

**Soluciones:**
1. Verificar que PrintJobProcessor esté ejecutándose
2. Comprobar configuración de impresora
3. Reiniciar el servicio de impresión

### Texto distorsionado o caracteres extraños

**Síntomas:**
- Salida de impresión con caracteres incorrectos
- Texto cortado o mal formateado

**Soluciones:**
1. Verificar codificación UTF-8
2. Comprobar comandos ESC/POS correctos
3. Verificar ancho de papel configurado

```csharp
// Ejemplo de texto con codificación correcta
var text = "Texto de prueba";
var bytes = Encoding.UTF8.GetBytes(text);
await _printerService.PrintRawAsync(bytes);
```

### Impresora se desconecta durante operación

**Síntomas:**
- Impresión se interrumpe
- Error: "Impresora no conectada"

**Soluciones:**
1. Implementar reconexión automática
2. Verificar estabilidad de conexión
3. Aumentar timeouts de operación

```csharp
public async Task PrintWithReconnectAsync(string data)
{
    var result = await _printerService.PrintAsync(data);
    if (result.IsFailed && result.Errors.First().Message.Contains("conectada"))
    {
        // Intentar reconectar
        var reconnectResult = await _printerService.ConnectAsync("RPT008_Default");
        if (reconnectResult.IsSuccess)
        {
            // Reintentar impresión
            result = await _printerService.PrintAsync(data);
        }
    }
}
```

## Problemas de Cola de Impresión

### Cola congestionada

**Síntomas:**
- Muchos trabajos pendientes
- Impresiones lentas o retrasadas

**Diagnóstico:**
```csharp
var pendingJobs = await _printJobQueue.GetPendingJobsAsync();
foreach (var job in pendingJobs.Value)
{
    Console.WriteLine($"Job {job.Id}: {job.Status} - Prioridad: {job.Priority}");
}
```

**Soluciones:**
1. Verificar que PrintJobProcessor esté activo
2. Cancelar trabajos atascados
3. Reiniciar el procesador de cola

### Trabajos se quedan en estado "Processing"

**Síntomas:**
- Trabajo marcado como procesando pero no avanza
- Impresora no responde

**Soluciones:**
1. Cancelar trabajos atascados manualmente
2. Verificar conectividad de impresora
3. Reiniciar PrintJobProcessor

```csharp
// Cancelar trabajo específico
await _printJobQueue.CancelAsync(jobId);

// O cancelar todos los trabajos atascados
var jobs = await _printJobQueue.GetPendingJobsAsync();
foreach (var job in jobs.Value.Where(j => j.Status == PrintJobStatus.Processing))
{
    await _printJobQueue.CancelAsync(job.Id);
}
```

## Problemas Específicos de Modelos

### RPT008 no responde a comandos

**Síntomas:**
- Impresora RPT008 recibe comandos pero no imprime
- Luces de la impresora parpadean pero no hay salida

**Soluciones:**
1. Verificar firmware de la impresora actualizado
2. Comprobar comandos ESC/POS específicos para RPT008
3. Verificar modo de operación (ESC/POS vs otros)

### Impresora genérica no compatible

**Síntomas:**
- Error: "Modelo de impresora no compatible"
- Impresora no responde a comandos estándar

**Soluciones:**
1. Verificar lista de modelos soportados
2. Implementar soporte personalizado si es necesario
3. Usar modo "Generic" con comandos básicos

## Problemas de Rendimiento

### Impresiones lentas

**Síntomas:**
- Impresión toma mucho tiempo
- Aplicación se congela durante impresión

**Soluciones:**
1. Usar impresión asíncrona siempre
2. Implementar timeouts apropiados
3. Optimizar tamaño de datos de impresión

```csharp
// Impresión asíncrona correcta
public async Task PrintAsync(Sale sale)
{
    // NO BLOQUEAR UI
    var result = await _printerService.PrintReceiptAsync(sale);

    // Actualizar UI en hilo principal si es necesario
    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
    {
        UpdatePrintStatus(result);
    });
}
```

### Memoria insuficiente

**Síntomas:**
- Errores de OutOfMemoryException
- Aplicación se cierra durante impresión

**Soluciones:**
1. Procesar datos en chunks
2. Liberar recursos después de impresión
3. Limitar tamaño de trabajos de impresión

## Problemas de Configuración

### Configuración no se carga

**Síntomas:**
- Error: "No se encontró configuración para la impresora"
- Impresora no aparece en lista

**Soluciones:**
1. Verificar sintaxis JSON en appsettings.json
2. Comprobar nombre de sección "ThermalPrinters"
3. Validar estructura de configuración

### Configuración inválida

**Síntomas:**
- Errores de validación al iniciar
- Timeout o baud rate incorrectos

**Soluciones:**
1. Usar valores por defecto válidos
2. Validar configuración antes de guardar
3. Probar configuración con impresión de prueba

## Problemas de Red (LAN)

### Conexión de red falla

**Síntomas:**
- Error: "No se puede conectar al host remoto"
- Timeout en conexiones LAN

**Soluciones:**
1. Verificar dirección IP y puerto
2. Comprobar conectividad de red
3. Verificar firewall y antivirus
4. Configurar impresora en misma subred

### Impresora LAN no visible

**Síntomas:**
- Impresora no responde a ping
- Error de conexión de red

**Soluciones:**
1. Verificar configuración IP de impresora
2. Comprobar cableado de red
3. Reiniciar impresora y router
4. Verificar puerto 9100 abierto

## Logging y Diagnóstico

### Habilitar logging detallado

```json
{
  "Logging": {
    "LogLevel": {
      "SistemaDeVentas.Infrastructure.Services.Printer": "Debug",
      "Microsoft.Extensions.Hosting": "Information"
    }
  }
}
```

### Logs comunes de error

- **"Error al conectar a {PrinterName}"**: Problema de conexión física
- **"Impresora no conectada"**: Conexión perdida durante operación
- **"Error al formatear datos"**: Problema con datos de entrada
- **"Cola de impresión llena"**: Demasiados trabajos pendientes

### Herramientas de diagnóstico

```csharp
// Verificar estado completo del sistema
public async Task DiagnosePrintingSystemAsync()
{
    Console.WriteLine("=== DIAGNÓSTICO DE SISTEMA DE IMPRESIÓN ===");

    // Verificar configuración
    var config = await _printerConfiguration.GetAllConfigurationsAsync();
    Console.WriteLine($"Configuraciones: {config.Value.Count}");

    // Verificar conexión
    var status = await _printerService.GetStatusAsync();
    Console.WriteLine($"Estado impresora: {status.Value}");

    // Verificar cola
    var jobs = await _printJobQueue.GetPendingJobsAsync();
    Console.WriteLine($"Trabajos pendientes: {jobs.Value.Count}");

    // Verificar servicios
    var processorRunning = CheckPrintJobProcessorStatus();
    Console.WriteLine($"Procesador activo: {processorRunning}");
}
```

## Recuperación de Errores

### Estrategia de fallback

```csharp
public async Task PrintWithFallbackAsync(Sale sale)
{
    // Intentar impresora principal
    var result = await _printerService.PrintReceiptAsync(sale);

    if (result.IsFailed)
    {
        _logger.LogWarning("Fallo en impresora principal, intentando backup");

        // Cambiar a impresora backup
        var backupResult = await _printerService.ConnectAsync("RPT008_Backup");
        if (backupResult.IsSuccess)
        {
            result = await _printerService.PrintReceiptAsync(sale);
        }

        if (result.IsFailed)
        {
            // Guardar para reintento posterior
            await SaveFailedPrintJobAsync(sale, result.Errors.First().Message);
        }
    }
}
```

### Reintento automático

```csharp
public async Task PrintWithRetryAsync(string data, int maxRetries = 3)
{
    var retryCount = 0;
    var delay = 1000; // 1 segundo

    while (retryCount < maxRetries)
    {
        var result = await _printerService.PrintAsync(data);
        if (result.IsSuccess)
        {
            return;
        }

        retryCount++;
        _logger.LogWarning("Intento {Retry} fallido: {Error}", retryCount, result.Errors.First().Message);

        await Task.Delay(delay);
        delay *= 2; // Backoff exponencial
    }

    throw new Exception($"Impresión fallida después de {maxRetries} intentos");
}
```

## Contacto y Soporte

Para problemas no resueltos:

1. Revisar logs detallados
2. Verificar documentación del fabricante
3. Contactar soporte técnico
4. Reportar bugs con información completa del entorno