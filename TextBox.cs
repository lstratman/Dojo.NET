using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace Dojo.Net
{
    public class TextBox : DojoInputControl<string>
    {
        public override string Value
        {
            get
            {
                return Text;
            }

            set
            {
                Text = value;
            }
        }

        protected override string DojoType
        {
            get
            {
                return "dijit.form.TextBox";
            }
        }

        public string PromptMessage
        {
            get
            {
                return Attributes["promptMessage"];
            }

            set
            {
                Attributes["promptMessage"] = value;
            }
        }

        public TooltipPosition Position
        {
            get
            {
                return
                    (TooltipPosition)
                    Enum.Parse(typeof (TooltipPosition),
                               Attributes["position"].Substring(0, 1).ToUpper() + Attributes["position"].Substring(1));
            }

            set
            {
                Attributes["position"] = value.ToString("G").ToLower();
            }
        }

        public string RegExp
        {
            get
            {
                return Attributes["regExp"];
            }

            set
            {
                Attributes["regExp"] = value;

                if (!String.IsNullOrEmpty(value))
                {
                    SetType("dijit.form.ValidationTextBox");
                    Attributes.Remove("regExpGen");
                }

                else
                    SetType("dijit.form.TextBox");
            }
        }

        public string RegExpGeneratorFunction
        {
            get
            {
                return Attributes["regExpGen"];
            }

            set
            {
                Attributes["regExpGen"] = value;

                if (!String.IsNullOrEmpty(value))
                {
                    SetType("dijit.form.ValidationTextBox");
                    Attributes.Remove("regExp");
                }

                else
                    SetType("dijit.form.TextBox");
            }
        }

        public bool Required
        {
            get
            {
                if (Attributes["required"] == null)
                    return false;

                return bool.Parse(Attributes["required"]);
            }

            set
            {
                Attributes["required"] = value.ToString().ToLower();
                SetType(value
                            ? "dijit.form.ValidationTextBox"
                            : "dijit.form.TextBox");
            }
        }

        public int? MaxLength
        {
            get
            {
                if (Attributes["maxlength"] == null)
                    return null;

                return int.Parse(Attributes["maxlength"]);
            }

            set
            {
                Attributes["maxlength"] = value.ToString();
            }
        }

        public string InvalidMessage
        {
            get
            {
                return Attributes["invalidMessage"];
            }

            set
            {
                Attributes["invalidMessage"] = value;
            }
        }
    }
}
