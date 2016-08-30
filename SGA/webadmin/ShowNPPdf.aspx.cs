using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.webadmin
{
    public partial class ShowNPPdf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.cmc1.testId = System.Convert.ToInt32(base.Request.QueryString["Id"].ToString());
            this.cmc1.userId = System.Convert.ToInt32(base.Request.QueryString["userId"].ToString());
        }
    }
}