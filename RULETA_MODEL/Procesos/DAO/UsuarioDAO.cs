using RULETA_MODEL.Maestros;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RULETA_MODEL.Procesos.DAO
{
    internal class UsuarioDAO
    {
        private string conexion = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
        private string querySQL(int opc, Usuario usr)
        {
            return String.Format(
                @"APIRuleta_UsuarioCRUD {0}, '{1}', '{2}', '{3}','{4}', '{5}', '{6}' ;",
                opc, usr.Nombre, usr.User, usr.Contrasena, usr.Nit, usr.Email, usr.Estado);
        }
        internal bool ValidarUsuario(LoginRequest usr)
        {
            bool validacion = false;
            using (SqlConnection con = new SqlConnection(conexion))
            {
                string sentencia = querySQL(1, new Usuario { User = usr.Username, Contrasena = usr.Password });
                SqlCommand cmd = new SqlCommand(sentencia, con);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    validacion = rd[0] == DBNull.Value ? false : rd.GetBoolean(0);
                }
            }
            return validacion;
        }
        internal void RegistrarUsuario(Usuario user)
        {
            using (SqlConnection con = new SqlConnection(conexion))
            {
                con.Open();
                SqlCommand sql = new SqlCommand("APIRuleta_UsuarioCRUD", con);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.Clear();
                sql.Parameters.AddWithValue("@opc", 2);
                sql.Parameters.AddWithValue("@NombreUsuario", user.Nombre);
                sql.Parameters.AddWithValue("@Usser", user.User);
                sql.Parameters.AddWithValue("@Pass", user.Contrasena);
                sql.Parameters.AddWithValue("@Nit", user.Nit);
                sql.Parameters.AddWithValue("@Email", user.Email);
                sql.Parameters.AddWithValue("@Estado", user.Estado);
                sql.ExecuteNonQuery();
            }
        }

    }
}
