using System;
using System.Collections.Generic;
using System.Text;
using Dominio.Entidades;
using Dominio.InterfacesRepositorio;

namespace Datos
{
    public class RepositorioTiposMemoria : IRepositorioTipos
    {

        public List<Tipo> Tipos { get; set; } = new List<Tipo>();

        public int UltId { get; set; } = 1;

        #region Add/Remove
        public bool Add(Tipo obj)
        {
            bool ok = false;

            if (obj.Validar() && BuscarPorNombre(obj.Nombre) == null)
            {
                obj.IdTipo = UltId++;
                Tipos.Add(obj);
                ok = true;
            }
            return ok;
        }

        public bool Remove(int id)
        {
            bool ok = false;
            Tipo aBorrar = FindById(id);

            if (aBorrar != null)
            {
                ok = Tipos.Remove(aBorrar);
            }
            return ok;
        }
        public bool EliminarPorNombre(string nombre)
        {
            bool ok = false;
            Tipo aBorrar = BuscarPorNombre(nombre);

            if (aBorrar != null)
            {
                ok = Tipos.Remove(aBorrar);
            }
            return ok; ;
        }


        #endregion

        #region Update

        public bool update(Tipo obj)
        {
            bool ok = false;
            if (obj.Validar())
            {
                Tipo aModificar = FindById(obj.IdTipo);

                if (aModificar != null)
                {
                    aModificar.Nombre = obj.Nombre;
                    aModificar.Descripcion = obj.Descripcion;
                    ok = true;
                }
            }
            return ok;
        }
        public bool ActualizarDescripcion(string nombre)
        {
            bool ok = false;

            Tipo aModificar = BuscarPorNombre(nombre);

            if (aModificar != null)
            {
                //aModificar.Descripcion = "";
                ok = true;
            }
            return ok;
        }
        #endregion

        #region Buscar

        public IEnumerable<Tipo> FindAll()
        {
            return Tipos;
        }

        public Tipo FindById(int id)
        {
            Tipo buscado = null;

            int indice = 0;

            while (buscado == null && indice < Tipos.Count)
            {
                Tipo t = Tipos[indice];
                if (t.IdTipo == id) buscado = t;
                indice++;
            }

            return buscado;
        }

        public Tipo BuscarPorNombre(string nombre)
        {
            Tipo buscado = null;

            int indice = 0;

            while (buscado == null && indice < Tipos.Count)
            {
                Tipo t = Tipos[indice];
                if (t.Nombre.ToLower() == nombre.ToLower()) buscado = t;
                indice++;
            }

            return buscado;
        }
        #endregion

    }
}
