using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.InterfacesRepositorio
{
    public interface IValidate<T>
    {
        public bool Validar(T obj);
    }
}
