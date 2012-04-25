using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Dojo.Net
{
	public class Button : DojoControl
	{
		protected LiteralControl _label = new LiteralControl();
		protected string _iconClass = "";
		protected bool _iconAboveLabel = false;
		protected string _iconImageUrl = null;

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			Attributes["type"] = "button";
			Controls.Add(_label);

			ShowLabel = true;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			Page.ClientScript.RegisterClientScriptBlock(GetType(), "IconAboveLabelCss", @"<style type=""text/css"">
	.dijitIconAboveLabel
	{
		background-repeat: no-repeat;
		text-align: center;
		background-position: center;
		display: block;
		width: 100%;
	}
</style>
");
		}

		protected override string DojoType
		{
			get
			{
				return "dijit.form.Button";
			}
		}

		public bool ShowLabel
		{
			get
			{
				return Convert.ToBoolean(Attributes["showLabel"]);
			}

			set
			{
				Attributes["showLabel"] = value.ToString().ToLower();
			}
		}

		public string Label
		{
			get
			{
				return _label.Text;
			}

			set
			{
				_label.Text = value;
			}
		}

		public string IconClass
		{
			get
			{
				return _iconClass;
			}

			set
			{
				_iconClass = value;
				Attributes["iconClass"] = value + (IconAboveLabel ? " dijitIconAboveLabel" : "");
			}
		}

		public string OnClientClick
		{
			get
			{
				return Attributes["onclick"];
			}

			set
			{
				Attributes["onclick"] = value;
			}
		}

		public bool IconAboveLabel
		{
			get
			{
				return _iconAboveLabel;
			}

			set
			{
				_iconAboveLabel = value;
				Attributes["iconClass"] = _iconClass + (value ? " dijitIconAboveLabel" : "");
			}
		}

		public string IconImageUrl
		{
			get
			{
				return _iconImageUrl;
			}

			set
			{
				_iconImageUrl = value;
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			if (!String.IsNullOrEmpty(IconImageUrl))
			{
				Page.ClientScript.RegisterClientScriptBlock(GetType(), ID + "IconImageUrlCss", String.Format(@"<style type=""text/css"">
	.{0}Icon
	{{
		background-image: url({1});
	}}
</style>
", ID, ResolveClientUrl(IconImageUrl)));
				Attributes["iconClass"] += " " + ID + "Icon";
			}
		}

		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Button;
			}
		}
	}
}
