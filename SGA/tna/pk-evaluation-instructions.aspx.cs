using DataTier;
using SGA.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.tna
{
    public partial class pk_evaluation_instructions : System.Web.UI.Page
    {
        protected int directSend = 0;


        protected void Page_Load(object sender, System.EventArgs e)
        {
            SGACommon.AddPageTitle(this.Page, "Procurement Knowledge Evaluation Instructions Page", "");
            SGACommon.IsTakeTest("viewPkeTest");
            if (!base.IsPostBack)
            {
                this.lblName.Text = "Hi " + SGACommon.GetName() + "!";
                this.PassProfile();
            }
        }

        private void PassProfile()
        {
            int count = System.Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spProfileComplete", new SqlParameter[]
			{
				new SqlParameter("@userId", SGACommon.LoginUserInfo.userId)
			}));
            this.directSend = ((count <= 0) ? 0 : 1);
        }
    }
}