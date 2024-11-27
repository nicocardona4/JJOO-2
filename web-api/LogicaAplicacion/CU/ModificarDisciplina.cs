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
    public class ModificarDisciplina: IModificarDisciplina
    {
        public IRepositorioDisciplinas Repo {  get; set; }

        public ModificarDisciplina(IRepositorioDisciplinas repo)
        {
            Repo = repo;
        }

        public void Modificar(AltaDisciplinaDTO dto)
        {
            Repo.Update(DisciplinaMapper.FromDTO(dto));
        }
    }
}
