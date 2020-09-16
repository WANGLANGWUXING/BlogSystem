using BlogSystem.WebApp.App_Start;
using System.Web.Mvc;
using System.Web.Routing;

namespace BlogSystem.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutofacConfig.Register();
            log4net
                .Config
                .XmlConfigurator
                .Configure(
                new System.IO.FileInfo(
                    Server.MapPath("~/Web.config")));
        }
    }
}
