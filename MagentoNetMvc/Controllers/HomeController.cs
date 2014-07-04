using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.WebPages;
using MagentoNetMvc.MagentoNetCategoryClient;
using System.ServiceModel;
//using tempuri.org;

namespace MagentoNetMvc.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index ()
		{
/*			ViewBag.Message = "Welcome to ASP.NET MVC on Mono! (viewbag)";
//			ViewData ["Message"] = "Welcome to ASP.NET MVC on Mono!";

			var binding = new BasicHttpBinding ();
			var address = new EndpointAddress( "http://192.168.1.109:8001/MagentoNetCategory.svc");

			ICategoryContract client = new CategoryContractClient (binding, address);
			IEnumerable<GetCatItemResult> cats = client.GetCats().ToList();
*/		
			ViewBag.Title = "Welcome to MagentoNet";
			return View ();
		}
	}
}

