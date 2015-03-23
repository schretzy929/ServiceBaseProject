using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiceBase.Pages
{
    public partial class CompatiblityIssue : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string userBrowser = Request.UserAgent;
            if (!userBrowser.Contains("MSIE 7.0"))
            {
                Response.Redirect("home.aspx");
            }
        }

        protected void FixedBtn_Click(object sender, EventArgs e)
        {
            string userBrowser = Request.UserAgent;
            if(!userBrowser.Contains("MSIE 7.0"))
            {
                Response.Redirect("home.aspx");
            }
        }
    }
}