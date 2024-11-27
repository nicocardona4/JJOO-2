using LogicaNegocio.EntidadesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioEventos: IRepositorio<Evento>
    {
        public Evento BuscarPorNombre(string nombre);

        public IEnumerable<Evento> BuscarPorFecha(DateTime fecha);

        public IEnumerable<Evento> BuscarPorFiltro(int id, DateTime fechaInicio, DateTime fechaFin, string nombre, decimal minPje, decimal maxPje);
    }
}
