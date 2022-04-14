using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.InterfacesRepositorio;

namespace Dominio.Entidades
{
    public class Planta : IValidate<Planta>

    {
        public int IdPlanta { get; set; }

        public Tipo TipoPlanta { get; set; }

        public string NombreCientifico { get; set; }

        public List<string> NombreVulgar { get; set; }
        
        public string Descripcion { get; set; }
        
        public Ambiente Ambiente { get; set; }

        public decimal AlturaMaxima { get; set; }

        public string Foto { get; set; }

        public string FrecuenciaRiego { get; set; }

        public Iluminacion TipoIluminacion { get; set; }

        public int TemperaturaMantenimiento { get; set; }

        public bool Validar(Planta obj)
        {
            return true;
        }
    }
}
