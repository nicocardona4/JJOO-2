using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AltaDisciplinaDTO
    {
        public string Nombre { get; set; }
        public int Codigo { get; set; }

        [Display(Name = "Año de ingreso a los JJOO")]
        public int Anio {  get; set; }
        
        public int Id {  get; set; }
    }
}
