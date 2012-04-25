using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Dojo.Net
{
    public class ResourceManager
    {
        protected List<string> _requires = new List<string>();

        public ResourceManager()
        {
            Theme = DojoTheme.Claro;
        }

        public void Require(string feature)
        {
            if (_requires.Contains(feature))
                return;

            _requires.Add(feature);
        }

        public string Render(HttpContext httpContext)
        {
			StringBuilder output = new StringBuilder();

			output.AppendFormat(@"
	<script src=""{0}/dojo.axd/Scripts/dojo/dojo.js"" type=""text/javascript"" djConfig=""parseOnLoad: true""></script>
", httpContext.Request.ApplicationPath);
			output.AppendFormat(@"	<link rel=""stylesheet"" type=""text/css"" href=""{0}/dojo.axd/Scripts/dijit/themes/{1}/{1}.css""/>
", httpContext.Request.ApplicationPath, ThemeName ?? Theme.ToString("G").ToLower());
			output.Append(@"	<script type=""text/javascript"">
");

			foreach (string feature in _requires)
				output.AppendFormat("\t\tdojo.require(\"{0}\");\n", feature);

			output.Append(@"	</script>
");

        	return output.ToString();
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

    	public static ResourceManager Current
    	{
    		get
    		{
				return HttpContext.Current.Items["__DojoResourceManager"] as ResourceManager;
    		}

			set
			{
				HttpContext.Current.Items["__DojoResourceManager"] = value;
			}
    	}
    }
}
