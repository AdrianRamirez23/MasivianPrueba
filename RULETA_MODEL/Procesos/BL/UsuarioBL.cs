using RULETA_MODEL.Maestros;
using RULETA_MODEL.Procesos.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RULETA_MODEL.Procesos.BL
{
    internal class UsuarioBL
    {
        internal bool ValidarUsuario(LoginRequest usr)
        {
            return new UsuarioDAO().ValidarUsuario(usr);
        }
        public void RegistrarUsuario(Usuario user)
        {
            new UsuarioDAO().RegistrarUsuario(user);
        }
    }
}
