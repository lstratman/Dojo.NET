using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Dojo.Net
{
    public class NumberTextBox : DojoInputControl<double?>
    {
        public NumberTextBox()
        {
            Minimum = 0;
        }

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

        public int DecimalPlaces
        {
            get;
            set;
        }

        public override double? Value
        {
            get
            {
                if (Attributes["value"] == null)
                    return null;

                return double.Parse(Attributes["value"]);
            }

            set
            {
                if (value == null)
                    Attributes["value"] = null;

                else
                    Attributes["value"] = value.ToString();
            }
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            StringBuilder constraints = new StringBuilder("{");
            constraints.AppendFormat("places: {0}", DecimalPlaces);

            if (Minimum != null)
                constraints.AppendFormat(", min: {0}", Minimum);

            if (Maximum != null)
                constraints.AppendFormat(", max: {0}", Maximum);

            constraints.Append("}");
            writer.AddAttribute("constraints", constraints.ToString());

            base.RenderBeginTag(writer);
        }

        protected override string DojoType
        {
            get
            {
                return "dijit.form.NumberTextBox";
            }
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
