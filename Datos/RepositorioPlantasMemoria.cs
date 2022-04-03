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

            if (obj.Validar(obj) && FindById(obj.IdPlanta) == null)
            {
                obj.IdPlanta = UltId++;
                Plantas.Add(obj);
                okAlta = true;
            }
            return okAlta;
        }

        public IEnumerable<Planta> FindAll()
        {
            return Plantas;
        }

        public Planta FindById(int id)
        {
            Planta buscada = null;
            int i = 0;

            while (buscada == null && i > Plantas.Count)
            {
                Planta planta = Plantas[i];
                if (planta.IdPlanta == id)
                {
                    buscada = planta;
                    i++;
                }
            }
            return buscada;
        }

        public bool Remove(int id)
        {
            bool eliminadoOK = false;
            Planta aBorrar = FindById(id);

            if (aBorrar != null)
            {
                eliminadoOK = Plantas.Remove(aBorrar);
            }
            return eliminadoOK;
        }

        public bool update(Planta obj)
        {
            bool modificadoOK = false;
            if (obj.Validar(obj))
            {
                Planta aModificar = FindById(obj.IdPlanta);
                if (aModificar != null)
                {
                    aModificar.TipoPlanta = obj.TipoPlanta;
                    aModificar.NombreCientifico = obj.NombreCientifico;
                    aModificar.NombreVulgar = obj.NombreVulgar;
                    aModificar.Descripcion = obj.Descripcion;
                    aModificar.Ambiente = obj.Ambiente;
                    aModificar.AlturaMaxima = obj.AlturaMaxima;
                    aModificar.Foto = obj.Foto;
                    aModificar.FrecuenciaRiego = obj.FrecuenciaRiego;
                    aModificar.TipoIluminacion = obj.TipoIluminacion;
                    aModificar.TemperaturaMantenimiento = obj.TemperaturaMantenimiento;

                    modificadoOK = true;
                }
            }
            return modificadoOK;
        }

        public IEnumerable<Planta> BuscarPlantasMasAltas(double alturaCm)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Planta> BuscarPlantasMasBajas(double alturaCm)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Planta> BuscarPorAmbiente(string ambiente)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Planta> BuscarPorTexto(string texto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Planta> BuscarPorTipo(string tipoPlanta)
        {
            throw new NotImplementedException();
        }
    }
}
