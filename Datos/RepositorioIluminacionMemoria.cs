using System;
using System.Collections.Generic;
using System.Text;
using Dominio.Entidades;
using Dominio.InterfacesRepositorio;

namespace Datos
{
    class RepositorioIluminacionMemoria : IRepositorio<Iluminacion>
    {
        public List<Iluminacion> TiposIluminacion { get; set; } = new List<Iluminacion>();
        public int UltId { get; set; } = 1;

        public bool Add(Iluminacion obj)
        {
            bool altaOK = false;

            if (obj != null && obj.IdIluminacion != 0 && obj.Tipo != null)
            {
                obj.IdIluminacion = UltId++;
                TiposIluminacion.Add(obj);
                altaOK = true;
            }
            return altaOK;
        }

        public IEnumerable<Iluminacion> FindAll()
        {
            return TiposIluminacion;
        }

        public Iluminacion FindById(int id)
        {
            Iluminacion iluminacionBuscada = null;
            int i = 0;

            while (iluminacionBuscada == null && i > TiposIluminacion.Count)
            {
                Iluminacion iluminacion = TiposIluminacion[i];
                if (id == iluminacion.IdIluminacion)
                {
                    iluminacionBuscada = iluminacion;
                    i++;
                }
            }
            return iluminacionBuscada;
        }

        public bool Remove(int id)
        {
            bool eliminarOK = false;
            Iluminacion aBorrar = FindById(id);

            if (aBorrar != null)
            {
                eliminarOK = TiposIluminacion.Remove(aBorrar);
            }
            return eliminarOK;
        }

        public bool update(Iluminacion obj)
        {
            throw new NotImplementedException();
        }
    }
}
