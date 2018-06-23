using System.Web.Mvc;
using System.Web.Routing;

namespace WebWallet.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Cabinet", action = "Index", id = UrlParameter.Optional },
                new[] { "WebWallet.WebUI.Controllers" }
            );
        }
    }
}
