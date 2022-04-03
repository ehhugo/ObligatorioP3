﻿using System;
using System.Collections.Generic;
using System.Text;
using Dominio.Entidades;

namespace CasosUso
{
    public interface IManejadorTipos
    {
        bool AgregarNuevoTipo(Tipo t);
        bool DarDeBajaTipo(string  nombre);
        IEnumerable<Tipo> TraerTodos();
        Tipo BuscarTipoPorNombre(string nombre);
        bool ActualizarDescripcionTipo(string nombre);

        //RESTO DE CASOS DE USO (FUNCIONALIDADES) DE TIPOS
    }
}
