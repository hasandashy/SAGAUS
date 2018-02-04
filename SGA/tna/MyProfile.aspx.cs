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
                    this.txtPhoneNo.Value = ((ds.Tables[0].Rows[0]["phone"].ToString() == "") ? "Phone" : ds.Tables[0].Rows[0]["phone"].ToString());
                    this.txtBranch.Value = ((ds.Tables[0].Rows[0]["branch"].ToString() == "") ? "branch" : ds.Tables[0].Rows[0]["branch"].ToString());
                    this.txtJobTitle.Value = ((ds.Tables[0].Rows[0]["jobTitle"].ToString() == "") ? "jobTitle" : ds.Tables[0].Rows[0]["jobTitle"].ToString());                                
                    SGACommon.SelectListItem(this.ddlJobRole, ds.Tables[0].Rows[0]["jobrole"].ToString());
                    SGACommon.SelectListItem(this.ddlAgency, ds.Tables[0].Rows[0]["agencyId"].ToString());
                    SGACommon.SelectListItem(this.ddlCentral, ds.Tables[0].Rows[0]["isCentralProcurement"].ToString());
                    SGACommon.SelectListItem(this.ddlCurrentJobClassification, ds.Tables[0].Rows[0]["jobClassification"].ToString());
                    SGACommon.SelectListItem(this.ddlExperience, ds.Tables[0].Rows[0]["experience"].ToString());
                    SGACommon.SelectListItem(this.ddlQualification, ds.Tables[0].Rows[0]["qualification"].ToString());
                    SGACommon.SelectListItem(this.ddlTimeAlloc, ds.Tables[0].Rows[0]["time"].ToString());
                    SGACommon.SelectListItem(this.ddlNature, ds.Tables[0].Rows[0]["nature"].ToString());
                    SGACommon.SelectListItem(this.ddlInfluence, ds.Tables[0].Rows[0]["size"].ToString());
                    SGACommon.SelectListItem(this.ddlContract, ds.Tables[0].Rows[0]["noOfContracts"].ToString());
                    SGACommon.SelectListItem(this.ddlRange, ds.Tables[0].Rows[0]["activities"].ToString());

                    this.Page.ClientScript.RegisterStartupScript(base.GetType(), "statusup", "setPassword('" + ds.Tables[0].Rows[0]["plainpassword"].ToString() + "');", true);
                }
            }
        }

        [WebMethod]
        public static string UpdateProfile(string fname, string lname, string phone, string password, int department, int central, int classification, int experience, int qualification, int time, int nature, int size, int noOfContracts, int activities, string branch, string jobTitle )
        {
            string passwordSalt = SGACommon.CreateSalt(5);
            string passwordHash = SGACommon.CreatePasswordHash(password, passwordSalt);
            SqlParameter[] param = new SqlParameter[19];
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
            param[5] = new SqlParameter("@phone", SqlDbType.VarChar);
            param[5].Value = phone;
            param[6] = new SqlParameter("@branch", SqlDbType.VarChar);
            param[6].Value = branch;
            param[7] = new SqlParameter("@jobTitle", SqlDbType.VarChar);
            param[7].Value = jobTitle;
            param[8] = new SqlParameter("@agencyId", SqlDbType.VarChar);
            param[8].Value = department;
            param[9] = new SqlParameter("@central", SqlDbType.VarChar);
            param[9].Value = central;
            param[10] = new SqlParameter("@classification", SqlDbType.VarChar);
            param[10].Value = classification;
            param[11] = new SqlParameter("@experience", SqlDbType.VarChar);
            param[11].Value = experience;
            param[12] = new SqlParameter("@qualification", SqlDbType.VarChar);
            param[12].Value = qualification;
            param[13] = new SqlParameter("@time", SqlDbType.VarChar);
            param[13].Value = time;
            param[14] = new SqlParameter("@nature", SqlDbType.VarChar);
            param[14].Value = nature;
            param[15] = new SqlParameter("@size", SqlDbType.VarChar);
            param[15].Value = size;
            param[16] = new SqlParameter("@numberOfContracts", SqlDbType.VarChar);
            param[16].Value = noOfContracts;
            param[17] = new SqlParameter("@activities", SqlDbType.VarChar);
            param[17].Value = activities;
            param[18] = new SqlParameter("@userId", SqlDbType.Int);
            param[18].Value = SGACommon.LoginUserInfo.userId;
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
                        Profile.GetOrganisation(department)
                    },
                    //{
                    //    "_Location",
                    //    Profile.GetLocation(locationId)
                    //},
                    //{
                    //    "_MegaCategory",
                    //    Profile.GetGoodsLevel(goodsId)
                    //},
                    //{
                    //    "_OrganisationDivision",
                    //    division
                    //},
                    {
                        "JobTitle",
                        jobTitle
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
