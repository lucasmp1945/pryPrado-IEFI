using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemaControl.Models
{
    public class AuditoriaDTO
    {
        public DateTime Fecha { get; set; }
        public int TiempoUso { get; set; }
        public string NombreUsuario { get; set; }
    }
}
