using AjaxControlToolkit;
using DataTier;
using SGA.App_Code;
using SGA.controls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SGA.webadmin
{
    public partial class EmailTracker : Page
    {
        

        public string SortExpression
        {
            get
            {
                return (this.ViewState["SortExpression"] == null) ? "" : this.ViewState["SortExpression"].ToString();
            }
            set
            {
                this.ViewState["SortExpression"] = value;
            }
        }

        public bool SortOrder
        {
            get
            {
                return this.ViewState["SortOrder"] != null && System.Convert.ToBoolean(this.ViewState["SortOrder"].ToString());
            }
            set
            {
                this.ViewState["SortOrder"] = value;
            }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.txtSenddate.Attributes.Add("readonly", "readonly");
            if (!base.IsPostBack)
            {
                this.BindDropDown();
                this.BindGrid();
            }
        }

        private void BindDropDown()
        {
            SqlParameter[] param = new SqlParameter[]
			{
				new SqlParameter("@flag", "4")
			};
            this.ddlEmailTemplate.DataSource = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spEmailTracking", param);
            this.ddlEmailTemplate.DataTextField = "subject";
            this.ddlEmailTemplate.DataValueField = "id";
            this.ddlEmailTemplate.DataBind();
            SGACommon.InsertDefaultItem(this.ddlEmailTemplate, "Select email template", "0");
        }

        private void BindGrid()
        {
            string strOrderBy = " sendDate desc ";
            if (this.SortExpression.Length > 0)
            {
                strOrderBy = (this.SortOrder ? (this.SortExpression + " Asc") : (this.SortExpression + " Desc"));
            }
            SqlParameter[] param;
            if (this.txtSenddate.Text.Length > 0)
            {
                param = new SqlParameter[5];
            }
            else
            {
                param = new SqlParameter[4];
            }
            param[0] = new SqlParameter("@flag", "2");
            param[1] = new SqlParameter("@emailReceiver", this.txtEmail.Value.Trim());
            param[2] = new SqlParameter("@Id", System.Convert.ToInt32(this.ddlEmailTemplate.SelectedValue));
            param[3] = new SqlParameter("@orderBy", strOrderBy);
            if (this.txtSenddate.Text.Length > 0)
            {
                System.Globalization.DateTimeFormatInfo dtfi = new System.Globalization.DateTimeFormatInfo();
                dtfi.ShortDatePattern = "dd/MM/yyyy";
                dtfi.DateSeparator = "/";
                param[4] = new SqlParameter("@sendDate", System.Convert.ToDateTime(this.txtSenddate.Text.Trim(), dtfi));
            }
            this.dtgList.DataSource = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spEmailTracking", param);
            this.dtgList.DataBind();
        }

        protected void lnkSearch_Click(object sender, System.EventArgs e)
        {
            this.dtgList.CurrentPageIndex = 0;
            this.BindGrid();
        }

        protected void lnkCancel_Click(object sender, System.EventArgs e)
        {
            this.dtgList.CurrentPageIndex = 0;
            this.ddlEmailTemplate.SelectedIndex = -1;
            this.txtSenddate.Text = "";
            this.txtEmail.Value = "";
            this.BindGrid();
        }

        protected void dtgList_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblSenddate = (Label)e.Item.FindControl("lblSenddate");
                if (lblSenddate != null)
                {
                    lblSenddate.Text = SGACommon.ToAusTimeZone(System.Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "sendDate"))).ToString("dd MMM yyyy HH:mm tt");
                }
            }
        }

        protected void dtgList_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spEmailTracking", new SqlParameter[]
			{
				new SqlParameter("@flag", "3"),
				new SqlParameter("@Id", e.CommandArgument)
			});
            if (e.CommandName == "Email")
            {
                this.pnlDetail.Visible = true;
                this.pnlList.Visible = false;
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        this.lblDate.Text = SGACommon.ToAusTimeZone(System.Convert.ToDateTime(ds.Tables[0].Rows[0]["senddate"])).ToString("dd MMM yyyy HH:mm tt");
                        this.lblEmailBody.Text = ds.Tables[0].Rows[0]["emailBody"].ToString();
                        this.lblReceiver.Text = ds.Tables[0].Rows[0]["emailReceiver"].ToString();
                        this.lblSubject.Text = ds.Tables[0].Rows[0]["subject"].ToString();
                    }
                }
            }
            else if (e.CommandName == "Resend")
            {
                MailSending.SendMail(ConfigurationManager.AppSettings["nameDisplay"].ToString(), ConfigurationManager.AppSettings["UserName"].ToString(), ds.Tables[0].Rows[0]["emailReceiver"].ToString(), ds.Tables[0].Rows[0]["subject"].ToString(), ds.Tables[0].Rows[0]["emailBody"].ToString(), "");
                this.lblMsg.Text = "Email resend successfully";
            }
        }

        protected void btnBack_Click(object sender, System.EventArgs e)
        {
            this.pnlDetail.Visible = false;
            this.pnlList.Visible = true;
        }

        protected void dtgList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            this.dtgList.CurrentPageIndex = e.NewPageIndex;
            this.BindGrid();
        }

        protected void dtgList_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            if (e.SortExpression.ToString() == this.SortExpression)
            {
                this.SortOrder = !this.SortOrder;
            }
            else
            {
                this.SortOrder = true;
            }
            this.SortExpression = e.SortExpression;
            int i = 0;
            string strOrder = this.SortOrder ? "ASC" : "DESC";
            foreach (DataGridColumn col in this.dtgList.Columns)
            {
                if (col.SortExpression == e.SortExpression)
                {
                    this.dtgList.Columns[i].HeaderStyle.CssClass = "gridHeaderSort" + strOrder;
                }
                else
                {
                    this.dtgList.Columns[i].HeaderStyle.CssClass = "gridHeader";
                }
                i++;
            }
            this.BindGrid();
        }

        [WebMethod]
        public static System.Collections.Generic.List<string> GetAutoCompleteData(string email)
        {
            System.Collections.Generic.List<string> result = new System.Collections.Generic.List<string>();
            SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "spSearchEmail", new SqlParameter[]
			{
				new SqlParameter("@email", email)
			});
            while (dr.Read())
            {
                result.Add(dr["email"].ToString());
            }
            return result;
        }
    }
}
