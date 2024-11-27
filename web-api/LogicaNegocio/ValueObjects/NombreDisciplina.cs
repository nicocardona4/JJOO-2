using ExcepcionesPropias;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.ValueObjects
{
    [Owned]
    public class NombreDisciplina : IEquatable<NombreDisciplina>
    {
        public string Valor { get; set; }
        public NombreDisciplina(string valor) 
        {
            Valor = valor;
        }

        protected NombreDisciplina() 
        {
        }

       public void Validar()
        {
            if (Valor.Length < 10 || Valor.Length > 50) throw new DisciplinaInvalidaException("El nombre de la disciplina debe tener entre 10 y 50 caracteres");
        }
        public bool Equals(NombreDisciplina? obj)
        {
            if (obj == null)
                return false;

            NombreDisciplina other = obj;
            return Valor == other.Valor;
        }

        public override int GetHashCode()
        {
            return Valor.GetHashCode();
        }

    }
}
