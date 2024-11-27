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
    public class RepositorioDisciplinasBD : IRepositorioDisciplinas
    {
        public OlimpiadasContext Contexto { get; set; }

        public RepositorioDisciplinasBD(OlimpiadasContext ctx)
        {
            Contexto = ctx;
        }

        public void Add(Disciplina obj)
        {
            if (obj != null)
            {
                obj.Validar();
                Disciplina dis = BuscarPorNombre(obj.NombreDisciplina.Valor);
                if (dis != null) 
                {
                    throw new OperacionConflictivaException("Ya existe una disciplina con ese nombre");
                }
                dis = BuscarPorCodigo(obj.Codigo.Valor);
                if (dis != null)
                {
                    throw new OperacionConflictivaException("Ya existe una disciplina con ese codigo");
                }

                Contexto.Disciplinas.Add(obj);
                    Contexto.SaveChanges();
                }
            else
            {
            throw new Exception("Error al enviar datos de disciplina");
            }
        }

        public IEnumerable<Disciplina> FindAll()
        {
            return Contexto.Disciplinas.Include(d => d._atletas)
                .OrderBy(d => d.NombreDisciplina.Valor).ToList();
        }

        public Disciplina FindById(int id)
        {
            return Contexto.Disciplinas.Where(d => d.Id == id).Include(d=>d._atletas).SingleOrDefault();
        }
        public Disciplina AtletasPorDisciplina(int IdDisciplina)
        {
            var resultado = Contexto.Disciplinas.Where(d => d.Id == IdDisciplina).Include(d => d._atletas).SingleOrDefault();
            return resultado;
        }

        public void Remove(int id)
        {
            Disciplina buscado = Contexto.Disciplinas.Where(d => d.Id == id).Include(d=> d._atletas).SingleOrDefault();
            if(buscado != null)
            {
                if (buscado._atletas.Count() == 0)
                {
                    Contexto.Disciplinas.Remove(buscado);
                    Contexto.SaveChanges();
                }
                else
                {
                    throw new OperacionConflictivaException("La disciplina tiene atletas registrados");
                }
            }
            else
            {
                throw new DisciplinaInvalidaException("La disciplina con el id " + id + " no existe");
            }

        }

        public void Update(Disciplina obj)
        {
            if (obj == null)
                throw new Exception("No se pudo realizar la modificación");

            obj.Validar();

            Disciplina d = FindById(obj.Id);

            if (d == null)
                throw new DisciplinaInvalidaException("No existe disciplina con ese id");

            d.Anio = obj.Anio;

            Disciplina aux = BuscarPorNombre(obj.NombreDisciplina.Valor);
            if (aux == null || (aux != null && aux.Id == obj.Id))
            {
                d.NombreDisciplina = obj.NombreDisciplina;
            }
            else
            {
                throw new OperacionConflictivaException("Ya existe una disciplina con ese nombre");
            }

            aux = BuscarPorCodigo(obj.Codigo.Valor);
            if (aux == null || (aux != null && aux.Id == obj.Id))
            {
                d.Codigo = obj.Codigo;
            }
            else
            {
                throw new OperacionConflictivaException("Ya existe una disciplina con ese código");
            }

            Contexto.SaveChanges();
        }


        public Disciplina BuscarPorNombre(string nombre)
        {
            return Contexto.Disciplinas.Where(d => d.NombreDisciplina.Valor == nombre).Include(d => d.NombreDisciplina).Include(d => d._atletas).SingleOrDefault(); 
        }

        public Disciplina BuscarPorCodigo(int codigo)
        {
            return Contexto.Disciplinas.Where(d => d.Codigo.Valor == codigo).SingleOrDefault();
        }

        public IEnumerable<Atleta> BuscarAtletasPorDisciplina(int id)
        {
            return Contexto.Disciplinas.Where(d => d.Id == id).Include(d => d._atletas)
                    .SelectMany(d => d._atletas).Include(a => a.Pais).OrderBy(a => a.Nombre).ThenBy(a => a.Apellido);
        }
    }
}
