using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.WebPages;
//using MagentoNetMvc.MagentoNetCategoryClient;
using MagentoNetMvc.mg_dev.mdoresourcing.com;
using System.ServiceModel;
using MagentoNetMvc.Models;
//using tempuri.org;

namespace MagentoNetMvc.Models
{
    public class HomeModelContainer {
        public catalogCategoryEntity[] catItems;
    }
}
namespace MagentoNetMvc.Controllers
{
	public class HomeController : Controller
	{
        HomeModelContainer homeModelContainer = new HomeModelContainer ();
        static String sessionId = null;
        static MagentoService client = null;

        void initMagentoService(){
            try{
                var soapConnectionString = System.Configuration.ConfigurationManager.AppSettings ["MySOAPConnectionString"];
                client = new MagentoService(soapConnectionString);
                if (String.IsNullOrEmpty(sessionId)){
                    var soapUsername = System.Configuration.ConfigurationManager.AppSettings ["MySOAPUserName"];
                    var soapPassword = System.Configuration.ConfigurationManager.AppSettings ["MySOAPPassword"];
                    sessionId = client.login (soapUsername, soapPassword);
                }
            } catch(Exception e){
                sessionId = "0";
            }
        }

        void fetchCatItems(MagentoService client, string sessionId, string topCategory){
            catalogCategoryTree catTree = client.catalogCategoryTree(sessionId, topCategory, "1");
            homeModelContainer.catItems = catTree.children;
        }

        public ActionResult Index ()
		{
/*			ViewBag.Message = "Welcome to ASP.NET MVC on Mono! (viewbag)";
//			ViewData ["Message"] = "Welcome to ASP.NET MVC on Mono!";

			var binding = new BasicHttpBinding ();
			var address = new EndpointAddress( "http://192.168.1.109:8001/MagentoNetCategory.svc");

			ICategoryContract client = new CategoryContractClient (binding, address);
			IEnumerable<GetCatItemResult> cats = client.GetCats().ToList();
*/
            initMagentoService ();
            try {
                this.fetchCatItems(client, sessionId, "2");
            } catch (Exception ex) {
                // do ntohign for now
            }
			ViewBag.Title = "Welcome to MagentoNet";
            return View (homeModelContainer);
		}
	}
}

