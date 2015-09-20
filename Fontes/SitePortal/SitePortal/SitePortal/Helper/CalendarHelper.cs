using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Text;

namespace SitePortal.Helper
{
    public static class CalendarHelper
    {
        public static HtmlString RenderControl(int inc)
        {
            Calendar cal = new Calendar();
            
            Page page = new Page();
            page.Controls.Add(cal);

            using (StringWriter sw = new StringWriter())
            {
                HttpContext.Current.Server.Execute("~/WebForm/WebForm1.aspx?inc=" + inc, sw, false);
                return new HtmlString(sw.ToString());
            }

        }
    }
}