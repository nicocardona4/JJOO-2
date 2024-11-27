using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcepcionesPropias
{
    public  class OperacionConflictivaException : Exception
    {
        public OperacionConflictivaException() { }
        public OperacionConflictivaException(string mensaje) : base(mensaje) { }
        public OperacionConflictivaException(string mensaje, Exception inner) : base(mensaje, inner) { }
    }
}
