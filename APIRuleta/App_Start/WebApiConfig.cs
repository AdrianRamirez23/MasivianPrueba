﻿using APIRuleta.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace APIRuleta
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // API services and routes configuration
            config.MapHttpAttributeRoutes();
            config.MessageHandlers.Add(new TokenValidationHandler());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
