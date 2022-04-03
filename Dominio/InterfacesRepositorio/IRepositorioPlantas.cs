using System;
using System.Collections.Generic;
using System.Text;
using Dominio.Entidades;

namespace Dominio.InterfacesRepositorio
{
    public interface IRepositorioPlantas : IRepositorio <Planta>
    {
        IEnumerable<Planta> BuscarPorTexto(string texto);

        IEnumerable<Planta> BuscarPorTipo(string tipoPlanta);

        IEnumerable<Planta> BuscarPorAmbiente(string ambiente);

        IEnumerable<Planta> BuscarPlantasMasBajas(double alturaCm);

        IEnumerable<Planta> BuscarPlantasMasAltas(double alturaCm);

    }
}
