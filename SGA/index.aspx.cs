using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (HttpContext.Current.User.IsInRole("User"))
                {
                    base.Response.Redirect("~/tna/updatepassword.aspx", false);
                }
                else if (HttpContext.Current.User.IsInRole("Manager"))
                {
                    base.Response.Redirect("~/Manager/Dashboard.aspx", false);
                }
                else if (HttpContext.Current.User.IsInRole("Administrator"))
                {
                    base.Response.Redirect("~/webadmin/DashBoard.aspx", false);
                }
            }
            else
            {
                base.Response.Redirect("default.aspx", false);
            }
        }
    }
}