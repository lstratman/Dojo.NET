using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dojo.Net
{
    public class DataGrid : DojoControl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            Attributes["plugins"] = "{pagination: {sizeSwitch: false, gotoButton: true, position: 'top'}}";
            Require("dojox.grid.enhanced.plugins.Pagination");

            Page.ClientScript.RegisterClientScriptBlock(GetType(), "Css", String.Format(@"<link rel=""stylesheet"" type=""text/css"" href=""{0}/dojo.axd/Scripts/dojox/grid/enhanced/resources/EnhancedGrid.css""/>
", Page.Request.ApplicationPath));
            Page.ClientScript.RegisterClientScriptBlock(GetType(), "ThemeCss", String.Format(@"<link rel=""stylesheet"" type=""text/css"" href=""{0}/dojo.axd/Scripts/dojox/grid/enhanced/resources/{1}/EnhancedGrid.css""/>
", Page.Request.ApplicationPath, ResourceManager.ThemeName ?? ResourceManager.Theme.ToString("G").ToLower()));
        }

        protected override string DojoType
        {
            get
            {
                return "dojox.grid.EnhancedGrid";
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            StringBuilder storeJavaScript = new StringBuilder(ID + "Store = new ");

            if (Store[0] is JsonStore)
            {
                JsonStore store = (JsonStore) Store[0];

                storeJavaScript.Append("dojox.data.JsonRestStore(");
                Require("dojox.data.JsonRestStore");

                if (String.IsNullOrEmpty(store.ServiceObject))
                    storeJavaScript.AppendFormat(@"{{
    target: ""{0}"",
    idAttribute: ""{1}""
}});", store.Target, store.IdAttribute);

                else
                    storeJavaScript.AppendFormat(@"{{
    service: {0}
}});", store.ServiceObject);
            }

            Page.ClientScript.RegisterClientScriptBlock(GetType(), ID + "StoreRegistration", storeJavaScript.ToString(), true);
            Attributes["store"] = ID + "Store";
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (Columns == null)
                return;

            writer.RenderBeginTag("thead");
            writer.RenderBeginTag("tr");

            foreach (Column column in Columns)
            {
				writer.AddAttribute("field", column.DataField);

            	if (column.Width.Value > 0)
                    writer.AddAttribute("width", column.Width.ToString());

				if (!String.IsNullOrEmpty(column.Formatter))
					writer.AddAttribute("formatter", column.Formatter);

                writer.RenderBeginTag("th");
                writer.Write(column.HeaderText);
                writer.RenderEndTag();
            }

            writer.RenderEndTag();
            writer.RenderEndTag();
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Table;
            }
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<Column> Columns
        {
            get;
            set;
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<Store> Store
        {
            get;
            set;
        }
    }
}
