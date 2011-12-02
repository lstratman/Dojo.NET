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

            return dataGridRequest;
        }
    }
}
