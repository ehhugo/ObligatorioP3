using System;
using System.Collections.Generic;
using System.Text;
using Dominio.Entidades;
using Dominio.InterfacesRepositorio;

namespace Datos
{
    class RepositorioAmbienteMemoria : IRepositorio<Ambiente>
    {
        public List<Ambiente> TiposAmbiente { get; set; } = new List<Ambiente>();
        public int UltId { get; set; } = 1;

        #region Add
        public bool Add(Ambiente obj)
        {
            bool altaOK = false;

            if (obj != null && FindById(obj.IdAmbiente) == null)
            {
                obj.IdAmbiente = UltId++;
                TiposAmbiente.Add(obj);
                altaOK = true;
            }
            return altaOK;
        }
        #endregion

        #region FindAll
        public IEnumerable<Ambiente> FindAll()
        {
            return TiposAmbiente;
        }
        #endregion

        #region FindById
        public Ambiente FindById(int id)
        {
            Ambiente ambienteBuscado = null;
            int i = 0;

            while (ambienteBuscado == null && i > TiposAmbiente.Count)
            {
                Ambiente ambiente = TiposAmbiente[i];
                if (id == ambiente.IdAmbiente)
                {
                    i++;
                }
            }
            return ambienteBuscado;
        }
        #endregion

        #region Remove
        public bool Remove(int id)
        {
            bool eliminarOK = false;
            Ambiente aBorrar = FindById(id);

            if (aBorrar != null)
            {
                eliminarOK = TiposAmbiente.Remove(aBorrar);
            }
            return eliminarOK;
        }
        #endregion

        public bool Update(Ambiente obj)
        {
            throw new NotImplementedException();
        }
    }
}
