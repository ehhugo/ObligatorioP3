using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObligatorioP3.Models
{
    public class Importacion : Compra
    {
        public double ImpuestoImportacion { get; set; }

        public bool EsSurAmerica { get; set; }

        public double TasaArancelariaSA { get; set; }

        public string MedidasSantiarias { get; set; }


        public override double CalcularPrecioCompra()
        {
            throw new NotImplementedException();
        }
    }
}
