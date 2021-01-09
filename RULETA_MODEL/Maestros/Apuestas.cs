using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RULETA_MODEL.Maestros
{
    public class Apuestas
    {
        public LoginRequest Login { get; set; }
        public int idRuleta { get; set; }
        public string Usser { get; set; }
        public decimal MontoApuesta { get; set; }
        public string Apuesta { get; set; }
    }
}
