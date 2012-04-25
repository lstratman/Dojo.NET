using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Dojo.Net
{
	public class TimeTextBox : DojoInputControl<TimeSpan?>
	{
		protected override string DojoType
		{
			get
			{
				return "dijit.form.TimeTextBox";
			}
		}

		public override TimeSpan? Value
		{
			get
			{
				if (Attributes["value"] == null)
					return null;

				return TimeSpan.Parse(Attributes["value"].Substring(1));
			}

			set
			{
				if (value == null)
					Attributes["value"] = null;

				else
					Attributes["value"] = "T" + value.Value.ToString("hh:mm:ss");
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
