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
		bool SubmitEval (string id); //Eval eval);

		[OperationContract]
		string GetTestString();

		[OperationContract]		
		List<CatalogCategory> GetEvals();

		[OperationContract]		
		bool RemoveEval (string id);
	}
}

