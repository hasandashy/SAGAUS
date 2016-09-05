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
    public partial class MyProfile : Page
    {
        

        protected void Page_Load(object sender, System.EventArgs e)
        {
            SGACommon.AddPageTitle(this.Page, "The individuals profile page", "");
            this.ddlJobRole.Attributes.Add("disabled", "disabled");
            this.ddlJobLevel.Attributes.Add("disabled", "disabled");
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
                    this.fname.Value = ds.Tables[0].Rows[0]["firstname"].ToString();
                    this.lname.Value = ds.Tables[0].Rows[0]["lastname"].ToString();
                    this.email.Value = ds.Tables[0].Rows[0]["email"].ToString();
                    this.txtDivision.Value = ((ds.Tables[0].Rows[0]["Division"].ToString() == "") ? "Division" : ds.Tables[0].Rows[0]["Division"].ToString());
                    this.txtPhoneNo.Value = ((ds.Tables[0].Rows[0]["phone"].ToString() == "") ? "Phone" : ds.Tables[0].Rows[0]["phone"].ToString());
                    this.txtPosition.Value = ((ds.Tables[0].Rows[0]["Position"].ToString() == "") ? "Position" : ds.Tables[0].Rows[0]["Position"].ToString());
                    SGACommon.SelectListItem(this.ddlAgency, ds.Tables[0].Rows[0]["agencyId"].ToString());
                    SGACommon.SelectListItem(this.ddlJobLevel, ds.Tables[0].Rows[0]["jobLevel"].ToString());
                    SGACommon.SelectListItem(this.ddlJobRole, ds.Tables[0].Rows[0]["jobrole"].ToString());
                    SGACommon.SelectListItem(this.ddlLocation, ds.Tables[0].Rows[0]["locationId"].ToString());
                    SGACommon.SelectListItem(this.ddlGoods, ds.Tables[0].Rows[0]["goodsId"].ToString());
                    this.Page.ClientScript.RegisterStartupScript(base.GetType(), "statusup", "setPassword('" + ds.Tables[0].Rows[0]["plainpassword"].ToString() + "');", true);
                }
            }
        }

        [WebMethod]
        public static string UpdateProfile(string fname, string lname, int agencyId, string phone, string division, int locationId, string position, int goodsId, string password)
        {
            string passwordSalt = SGACommon.CreateSalt(5);
            string passwordHash = SGACommon.CreatePasswordHash(password, passwordSalt);
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@password", SqlDbType.VarChar);
            param[0].Value = password;
            param[1] = new SqlParameter("@firstName", SqlDbType.VarChar);
            param[1].Value = fname;
            param[2] = new SqlParameter("@lastName", SqlDbType.VarChar);
            param[2].Value = lname;
            param[3] = new SqlParameter("@passwordHash", SqlDbType.VarChar);
            param[3].Value = passwordHash;
            param[4] = new SqlParameter("@passwordSalt", SqlDbType.VarChar);
            param[4].Value = passwordSalt;
            param[5] = new SqlParameter("@agencyId", SqlDbType.Int);
            param[5].Value = agencyId;
            param[6] = new SqlParameter("@phone", SqlDbType.VarChar);
            param[6].Value = phone;
            param[7] = new SqlParameter("@division", SqlDbType.VarChar);
            param[7].Value = division;
            param[8] = new SqlParameter("@locationId", SqlDbType.Int);
            param[8].Value = locationId;
            param[9] = new SqlParameter("@position", SqlDbType.VarChar);
            param[9].Value = position;
            param[10] = new SqlParameter("@goodsId", SqlDbType.Int);
            param[10].Value = goodsId;
            param[11] = new SqlParameter("@userId", SqlDbType.Int);
            param[11].Value = SGACommon.LoginUserInfo.userId;
            int result = System.Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spUpdateProfileSelf", param));
            string[] strField = new string[]
			{
				"Id"
			};
            XmlRpcStruct[] resultFound = isdnAPI.findByEmail(SGACommon.LoginUserInfo.name, strField);
            if (resultFound.Length > 0)
            {
                int Id = System.Convert.ToInt32(resultFound[0]["Id"]);
                isdnAPI.dsUpdate("Contact", Id, new XmlRpcStruct
				{
					{
						"FirstName",
						fname
					},
					{
						"LastName",
						lname
					},
					{
						"Email",
						SGACommon.LoginUserInfo.name
					},
					{
						"OwnerID",
						"50036"
					},
                    {
						"_CSBPassword",
						password
					},
					{
						"_YourOrganisation",
						Profile.GetOrganisation(agencyId)
					},
					{
						"_Location",
						Profile.GetLocation(locationId)
					},
					{
						"_MegaCategory",
						Profile.GetGoodsLevel(goodsId)
					},
					{
						"_OrganisationDivision",
						division
					},
					{
						"JobTitle",
						position
					},
					{
						"_Phone1",
						phone
					}
				});
            }
            return "s";
        }
    }
}
