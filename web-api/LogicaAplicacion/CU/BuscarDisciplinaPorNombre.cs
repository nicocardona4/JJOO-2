using DTO;
using DTO.Mappers;
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
    public class BuscarDisciplinaPorNombre: IBuscarDisciplinaPorNombre
    {
        public IRepositorioDisciplinas Repo { get; set; }

        public BuscarDisciplinaPorNombre(IRepositorioDisciplinas repo)
        {
            Repo = repo;
        }

        public ListadoDisciplinasDTO Buscar(string nombre) 
        {
            Disciplina d = Repo.BuscarPorNombre(nombre);
            if (d != null)
            {
                return DisciplinaMapper.FromDisciplina(d);
            }
            else
            {
                return null;
            }
        }
    }
}
