using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace MvcApplicationForMSTranslateExample.CodeLibrary
{
    public static class HtmlCommonIncludeGenerator
    {
        public static string getHomeHtmlAnchor ()
        {
            StringBuilder sb= new StringBuilder();
            sb.Append("<h3>home return link</h3>");
            sb.AppendLine();
            sb.Append("<a href=\"/\">Home</a>");
            return sb.ToString();
        }
        public static string getTESTWord ()
        {
            return "TEST";
        }
        
    }
}