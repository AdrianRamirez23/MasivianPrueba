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
    /// Controlador con los métods correspondientes a los Usuarios.
    /// </summary>
    [Authorize]
    [RoutePrefix("api/usuarios")]
    public class UsuariosController : ApiController
    {
        /// <summary>
        /// Método para realizar la creación de un usuario.
        /// </summary>
        /// <param name="user"></param>
        /// <response code="200">OK. Usuario creado correctamente.</response>     
        /// <response code="400">Bad Request. Usuario ya creado en la DB.</response>     
        /// <response code="401">No autorizado. Token o parámetros incorrectos.</response>     
        /// <returns></returns>
        [HttpPost]
        [Route("InsertarUsuario")]
        public IHttpActionResult InsertarUsuario(Usuario user)
        {
            if (user.Login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var identity = Thread.CurrentPrincipal.Identity;

            if (identity.Name == user.Login.Username)
            {
                LoginRequest usr = new LoginRequest();
                usr.Username = user.Login.Username.Trim();
                usr.Password = user.Login.Password.Trim();
                bool validacion = new Fachada().ValidarUsuario(usr);
                if (validacion)
                {                 
                        try
                        {
                            Usuario usuario = new Usuario();
                            usuario.Nombre = user.Nombre;
                            usuario.User = user.User;
                            usuario.Contrasena = user.Contrasena;
                            usuario.Nit = user.Nit;
                            usuario.Email = user.Email;
                            usuario.Estado = user.Estado;

                            new Fachada().RegistrarUsuario(usuario);
                            return Ok();
                        }
                        catch (Exception )
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
