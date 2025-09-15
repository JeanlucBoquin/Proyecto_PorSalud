using Proyecto_PorSalud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Proyecto_PorSalud
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var db = new AppDbContext();
            var clienteService = new Services.ClienteService(db);
            Application["ClienteService"] = clienteService;
        }
    }
}
