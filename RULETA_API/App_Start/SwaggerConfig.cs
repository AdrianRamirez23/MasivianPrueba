using System.Web.Http;
using WebActivatorEx;
using RULETA_API;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace RULETA_API
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    // DEFINIMOS LAS CARACTER�STICAS DEL WEB API.
                    c.SingleApiVersion("v1", "API Ruleta")
                        .Description("API Ruleta Cassino")
                        .TermsOfService("T�rminos de servicio.");
                    //.Contact(x => x
                    //    .Name("Landers & Cia")
                    //    .Url("http://www.rafaelacosta.net/api-doc.pdf")
                    //    .Email("info@rafaelacosta.net"))
                    //.License(x => x
                    //    .Name("Licencia")
                    //    .Url("http://www.rafaelacosta.net/license"));


                    // HABILITAMOS EL ARCHIVO DE DOCUMENTACI�N XML.
                   // c.IncludeXmlComments(GetXmlCommentsPath());

                    // HABILITAMOS LA AUTENTICACI�N JWT.

                    c.ApiKey("Authorization")
                    .Description("Introduce el Token JWT aqu�.")
                    .Name("Bearer")
                    .In("header");

                    // If you want the output Swagger docs to be indented properly, enable the "PrettyPrint" option.
                    c.PrettyPrint();
                })
                .EnableSwaggerUi(c =>
                {
                    c.EnableApiKeySupport("Authorization", "header");

                    c.DocumentTitle("API General Landers");
                });
        }
    }
}
