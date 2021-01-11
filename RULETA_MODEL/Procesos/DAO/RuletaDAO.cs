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
    internal class RuletaDAO
    {
        private string conexion = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
        private string querySQL(int opc, Ruleta rull)
        {
            return String.Format(
                @"APIRuleta_RuletaCRUD {0}, '{1}', '{2}', '{3}'  ;",
                opc, rull.idRuleta,  rull.Resultado, rull.EstadoRuleta);
        }
        private string querySQL1(int opc, Apuestas apu)
        {
            return String.Format(
                @"APIRuleta_ApuestasCRUD {0}, '{1}', '{2}', '{3}','{4}'  ;",
                opc, apu.idRuleta, apu.Usser, apu.MontoApuesta,apu.Apuesta);
        }
        internal int CrearRuelta()
        {
            int idRuleta = 0;
            using (SqlConnection con = new SqlConnection(conexion))
            {
                string sentencia = querySQL(1, new Ruleta { } );
                SqlCommand cmd = new SqlCommand(sentencia, con);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    idRuleta = rd[0] == DBNull.Value ? 0 : rd.GetInt32(0);
                }
            }
            return idRuleta;
        }
        internal void CrearApuesta(Apuestas Apuest)
        {
            using (SqlConnection con = new SqlConnection(conexion))
            {
                con.Open();
                SqlCommand sql = new SqlCommand("APIRuleta_ApuestasCRUD", con);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.Clear();
                sql.Parameters.AddWithValue("@opc", 1);
                sql.Parameters.AddWithValue("@idRuleta", Apuest.idRuleta);
                sql.Parameters.AddWithValue("@Usser", Apuest.Usser);
                sql.Parameters.AddWithValue("@ValorApuesta", Apuest.MontoApuesta);
                sql.Parameters.AddWithValue("@Apuesta", Apuest.Apuesta);
                sql.ExecuteNonQuery();
            }
        }
        internal int ResultadoRuleta()
        {
            int Resultado = 0;
            using (SqlConnection con = new SqlConnection(conexion))
            {
                string sentencia = querySQL(2, new Ruleta { });
                SqlCommand cmd = new SqlCommand(sentencia, con);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    Resultado = rd[0] == DBNull.Value ? 0 : rd.GetInt32(0);
                }
            }
            return Resultado;
        }
        internal void CerrarRuleta(ApuestasCierre Apuest, Ruleta rull)
        {
            using (SqlConnection con = new SqlConnection(conexion))
            {
                con.Open();
                SqlCommand sql = new SqlCommand("APIRuleta_CierreCRUD", con);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.Clear();
                sql.Parameters.AddWithValue("@Resultado", rull.Resultado);
                sql.Parameters.AddWithValue("@idRuleta", rull.idRuleta);
                sql.Parameters.AddWithValue("@MontoApostado", Apuest.MontoApuesta);
                sql.Parameters.AddWithValue("@Apuesta", Apuest.Apuesta);
                sql.Parameters.AddWithValue("@Usser", Apuest.Usser);
                sql.ExecuteNonQuery();
            }
        }
        internal List<ApuestasCierre> ConsultarApuestas(Ruleta idRuleta)
        {
            List<ApuestasCierre> ListApu = new List<ApuestasCierre>();
            using (SqlConnection con = new SqlConnection(conexion))
            {
                string sentencia = querySQL1(2, new Apuestas {idRuleta= idRuleta.idRuleta });
                SqlCommand cmd = new SqlCommand(sentencia, con);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    ApuestasCierre apuest = new ApuestasCierre();
                    apuest.MontoApuesta =  rd.GetDecimal(0);
                    apuest.Apuesta =  rd.GetString(1);
                    apuest.Usser =  rd.GetString(2);
                    ListApu.Add(apuest);
                }
            }
            return ListApu;
        }
        internal List<Ruletas> ConsultarRuletas()
        {
            List<Ruletas> ListRull = new List<Ruletas>();
            using (SqlConnection con = new SqlConnection(conexion))
            {
                string sentencia = querySQL(3, new Ruleta { });
                SqlCommand cmd = new SqlCommand(sentencia, con);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    Ruletas rull = new Ruletas();
                    rull.idRuleta = rd.GetInt32(0);
                    rull.Resultado = rd.GetInt32(1);
                    rull.EstadoRuleta = rd.GetBoolean(2);
                    rull.Cierres = new RuletaDAO().ConsultarCierres(rull.idRuleta);
                    ListRull.Add(rull);
                }
            }
            return ListRull;
        }
        internal List<CierreRuletas> ConsultarCierres(int idRuleta)
        {
            List<CierreRuletas> ListCierr = new List<CierreRuletas>();
            using (SqlConnection con = new SqlConnection(conexion))
            {
                string sentencia = querySQL(4, new Ruleta { idRuleta = idRuleta });
                SqlCommand cmd = new SqlCommand(sentencia, con);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    CierreRuletas cierr = new CierreRuletas();
                    cierr.idRuleta = rd.GetInt32(0);
                    cierr.Resultado = rd.GetInt32(1);
                    cierr.MotoApostado = rd.GetDecimal(2);
                    cierr.Usser = rd.GetString(3);
                    cierr.Apuesta = rd.GetString(4);
                    cierr.Gano = rd.GetString(5);
                    cierr.ValorFina = rd.GetDecimal(6);
                   
                    ListCierr.Add(cierr);
                }
            }
            return ListCierr;
        }
    }
}
