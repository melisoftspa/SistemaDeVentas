using System.Drawing.Printing;

namespace SistemaDeVentas
{
    public partial class Print
    {
        private PrintingPermission permission;
        private PrintDocument document;
        private bool landscape;
        private string printerName;
        private string paperSizes;

        public bool LandScape { get { return landscape; } set { landscape = value; } }
        public string PrinterName { get { return printerName; } set { printerName = value; } }
        public string PaperSizes { get { return paperSizes; } set { paperSizes = value; } }

        public Print(string printerName, string paperSizes, bool landscape)
        {
            this.landscape = landscape;
            this.printerName = printerName;
            this.paperSizes = paperSizes;

            document = new PrintDocument();
            permission = new PrintingPermission(System.Security.Permissions.PermissionState.Unrestricted);
            permission.Level = PrintingPermissionLevel.AllPrinting;//permisos para las impresoras

            document.DefaultPageSettings.Landscape = landscape;//horizontal o vertical
            document.PrinterSettings.PrinterName = printerName;//nombre de la impresora que utilizaremos

            foreach (PaperSize item in document.PrinterSettings.DefaultPageSettings.PrinterSettings.PaperSizes)
            {
                if (item.PaperName.Equals(paperSizes))
                {
                    document.PrinterSettings.DefaultPageSettings.PaperSize = item;//configuramos el tamaño de papel
                    break;
                }
            }

            document.PrintPage += new PrintPageEventHandler(FormatoImpresion);//configuramos el evento

        }

        private void FormatoImpresion(object sender, PrintPageEventArgs e)//creamos la impresion
        {
            Font fuente;
            const int CONST_Y = 15;//constante eje Y
            const int CONST_X = 50;//constante eje X
            int y = 0;//eje Y
            int cont = 1;

            fuente = new Font("Arial", 7, FontStyle.Bold, GraphicsUnit.Point);//asignamos el tipo de fuente
            e.Graphics.DrawString(string.Format("{0}", "Prueba de impresión"), fuente, Brushes.Black, CONST_X, y, new StringFormat());//dibujamos la impresón

            y = CONST_Y * cont;//asignamos la nueva posición a eje Y
            cont++;//Incrementamos el contador

        }

        public void PrintDocument()
        {
            document.Print();//imprimimos el documento
        }
    }
}
