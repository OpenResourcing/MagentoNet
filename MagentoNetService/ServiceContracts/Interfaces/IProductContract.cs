using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace MagentoNetService
{
	[ServiceContract]
	public interface IProductContract
	{
		[OperationContract]
		bool SubmitEval2 (string id); //Eval eval);

		[OperationContract]
		string GetTestString2();

		[OperationContract]		
		List<CatalogProduct> GetEvals2();

		[OperationContract]		
		bool RemoveEval2 (string id);
	}
}

