using Newtonsoft.Json;
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
    /// Controlador con los métodos correspondientes a las Apuestas.
    /// </summary>
    [Authorize]
    [RoutePrefix("api/apuestas")]
    public class ApuestaController : ApiController
    {
        /// <summary>
        /// Método para realizar la creación de una Apeusta Nueva.
        /// </summary>
        /// <param name="Apuest"></param>
        /// <response code="200">OK. Apuesta Creada.</response>     
        /// <response code="400">Bad Request.</response>     
        /// <response code="401">No autorizado.</response>     
        /// <returns></returns>
        [HttpPost]
        [Route("CrearApuesta")]
        public IHttpActionResult CrearRuleta(Apuestas Apuest)
        {
            if (Apuest.Login.Username == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var identity = Thread.CurrentPrincipal.Identity;

            if (identity.Name == Apuest.Login.Username)
            {
                LoginRequest usr = new LoginRequest();
                usr.Username = Apuest.Login.Username.Trim();
                usr.Password = Apuest.Login.Password.Trim();

                bool validacion = new Fachada().ValidarUsuario(usr);
                if (validacion)
                {
                    try
                    {
                        if(Apuest.MontoApuesta<=10000 && Apuest.MontoApuesta > 0 )
                        {
                            if(Apuest.Apuesta=="Negro" || Apuest.Apuesta=="Rojo")
                            {
                                new Fachada().CrearApuesta(Apuest);
                                return Ok();
                            }
                            else if (Convert.ToInt32(Apuest.Apuesta)>0 && Convert.ToInt32(Apuest.Apuesta) <= 36){
                                new Fachada().CrearApuesta(Apuest);
                                return Ok();
                            }
                            return BadRequest();
                        }
                        return BadRequest();
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
