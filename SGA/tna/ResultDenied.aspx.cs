using DataTier;
using SGA.App_Code;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace SGA.tna
{
    public partial class ResultDenied : Page
    {
        protected bool isSgaResult = false;

        protected bool isTnaResult = false;

        protected bool isPmpResult = false;

        protected bool isDmpResult = false;

        protected bool isNpResult = false;

        protected bool isCMAResult = false;

       
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!base.IsPostBack)
            {
                DataSet dsPermission = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetPremission", new SqlParameter[]
				{
					new SqlParameter("@userId", SGACommon.LoginUserInfo.userId)
				});
                if (dsPermission != null)
                {
                    if (dsPermission.Tables.Count > 0 && dsPermission.Tables[0].Rows.Count > 0)
                    {
                        this.isSgaResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewSgaResult"].ToString());
                        this.isTnaResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewTnaResult"].ToString());
                        this.isPmpResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewPMPResult"].ToString());
                        this.isDmpResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewDMPResult"].ToString());
                        this.isNpResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewNPResult"].ToString());
                        this.isCMAResult = System.Convert.ToBoolean(dsPermission.Tables[0].Rows[0]["viewCMAResult"].ToString());
                    }
                }
                this.spSkills.Attributes["class"] = (this.isTnaResult ? "" : "lock");
                this.spBehaviour.Attributes["class"] = (this.isPmpResult ? "" : "lock");
                this.spCMA.Attributes["class"] = (this.isCMAResult ? "" : "lock");
            }
        }
    }
}
