using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;
using System.Data.Common;
using System.Data;



namespace MagentoNetService
{
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class CategoryContract : ICategoryContract
	{
		List<CatalogCategory> catsList = new List<CatalogCategory> ();

		#region ICategoryContract Members

		public bool SubmitEval (int id) //Eval eval)
		{
			CatalogCategory cat = new CatalogCategory ();
			cat.Id = id;// Guid.NewGuid ().ToString ();
			catsList.Add (cat);
			return true;
		}
		public List<CatalogCategory> GetCats()
		{
			CategoryContext _categoryContext = new CategoryContext ();

			Console.WriteLine("opening connection");
			DbDataAdapter adapter = null;
			DbConnection connection = _categoryContext.getConnection ();

			try{
				adapter = _categoryContext.getDbDataAdaptor (connection);

				Console.WriteLine("filling datatable");
				System.Data.DataTable cats = new System.Data.DataTable ();
				adapter.Fill (cats);
				DataSet ds = cats.DataSet;
				IEnumerable<DataRow> query =
					from _cat in cats.AsEnumerable()
					select _cat;

				catsList = new List<CatalogCategory> ();
				Console.WriteLine("looping through dataset");
				foreach (DataRow p in query)
				{
					//Console.WriteLine(p.Field<string>("Name"));
	//				try{
						CatalogCategory _newCategory = new CatalogCategory (){Id = (int)p.Field<uint>("entity_id"), 
							Name = String.Format("type:{0}, attribute_set:{1}, parent:{2}, position:{3}, path:{4}",
								p.Field<ushort>("entity_type_id"),
								p.Field<ushort>("attribute_set_id"),
								p.Field<uint>("parent_id"),
								p.Field<int>("position"),
								p.Field<string>("path"))};
						catsList.Add (_newCategory);
	//				}
	//				catch (Exception e)
	//				{
	//					Console.WriteLine(string.Format("error: {0}",e.Message));
	//				}
				}
			}
			catch (Exception e) 
			{
				Console.WriteLine(string.Format("error: {0}",e.Message));
			}
			finally {
				if (connection != null)
					connection.Close ();
			}
			return catsList;
		}
		public string GetTestString()
		{
			return "successful";
//			return @"test successful";
		}

		public bool RemoveEval(string id)
		{
			catsList.Remove (catsList.Find (e => e.Id.Equals (id)));
			return true;
		}

		#endregion

	}
}

