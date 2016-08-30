using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA
{
    public partial class Negotiation_Profile_Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                this.cmc1.sessionId = base.Request.QueryString["id"].ToString();
            }
            else
            {
                Response.Redirect("~/index.aspx");
            }
        }
    }
}