using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemaControl.Model
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string UsuarioNombre { get; set; }
        public string Contrasena { get; set; }
        public DateTime? UltimoLogin { get; set; }
        public int Nivel { get; set; }
        public bool Vigente { get; set; }
    }
}
