using DTO.Mappers;
using DTO;
using LogicaAplicacion.InterfacesCU;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.EntidadesDominio;
using ExcepcionesPropias;

namespace LogicaAplicacion.CU
{
    public class BuscarDisciplinaPorId: IBuscarDisciplinaPorId
    {
        public IRepositorioDisciplinas Repo { get; set; }

        public BuscarDisciplinaPorId(IRepositorioDisciplinas repo)
        {
            Repo = repo;
        }

        public ListadoDisciplinasDTO Buscar(int id)
        {
            Disciplina d = Repo.FindById(id);
            if (d == null) throw new DisciplinaInvalidaException("No se encontró disciplina con ese id");
            var result = DisciplinaMapper.FromDisciplina(d);
            return result;
        }
    }
}
