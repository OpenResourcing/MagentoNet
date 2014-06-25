﻿using System;
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

			DbDataAdapter adapter = null;
			DbConnection connection = _categoryContext.getConnection ();
			string sqlString = "SELECT ent.entity_id, " +
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
							   "att_url_path.value as url_path_value "+
			                   "FROM catalog_category_entity ent " +

			                   "LEFT OUTER JOIN catalog_category_entity_varchar att_name " +
			                   "ON att_name.entity_id = ent.entity_id " +
			                   "AND att_name.entity_type_id = ent.entity_type_id " +
							   "AND att_name.attribute_id = "+
									"(select attribute_id from eav_attribute eav_name "+
										"WHERE eav_name.entity_type_id = ent.entity_type_id " +
										"AND eav_name.attribute_code = 'name') "+
			                   "AND att_name.store_id = 0 " +


			                   "LEFT OUTER JOIN catalog_category_entity_text att_description " +
			                   "ON att_description.entity_id = ent.entity_id " +
			                   "AND att_description.entity_type_id = ent.entity_type_id " +
							   "AND att_description.attribute_id = "+
									"(select attribute_id from eav_attribute eav_description "+
									"WHERE eav_description.entity_type_id = ent.entity_type_id " +
									"AND eav_description.attribute_code = 'description') "+
			                   "AND att_description.store_id = 0 " +


			                   "LEFT OUTER JOIN catalog_category_entity_varchar att_image " +
			                   "ON att_image.entity_id = ent.entity_id " +
			                   "AND att_image.entity_type_id = ent.entity_type_id " +
							   "AND att_image.attribute_id = "+
									"(select attribute_id from eav_attribute eav_image "+
									"WHERE eav_image.entity_type_id = ent.entity_type_id " +
									"AND eav_image.attribute_code = 'image') "+
			                   "AND att_image.store_id = 0 " +


			                   "JOIN catalog_category_entity_int att_is_active " +
			                   "ON att_is_active.entity_id = ent.entity_id " +
			                   "AND att_is_active.entity_type_id = ent.entity_type_id " +
							   "AND att_is_active.attribute_id = "+
									"(select attribute_id from eav_attribute eav_is_active "+
									"WHERE eav_is_active.entity_type_id = ent.entity_type_id " +
									"AND eav_is_active.attribute_code = 'is_active') "+
			                   "AND att_is_active.store_id = 0 " +


			                   "JOIN catalog_category_entity_varchar att_url_key " +
			                   "ON att_url_key.entity_id = ent.entity_id " +
			                   "AND att_url_key.entity_type_id = ent.entity_type_id " +
							   "AND att_url_key.attribute_id = "+
									"(select attribute_id from eav_attribute eav_url_key "+
									"WHERE eav_url_key.entity_type_id = ent.entity_type_id " +
									"AND eav_url_key.attribute_code = 'url_key') "+
			                   "AND att_url_key.store_id = 0 "+


								"JOIN catalog_category_entity_varchar att_url_path "+
								"ON att_url_path.entity_id = ent.entity_id "+
								"AND att_url_path.entity_type_id = ent.entity_type_id "+
								"AND att_url_path.attribute_id = "+
									"(select attribute_id from eav_attribute eav_url_path "+
									"WHERE eav_url_path.entity_type_id = ent.entity_type_id " +
									"AND eav_url_path.attribute_code = 'url_path') "+
								"AND att_url_path.store_id = 0 ";


			try{
				adapter = _categoryContext.getDbDataAdaptor (
					connection,
					sqlString
				);
				//ent.entity_id
				//ent.entity_type_id (3)
				//eav_name.attribute_id
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
						CatalogCategory _newCategory = new CatalogCategory ()
						{
							Id = (int)p.Field<uint>("entity_id"), 
							Name = (string)p.Field<string>("name_value"),
							Description = (string)p.Field<string>("description_value"),
							Image = (string)p.Field<string>("image_value"),
							Path = (string)p.Field<string>("path_value"),
							UrlKey = (string)p.Field<string>("url_key_value"),
							UrlPath = (string)p.Field<string>("url_path_value")
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

