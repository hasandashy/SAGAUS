using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SGA
{
    public class Global : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Home", "CMCChart/{id}", new
            {
                controller = "Home",
                action = "Index",
                id = UrlParameter.Optional
            });
            routes.MapRoute("Logout", "Logout", new
            {
                controller = "Home",
                action = "Logout",
                id = UrlParameter.Optional
            });
            routes.MapRoute("TnaResult", "SSAChart/{id}", new
            {
                controller = "Home",
                action = "SSAResult",
                id = UrlParameter.Optional
            });
            routes.MapRoute("BAResult", "BAChart/{id}", new
            {
                controller = "Home",
                action = "BAResult",
                id = UrlParameter.Optional
            });
            routes.MapRoute("NPResult", "NPChart/{id}", new
            {
                controller = "Home",
                action = "NPResult",
                id = UrlParameter.Optional
            });
            routes.MapRoute("MPResult", "MPChart/{id}", new
            {
                controller = "Home",
                action = "MPResult",
                id = UrlParameter.Optional
            });
            routes.MapRoute("CMAResult", "CMAChart/{id}", new
            {
                controller = "Home",
                action = "CMAResult",
                id = UrlParameter.Optional
            });
            routes.MapRoute("CompareResult", "manager/CompareUserResult", new
            {
                controller = "Home",
                action = "CompareResult",
                id = UrlParameter.Optional
            });
            routes.MapRoute("ReviewResult", "manager/ReviewUserResult", new
            {
                controller = "Home",
                action = "ReviewResult",
                id = UrlParameter.Optional
            });
            routes.MapRoute("GetTopicsByTest", "manager/GetTopicsByTest", new
            {
                controller = "Home",
                action = "GetTopicsByTest",
                id = UrlParameter.Optional
            });
        }

        protected void Application_Start(object sender, System.EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            Global.RegisterGlobalFilters(GlobalFilters.Filters);
            Global.RegisterRoutes(RouteTable.Routes);
        }

        private void Application_End(object sender, System.EventArgs e)
        {
        }

        private void Application_Error(object sender, System.EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, System.EventArgs e)
        {
        }

        private void Session_Start(object sender, System.EventArgs e)
        {
        }

        private void Session_End(object sender, System.EventArgs e)
        {
        }
    }
}
