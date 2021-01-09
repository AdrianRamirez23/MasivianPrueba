using Newtonsoft.Json;
using RULETA_API.Utilidades;
using RULETA_MODEL.Maestros;
using RULETA_MODEL.Procesos.FRONT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace RULETA_API.Controllers
{
    /// <summary>
    /// Controlador con los métodos correspondientes a la Ruleta.
    /// </summary>
    [Authorize]
    [RoutePrefix("api/ruleta")]
    public class RuletaController : ApiController
    {
        /// <summary>
        /// Método para realizar la creación de una Ruleta Nueva.
        /// </summary>
        /// <param name="Rull"></param>
        /// <response code="200">OK. Ruleta Creada Correctamente.</response>     
        /// <response code="400">Bad Request.</response>     
        /// <response code="401">No autorizado.</response>     
        /// <returns></returns>
        [HttpPost]
        [Route("CrearRuleta")]
        public IHttpActionResult CrearRuleta(Ruleta Rull)
        {
            if (Rull.Login.Username == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var identity = Thread.CurrentPrincipal.Identity;

            if (identity.Name == Rull.Login.Username)
            {
                LoginRequest usr = new LoginRequest();
                usr.Username = Rull.Login.Username.Trim();
                usr.Password = Rull.Login.Password.Trim();

                bool validacion = new Fachada().ValidarUsuario(usr);
                if (validacion)
                {
                        try
                        {
                            RuletaCreada rulers = new RuletaCreada();
                            rulers.idRuleta = new Fachada().CrearRuleta();
                            var json = JsonConvert.SerializeObject(rulers);
                        return Ok(json);
                        }
                        catch (Exception)
                        {
                            return BadRequest();
                        }
                }
                else
                    return BadRequest();
            }
            else
                return Unauthorized();
        }
        /// <summary>
        /// Método para realizar el Cierre de una Ruleta.
        /// </summary>
        /// <param name="Rull"></param>
        /// <response code="200">OK. Ruleta Cerrada Correctamente.</response>     
        /// <response code="400">Bad Request.</response>     
        /// <response code="401">No autorizado.</response>     
        /// <returns></returns>
        [HttpPost]
        [Route("CerrarRuleta")]
        public IHttpActionResult CerrarRuleta(Ruleta Rull)
        {
            if (Rull.Login.Username == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var identity = Thread.CurrentPrincipal.Identity;

            if (identity.Name == Rull.Login.Username)
            {
                LoginRequest usr = new LoginRequest();
                usr.Username = Rull.Login.Username.Trim();
                usr.Password = Rull.Login.Password.Trim();

                bool validacion = new Fachada().ValidarUsuario(usr);
                if (validacion)
                {
                    try
                    {
                        Rull.Resultado = new Fachada().ResultadoRuleta();
                        List<ApuestasCierre> ListApus = new Fachada().ConsultarApuestas(Rull);
                        foreach(ApuestasCierre ListApu in ListApus)
                        {
                            new Fachada().CerrarRuelta(ListApu,Rull);
                        }
                        return Ok();
                    }
                    catch (Exception e)
                    {
                        return BadRequest();
                    }
                }
                else
                    return BadRequest();
            }
            else
                return Unauthorized();
        }
        /// <summary>
        /// Método para realizar listas todas las ruletas.
        /// </summary>
        /// <param name="Login"></param>
        /// <response code="200">OK.</response>     
        /// <response code="400">Bad Request.</response>     
        /// <response code="401">No autorizado.</response>     
        /// <returns></returns>
        [HttpPost]
        [Route("ConsultarRuletas")]
        public IHttpActionResult ConsultarRuletas(LoginRequest Login)
        {
            if (Login.Username == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var identity = Thread.CurrentPrincipal.Identity;

            if (identity.Name == Login.Username)
            {
                LoginRequest usr = new LoginRequest();
                usr.Username = Login.Username.Trim();
                usr.Password = Login.Password.Trim();

                bool validacion = new Fachada().ValidarUsuario(usr);
                if (validacion)
                {
                    try
                    {
                        List<Ruletas> ruletaLis = new List<Ruletas>();
                        ruletaLis = new Fachada().ConsultarRuletas();
                        var json = JsonConvert.SerializeObject(ruletaLis);
                        var jsonString = new Utils().FormatJson(json);
                        return Ok(jsonString);
                    }
                    catch (Exception)
                    {
                        return BadRequest();
                    }
                }
                else
                    return BadRequest();
            }
            else
                return Unauthorized();
        }
    }
}
