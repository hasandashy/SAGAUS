using SGA.App_Code;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SGA.tna
{
    public partial class cma_assessment_instructions : Page
    {
        protected int directSend = 1;

        
        protected void Page_Load(object sender, System.EventArgs e)
        {
            SGACommon.AddPageTitle(this.Page, "Commercial Contract Management Assessment Instructions Page", "");
            SGACommon.IsTakeTest("takeCMATest");
            if (!base.IsPostBack)
            {
                this.lblName.Text = "Hi " + SGACommon.GetName() + "!";
            }
        }
    }
}
