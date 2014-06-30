using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using MagentoNetMvc.MagentoNetCategoryClient;
using System.ServiceModel;

//using tempuri.org;
namespace MagentoNetMvc.Controllers
{
	public class CatItemController : Controller
	{
		public ActionResult Index (int id)
		{
			ViewBag.Message = "Welcome to ASP.NET MVC on Mono! (viewbag)";
			//			ViewData ["Message"] = "Welcome to ASP.NET MVC on Mono!";

			var binding = new BasicHttpBinding ();
			var address = new EndpointAddress( "http://192.168.1.109:8001/MagentoNetCategory.svc");

			ICategoryContract client = new CategoryContractClient (binding, address);
			GetCatItemResult catItem = client.GetCatItem(id);

			ViewBag.catItem = catItem;
			return View ();
		}

	}
}

