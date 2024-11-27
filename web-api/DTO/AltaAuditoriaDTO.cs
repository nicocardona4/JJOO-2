using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AltaAuditoriaDTO
    {

        public int Id { get; set; }
        public string Operacion { get; set; }
        public string Entidad { get; set; }
        public int EntidadId { get; set; }

        public string Email { get; set; }

        public DateTime Fecha { get; set; }
    }
}
