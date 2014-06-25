
// program.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description; 
using System.ServiceModel.Web;
using System.Threading;

namespace MagentoNetService
{
	class ServiceThread {
		public bool stopFlag = false;  

		public void startThread(){
			// set MONO_STRICT_MS_COMPLIANT

			if (Environment.GetEnvironmentVariable("MONO_STRICT_MS_COMPLIANT") != "yes") 
			{ 
				Environment.SetEnvironmentVariable("MONO_STRICT_MS_COMPLIANT", "yes"); 
			}

			var host = new ServiceHost (typeof(CategoryContract));
			var host2 = new ServiceHost (typeof(ProductContract));

			host.Open ();
			host2.Open ();

			while (!stopFlag)
			{
				//just continue to so your service'ing stuff
//				Console.WriteLine("Alpha.Beta is running in its own thread.");
			}

			host.Close ();
			host2.Close ();
		}
	}

	class Program
	{
		private static bool stopFlag = false;
		public static int Main(string[] args)
		{

			ServiceThread thread = new ServiceThread ();
			// Create the thread object, passing in the Alpha.Beta method
			// via a ThreadStart delegate. This does not start the thread.
			Thread oThread = new Thread(new ThreadStart(thread.startThread));

			// Start the thread
			oThread.Start();

			// Spin for a while waiting for the started thread to become
			// alive:
			while (!oThread.IsAlive);

/*
			Console.WriteLine ("Type [CR] to stop...");
			Console.ReadLine ();
			oThread.Abort ();
*/
			while (!stopFlag) {
				// Put the Main thread to sleep for 1 millisecond to allow oThread
				// to do some work:
				Thread.Sleep (1);
			}
			thread.stopFlag = true;
			return 0;

		}
	}
}
