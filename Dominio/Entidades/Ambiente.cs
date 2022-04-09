using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.InterfacesRepositorio;

namespace Dominio.Entidades
{
    public class Ambiente { 

        public int IdAmbiente { get; set; }

        public string TipoAmbiente { get; set; }

        public bool Validar()
        {
            if (TipoAmbiente != null)
            {
                if (TipoAmbiente.Trim().Length > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
