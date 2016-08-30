using DataTier;
using SGA.App_Code;
using SGA.controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SGA.webadmin
{
    public partial class BrowserTracker : Page
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
				new SqlParameter("@flag", "1"),
				new SqlParameter("@email", ""),
				new SqlParameter("@browserName", "")
			};
            this.ddlEmailTemplate.DataSource = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetBrowserName", param);
            this.ddlEmailTemplate.DataTextField = "browserName";
            this.ddlEmailTemplate.DataValueField = "browserName";
            this.ddlEmailTemplate.DataBind();
            SGACommon.InsertDefaultItem(this.ddlEmailTemplate, "Select Browser", "");
        }

        private void BindGrid()
        {
            SqlParameter[] param = new SqlParameter[]
			{
				new SqlParameter("@flag", "3"),
				new SqlParameter("@email", this.txtEmail.Value),
				new SqlParameter("@browserName", this.ddlEmailTemplate.SelectedValue)
			};
            this.dtgList.DataSource = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetBrowserName", param);
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
            this.txtEmail.Value = "";
            this.BindGrid();
        }

        protected void dtgList_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblSenddate = (Label)e.Item.FindControl("lblSenddate");
                Label lblOsname = (Label)e.Item.FindControl("lblOsname");
                string strUserAgent = DataBinder.Eval(e.Item.DataItem, "userAgent").ToString();
                string os;
                if (strUserAgent.IndexOf("Windows NT 5.1") > 0)
                {
                    os = "Windows XP";
                }
                else if (strUserAgent.IndexOf("Windows NT 6.0") > 0)
                {
                    os = "Windows VISTA";
                }
                else if (strUserAgent.IndexOf("Windows NT 6.1") > 0)
                {
                    os = "Windows 7";
                }
                else if (strUserAgent.IndexOf("Windows NT 6.3") > 0)
                {
                    os = "Windows 8";
                }
                else if (strUserAgent.IndexOf("Intel Mac OS X") > 0)
                {
                    os = "Intel Mac OS X";
                }
                else
                {
                    os = "You are using older version of Windows or Mac OS";
                }
                lblOsname.Text = os.ToString();
                if (lblSenddate != null)
                {
                    lblSenddate.Text = SGACommon.ToAusTimeZone(System.Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "insDt"))).ToString("dd MMM yyyy HH:mm tt");
                }
            }
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
            SqlDataReader dr = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "spGetBrowserName", new SqlParameter[]
			{
				new SqlParameter("@flag", "2"),
				new SqlParameter("@email", email),
				new SqlParameter("@browserName", "")
			});
            while (dr.Read())
            {
                result.Add(dr["email"].ToString());
            }
            return result;
        }
    }
}
