# Guía de Uso para Desarrolladores

## Inyección de Dependencias

### Registro de Servicios

```csharp
// En Program.cs o Startup.cs
using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.ViewModels.ViewModels;
using SistemaDeVentas.Infrastructure.Services.Printer;

public void ConfigureServices(IServiceCollection services)
{
    // Servicios de impresión
    services.AddScoped<IPrinterConfiguration, PrinterConfiguration>();
    services.AddScoped<IThermalPrinterService, ThermalPrinterService>();
    services.AddScoped<IPrintJobQueue, PrintJobQueue>();
    services.AddHostedService<PrintJobProcessor>();

    // ViewModels
    services.AddTransient<PrintViewModel>();
}
```

## Uso Básico del Servicio

### Conexión a Impresora

```csharp
public class SalesService
{
    private readonly IThermalPrinterService _printerService;

    public SalesService(IThermalPrinterService printerService)
    {
        _printerService = printerService;
    }

    public async Task ConnectToPrinterAsync()
    {
        var result = await _printerService.ConnectAsync("RPT008_Default");

        if (result.IsSuccess)
        {
            Console.WriteLine("Conectado a impresora exitosamente");
        }
        else
        {
            Console.WriteLine($"Error de conexión: {result.Errors.First().Message}");
        }
    }
}
```

### Impresión de Boleta de Venta

```csharp
public async Task PrintReceiptAsync(Sale sale)
{
    var result = await _printerService.PrintReceiptAsync(sale);

    if (result.IsSuccess)
    {
        Console.WriteLine($"Boleta impresa. ID trabajo: {result.Value}");
    }
    else
    {
        Console.WriteLine($"Error al imprimir boleta: {result.Errors.First().Message}");
    }
}
```

### Impresión de Factura DTE

```csharp
public async Task PrintInvoiceAsync(DteDocument dteDocument, string qrCodeData)
{
    var result = await _printerService.PrintInvoiceAsync(dteDocument, qrCodeData);

    if (result.IsSuccess)
    {
        Console.WriteLine($"Factura impresa. ID trabajo: {result.Value}");
    }
    else
    {
        Console.WriteLine($"Error al imprimir factura: {result.Errors.First().Message}");
    }
}
```

## Uso del PrintViewModel

### En WinUI (MVVM)

```csharp
// En el ViewModel de la página de ventas
public class SalesPageViewModel : BaseViewModel
{
    private readonly PrintViewModel _printViewModel;

    public SalesPageViewModel(PrintViewModel printViewModel)
    {
        _printViewModel = printViewModel;
        PrintCommand = new RelayCommand(async () => await PrintCurrentSaleAsync());
    }

    public PrintViewModel PrintViewModel => _printViewModel;

    public ICommand PrintCommand { get; }

    private async Task PrintCurrentSaleAsync()
    {
        if (CurrentSale != null)
        {
            _printViewModel.CurrentSale = CurrentSale;
            await _printViewModel.PrintReceiptCommand.ExecuteAsync(null);
        }
    }
}
```

### En XAML

```xml
<!-- SalesPage.xaml -->
<UserControl x:Class="SistemaDeVentas.WinUI.Pages.SalesPage"
             xmlns:viewModels="using:SistemaDeVentas.Core.ViewModels.ViewModels">
    <UserControl.DataContext>
        <viewModels:SalesPageViewModel />
    </UserControl.DataContext>

    <Grid>
        <!-- Contenido de ventas -->

        <!-- Panel de impresión -->
        <Border Grid.Row="1" BorderBrush="Gray" BorderThickness="1" Margin="10">
            <StackPanel DataContext="{Binding PrintViewModel}">
                <TextBlock Text="Impresión" FontWeight="Bold" Margin="5" />

                <ComboBox ItemsSource="{Binding AvailablePrinters}"
                         SelectedItem="{Binding SelectedPrinter, Mode=TwoWay}"
                         Margin="5" />

                <Button Content="Imprimir Boleta"
                        Command="{Binding PrintReceiptCommand}"
                        Margin="5" />

                <Button Content="Imprimir Factura"
                        Command="{Binding PrintInvoiceCommand}"
                        Margin="5" />

                <Button Content="Prueba de Impresión"
                        Command="{Binding TestPrintCommand}"
                        Margin="5" />

                <TextBlock Text="{Binding PrintStatus}" Margin="5" />
                <TextBlock Text="{Binding SuccessMessage}"
                          Foreground="Green" Margin="5" />
                <TextBlock Text="{Binding ErrorMessage}"
                          Foreground="Red" Margin="5" />
            </StackPanel>
        </Border>
    </Grid>
</UserControl>
```

## Formateo Personalizado

### Impresión Directa con Datos Personalizados

```csharp
public async Task PrintCustomReceiptAsync()
{
    var customData = new StringBuilder();
    customData.AppendLine("TIENDA EJEMPLO");
    customData.AppendLine("====================");
    customData.AppendLine($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm}");
    customData.AppendLine();
    customData.AppendLine("Producto A     2 x $1000 = $2000");
    customData.AppendLine("Producto B     1 x $1500 = $1500");
    customData.AppendLine("---------------------");
    customData.AppendLine("TOTAL:              $3500");
    customData.AppendLine();
    customData.AppendLine("¡Gracias por su compra!");

    var result = await _printerService.PrintAsync(customData.ToString(), PrintJobPriority.Normal);
}
```

### Formateo con Comandos ESC/POS

```csharp
public async Task PrintFormattedDataAsync()
{
    var formattedData = new List<byte>();

    // Inicializar impresora
    formattedData.AddRange(new byte[] { 0x1B, 0x40 }); // ESC @

    // Texto centrado y negrita
    formattedData.AddRange(new byte[] { 0x1B, 0x61, 0x01 }); // ESC a 1 (centrado)
    formattedData.AddRange(new byte[] { 0x1B, 0x45, 0x01 }); // ESC E 1 (negrita)

    var title = Encoding.UTF8.GetBytes("RECIBO DE PAGO\n");
    formattedData.AddRange(title);

    // Texto normal alineado a izquierda
    formattedData.AddRange(new byte[] { 0x1B, 0x61, 0x00 }); // ESC a 0 (izquierda)
    formattedData.AddRange(new byte[] { 0x1B, 0x45, 0x00 }); // ESC E 0 (normal)

    var content = Encoding.UTF8.GetBytes("Contenido del recibo...\n");
    formattedData.AddRange(content);

    // Corte de papel
    formattedData.AddRange(new byte[] { 0x1D, 0x56, 0x42, 0x00 }); // GS V B 0

    var result = await _printerService.PrintRawAsync(formattedData.ToArray());
}
```

## Manejo de Errores

### Patrón de Manejo de Errores

```csharp
public async Task SafePrintAsync(Sale sale)
{
    try
    {
        // Verificar estado de la impresora
        var statusResult = await _printerService.GetStatusAsync();
        if (statusResult.IsFailed)
        {
            _logger.LogWarning("Impresora no disponible: {Error}", statusResult.Errors.First().Message);
            // Intentar reconectar o usar impresora alternativa
            return;
        }

        // Intentar imprimir
        var printResult = await _printerService.PrintReceiptAsync(sale);
        if (printResult.IsSuccess)
        {
            _logger.LogInformation("Boleta impresa exitosamente. JobId: {JobId}", printResult.Value);
        }
        else
        {
            _logger.LogError("Error al imprimir boleta: {Error}", printResult.Errors.First().Message);

            // Estrategia de reintento
            await RetryPrintAsync(sale, printResult.Errors.First().Message);
        }
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error inesperado en impresión");
        // Notificar al usuario o guardar para reintento posterior
    }
}

private async Task RetryPrintAsync(Sale sale, string originalError)
{
    // Implementar lógica de reintento con backoff
    await Task.Delay(1000); // Esperar 1 segundo

    var retryResult = await _printerService.PrintReceiptAsync(sale);
    if (retryResult.IsSuccess)
    {
        _logger.LogInformation("Reintento exitoso de impresión");
    }
    else
    {
        _logger.LogError("Reintento fallido: {Error}. Error original: {Original}",
            retryResult.Errors.First().Message, originalError);
    }
}
```

## Monitoreo de Cola de Impresión

### Verificación de Trabajos Pendientes

```csharp
public async Task MonitorPrintQueueAsync()
{
    var queueService = _serviceProvider.GetRequiredService<IPrintJobQueue>();

    while (true)
    {
        var pendingJobs = await queueService.GetPendingJobsAsync();
        if (pendingJobs.IsSuccess)
        {
            var count = pendingJobs.Value.Count;
            if (count > 10)
            {
                _logger.LogWarning("Cola de impresión congestionada: {Count} trabajos pendientes", count);
            }
        }

        await Task.Delay(30000); // Verificar cada 30 segundos
    }
}
```

## Integración con DTE

### Impresión de Factura Electrónica Completa

```csharp
public async Task PrintCompleteDteInvoiceAsync(DteDocument dteDocument)
{
    // Generar código QR (implementación depende del servicio QR)
    var qrCodeData = await GenerateQrCodeForDteAsync(dteDocument);

    // Verificar que el documento esté completo
    if (string.IsNullOrEmpty(qrCodeData))
    {
        throw new InvalidOperationException("Código QR requerido para DTE");
    }

    // Imprimir con prioridad alta
    var result = await _printerService.PrintInvoiceAsync(dteDocument, qrCodeData);

    if (result.IsSuccess)
    {
        // Marcar documento como impreso en el sistema
        await MarkDteAsPrintedAsync(dteDocument.Id, result.Value);
    }
}

private async Task<string> GenerateQrCodeForDteAsync(DteDocument dteDocument)
{
    // Implementación específica para generar QR según estándares SII
    // Esto normalmente involucra el servicio de PDF417
    var pdf417Service = _serviceProvider.GetRequiredService<IPdf417Service>();
    return await pdf417Service.GenerateQrCodeDataAsync(dteDocument);
}
```

## Testing y Desarrollo

### Impresión de Prueba

```csharp
public async Task TestPrinterSetupAsync()
{
    // Conectar
    var connectResult = await _printerService.ConnectAsync("RPT008_Default");
    if (connectResult.IsFailed)
    {
        Console.WriteLine("Error de conexión: " + connectResult.Errors.First().Message);
        return;
    }

    // Imprimir prueba
    var testData = "PRUEBA DE IMPRESION\n" +
                   "==================\n" +
                   "Esta es una prueba del sistema de impresión térmica.\n" +
                   "Fecha: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "\n" +
                   "Estado: OK\n\n";

    var printResult = await _printerService.PrintAsync(testData, PrintJobPriority.Normal);

    if (printResult.IsSuccess)
    {
        Console.WriteLine("Prueba exitosa. JobId: " + printResult.Value);
    }
    else
    {
        Console.WriteLine("Error en prueba: " + printResult.Errors.First().Message);
    }

    // Cortar papel
    await _printerService.CutPaperAsync();
}
```

## Mejores Prácticas

1. **Validación**: Siempre validar datos antes de imprimir
2. **Timeouts**: Configurar timeouts apropiados para el entorno
3. **Logging**: Registrar todas las operaciones de impresión
4. **Reintentos**: Implementar lógica de reintento para fallos temporales
5. **Prioridades**: Usar prioridades apropiadas (Normal para boletas, High para facturas)
6. **Monitoreo**: Monitorear el estado de la cola de impresión
7. **UI Feedback**: Proporcionar feedback claro al usuario sobre el estado de impresión
8. **Error Handling**: Manejar errores gracefully sin bloquear la aplicación
9. **Testing**: Probar regularmente la funcionalidad de impresión
10. **Documentación**: Mantener documentación actualizada de formatos personalizados