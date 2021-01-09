using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RULETA_MODEL.Maestros
{
    public class Usuario
    {
        public LoginRequest Login { get; set; }
        public string Nombre { get; set; }
        public string User { get; set; }
        public string Contrasena { get; set; }
        public string Nit { get; set; }
        public string Email { get; set; }
        public bool Estado { get; set; }
    }
}
