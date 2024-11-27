using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCU
{
    public interface IBuscarEventos
    {
        IEnumerable<ListadoEventosDTO> Buscar(int id, string fechaInicio, string fechaFin, string nombreEvento, decimal minPje, decimal maxPje);
    }
}
