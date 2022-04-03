using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObligatorioP3.Models
{
    public abstract class Compra
    {
        public int IdCompra { get; set; }

        public  DateTime FechaCompra { get; set; }

        public List<Item> Items { get; set; }
    public abstract double CalcularPrecioCompra();

    }
}
