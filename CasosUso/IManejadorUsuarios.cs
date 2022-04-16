using System;
using System.Collections.Generic;
using System.Text;
using Dominio.Entidades;

namespace CasosUso
{
    public interface IManejadorUsuarios
    {
        Usuario UsuarioALoguear(Usuario u);
       /* bool AgregarNuevoUsuario(Usuario u);
        bool DarDeBajaUsuario(int id);
        IEnumerable<Usuario> TraerTodos();      
        Usuario buscarPorId(int id);*/
    }
}
