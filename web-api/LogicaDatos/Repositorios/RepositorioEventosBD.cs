using ExcepcionesPropias;
using LogicaNegocio.EntidadesDominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDatos.Repositorios
{
    public class RepositorioEventosBD : IRepositorioEventos
    {
        public OlimpiadasContext Contexto { get; set; }

        public RepositorioEventosBD(OlimpiadasContext ctx)
        {
            Contexto = ctx;
        }

        public void Add(Evento obj)
        {
            if (obj != null)
            {
                obj.Validar();
                Contexto.Eventos.Add(obj);
                Contexto.SaveChanges();
            }
            else
            {
                throw new Exception("No se pudo completar el alta");
            }
        }

        public IEnumerable<Evento> FindAll()
        {
            return Contexto.Eventos.Include(e => e.Atletas).ToList();
                //.Include(e => e.Participaciones)
        }

        public Evento FindById(int id)
        {
            return Contexto.Eventos.Find(id);
        }

        public void Remove(int id)
        {
            Evento buscado = Contexto.Eventos.Find(id);
            if (buscado != null)
            {
                Contexto.Eventos.Remove(buscado);
                Contexto.SaveChanges();
            }
            else
            {
                throw new EventoInvalidoException("El evento con el id " + id + " no existe");
            }
        }

        public void Update(Evento obj)
        {
            if (obj != null)
            {
                obj.Validar();
                Contexto.Eventos.Update(obj);
                Contexto.SaveChanges();
            }
            else
            {
                //ERROR
            }
        }
        public Evento BuscarPorNombre(string nombre)
        {
            return Contexto.Eventos.Where(e => e.NombrePrueba == nombre).Include(e=>e.Disciplina.NombreDisciplina).SingleOrDefault();
        }

        public IEnumerable<Evento> BuscarPorFecha(DateTime fecha)
        {
            return Contexto.Eventos.Where(e=> e.FechaInicial < fecha).Where(e=> e.FechaFinal > fecha).ToList();
        }

        public IEnumerable<Evento> BuscarPorFiltro(int id, DateTime fechaInicio, DateTime fechaFin, string nombreEvento, decimal minPje, decimal maxPje)
        {
            var query = Contexto.Eventos.AsQueryable(); // Lo paso a queryable para las validaciones
            if (fechaInicio != DateTime.MinValue)
            {
                query = query.Where(e => e.FechaInicial >= fechaInicio);

            }

            if (fechaFin != DateTime.MinValue)
            {
                query = query.Where(e => e.FechaFinal <= fechaFin);
            }

            if (id > 0)
            {
                query = query.Where(e => e.DisciplinaId == id);
            }
                
            if (!string.IsNullOrEmpty(nombreEvento))
            {
                query = query.Where(e => e.NombrePrueba.Contains(nombreEvento));
            }

            if (minPje > 0)
            {
                query = query.Where(e => e.Participaciones.Any(p => p.Puntuacion >= minPje));
            }
            if (maxPje > 0)
            {
                query = query.Where(e => e.Participaciones.Any(p => p.Puntuacion <= maxPje));
            }

            return query.Include(e=>e.Atletas).Include(e => e.Disciplina).Include(e => e.Participaciones).ToList(); 
        }



    }
}
