using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesDominio
{
    public class Auditoria
    {
        public int Id { get; set; }
        public string Operacion { get; set; }
        public string Entidad { get; set; }
        public int EntidadId { get; set; }

        public string Email { get; set; }
        public DateTime FechaOperacion { get; set; }

        public Auditoria (int id, string operacion, string entidad, int entidadId, string email, DateTime fechaOperacion)
        {
            Id = id;
            Operacion = operacion;
            Entidad = entidad;
            EntidadId = entidadId;
            Email = email;
            FechaOperacion = fechaOperacion;
        }
    }
}
