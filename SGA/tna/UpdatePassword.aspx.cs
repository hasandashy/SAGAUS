using CookComputing.XmlRpc;
using DataTier;
using InfusionSoftDotNet;
using SGA.App_Code;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SGA.tna
{
    public partial class UpdatePassword : Page
    {
        

        protected void Page_Load(object sender, System.EventArgs e)
        {
            SGACommon.AddPageTitle(this.Page, "Update password", "");
            if (!base.IsPostBack)
            {
                MasterPage mp = this.Page.Master;
                if (mp != null)
                {
                    UserControl uc = (UserControl)mp.FindControl("header1");
                    if (uc != null)
                    {
                        Panel pnlTopMenu = (Panel)uc.FindControl("pnlTopMenu");
                        if (pnlTopMenu != null)
                        {
                            pnlTopMenu.Visible = false;
                        }
                    }
                }
                this.lblName.Text = "Hi " + SGACommon.GetName() + "!";
                this.LoadProfile();
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
                    if (System.Convert.ToBoolean(ds.Tables[0].Rows[0]["passwordUpdated"].ToString()))
                    {
                        int complete = System.Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spUserProfileComplete", param));
                        base.Response.Redirect("~/tna/" + ((complete == 1) ? "MyProfile.aspx" : "default.aspx"), false);
                    }
                    this.password.Value = ds.Tables[0].Rows[0]["plainpassword"].ToString();
                    this.email.Value = ds.Tables[0].Rows[0]["email"].ToString();
                }
            }
        }

        [WebMethod]
        public static string UpdateNewPassword(string password)
        {
            string passwordSalt = SGACommon.CreateSalt(5);
            string passwordHash = SGACommon.CreatePasswordHash(password, passwordSalt);
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@password", SqlDbType.VarChar);
            param[0].Value = password;
            param[1] = new SqlParameter("@passwordHash", SqlDbType.VarChar);
            param[1].Value = passwordHash;
            param[2] = new SqlParameter("@passwordSalt", SqlDbType.VarChar);
            param[2].Value = passwordSalt;
            param[3] = new SqlParameter("@userId", SqlDbType.Int);
            param[3].Value = SGACommon.LoginUserInfo.userId;
            string[] strField = new string[]
			{
				"Id"
			};
    //        XmlRpcStruct[] resultFound = isdnAPI.findByEmail(SGACommon.LoginUserInfo.name, strField);
    //        if (resultFound.Length > 0)
    //        {
    //            int Id = System.Convert.ToInt32(resultFound[0]["Id"]);
    //            isdnAPI.dsUpdate("Contact", Id, new XmlRpcStruct
				//{
				//	{
				//		"_CSBPassword",
				//		password
				//	}
				//});
    //        }
            int result = System.Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spUpdatePassword", param));
            return "s";
        }
    }
}
