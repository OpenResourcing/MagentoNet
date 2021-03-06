﻿
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MagentoNetMvc
{
	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterRoutes (RouteCollection routes)
		{
			routes.IgnoreRoute ("{resource}.axd/{*pathInfo}");

/*			routes.MapRoute (
				"CatItem",
                "CatItem/{action}/{id}",
                new { controller = "CatItem", action = "Index",  }
			);
*/       

            routes.MapRoute (
                "List",
                "{controller}",
                new { action = "List" }
            );
            routes.MapRoute (
                "Index",
                "{controller}/{id}",
                new { action = "Index" }
            );
			routes.MapRoute (
				"Default",
				"{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = "" }
			);

		}

		public static void RegisterGlobalFilters (GlobalFilterCollection filters)
		{
			filters.Add (new HandleErrorAttribute ());
		}

		protected void Application_Start ()
		{
			AreaRegistration.RegisterAllAreas ();
			RegisterGlobalFilters (GlobalFilters.Filters);
			RegisterRoutes (RouteTable.Routes);
		}
	}
}
