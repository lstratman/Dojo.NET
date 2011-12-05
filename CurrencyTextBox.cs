using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Dojo.Net
{
    public class CurrencyTextBox : DojoInputControl<double?>
    {
        public double? Minimum
        {
            get;
            set;
        }

        public double? Maximum
        {
            get;
            set;
        }

        protected override string DojoType
        {
            get
            {
                return "dijit.form.CurrencyTextBox";
            }
        }

        public override double? Value
        {
            get
            {
                return double.Parse(Text);
            }

            set
            {
                Text = value.ToString();
            }
        }

        public string Currency
        {
            get
            {
                return Attributes["currency"];
            }

            set
            {
                Attributes["currency"] = value;
            }
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            StringBuilder constraints = new StringBuilder("{");

            if (Minimum != null)
                constraints.AppendFormat("min: {0}, ", Minimum);

            if (Maximum != null)
                constraints.AppendFormat("max: {0}, ", Maximum);

            if (constraints.Length > 1)
                constraints.Length -= 2;

            constraints.Append("}");
            writer.AddAttribute("constraints", constraints.ToString());

            base.RenderBeginTag(writer);
        }

        public bool Required
        {
            get
            {
                return Attributes["required"] != null && bool.Parse(Attributes["required"]);
            }

            set
            {
                Attributes["required"] = value.ToString().ToLower();
            }
        }
    }
}
