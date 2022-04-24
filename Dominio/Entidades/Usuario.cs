using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Usuario
    {

        public int idUsuario { get; set; }

        public string Mail { get; set; }
        public string Contrasenia { get; set; }


        public bool VerificarMail()
        {
            bool validado = false;
           
            if (Mail.Contains("@"))
            {
                string[] emailArray = Mail.Split("@");

                if (emailArray[1].Contains("."))
                {
                    validado = true;
                }
            }
            return validado;
        }
        
        public bool VerificarPass()
        {
            bool ret = false;
            bool banderaMayuscula = false;
            bool banderaNumero = false;
            bool banderaMinuscula = false;

            if (Contrasenia.Length >= 6)
            {
                foreach (char letter in Contrasenia)
                {
                    if (letter >= 65 && letter <= 90)
                    {
                        banderaMayuscula = true;
                    }
                    if (letter >= 48 && letter <= 57)
                    {
                        banderaNumero = true;
                    }
                    if (letter >= 97 && letter <= 122)
                    {
                        banderaMinuscula = true;
                    }
                }
                if (banderaMinuscula && banderaNumero && banderaMayuscula)
                {
                    ret = true;
                }
            }
            return ret;
        }
    }
}
