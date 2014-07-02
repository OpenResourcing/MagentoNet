using NUnit.Framework;
using System.Collections.Generic;
using System;

namespace MagentoNetService
{
	[TestFixture ()]
	public class ServiceTests
	{
		CategoryContract cc;

		[Test ()]
		public void TestCase ()
		{
			Assert.AreEqual (true, true);
		}

		[Test ()]
		public void GetCatItem ()
		{
			cc = new CategoryContract ();
			CatalogCategory cat = cc.GetCatItem (3);
			Assert.AreEqual (3, cat.Id);

			cat = cc.GetCatItem (2);
			Assert.AreEqual (2, cat.Id);

			cat = cc.GetCatItem (1);
			Assert.AreEqual (1, cat.Id);

			cat = cc.GetCatItem (5);
			Assert.AreEqual (5, cat.Id);

			cat = cc.GetCatItem (4);
			Assert.IsNull (cat.Id);
		}

		[Test ()]
		public void GetTestString ()
		{
			cc = new CategoryContract ();
			string testString = cc.GetTestString ();
			Assert.IsNotNull (testString);
		}

		[Test ()]
		public void GetAllCatItems ()
		{
			cc = new CategoryContract ();
			IEnumerable<CatalogCategory> cats = cc.GetCats();

			Assert.IsNotEmpty (cats);
		}

	}
}

