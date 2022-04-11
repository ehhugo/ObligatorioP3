using System;
using System.Collections.Generic;
using System.Text;
using Dominio.Entidades;

namespace Dominio.InterfacesRepositorio
{
    public interface IRepositorioTipos : IRepositorio<Tipo>
    {
        Tipo BuscarPorNombre(string nombre);

        bool EliminarPorNombre(string nombre);

        bool ActualizarDescripcion(Tipo tipo);
    }
}
