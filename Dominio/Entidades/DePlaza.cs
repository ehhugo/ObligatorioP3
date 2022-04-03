using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class DePlaza : Compra
    {
        public int Tasa { get; set; }

        public double IVACompra { get; set; }

        public double CostoFlete { get; set; }

        public override double CalcularPrecioCompra()
        {
            throw new NotImplementedException();
        }
    }
}
