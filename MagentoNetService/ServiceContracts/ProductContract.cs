using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;


namespace MagentoNetService
{
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class ProductContract : IProductContract
	{
		List<CatalogProduct> prods = new List<CatalogProduct> ();

		#region ICategoryContract Members

		public bool SubmitEval2 (string id) //Eval eval)
		{
			CatalogProduct prod = new CatalogProduct ();
			prod.Id2 = id;// Guid.NewGuid ().ToString ();
			prods.Add (prod);
			return true;
		}
		public List<CatalogProduct> GetEvals2()
		{
			return prods;
		}
		public string GetTestString2()
		{
			return "successful";
//			return @"test successful";
		}

		public bool RemoveEval2(string id)
		{
			prods.Remove (prods.Find (e => e.Id2.Equals (id)));
			return true;
		}

		#endregion

	}
}

