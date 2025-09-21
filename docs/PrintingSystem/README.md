# Sistema de Impresión Térmica

## Descripción General

El sistema de impresión térmica del Sistema de Ventas proporciona una solución completa para la impresión de boletas de venta y facturas electrónicas (DTE) en impresoras térmicas compatibles con el estándar ESC/POS. El sistema está diseñado para ser robusto, escalable y fácil de integrar con aplicaciones .NET.

## Características Principales

- **Soporte Multi-Modelo**: Compatible con impresoras 3nStar RPT008 y otros modelos genéricos ESC/POS
- **Conexiones Múltiples**: Soporte para conexiones Serial, USB y LAN
- **Sistema de Cola**: Procesamiento asíncrono de trabajos de impresión con prioridades
- **Formateo Automático**: Generación automática de formatos para boletas y facturas DTE
- **Integración DTE**: Soporte completo para códigos QR y requisitos legales de facturación electrónica
- **Logging Completo**: Seguimiento detallado de operaciones y errores
- **UI Integrada**: ViewModel para integración con interfaces WinUI

## Arquitectura del Sistema

### Componentes Principales

1. **IThermalPrinterService**: Interfaz principal del servicio de impresión
2. **ThermalPrinterService**: Implementación principal que coordina las operaciones
3. **IThermalPrinter**: Interfaz interna para implementaciones específicas de impresoras
4. **Rpt008ThermalPrinter**: Implementación específica para impresoras 3nStar RPT008
5. **PrintJobProcessor**: Servicio en background para procesamiento de cola
6. **PrintViewModel**: ViewModel para la interfaz de usuario
7. **PrinterConfiguration**: Gestión de configuración de impresoras

### Flujo de Trabajo

```
Aplicación → PrintViewModel → IThermalPrinterService → PrintJobQueue
                                                            ↓
PrintJobProcessor → ThermalPrinterService → IThermalPrinter → Impresora
```

## Modelos de Impresoras Soportadas

- **RPT008**: Impresora térmica 3nStar RPT008 (80mm)
- **Generic**: Modelos genéricos compatibles con ESC/POS

## Comandos ESC/POS Implementados

- Inicialización de impresora (ESC @)
- Formato de texto: Negrita, doble altura, doble ancho
- Alineación: Izquierda, centro, derecha
- Corte de papel automático
- Estado de impresora
- Soporte básico para códigos QR

## Integración con Módulos

### SalesPage
- Impresión automática de boletas al completar ventas
- Formateo personalizado según configuración del negocio

### DTE System
- Generación de facturas electrónicas con códigos QR
- Cumplimiento con requisitos del SII (Servicio de Impuestos Internos)

## Requisitos del Sistema

- .NET 6.0 o superior
- Windows 10/11 con soporte para puertos seriales
- Impresora térmica compatible ESC/POS
- Conexión serial, USB o LAN según el modelo

## Documentación Relacionada

- [Configuración](Configuration.md) - Guía de configuración de impresoras
- [Uso para Desarrolladores](Usage.md) - Integración y uso del API
- [Referencia de APIs](API_Reference.md) - Documentación técnica completa
- [Solución de Problemas](Troubleshooting.md) - Problemas comunes y soluciones

## Referencias

- [Manual de Programación RPT008](../../docs/RPT008-Programming-Manual.pdf)
- [Especificaciones RPT008](../../docs/RPT008-SP.pdf)
- [Estándar ESC/POS](https://en.wikipedia.org/wiki/ESC/POS)

## Mejores Prácticas

1. **Configuración**: Verificar configuración antes del primer uso
2. **Manejo de Errores**: Implementar manejo robusto de errores de conexión
3. **Cola de Impresión**: Usar prioridades apropiadas para diferentes tipos de documentos
4. **Testing**: Realizar pruebas de impresión regulares
5. **Logging**: Monitorear logs para detectar problemas tempranos
6. **Compatibilidad**: Verificar compatibilidad con nuevos modelos de impresoras