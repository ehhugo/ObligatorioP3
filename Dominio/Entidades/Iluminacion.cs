using Dominio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Iluminacion : IValidate<Iluminacion>
    {
        public int IdIluminacion { get; set; }

        public string Tipo { get; set; }

        public bool Validar(Iluminacion obj)
        {
            if (obj.Tipo != null)
            {
                if (obj.Tipo.Trim().Length > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
