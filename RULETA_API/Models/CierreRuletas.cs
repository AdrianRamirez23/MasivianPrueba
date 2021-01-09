using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RULETA_API.Models
{
    public class CierreRuletas
    {
        public int idRuleta { get; set; }
        public int Resultado { get; set; }
        public decimal MotoApostado { get; set; }
        public string Usser { get; set; }
        public string Apuesta { get; set; }
        public string Gano { get; set; }
        public decimal ValorFina { get; set; }
    }
}