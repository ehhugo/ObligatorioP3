using System;
using System.Collections.Generic;
using System.Text;
using Dominio.Entidades;

namespace Dominio.InterfacesRepositorio
{
    public interface IRepositorioUsuarios: IRepositorio <Usuario>    
    {
        Usuario BuscarUsuarioLogin(Usuario u);
    }
}
