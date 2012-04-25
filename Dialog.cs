using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dojo.Net
{
    public class Dialog : DojoControl
    {
        protected PlaceHolder _placeHolder = new PlaceHolder();

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

			if (Content != null)
			{
				Content.InstantiateIn(_placeHolder);
				Controls.Add(_placeHolder);
			}
        }

        protected override string DojoType
        {
            get
            {
                return "dijit.Dialog";
            }
        }

        public string Title
        {
            get
            {
                return Attributes["title"];
            }

            set
            {
                Attributes["title"] = value;
            }
        }

    	public string Href
    	{
			get
			{
				return Attributes["href"];
			}

			set
			{
				Attributes["href"] = value;
			}
    	}

        public string Text
        {
            get;
            set;
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public ITemplate Content
        {
            get;
            set;
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (!String.IsNullOrEmpty(Text))
                writer.Write(Text);

            else
                base.RenderContents(writer);
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }
    }
}
