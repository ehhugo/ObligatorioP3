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
            if (Nombre != null && Descripcion != null && contieneSoloLetras(Nombre))
            {
                if (Nombre.Trim().Length > 0 && Descripcion.Trim().Length >=10 && Descripcion.Trim().Length <=200)
                {
                    return true;
                }
            }
            return false;
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
