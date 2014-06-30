using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.WebPages;

namespace MagentoNetMvc.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index ()
		{
			ViewBag.Message = "Welcome to ASP.NET MVC on Mono! (viewbag)";
//			ViewData ["Message"] = "Welcome to ASP.NET MVC on Mono!";
			return View ();
		}
	}
}

