using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace MagentoNetService
{
	[ServiceContract]
	public interface ICategoryContract
	{
		[OperationContract]
		bool SubmitEval (int id); //Eval eval);

		[OperationContract]
		string GetTestString();

		[OperationContract]
		CatalogCategory GetCatItem(int Id);

		[OperationContract]		
		List<CatalogCategory> GetCats();

		[OperationContract]		
		bool RemoveEval (string id);
	}
}

