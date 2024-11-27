using LogicaAplicacion.InterfacesCU;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CU
{
    public class BajaDisciplina: IBajaDisciplina
    {
        public IRepositorioDisciplinas Repo {  get; set; }

        public BajaDisciplina(IRepositorioDisciplinas repo)
        {
            Repo = repo;
        }

        public void Borrar(int id)
        {
            Repo.Remove(id);
        }
    }
}
