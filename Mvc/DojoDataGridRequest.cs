using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Dojo.Net.Mvc
{
    [ModelBinder(typeof(DojoDataGridRequestBinder))]
    public class DojoDataGridRequest
    {
        protected List<SortingColumn> _sortingColumns = new List<SortingColumn>();
		protected Dictionary<string, string> _filters = new Dictionary<string, string>();

        public int LowerBound
        {
            get;
            set;
        }

        public int UpperBound
        {
            get;
            set;
        }

        public int Count
        {
            get
            {
                return UpperBound - LowerBound + 1;
            }
        }

        public List<SortingColumn> SortingColumns
        {
            get
            {
                return _sortingColumns;
            }
        }

    	public Dictionary<string, string> Filters
    	{
    		get
    		{
    			return _filters;
    		}
    	}
    }

	public static class IEnumerableExtensions
	{
		public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, SortDirection sortDirection)
		{
			return sortDirection == SortDirection.Ascending
			       	? source.OrderBy(keySelector)
			       	: source.OrderByDescending(keySelector);
		}
	}

    public class SortingColumn
    {
        public string ColumnName
        {
            get;
            set;
        }

        public SortDirection SortDirection
        {
            get;
            set;
        }
    }

    public class DojoDataGridRequestBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            DojoDataGridRequest dataGridRequest = new DojoDataGridRequest();

            int[] bounds =
                (from string item in controllerContext.HttpContext.Request.Headers["Range"].Split('=')[1].Split('-')
                 select int.Parse(item)).ToArray();

            dataGridRequest.LowerBound = bounds[0];
            dataGridRequest.UpperBound = bounds[1];

			for (int i = 0; i < controllerContext.HttpContext.Request.QueryString.Count; i++)
			{
				string key = controllerContext.HttpContext.Request.QueryString.GetKey(i);
				string value = controllerContext.HttpContext.Request.QueryString.Get(i);

				if (key == null)
				{
					if (value.StartsWith("sort("))
						dataGridRequest.SortingColumns.Add(
							new SortingColumn
								{
									ColumnName = value.Substring(6, value.Length - 7),
									SortDirection = value.Substring(5, 1) == "-"
									                	? SortDirection.Descending
									                	: SortDirection.Ascending
								});
				}

				else
					dataGridRequest.Filters[key] = value;
			}

        	return dataGridRequest;
        }
    }
}
