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
    /// login controller class for authenticate users
    /// </summary>
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        // GET: api/login/echopoing       
        /// <response code="200">OK.</response>        
        /// <response code="404">NotFound.</response>
        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }
        /// <summary>
        /// Método para crear el token JWT para un usuario en específico.
        /// </summary>
        /// <param name="login"></param>
        /// <response code="200">OK. Token creado correctamente.</response>
        /// <response code="401">No autorizado. Token o parámetros incorrectos.</response>     
        /// <returns></returns>
        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(LoginRequest login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            //TODO: Validate credentials Correctly, this code is only for demo !!
            bool isCredentialValid =  new Fachada().ValidarUsuario(login);
            if (isCredentialValid)
            {
                var token = TokenGenerator.GenerateTokenJwt(login.Username);
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
