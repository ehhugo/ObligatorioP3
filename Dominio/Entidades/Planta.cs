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
            if (obj.NombreCientifico != null)
            {
                if (obj.NombreCientifico.Trim().Length > 0 && contieneSoloLetras(obj.NombreCientifico))
                {
                    if (obj.Descripcion != null && obj.Descripcion.Trim().Length>=10 && obj.Descripcion.Trim().Length <= 500)
                    {
                        if (obj.AlturaMaxima > 0)
                        {
                            if (validarAtributo(obj.Foto))
                            {
                                if (validarAtributo(obj.FrecuenciaRiego))
                                {
                                    if (obj.TemperaturaMantenimiento > 0)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        private bool validarAtributo(string atributo)
        {
            bool validado = false;
            if (atributo != null && atributo.Trim().Length > 0) validado = true;
            return validado;
        }

        public bool contieneSoloLetras(string cadena)
        {
            for (int i = 0; i < cadena.Length; i++)
            {
                Char c = cadena[i];
                // Si no está entre a y z, ni entre A y Z, ni es un espacio
                if (!((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == ' '))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
