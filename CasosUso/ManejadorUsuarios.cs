using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using Datos;
using Dominio.InterfacesRepositorio;

namespace CasosUso
{
    public class ManejadorUsuarios : IManejadorUsuarios
    {
        public IRepositorioUsuarios RepoUsuarios { get; set; }

        public ManejadorUsuarios(IRepositorioUsuarios repo)
        {
            RepoUsuarios = repo;
        }

        public Usuario UsuarioALoguear(Usuario u)
        {
            return RepoUsuarios.BuscarUsuarioLogin(u);
        }
        /*
        public bool AgregarNuevoUsuario(Usuario u)
        {
            return RepoUsuarios.Add(u);
        }

        public bool DarDeBajaUsuario(int id)
        {
            return RepoUsuarios.Remove(id);
        }

        public IEnumerable<Usuario> TraerTodos()
        {
            return RepoUsuarios.FindAll();
        }      

        public Usuario buscarPorId(int id)
        {
            return RepoUsuarios.FindById(id);
        }*/
    }
}
