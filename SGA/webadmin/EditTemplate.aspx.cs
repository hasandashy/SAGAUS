using DataTier;
using FredCK.FCKeditorV2;
using SGA.controls;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.webadmin
{
    public partial class EditTemplate : Page
    {
        private int id = 0;

        
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (base.Request.QueryString["id"] != null)
            {
                this.id = System.Convert.ToInt32(base.Request.QueryString["id"].ToString());
            }
            if (this.id <= 0)
            {
                base.Response.Redirect("ManageEmailTemplates.aspx", false);
            }
            if (!base.IsPostBack)
            {
                this.GetTemplateData();
            }
        }

        private void GetTemplateData()
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageTemplate", new SqlParameter[]
			{
				new SqlParameter("@flag", "0"),
				new SqlParameter("@id", this.id)
			});
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    this.txtTitle.Text = ds.Tables[0].Rows[0]["title"].ToString();
                    this.txtMailBody.Value = base.Server.HtmlDecode(ds.Tables[0].Rows[0]["emailBody"].ToString());
                    this.txtSubject.Text = ds.Tables[0].Rows[0]["subject"].ToString();
                }
                else
                {
                    base.Response.Redirect("ManageEmailTemplates.aspx", false);
                }
            }
            else
            {
                base.Response.Redirect("ManageEmailTemplates.aspx", false);
            }
        }

        protected void iBtnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (this.Page.IsValid)
            {
                SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spManageTemplate", new SqlParameter[]
				{
					new SqlParameter("@flag", "1"),
					new SqlParameter("@id", this.id),
					new SqlParameter("@title", this.txtTitle.Text.Trim()),
					new SqlParameter("@body", base.Server.HtmlEncode(this.txtMailBody.Value.Trim())),
					new SqlParameter("@insDt", System.DateTime.UtcNow),
					new SqlParameter("@subject", this.txtSubject.Text.Trim())
				});
                base.Response.Redirect("ManageEmailTemplates.aspx", false);
            }
        }
    }
}
