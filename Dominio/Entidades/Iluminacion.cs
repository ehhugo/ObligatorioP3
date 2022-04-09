using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Iluminacion
    {
        public int IdIluminacion { get; set; }

        public string Tipo { get; set; }

        public bool Validar()
        {
            if (Tipo != null)
            {
                if (Tipo.Trim().Length > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }

}
