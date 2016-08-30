using SGA.App_Code;
using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SGA.webadmin
{
    public partial class AdminMaster : MasterPage
    {
        
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.lblName.Text = SGACommon.GetName();
            }
        }

        protected void lnkLogout_Click(object sender, System.EventArgs e)
        {
            base.Session.Abandon();
            FormsAuthentication.SignOut();
            base.Response.Redirect("~/index.aspx");
        }
    }
}
