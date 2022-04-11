using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using Datos;
using Dominio.InterfacesRepositorio;

namespace CasosUso
{
    public class ManejadorTipos : IManejadorTipos
    {
        public IRepositorioTipos RepoTipos { get; set; }

        public ManejadorTipos(IRepositorioTipos repo)
        {
            RepoTipos = repo;
        }


        public bool AgregarNuevoTipo(Tipo t)
        {
            return RepoTipos.Add(t);
        }     

        public IEnumerable<Tipo> TraerTodos()
        {
            return RepoTipos.FindAll();
        }

        public bool DarDeBajaTipo(string nombre)
        {
            return RepoTipos.EliminarPorNombre(nombre);
        }

        public Tipo BuscarTipoPorNombre(string nombre)
        {
            return RepoTipos.BuscarPorNombre(nombre);
        }

        public bool ActualizarDescripcionTipo(Tipo tipo)
        {
            return RepoTipos.ActualizarDescripcion(tipo);
        }

        public Tipo buscarPorId(int id)
        {
            return RepoTipos.FindById(id);
        }
    }
}
