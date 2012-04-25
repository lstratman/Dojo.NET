using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.Net
{
	public class DateColumn : Column
	{
		public DateColumn()
		{
			Formatter = "formatDate";
		}
	}
}
