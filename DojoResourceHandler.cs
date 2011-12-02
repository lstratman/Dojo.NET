using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace Dojo.Net
{
    public class DojoResourceHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string resourceName = "Dojo.Net.Resources" + context.Request.Url.AbsolutePath.Substring(context.Request.Url.AbsolutePath.IndexOf("/dojo.axd") + 9).Replace("/", ".");
            Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);

            if (resourceStream == null)
            {
                context.Response.StatusCode = 404;
                return;
            }

            switch (resourceName.Substring(resourceName.LastIndexOf(".") + 1))
            {
                case "js":
                    context.Response.ContentType = "application/x-javascript";
                    break;

                case "css":
                    context.Response.ContentType = "text/css";
                    break;

                case "png":
                    context.Response.ContentType = "image/png";
                    break;

                case "gif":
                    context.Response.ContentType = "image/gif";
                    break;
            }

            byte[] buffer = new byte[102400];
            int bytesRead = 0;

            while ((bytesRead = resourceStream.Read(buffer, 0, buffer.Length)) > 0)
                context.Response.OutputStream.Write(buffer, 0, bytesRead);
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}
