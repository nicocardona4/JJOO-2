using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcepcionesPropias;
using LogicaNegocio.EntidadesDominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;

namespace LogicaDatos.Repositorios
{
    public class RepositorioAuditorias : IRepositorioAuditorias
    {
        public OlimpiadasContext Contexto { get; set; }

        public RepositorioAuditorias(OlimpiadasContext ctx)
        {
            Contexto = ctx;
        }
        public void Add(Auditoria obj)
        {
            if (obj != null) 
            {
                Contexto.Auditorias.Add(obj);
                Contexto.SaveChanges();
            }
        }

        public IEnumerable<Auditoria> FindAll()
        {
            throw new NotImplementedException();
        }

        public Auditoria FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Auditoria obj)
        {
            throw new NotImplementedException();
        }
    }
}
