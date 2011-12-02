using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Dojo.Net.Mvc
{
    public class DojoDataGridResult : JsonResult
    {
        protected DojoDataGridRequest _dataGridRequest = null;
        protected int _totalCount = 0;

        public DojoDataGridResult(IEnumerable data, DojoDataGridRequest dataGridRequest, int totalCount)
        {
            Data = data;
            JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            _dataGridRequest = dataGridRequest;
            _totalCount = totalCount;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Headers["Content-Range"] = "items " + _dataGridRequest.LowerBound + "-" +
                                                                    Math.Min(_totalCount, _dataGridRequest.UpperBound) +
                                                                    "/" +
                                                                    _totalCount;

            base.ExecuteResult(context);
        }
    }
}
