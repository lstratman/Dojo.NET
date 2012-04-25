using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dojo.Net
{
    public abstract class DojoControl : WebControl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

			if (ResourceManager.Current == null)
				ResourceManager.Current = new ResourceManager();

            if (Attributes["dojoType"] == null)
                SetType(DojoType);
        }

        protected void SetType(string type)
        {
            Attributes["dojoType"] = type;
            Require(type);
        }

        protected void Require(string type)
        {
            ResourceManager.Require(type);
        }

        protected ResourceManager ResourceManager
        {
            get
            {
                ResourceManager resourceManager = ResourceManager.Current;

                if (resourceManager == null)
                    throw new Exception("A DojoResourceManager instance has not been added to the page.");

                return resourceManager;
            }
        }

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			Attributes["data-dojo-id"] = ClientID;
		}

        protected abstract string DojoType
        {
            get;
        }
    }
}
