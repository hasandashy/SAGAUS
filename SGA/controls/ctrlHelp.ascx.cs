using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using SGA.App_Code;
using DataTier;

namespace SGA.controls
{
    public partial class ctrlHelp : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                if (HttpContext.Current.User.Identity.IsAuthenticated) {
                    LoadProfile();
                }
                
            }
        }

        private void LoadProfile()
        {
            SqlParameter[] param = new SqlParameter[]
			{
				new SqlParameter("@userId", SqlDbType.Int)
			};
            param[0].Value = SGACommon.LoginUserInfo.userId;
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetProfileDetails", param);
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    this.fname.Value = ds.Tables[0].Rows[0]["firstname"].ToString();
                    this.lname.Value = ds.Tables[0].Rows[0]["lastname"].ToString();
                    this.email.Value = ds.Tables[0].Rows[0]["email"].ToString();
                    this.company.SelectedValue = ds.Tables[0].Rows[0]["agencyId"].ToString(); 
                    
                }
            }
        }
    }
}