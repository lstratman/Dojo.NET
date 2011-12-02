using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.WebPages;

namespace Dojo.Net.Mvc
{
    public static class Dojo
    {
        public static MvcHtmlString Content<T>(T model, Func<T, HelperResult> content)
        {
            ResourceManager resourceManager = new ResourceManager();
            StringWriter textWriter = new StringWriter();
            Page pageHolder = new Page();

            pageHolder.Controls.Add(resourceManager);
            HttpContext.Current.Items["__DojoResourceManager"] = resourceManager;

            string contentOutput = content(model).ToHtmlString();
            HttpContext.Current.Server.Execute(pageHolder, textWriter, false);

            return new MvcHtmlString(textWriter + contentOutput);
        }
    }
}
