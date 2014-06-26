using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MagentoNetService
{
	[DataContract]
	public class CatalogCategory
	{
		[DataMember]
		public int? Id;
		[DataMember]
		public string Name;
		[DataMember]
		public string IsActive;
		[DataMember]
		public string UrlKey;
		[DataMember]
		public string Description;
		[DataMember]
		public string Image;
		[DataMember]
		public string MetaTitle;
		[DataMember]
		public string MetaKeywords;
		[DataMember]
		public string MetaDescription;
		[DataMember]
		public string DisplayMode;
		[DataMember]
		public string LandingPage;
		[DataMember]
		public string IsAnchor;
		[DataMember]
		public string Path;
		[DataMember]
		public string Position;
		[DataMember]
		public string AllChildren;
		[DataMember]
		public string PathInStore;
		[DataMember]
		public string Children;
		[DataMember]
		public string UrlPath;
		[DataMember]
		public string CustomDesign;
		[DataMember]
		public string CustomDesignFrom;
		[DataMember]
		public string CustomDesignTo;
		[DataMember]
		public string PageLayout;
		[DataMember]
		public string CustomLayoutUpdate;
		[DataMember]
		public string Level;
		[DataMember]
		public string ChildrenCount;
		[DataMember]
		public string AvailableSortBy;
		[DataMember]
		public string DefaultSortBy;
		[DataMember]
		public string IncludeInMenu;
		[DataMember]
		public string CustomUseParentSettings;
		[DataMember]
		public string CustomApplyToProducts;
		[DataMember]
		public string FilterPriceRange;
		[DataMember]
		public DateTime TimeSubmitted;
	}
}

