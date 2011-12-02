using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Dojo.Net
{
    public class ResourceManager : WebControl
    {
        protected LiteralControl _requiresScript = new LiteralControl(@"<script type=""text/javascript"">
</script>
");
        protected List<string> _requires = new List<string>();

        public ResourceManager()
        {
            Theme = DojoTheme.Claro;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (HttpContext.Current.Items["__DojoResourceManager"] == null)
                HttpContext.Current.Items["__DojoResourceManager"] = this;

            Control destination = (Control)Page.Header ?? this;

            destination.Controls.Add(new LiteralControl(String.Format(@"
<script src=""{0}/dojo.axd/Scripts/dojo/dojo.js"" type=""text/javascript"" djConfig=""parseOnLoad: true""></script>
", Page.Request.ApplicationPath)));
            destination.Controls.Add(new LiteralControl(String.Format(@"<link rel=""stylesheet"" type=""text/css"" href=""{0}/dojo.axd/Scripts/dijit/themes/{1}/{1}.css""/>
", Page.Request.ApplicationPath, ThemeName ?? Theme.ToString("G").ToLower())));
            destination.Controls.Add(_requiresScript);
        }

        public void Require(string feature)
        {
            if (_requires.Contains(feature))
                return;

            _requires.Add(feature);
            _requiresScript.Text = _requiresScript.Text.Insert(_requiresScript.Text.IndexOf("</script>"),
                                                               String.Format("\tdojo.require(\"{0}\");\n", feature));
        }

        protected override void Render(HtmlTextWriter writer)
        {
            RenderContents(writer);
        }

        public string ThemeName
        {
            get;
            set;
        }

        public DojoTheme Theme
        {
            get;
            set;
        }
    }
}
