using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using SistemaDeVentas.Core.Application.Services.DTE;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace SistemaDeVentas.Infrastructure.Services.DTE;

/// <summary>
/// Servicio para generación de PDFs de documentos DTE según especificaciones del SII.
/// Utiliza iText7 para crear PDFs con layout específico, timbre PDF417 y manejo de caracteres especiales.
/// </summary>
public class PdfGeneratorService : IPdfGeneratorService
{
    private readonly IPdf417Service _pdf417Service;

    // Dimensiones según especificaciones SII (21.5 x 11 cm)
    private const float PAGE_WIDTH_CM = 21.5f;
    private const float PAGE_HEIGHT_CM = 11.0f;
    private const float CM_TO_POINTS = 28.3465f;
    private const float PAGE_WIDTH = PAGE_WIDTH_CM * CM_TO_POINTS; // ≈ 609 puntos
    private const float PAGE_HEIGHT = PAGE_HEIGHT_CM * CM_TO_POINTS; // ≈ 312 puntos

    // Bordes mínimos (0.5 cm)
    private const float BORDER_CM = 0.5f;
    private const float BORDER_POINTS = BORDER_CM * CM_TO_POINTS; // ≈ 14 puntos

    // Dimensiones timbre PDF417 (2 x 5 cm mínimo)
    private const float PDF417_WIDTH_CM = 2.0f;
    private const float PDF417_HEIGHT_CM = 5.0f;
    private const float PDF417_WIDTH = PDF417_WIDTH_CM * CM_TO_POINTS; // ≈ 57 puntos
    private const float PDF417_HEIGHT = PDF417_HEIGHT_CM * CM_TO_POINTS; // ≈ 142 puntos

    public PdfGeneratorService(IPdf417Service pdf417Service)
    {
        _pdf417Service = pdf417Service ?? throw new ArgumentNullException(nameof(pdf417Service));
    }

    /// <inheritdoc/>
    public byte[] GeneratePdf(DteDocument dteDocument, bool isCedible = false)
    {
        if (dteDocument == null)
        {
            throw new ArgumentNullException(nameof(dteDocument));
        }

        try
        {
            using var memoryStream = new MemoryStream();
            using var pdfWriter = new PdfWriter(memoryStream);
            using var pdfDocument = new PdfDocument(pdfWriter);

            // Configurar página con dimensiones específicas
            var pageSize = new PageSize(PAGE_WIDTH, PAGE_HEIGHT);
            var page = pdfDocument.AddNewPage(pageSize);

            using var document = new Document(pdfDocument, pageSize);
            document.SetMargins(BORDER_POINTS, BORDER_POINTS, BORDER_POINTS, BORDER_POINTS);

            // Generar layout según tipo de documento
            if (dteDocument.IdDoc.TipoDTE == TipoDte.BoletaAfecta || dteDocument.IdDoc.TipoDTE == TipoDte.BoletaExenta)
            {
                GenerateBoletaLayout(document, dteDocument, isCedible);
            }
            else
            {
                GenerateFacturaLayout(document, dteDocument, isCedible);
            }

            document.Close();
            return memoryStream.ToArray();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error al generar PDF del documento DTE.", ex);
        }
    }

    /// <inheritdoc/>
    public byte[] GenerateBoletaPdf(DteDocument dteDocument, bool isCedible = false)
    {
        if (dteDocument == null)
        {
            throw new ArgumentNullException(nameof(dteDocument));
        }

        if (dteDocument.IdDoc.TipoDTE != TipoDte.BoletaAfecta && dteDocument.IdDoc.TipoDTE != TipoDte.BoletaExenta)
        {
            throw new ArgumentException("El documento debe ser una boleta (TipoDTE 39 o 41).", nameof(dteDocument));
        }

        return GeneratePdf(dteDocument, isCedible);
    }

    private void GenerateFacturaLayout(Document document, DteDocument dteDocument, bool isCedible)
    {
        // Configurar fuente con soporte para caracteres especiales
        var font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA, iText.IO.Font.PdfEncodings.UTF8);
        var boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD, iText.IO.Font.PdfEncodings.UTF8);

        // Agregar recuadro tipo documento (negro para facturas)
        AddDocumentBorder(document, ColorConstants.BLACK);

        // Encabezado
        AddHeader(document, dteDocument, font);

        // Información del emisor
        AddEmisorInfo(document, dteDocument.Emisor, font);

        // Información del receptor (si aplica)
        if (dteDocument.Receptor != null && !string.IsNullOrEmpty(dteDocument.Receptor.RutReceptor))
        {
            AddReceptorInfo(document, dteDocument.Receptor, font);
        }

        // Detalles
        AddDetalles(document, dteDocument.Detalles, font);

        // Totales
        AddTotales(document, dteDocument.Totales, font);

        // Timbre PDF417
        AddPdf417Stamp(document, dteDocument);

        // Leyendas requeridas
        AddRequiredLegends(document, dteDocument, isCedible, font);

        // Acuse de recibo si es cedible
        if (isCedible)
        {
            AddAcuseRecibo(document, font);
        }
    }

    private void GenerateBoletaLayout(Document document, DteDocument dteDocument, bool isCedible)
    {
        // Configurar fuente
        var font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA, iText.IO.Font.PdfEncodings.UTF8);
        var boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD, iText.IO.Font.PdfEncodings.UTF8);

        // Agregar recuadro tipo documento (rojo para boletas)
        AddDocumentBorder(document, ColorConstants.RED);

        // Template específico para boletas (más compacto)
        AddBoletaHeader(document, dteDocument, font);
        AddEmisorInfo(document, dteDocument.Emisor, font);
        AddBoletaDetalles(document, dteDocument.Detalles, font);
        AddTotales(document, dteDocument.Totales, font);
        AddPdf417Stamp(document, dteDocument);
        AddRequiredLegends(document, dteDocument, isCedible, font);

        if (isCedible)
        {
            AddAcuseRecibo(document, font);
        }
    }

    private void AddDocumentBorder(Document document, iText.Kernel.Colors.Color color)
    {
        var canvas = new PdfCanvas(document.GetPdfDocument().GetFirstPage());
        canvas.SetStrokeColor(color)
              .SetLineWidth(2)
              .Rectangle(BORDER_POINTS, BORDER_POINTS,
                        PAGE_WIDTH - 2 * BORDER_POINTS,
                        PAGE_HEIGHT - 2 * BORDER_POINTS)
              .Stroke();
    }

    private void AddHeader(Document document, DteDocument dteDocument, PdfFont font)
    {
        var boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD, iText.IO.Font.PdfEncodings.UTF8);
        var headerTable = new Table(UnitValue.CreatePercentArray(new float[] { 1, 1, 1 }))
            .SetWidth(UnitValue.CreatePercentValue(100));

        // Tipo de documento
        var tipoCell = new Cell()
            .Add(new Paragraph($"R.U.T.: {dteDocument.Emisor.RutEmisor}")
                .SetFont(font)
                .SetFontSize(10))
            .Add(new Paragraph(GetTipoDteDescription(dteDocument.IdDoc.TipoDTE))
                .SetFont(boldFont)
                .SetFontSize(12))
            .SetBorder(iText.Layout.Borders.Border.NO_BORDER);
        headerTable.AddCell(tipoCell);

        // Número de folio
        var folioCell = new Cell()
            .Add(new Paragraph($"N° {dteDocument.IdDoc.Folio}")
                .SetFont(font)
                .SetFontSize(14)
                .SetFont(boldFont)
                .SetTextAlignment(TextAlignment.CENTER))
            .SetBorder(iText.Layout.Borders.Border.NO_BORDER);
        headerTable.AddCell(folioCell);

        // Fecha
        var fechaCell = new Cell()
            .Add(new Paragraph($"Santiago, {dteDocument.IdDoc.FechaEmision:dd/MM/yyyy}")
                .SetFont(font)
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.RIGHT))
            .SetBorder(iText.Layout.Borders.Border.NO_BORDER);
        headerTable.AddCell(fechaCell);

        document.Add(headerTable);
    }

    private void AddBoletaHeader(Document document, DteDocument dteDocument, PdfFont font)
    {
        var boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD, iText.IO.Font.PdfEncodings.UTF8);
        var header = new Paragraph($"{GetTipoDteDescription(dteDocument.IdDoc.TipoDTE)} N° {dteDocument.IdDoc.Folio}")
            .SetFont(boldFont)
            .SetFontSize(12)
            .SetTextAlignment(TextAlignment.CENTER);
        document.Add(header);

        var fecha = new Paragraph($"Fecha: {dteDocument.IdDoc.FechaEmision:dd/MM/yyyy}")
            .SetFont(font)
            .SetFontSize(10)
            .SetTextAlignment(TextAlignment.CENTER);
        document.Add(fecha);
    }

    private void AddEmisorInfo(Document document, Emisor emisor, PdfFont font)
    {
        var boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD, iText.IO.Font.PdfEncodings.UTF8);
        var emisorTable = new Table(UnitValue.CreatePercentArray(new float[] { 1 }))
            .SetWidth(UnitValue.CreatePercentValue(100))
            .SetMarginTop(10);

        var emisorCell = new Cell()
            .Add(new Paragraph(emisor.RazonSocial)
                .SetFont(boldFont)
                .SetFontSize(10))
            .Add(new Paragraph($"R.U.T.: {emisor.RutEmisor}")
                .SetFont(font)
                .SetFontSize(9))
            .Add(new Paragraph(emisor.GiroEmisor)
                .SetFont(font)
                .SetFontSize(9))
            .Add(new Paragraph($"{emisor.DireccionOrigen}, {emisor.ComunaOrigen}, {emisor.CiudadOrigen}")
                .SetFont(font)
                .SetFontSize(9))
            .SetBorder(iText.Layout.Borders.Border.NO_BORDER);
        emisorTable.AddCell(emisorCell);

        document.Add(emisorTable);
    }

    private void AddReceptorInfo(Document document, Receptor receptor, PdfFont font)
    {
        var boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD, iText.IO.Font.PdfEncodings.UTF8);
        var receptorTable = new Table(UnitValue.CreatePercentArray(new float[] { 1 }))
            .SetWidth(UnitValue.CreatePercentValue(100))
            .SetMarginTop(10);

        var receptorCell = new Cell()
            .Add(new Paragraph("Receptor:")
                .SetFont(boldFont)
                .SetFontSize(10))
            .Add(new Paragraph(receptor.RazonSocialReceptor)
                .SetFont(font)
                .SetFontSize(9))
            .Add(new Paragraph($"R.U.T.: {receptor.RutReceptor}")
                .SetFont(font)
                .SetFontSize(9))
            .Add(new Paragraph($"{receptor.DireccionReceptor}, {receptor.ComunaReceptor}, {receptor.CiudadReceptor}")
                .SetFont(font)
                .SetFontSize(9))
            .SetBorder(iText.Layout.Borders.Border.NO_BORDER);
        receptorTable.AddCell(receptorCell);

        document.Add(receptorTable);
    }

    private void AddDetalles(Document document, List<DetalleDte> detalles, PdfFont font)
    {
        var boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD, iText.IO.Font.PdfEncodings.UTF8);
        var detallesTable = new Table(UnitValue.CreatePercentArray(new float[] { 3, 1, 1, 1 }))
            .SetWidth(UnitValue.CreatePercentValue(100))
            .SetMarginTop(15);

        // Encabezados
        detallesTable.AddHeaderCell(new Cell().Add(new Paragraph("Descripción").SetFont(boldFont).SetFontSize(9)).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        detallesTable.AddHeaderCell(new Cell().Add(new Paragraph("Cant.").SetFont(boldFont).SetFontSize(9)).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        detallesTable.AddHeaderCell(new Cell().Add(new Paragraph("Precio").SetFont(boldFont).SetFontSize(9)).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        detallesTable.AddHeaderCell(new Cell().Add(new Paragraph("Total").SetFont(boldFont).SetFontSize(9)).SetBackgroundColor(ColorConstants.LIGHT_GRAY));

        // Detalles
        foreach (var detalle in detalles)
        {
            detallesTable.AddCell(new Cell().Add(new Paragraph(detalle.NombreItem).SetFont(font).SetFontSize(8)));
            detallesTable.AddCell(new Cell().Add(new Paragraph(detalle.CantidadItem?.ToString("N2") ?? "").SetFont(font).SetFontSize(8)).SetTextAlignment(TextAlignment.RIGHT));
            detallesTable.AddCell(new Cell().Add(new Paragraph(detalle.PrecioItem?.ToString("N0") ?? "").SetFont(font).SetFontSize(8)).SetTextAlignment(TextAlignment.RIGHT));
            detallesTable.AddCell(new Cell().Add(new Paragraph(detalle.MontoItem?.ToString("N0") ?? "").SetFont(font).SetFontSize(8)).SetTextAlignment(TextAlignment.RIGHT));
        }

        document.Add(detallesTable);
    }

    private void AddBoletaDetalles(Document document, List<DetalleDte> detalles, PdfFont font)
    {
        // Versión más compacta para boletas
        var detallesTable = new Table(UnitValue.CreatePercentArray(new float[] { 4, 1, 1 }))
            .SetWidth(UnitValue.CreatePercentValue(100))
            .SetMarginTop(10);

        foreach (var detalle in detalles)
        {
            detallesTable.AddCell(new Cell().Add(new Paragraph(detalle.NombreItem).SetFont(font).SetFontSize(8)));
            detallesTable.AddCell(new Cell().Add(new Paragraph(detalle.CantidadItem?.ToString("N2") ?? "").SetFont(font).SetFontSize(8)).SetTextAlignment(TextAlignment.RIGHT));
            detallesTable.AddCell(new Cell().Add(new Paragraph(detalle.MontoItem?.ToString("N0") ?? "").SetFont(font).SetFontSize(8)).SetTextAlignment(TextAlignment.RIGHT));
        }

        document.Add(detallesTable);
    }

    private void AddTotales(Document document, TotalesDte totales, PdfFont font)
    {
        var boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD, iText.IO.Font.PdfEncodings.UTF8);
        var totalesTable = new Table(UnitValue.CreatePercentArray(new float[] { 1, 1 }))
            .SetWidth(UnitValue.CreatePercentValue(50))
            .SetHorizontalAlignment(HorizontalAlignment.RIGHT)
            .SetMarginTop(10);

        if (totales.MontoNeto.HasValue)
        {
            totalesTable.AddCell(new Cell().Add(new Paragraph("Neto:").SetFont(font).SetFontSize(9)));
            totalesTable.AddCell(new Cell().Add(new Paragraph(totales.MontoNeto.Value.ToString("N0")).SetFont(font).SetFontSize(9)).SetTextAlignment(TextAlignment.RIGHT));
        }

        if (totales.IVA.HasValue)
        {
            totalesTable.AddCell(new Cell().Add(new Paragraph("IVA:").SetFont(font).SetFontSize(9)));
            totalesTable.AddCell(new Cell().Add(new Paragraph(totales.IVA.Value.ToString("N0")).SetFont(font).SetFontSize(9)).SetTextAlignment(TextAlignment.RIGHT));
        }

        totalesTable.AddCell(new Cell().Add(new Paragraph("Total:").SetFont(boldFont).SetFontSize(10)));
        totalesTable.AddCell(new Cell().Add(new Paragraph(totales.MontoTotal.ToString("N0")).SetFont(boldFont).SetFontSize(10)).SetTextAlignment(TextAlignment.RIGHT));

        document.Add(totalesTable);
    }

    private void AddPdf417Stamp(Document document, DteDocument dteDocument)
    {
        // Generar datos para el timbre (simplificado - en producción usar datos reales del TED)
        var tedData = GenerateTedData(dteDocument);
        var pdf417Bitmap = _pdf417Service.GeneratePdf417(tedData);

        // Convertir Bitmap a ImageData para iText
        using var ms = new MemoryStream();
        pdf417Bitmap.Save(ms, ImageFormat.Png);
        var imageData = iText.IO.Image.ImageDataFactory.Create(ms.ToArray());

        var pdf417Image = new iText.Layout.Element.Image(imageData)
            .SetWidth(PDF417_WIDTH)
            .SetHeight(PDF417_HEIGHT)
            .SetFixedPosition(PAGE_WIDTH - PDF417_WIDTH - BORDER_POINTS - 10,
                            BORDER_POINTS + 10);

        document.Add(pdf417Image);
    }

    private void AddRequiredLegends(Document document, DteDocument dteDocument, bool isCedible, PdfFont font)
    {
        var legends = new List<string>();

        // Agregar leyendas según tipo de documento
        legends.Add("Timbre Electrónico SII");
        legends.Add("Res. 11/2023");

        if (isCedible)
        {
            legends.Add("CEDIBLE");
        }

        // Agregar leyendas al final del documento
        foreach (var legend in legends)
        {
            document.Add(new Paragraph(legend)
                .SetFont(font)
                .SetFontSize(8)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetMarginTop(5));
        }
    }

    private void AddAcuseRecibo(Document document, PdfFont font)
    {
        document.Add(new AreaBreak());

        var acuseTable = new Table(UnitValue.CreatePercentArray(new float[] { 1, 1, 1 }))
            .SetWidth(UnitValue.CreatePercentValue(100))
            .SetMarginTop(20);

        acuseTable.AddCell(new Cell().Add(new Paragraph("Nombre").SetFont(font).SetFontSize(9)).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
        acuseTable.AddCell(new Cell().Add(new Paragraph("R.U.T.").SetFont(font).SetFontSize(9)).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
        acuseTable.AddCell(new Cell().Add(new Paragraph("Fecha").SetFont(font).SetFontSize(9)).SetBorder(iText.Layout.Borders.Border.NO_BORDER));

        // Líneas para firma
        acuseTable.AddCell(new Cell().Add(new Paragraph("______________________________").SetFont(font).SetFontSize(9)).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
        acuseTable.AddCell(new Cell().Add(new Paragraph("______________________________").SetFont(font).SetFontSize(9)).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
        acuseTable.AddCell(new Cell().Add(new Paragraph("______________________________").SetFont(font).SetFontSize(9)).SetBorder(iText.Layout.Borders.Border.NO_BORDER));

        acuseTable.AddCell(new Cell().Add(new Paragraph("Recibí conforme").SetFont(font).SetFontSize(8)).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
        acuseTable.AddCell(new Cell().Add(new Paragraph("")).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
        acuseTable.AddCell(new Cell().Add(new Paragraph("")).SetBorder(iText.Layout.Borders.Border.NO_BORDER));

        document.Add(acuseTable);
    }

    private string GetTipoDteDescription(TipoDte tipoDte)
    {
        return tipoDte switch
        {
            TipoDte.FacturaAfecta => "FACTURA ELECTRÓNICA",
            TipoDte.FacturaExenta => "FACTURA EXENTA ELECTRÓNICA",
            TipoDte.BoletaAfecta => "BOLETA ELECTRÓNICA",
            TipoDte.BoletaExenta => "BOLETA EXENTA ELECTRÓNICA",
            _ => "DOCUMENTO TRIBUTARIO ELECTRÓNICO"
        };
    }

    private string GenerateTedData(DteDocument dteDocument)
    {
        // Generar datos simplificados para el timbre (en producción usar algoritmo real del SII)
        var sb = new StringBuilder();
        sb.Append($"TipoDTE:{(int)dteDocument.IdDoc.TipoDTE};");
        sb.Append($"Folio:{dteDocument.IdDoc.Folio};");
        sb.Append($"Fecha:{dteDocument.IdDoc.FechaEmision:yyyyMMdd};");
        sb.Append($"RUTE:{dteDocument.Emisor.RutEmisor};");
        sb.Append($"Monto:{dteDocument.Totales.MontoTotal};");
        return sb.ToString();
    }
}