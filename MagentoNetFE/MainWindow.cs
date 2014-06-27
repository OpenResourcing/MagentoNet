using System;
using Gtk;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;
using MagentoNetFE.MagentoNetService;
using System.Collections.Generic;

/*
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
	*/
	public partial class MainWindow: Gtk.Window
	{
		CategoryContractClient client;
		public MainWindow () : base (Gtk.WindowType.Toplevel)
		{
			Build ();
			lblOutput.LabelProp = "onlyme!";

			var binding = new BasicHttpBinding ();
			//			var binding = new WebHttpBinding ();
			var address = new EndpointAddress( "http://localhost:8001/MagentoNetCategory.svc");
			//			var address = new EndpointAddress( "http://localhost:8001/EvalService.svc/WebEvalService");

			client = new CategoryContractClient (binding, address);
			Console.WriteLine (client.GetTestString());


		}

		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}

		protected void onBtnClick (object sender, EventArgs e)
		{
		try{
			int defId = 3;
			int.TryParse (txtID.Text, out defId);
			GetCatItemResult thisCat = client.GetCatItem(defId);
			lblOutput.Text = thisCat.Name;
		}catch (Exception exc){
			Console.WriteLine(String.Format("error: {0} in : {1}",exc.Message, exc.StackTrace));
		}
		}
	}

