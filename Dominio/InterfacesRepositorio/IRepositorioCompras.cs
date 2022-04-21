using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.InterfacesRepositorio
{
    public class IRepositorioCompras : IRepositorio<Compra>
    {
        bool IRepositorio<Compra>.Add(Compra obj)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Compra> IRepositorio<Compra>.FindAll()
        {
            throw new NotImplementedException();
        }

        Compra IRepositorio<Compra>.FindById(int id)
        {
            throw new NotImplementedException();
        }

        bool IRepositorio<Compra>.Remove(int id)
        {
            throw new NotImplementedException();
        }

        bool IRepositorio<Compra>.update(Compra obj)
        {
            throw new NotImplementedException();
        }
    }
}
