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
        protected StringBuilder _requiresScript = new StringBuilder();

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

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
            if (Page.GetType().Name == "ViewUserControlContainerPage")
                _requiresScript.Append("dojo.require(\"" + type + "\");\n");

            else
                ResourceManager.Require(type);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);

            writer.AddAttribute("type", "text/javascript");
            writer.RenderBeginTag("script");
            writer.Write(_requiresScript.ToString());
            writer.RenderEndTag();
        }

        protected ResourceManager ResourceManager
        {
            get
            {
                ResourceManager resourceManager =
                    HttpContext.Current.Items["__DojoResourceManager"] as
                    ResourceManager;

                if (resourceManager == null)
                    throw new Exception("A DojoResourceManager instance has not been added to the page.");

                return resourceManager;
            }
        }

        protected abstract string DojoType
        {
            get;
        }
    }
}
