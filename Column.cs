using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Dojo.Net
{
	public class Column
	{
		public Unit Width
		{
			get;
			set;
		}

		public string DataField
		{
			get;
			set;
		}

		public string Formatter
		{
			get;
			set;
		}

		public string HeaderText
		{
			get;
			set;
		}
	}
}
