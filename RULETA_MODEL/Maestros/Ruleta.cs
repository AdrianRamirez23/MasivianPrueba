using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RULETA_MODEL.Maestros
{
    public class Ruleta
    {
        public LoginRequest Login { get; set; }
        public int idRuleta { get; set; }      
        public int Resultado { get; set; }
        public bool EstadoRuleta { get; set; }
    }
}
