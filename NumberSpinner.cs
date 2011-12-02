using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Dojo.Net
{
    public class NumberSpinner : DojoInputControl<double?>
    {
        public NumberSpinner()
        {
            StepInterval = 1;
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

        public double StepInterval
        {
            get;
            set;
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.AddAttribute("smallDelta", StepInterval.ToString());
            
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
                return "dijit.form.NumberSpinner";
            }
        }
    }
}
