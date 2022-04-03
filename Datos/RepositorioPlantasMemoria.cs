using System;
using System.Collections.Generic;
using System.Text;
using Dominio.Entidades;
using Dominio.InterfacesRepositorio;

namespace Datos
{
    class RepositorioPlantasMemoria : IRepositorioPlantas
    {
        public List<Planta> Plantas { get; set; } = new List<Planta>();
        public int UltId { get; set; } = 1;

        public bool Add(Planta obj)
        {
            bool okAlta = false;

            if (obj.Validar(obj) && FindById(obj.IdPlanta) == null) // ver validar
            {
                obj.IdPlanta = UltId++;
                Plantas.Add(obj);
                okAlta = true;
            }
            return okAlta;
        }

        IEnumerable<Planta> IRepositorioPlantas.BuscarPlantasMasAltas(double alturaCm)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Planta> IRepositorioPlantas.BuscarPlantasMasBajas(double alturaCm)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Planta> IRepositorioPlantas.BuscarPorAmbiente(string ambiente)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Planta> IRepositorioPlantas.BuscarPorTexto(string texto)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Planta> IRepositorioPlantas.BuscarPorTipo(string tipoPlanta)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Planta> IRepositorio<Planta>.FindAll()
        {
            throw new NotImplementedException();
        }

        public Planta FindById(int id)
        {
            throw new NotImplementedException();
        }

        bool IRepositorio<Planta>.Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool update(Planta obj)
        {
            throw new NotImplementedException();
        }
    }
}
