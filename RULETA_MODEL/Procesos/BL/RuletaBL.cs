using RULETA_MODEL.Maestros;
using RULETA_MODEL.Procesos.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RULETA_MODEL.Procesos.BL
{
    internal class RuletaBL
    {
        internal int CrearRuleta()
        {
            return new RuletaDAO().CrearRuelta();
        }
        internal void CrearApuesta(Apuestas Apuest)
        {
            new RuletaDAO().CrearApuesta(Apuest);
        }
        internal int ResultadoRuleta()
        {
            return new RuletaDAO().ResultadoRuleta();
        }
        internal void CerrarRuleta(ApuestasCierre Apuest, Ruleta Rull)
        {
            new RuletaDAO().CerrarRuleta(Apuest, Rull);
        }
        internal List<ApuestasCierre> ConsultarApuestas(Ruleta idRuleta)
        {
            return new RuletaDAO().ConsultarApuestas(idRuleta);
        }
        internal List<Ruletas> ConsultarRuletas()
        {
            return new RuletaDAO().ConsultarRuletas();
        }
    }
}
