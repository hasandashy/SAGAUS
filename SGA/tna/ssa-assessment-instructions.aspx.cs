using DataTier;
using SGA.App_Code;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SGA.tna
{
    public partial class ssa_assessment_instructions : Page
    {
        protected int directSend = 1;

       

        protected void Page_Load(object sender, System.EventArgs e)
        {
            SGACommon.AddPageTitle(this.Page, "Procurement Assessment Instructions Page", "");
            SGACommon.IsTakeTest("viewTnaTest");
            if (!base.IsPostBack)
            {
                this.lblName.Text = "Hi " + SGACommon.GetName() + "!";
                //this.PassProfile();
            }
        }

        private void PassProfile()
        {
            int count = System.Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spUserProfileComplete", new SqlParameter[]
			{
				new SqlParameter("@userId", SGACommon.LoginUserInfo.userId)
			}));
            this.directSend = count;
        }
    }
}
