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
		Dictionary<string, int> attrList = new Dictionary<string, int> ();
		List<CatalogCategory> catsList = new List<CatalogCategory> ();
		CatalogCategory catItem = new CatalogCategory();

		private bool ReloadAttributes(CategoryContext categoryContext){
			DbConnection connection = categoryContext.getConnection ();

			if (attrList.Count == 0) {
				string sqlAttributeCollectionString = "select attribute_id, attribute_code from eav_attribute " +
					"WHERE eav_attribute.entity_type_id = 3 "; 
				//(catalog_category_entity.entity_type_id ALWAYS equals 3)
				try {
					DbDataAdapter adapter = categoryContext.getDbDataAdaptor (
						connection,
						sqlAttributeCollectionString
					);

					System.Data.DataTable attributes = new System.Data.DataTable ();
					adapter.Fill (attributes);
					foreach (DataRow p in attributes.AsEnumerable()) {
						attrList.Add (p.Field<string> ("attribute_code"), (int)p.Field<ushort> ("attribute_id"));
					}

				} catch (Exception e) {
					Console.WriteLine (String.Format ("error fetching attributes for catalog/category: {0}", e.Message));
					return false;
				}
			} else {
				// otherwise this list is already propulated, reuse it
				Console.WriteLine ("using cached attributes for catalog/category ");
			}
			connection.Close ();
			return true;
		}

		#region ICategoryContract Members
		public CatalogCategory GetCatItem(int Id)
		{
/*			if (catItem.Id != null && catItem.Id == Id) {
				return catItem;
			}
*/
			// otherwise if this haven't previously been fetched....

			catItem = new CatalogCategory ();

			CategoryContext _categoryContext = new CategoryContext ();
			if (!ReloadAttributes (_categoryContext)) {
				return catItem;
			}
			// adapter = null;
			DbConnection connection = _categoryContext.getConnection ();



			string sqlStringInit = "SELECT ent.entity_id, " +
				"ent.entity_type_id, " +
				"ent.attribute_set_id, " +
				"ent.parent_id, " +
				"ent.position, " +
				"ent.path, " +
				"att_name.value as name_value, " +
				"att_description.value as description_value, " +
				"att_image.value as image_value, " +
				"att_is_active.value as is_active_value, " +
				"ent.path as path_value, "+
				"att_url_key.value as url_key_value, "+
				"att_url_path.value as url_path_value, "+
				"att_include_in_menu.value as include_in_menu_value "+
				"FROM catalog_category_entity ent " +

				"LEFT OUTER JOIN catalog_category_entity_varchar att_name " +
				"ON att_name.entity_id = ent.entity_id " +
				"AND att_name.entity_type_id = ent.entity_type_id " +
				"AND att_name.attribute_id = "+
				attrList["name"]+
				" AND att_name.store_id = 0 " +


				"LEFT OUTER JOIN catalog_category_entity_text att_description " +
				"ON att_description.entity_id = ent.entity_id " +
				"AND att_description.entity_type_id = ent.entity_type_id " +
				"AND att_description.attribute_id = "+
				attrList["description"]+
				" AND att_description.store_id = 0 " +


				"LEFT OUTER JOIN catalog_category_entity_varchar att_image " +
				"ON att_image.entity_id = ent.entity_id " +
				"AND att_image.entity_type_id = ent.entity_type_id " +
				"AND att_image.attribute_id = "+
				attrList["image"]+
				" AND att_image.store_id = 0 " +


				"LEFT OUTER JOIN catalog_category_entity_int att_is_active " +
				"ON att_is_active.entity_id = ent.entity_id " +
				"AND att_is_active.entity_type_id = ent.entity_type_id " +
				"AND att_is_active.attribute_id = "+
				attrList["is_active"]+
				" AND att_is_active.store_id = 0 " +


				"LEFT OUTER JOIN catalog_category_entity_varchar att_url_key " +
				"ON att_url_key.entity_id = ent.entity_id " +
				"AND att_url_key.entity_type_id = ent.entity_type_id " +
				"AND att_url_key.attribute_id = "+
				attrList["url_key"]+
				" AND att_url_key.store_id = 0 "+


				"LEFT OUTER JOIN catalog_category_entity_varchar att_url_path "+
				"ON att_url_path.entity_id = ent.entity_id "+
				"AND att_url_path.entity_type_id = ent.entity_type_id "+
				"AND att_url_path.attribute_id = "+
				attrList["url_path"]+
				" AND att_url_path.store_id = 0 " +


				"LEFT OUTER JOIN catalog_category_entity_int att_include_in_menu "+
				"ON att_include_in_menu.entity_id = ent.entity_id "+
				"AND att_include_in_menu.entity_type_id = ent.entity_type_id "+
				"AND att_include_in_menu.attribute_id = "+
				attrList["include_in_menu"]+
				" AND att_include_in_menu.store_id = 0 " +

				"WHERE ent.entity_id = {0}";

			string sqlString = String.Format (sqlStringInit, Id);

			try{
				DbDataAdapter adapter = _categoryContext.getDbDataAdaptor (
					connection,
					sqlString
				);
				System.Data.DataTable cats = new System.Data.DataTable ();
				adapter.Fill (cats);
				DataSet ds = cats.DataSet;
				IEnumerable<DataRow> query =
					from _cat in cats.AsEnumerable()
					//where _cat.Field<uint>("entity_id") == 3
					select _cat;

				catsList = new List<CatalogCategory> ();
				foreach (DataRow p in query)
				{
					catItem = new CatalogCategory ()
					{
						Id = (int)p.Field<uint>("entity_id"), 
						Name = String.IsNullOrEmpty(p.Field<string>("name_value")) ? "" : p.Field<string>("name_value"),
						Description = String.IsNullOrEmpty(p.Field<string>("description_value")) ? "" : p.Field<string>("description_value"),
						Image = String.IsNullOrEmpty(p.Field<string>("image_value")) ? "" : p.Field<string>("image_value"),
						IsActive = (p.Field<int?>("is_active_value") == null || p.Field<int?>("is_active_value") != 1) ? false : true,
						Path = String.IsNullOrEmpty(p.Field<string>("path_value")) ? "" : p.Field<string>("path_value"),
						UrlKey = String.IsNullOrEmpty(p.Field<string>("url_key_value")) ? "" : p.Field<string>("url_key_value"),
						UrlPath = String.IsNullOrEmpty(p.Field<string>("url_path_value")) ? "" : p.Field<string>("url_path_value"),
						IncludeInMenu = (p.Field<int?>("include_in_menu_value") == null || p.Field<int?>("include_in_menu_value") != 1) ? false : true
							
					};
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

			return catItem;
		}

		public bool SubmitEval (int id) //Eval eval)
		{
			CatalogCategory cat = new CatalogCategory ();
			cat.Id = id;// Guid.NewGuid ().ToString ();
			catsList.Add (cat);
			return true;
		}
		public List<CatalogCategory> GetCats()
		{
/*			if (catsList.Count != 0) 
			{
				return catsList;
			}*/
			// otherwise, if the list hasn't previously been fetched...

			catsList = new List<CatalogCategory> ();

			CategoryContext _categoryContext = new CategoryContext ();
			if (!ReloadAttributes (_categoryContext)){
				return catsList;
			}

			DbConnection connection = _categoryContext.getConnection ();

			string sqlString = "SELECT ent.entity_id, " +
			                   "ent.entity_type_id, " +
			                   "ent.attribute_set_id, " +
			                   "ent.parent_id, " +
			                   "ent.position, " +
			                   "ent.path, " +
			                   "att_name.value as name_value, " +
			                   "att_is_active.value as is_active_value, " +
							   "ent.path as path_value, "+
							   "att_url_key.value as url_key_value, "+
							   "att_url_path.value as url_path_value, "+
							   "att_include_in_menu.value as include_in_menu_value "+
			                   "FROM catalog_category_entity ent " +

			                   "LEFT OUTER JOIN catalog_category_entity_varchar att_name " +
			                   "ON att_name.entity_id = ent.entity_id " +
			                   "AND att_name.entity_type_id = ent.entity_type_id " +
							   "AND att_name.attribute_id = "+
								attrList["name"]+
//									"(select attribute_id from eav_attribute eav_name "+
//										"WHERE eav_name.entity_type_id = ent.entity_type_id " +
//										"AND eav_name.attribute_code = 'name') "+
			                   " AND att_name.store_id = 0 " +


							   "LEFT OUTER JOIN catalog_category_entity_int att_is_active " +
			                   "ON att_is_active.entity_id = ent.entity_id " +
			                   "AND att_is_active.entity_type_id = ent.entity_type_id " +
							   "AND att_is_active.attribute_id = "+
								attrList["is_active"]+
//									"(select attribute_id from eav_attribute eav_is_active "+
//									"WHERE eav_is_active.entity_type_id = ent.entity_type_id " +
//									"AND eav_is_active.attribute_code = 'is_active') "+
			                   " AND att_is_active.store_id = 0 " +


							   "LEFT OUTER JOIN catalog_category_entity_varchar att_url_key " +
			                   "ON att_url_key.entity_id = ent.entity_id " +
			                   "AND att_url_key.entity_type_id = ent.entity_type_id " +
							   "AND att_url_key.attribute_id = "+
								attrList["url_key"]+
//									"(select attribute_id from eav_attribute eav_url_key "+
//									"WHERE eav_url_key.entity_type_id = ent.entity_type_id " +
//									"AND eav_url_key.attribute_code = 'url_key') "+
			                   " AND att_url_key.store_id = 0 "+



								"LEFT OUTER JOIN catalog_category_entity_int att_include_in_menu "+
								"ON att_include_in_menu.entity_id = ent.entity_id "+
								"AND att_include_in_menu.entity_type_id = ent.entity_type_id "+
								"AND att_include_in_menu.attribute_id = "+
								attrList["include_in_menu"]+
//									"(select attribute_id from eav_attribute eav_include_in_menu "+
//									"WHERE eav_include_in_menu.entity_type_id = ent.entity_type_id " +
//									"AND eav_include_in_menu.attribute_code = 'include_in_menu') "+
								" AND att_include_in_menu.store_id = 0 " +

							    "LEFT OUTER JOIN catalog_category_entity_varchar att_url_path "+
								"ON att_url_path.entity_id = ent.entity_id "+
								"AND att_url_path.entity_type_id = ent.entity_type_id "+
								"AND att_url_path.attribute_id = "+
								attrList["url_path"]+
//									"(select attribute_id from eav_attribute eav_url_path "+
//									"WHERE eav_url_path.entity_type_id = ent.entity_type_id " +
//									"AND eav_url_path.attribute_code = 'url_path') "+
								" AND att_url_path.store_id = 0 ";


			try{
				DbDataAdapter adapter = _categoryContext.getDbDataAdaptor (
					connection,
					sqlString
				);
				//ent.entity_id
				//ent.entity_type_id (3)
				//eav_name.attribute_id
				System.Data.DataTable cats = new System.Data.DataTable ();
				adapter.Fill (cats);
//				DataSet ds = cats.DataSet;
				IEnumerable<DataRow> query =
					from _cat in cats.AsEnumerable()
					//where _cat.Field<uint>("entity_id") == 3
					select _cat;

				foreach (DataRow p in query)
				{
						CatalogCategory _newCategory = new CatalogCategory ()
						{
							Id = (int)p.Field<uint>("entity_id"), 
							Name = String.IsNullOrEmpty(p.Field<string>("name_value")) ? "" : p.Field<string>("name_value"),
							Path =  String.IsNullOrEmpty(p.Field<string>("path_value")) ? "" : p.Field<string>("path_value"),
							IsActive = (p.Field<int?>("is_active_value") == null || p.Field<int?>("is_active_value") != 1) ? false: true,
							UrlKey = String.IsNullOrEmpty(p.Field<string>("url_key_value")) ? "" : p.Field<string>("url_key_value"),
							UrlPath = String.IsNullOrEmpty(p.Field<string>("url_path_value")) ? "" : p.Field<string>("url_path_value"),
							IncludeInMenu = (p.Field<int?>("include_in_menu_value") == null || p.Field<int?>("include_in_menu_value") != 1) ? false : true
						};
						catsList.Add (_newCategory);
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

