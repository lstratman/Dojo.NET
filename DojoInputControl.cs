using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Dojo.Net
{
    public abstract class DojoInputControl<T> : DojoControl, ITextControl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            Attributes["type"] = "text";
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Input;
            }
        }

        public virtual string Text
        {
            get
            {
                return Attributes["value"];
            }

            set
            {
                Attributes["value"] = value;
            }
        }

        public abstract T Value
        {
            get;
            set;
        }

        public virtual string Name
        {
            get;
            set;
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            if (String.IsNullOrEmpty(Name))
                writer.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID);

            base.RenderBeginTag(writer);
        }
    }
}
