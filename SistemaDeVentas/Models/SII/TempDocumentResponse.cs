using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeVentas.Models.SII
{
    public class TempDocumentResponse : IResponse
    {
        public int emisor { get; set; }
        public int receptor { get; set; }
        public int dte { get; set; }
        public string codigo { get; set; }
    }
}
