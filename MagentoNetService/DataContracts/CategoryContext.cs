using System;
using System.Data.Common;
using System.Data;
using System.Configuration;

namespace MagentoNetService
{
	public class CategoryContext
	{

				static DbProviderFactory dbFactory;

				public DbConnection getConnection(){
					var connectionDetails = ConfigurationManager.ConnectionStrings["MyDatabase"];
					var providerName = connectionDetails.ProviderName;
					var connectionString = connectionDetails.ConnectionString;

					dbFactory = DbProviderFactories.GetFactory(providerName);
					DbConnection connection = dbFactory.CreateConnection ();
					connection.ConnectionString = connectionString;
					return connection;
				}

				public DbDataAdapter getDbDataAdaptor(DbConnection connection){

					using (connection) {
						// Define the query. 
						string queryString =
							"SELECT entity_id, entity_type_id, attribute_set_id, parent_id, position, path FROM catalog_category_entity";

						// Create the select command.
						DbCommand command = dbFactory.CreateCommand();
						command.CommandText = queryString;
						command.Connection = connection;

						// Create the DbDataAdapter.
						DbDataAdapter adapter = dbFactory.CreateDataAdapter();
						adapter.SelectCommand = command;

						// Create the DbCommandBuilder.
						DbCommandBuilder builder = dbFactory.CreateCommandBuilder();
						builder.DataAdapter = adapter;

						// Get the insert, update and delete commands.
						adapter.InsertCommand = builder.GetInsertCommand();
						adapter.UpdateCommand = builder.GetUpdateCommand();
						adapter.DeleteCommand = builder.GetDeleteCommand();

						return adapter;
					}
				}

	}
}

