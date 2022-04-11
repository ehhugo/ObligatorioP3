using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.InterfacesRepositorio;

namespace Dominio.Entidades
{
    public class Tipo
    {
        public int IdTipo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }


        public bool Validar()
        {
            if (Nombre != null && Descripcion != null)
            {
                if (Nombre.Trim().Length > 0 && Descripcion.Trim().Length > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
