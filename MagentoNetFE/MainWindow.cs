using System;
using Gtk;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;
using MagentoNetService;
using System.Collections.Generic;

public class CategoryClient : ClientBase<ICategoryContract>, ICategoryContract
	{
		public CategoryClient (Binding binding, EndpointAddress address)
			: base (binding, address)
		{
		}
		public bool SubmitEval(int id)
		{
			return Channel.SubmitEval(id);
		}
		public List<CatalogCategory> GetCats()
		{
			return Channel.GetCats();
		}
		public CatalogCategory GetCatItem(int id)
		{
			return Channel.GetCatItem (id);
		}
		public string GetTestString ()
		{
			return string.Format("Coming straight from the Service:-{0}-!",Channel.GetTestString ());
		}
		public bool RemoveEval(string id)
		{
			return Channel.RemoveEval (id);
		}
	}
	public partial class MainWindow: Gtk.Window
	{
		CategoryClient client;
		public MainWindow () : base (Gtk.WindowType.Toplevel)
		{
			Build ();
			lblOutput.LabelProp = "onlyme!";

			var binding = new BasicHttpBinding ();
			//			var binding = new WebHttpBinding ();
			var address = new EndpointAddress( "http://localhost:8001/MagentoNetCategory.svc");
			//			var address = new EndpointAddress( "http://localhost:8001/EvalService.svc/WebEvalService");

			client = new CategoryClient (binding, address);
			Console.WriteLine (client.GetTestString());


		}

		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}

		protected void onBtnClick (object sender, EventArgs e)
		{
			int defId = 3;
			int.TryParse (txtID.Text, out defId);
			CatalogCategory thisCat = client.GetCatItem(defId);
			lblOutput.Text = thisCat.Name;
		}
	}

