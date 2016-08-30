using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGA.App_Code;
using System.Data;
using DataTier;
using System.Data.SqlClient;

namespace SGA.tna
{
    public partial class np_assessment_instructions : System.Web.UI.Page
    {
        protected int directSend = 0;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            SGACommon.AddPageTitle(this.Page, "Negotiation Profile Instructions Page", "");
            SGACommon.IsTakeTest("viewNPTest");
            if (!base.IsPostBack)
            {
                this.lblName.Text = "Hi " + SGACommon.GetName() + "!";
                this.PassProfile();
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