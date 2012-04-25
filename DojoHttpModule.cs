using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Dojo.Net
{
	public class DojoHttpModule : IHttpModule
	{
		public void Init(HttpApplication context)
		{
			context.BeginRequest += context_BeginRequest;
		}

		protected void context_BeginRequest(object sender, EventArgs e)
		{
			HttpApplication application = (HttpApplication)sender;
			application.Context.Response.Filter = new ResourceManagerStream(application.Context.Response.Filter, application.Context);
		}

		public void Dispose()
		{
		}
	}
}
