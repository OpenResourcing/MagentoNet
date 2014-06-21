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
	public class CategoryContract : ICategoryContract
	{
		List<CatalogCategory> cats = new List<CatalogCategory> ();

		#region ICategoryContract Members

		public bool SubmitEval (int id) //Eval eval)
		{
			CatalogCategory cat = new CatalogCategory ();
			cat.Id = id;// Guid.NewGuid ().ToString ();
			cats.Add (cat);
			return true;
		}
		public List<CatalogCategory> GetEvals()
		{
			return cats;
		}
		public string GetTestString()
		{
			return "successful";
//			return @"test successful";
		}

		public bool RemoveEval(string id)
		{
			cats.Remove (cats.Find (e => e.Id.Equals (id)));
			return true;
		}

		#endregion

	}
}

