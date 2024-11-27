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
    public class BuscarAtletasPorDisciplina : IBuscarAtletasPorDisciplina
    {
        public IRepositorioDisciplinas Repo;
        public BuscarAtletasPorDisciplina(IRepositorioDisciplinas repo)
        {
            Repo = repo;
        }
        public IEnumerable<ListadoAtletasDTO> BuscarAtletas(int id)
        {
            IEnumerable<ListadoAtletasDTO> atletas = AtletaMapper.FromAtletas(Repo.BuscarAtletasPorDisciplina(id));
            return atletas;
        }


    }
}
