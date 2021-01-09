using RULETA_MODEL.Maestros;
using RULETA_MODEL.Procesos.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RULETA_MODEL.Procesos.FRONT
{
    public class Fachada
    {
        public bool ValidarUsuario(LoginRequest usr)
        {
            return new UsuarioBL().ValidarUsuario(usr);
        }
        public void RegistrarUsuario(Usuario user)
        {
            new UsuarioBL().RegistrarUsuario(user);
        }
        public int CrearRuleta()
        {
            return new RuletaBL().CrearRuleta();
        }
        public void CrearApuesta(Apuestas Apuest)
        {
            new RuletaBL().CrearApuesta(Apuest);
        }
        public int ResultadoRuleta()
        {
            return new RuletaBL().ResultadoRuleta();
        }
        public void CerrarRuelta(ApuestasCierre Apuest, Ruleta rull)
        {
            new RuletaBL().CerrarRuleta(Apuest, rull);
        }
        public List<ApuestasCierre> ConsultarApuestas(Ruleta idRuleta)
        {
            return new RuletaBL().ConsultarApuestas(idRuleta);
        }
        public List<Ruletas> ConsultarRuletas()
        {
            return new RuletaBL().ConsultarRuletas();
        }
    }
}
