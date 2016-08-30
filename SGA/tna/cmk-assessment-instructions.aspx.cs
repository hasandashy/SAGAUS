using SGA.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.tna
{
    public partial class cmk_assessment_instructions : System.Web.UI.Page
    {
        protected int directSend = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            SGACommon.AddPageTitle(this.Page, "Commercial Contract Management Assessment Instructions Page", "");
            SGACommon.IsTakeTest("viewCmkTest");
            if (!base.IsPostBack)
            {
                this.lblName.Text = "Hi " + SGACommon.GetName() + "!";
            }
        }
    }
}