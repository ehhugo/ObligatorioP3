using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Item
    {

        public Planta Planta { get; set; }

        public int Cantidad { get; set; }

        public double Precio { get; set; }
    }
}
