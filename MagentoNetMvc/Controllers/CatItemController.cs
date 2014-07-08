using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using MagentoNetMvc.MagentoNetCategoryClient;
using MagentoNetMvc.mg_dev.mdoresourcing.com;
using System.ServiceModel;
using System.Configuration;

//using tempuri.org;
namespace MagentoNetMvc.Controllers
{
	public class CatItemController : Controller
	{
		static String sessionId = null;
		public ActionResult Index (int id)
		{
			ViewBag.Message = "Welcome to ASP.NET MVC on Mono! (viewbag)";
			//			ViewData ["Message"] = "Welcome to ASP.NET MVC on Mono!";
/*
			// keeping this endpoint reference here in preps for when this service is ready for use
			// ... until then we will use the SOAP services provided by the Magento-php
			var binding = new BasicHttpBinding ();
			var address = new EndpointAddress( "http://192.168.1.109:8001/MagentoNetCategory.svc");

			ICategoryContract client = new CategoryContractClient (binding, address);
			GetCatItemResult catItem = client.GetCatItem(id);
*/


			var soapConnectionString = System.Configuration.ConfigurationManager.AppSettings ["MySOAPConnectionString"];
			MagentoService client = new MagentoService(soapConnectionString);
			if (String.IsNullOrEmpty(sessionId)){
				var soapUsername = System.Configuration.ConfigurationManager.AppSettings ["MySOAPUserName"];
				var soapPassword = System.Configuration.ConfigurationManager.AppSettings ["MySOAPPassword"];
				sessionId = client.login (soapUsername, soapPassword);
			}
			catalogCategoryInfo catInfo = client.catalogCategoryInfo(sessionId, id, "1", null);
			ViewBag.sessionId = sessionId;
			ViewBag.catInfo = catInfo;


			return View ();
		}

	}
}

