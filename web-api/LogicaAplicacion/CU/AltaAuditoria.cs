using DTO;
using DTO.Mappers;
using LogicaAplicacion.InterfacesCU;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CU
{
    public class AltaAuditoria : IAltaAuditoria
    {
        public IRepositorioAuditorias Repo {  get; set; }

        public AltaAuditoria (IRepositorioAuditorias repo)
        {
            Repo = repo;
        }

        public void Alta(AltaAuditoriaDTO dto)
        {
             Repo.Add(AuditoriaMapper.FromDTO(dto));
        }
    }
}
