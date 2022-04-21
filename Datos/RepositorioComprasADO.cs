using Dominio.Entidades;
using Dominio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos
{
    public class RepositorioComprasADO : IRepositorioCompras
    {
        #region Add
        public bool Add(Compra obj)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region FindAll y FindById
        public IEnumerable<Compra> FindAll()
        {
            throw new NotImplementedException();
        }

        public Compra FindById(int id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Remove
        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Update
        public bool update(Compra obj)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
