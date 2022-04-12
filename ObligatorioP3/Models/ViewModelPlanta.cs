using Dominio.Entidades;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObligatorioP3.Models
{
    public class ViewModelPlanta
    {
        public Planta Planta { get; set; }
        public IEnumerable<Tipo> Tipos { get; set; }
        public IEnumerable<Ambiente> Ambientes { get; set; }
        public IEnumerable<Iluminacion> Iluminaciones { get; set; }
        public int IdTipoSeleccionado { get; set; }
        public int IdAmbienteSeleccionado { get; set; }
        public int IdIluminacionSeleccionada { get; set; }
        public IFormFile Imagen { get; set; }
    }
}
