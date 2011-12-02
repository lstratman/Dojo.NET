using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Dojo.Net
{
    public class DateTextBox : DojoInputControl<DateTime?>
    {
        protected override string DojoType
        {
            get
            {
                return "dijit.form.DateTextBox";
            }
        }

        public override DateTime? Value
        {
            get
            {
                if (Attributes["value"] == null)
                    return null;

                return DateTime.Parse(Attributes["value"]);
            }

            set
            {
                if (value == null)
                    Attributes["value"] = null;

                else
                    Attributes["value"] = value.Value.ToString("yyyy-MM-dd");
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
