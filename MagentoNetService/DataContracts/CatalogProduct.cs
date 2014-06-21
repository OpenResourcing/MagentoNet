using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MagentoNetService
{
	[DataContract]
	public class CatalogProduct
	{
		[DataMember]
		public string Id2;
		[DataMember]
		public string Submitter2;
		[DataMember]
		public string Comments2;
		[DataMember]
		public DateTime TimeSubmitted2;
	}
}

