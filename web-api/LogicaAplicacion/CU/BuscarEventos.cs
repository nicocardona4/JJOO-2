using DTO;
using DTO.Mappers;
using ExcepcionesPropias;
using LogicaAplicacion.InterfacesCU;
using LogicaNegocio.EntidadesDominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CU
{
    public class BuscarEventos: IBuscarEventos
    {
        public IRepositorioEventos Repo { get; set; }

        public BuscarEventos(IRepositorioEventos repo)
        {
            Repo = repo;
        }

        public IEnumerable<ListadoEventosDTO> Buscar(int id, string fechaInicio, string fechaFin, string nombreEvento, decimal minPje, decimal maxPje)
        {
            DateTime fechaInicial = DateTime.MinValue;
            DateTime fechaFinal = DateTime.MinValue;


            if (!string.IsNullOrEmpty(fechaInicio))
            {
                if (!DateTime.TryParse(fechaInicio, out DateTime fechaParseadaInicial))
                {
                    throw new EventoInvalidoException("La fecha inicial no tiene un formato válido.");
                }
                fechaInicial = fechaParseadaInicial;

            }

            if (!string.IsNullOrEmpty(fechaFin))
            {
                if (!DateTime.TryParse(fechaFin, out DateTime fechaParseadaFin))
                {
                    throw new EventoInvalidoException("La fecha final no tiene un formato válido.");
                }
                fechaFinal = fechaParseadaFin;
            }
            if(fechaFinal != DateTime.MinValue) fechaFinal = fechaFinal.AddHours(24);
            IEnumerable<Evento> eventos = Repo.BuscarPorFiltro(id, fechaInicial, fechaFinal, nombreEvento, minPje, maxPje);
            return EventoMapper.FromEventos(eventos);
        }


    }
}
