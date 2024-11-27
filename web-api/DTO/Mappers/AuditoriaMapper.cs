using LogicaNegocio.EntidadesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Mappers
{
    public class AuditoriaMapper
    {
        public static Auditoria FromDTO(AltaAuditoriaDTO dto)
        {
            Auditoria a = new Auditoria(dto.Id,dto.Operacion, dto.Entidad,dto.EntidadId,dto.Email,dto.Fecha);
            
            return a;
        }
    }
}
