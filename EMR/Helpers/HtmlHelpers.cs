using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Net;

namespace EMR.Helpers
{
    public static class HtmlHelpers
    {
        public static HtmlString ImageOrDefault(this IHtmlHelper helper, string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "HEAD";

            try
            {
                request.GetResponse();
                return new HtmlString(url);
            }
            catch
            {
                return new HtmlString("/img/0.jpg");
            }
        }
    }
}
