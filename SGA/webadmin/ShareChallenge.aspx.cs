using DataTier;
using SGA.App_Code;
using SGA.controls;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA.webadmin
{
    public partial class ShareChallenge : Page
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
                this.BindGrid();
            }
        }

        private void BindGrid()
        {
            string strOrderBy = " insDt desc ";
            if (this.SortExpression.Length > 0)
            {
                strOrderBy = (this.SortOrder ? (this.SortExpression + " Asc") : (this.SortExpression + " Desc"));
            }
            SqlParameter[] param = new SqlParameter[]
			{
				new SqlParameter("@orderBy", strOrderBy)
			};
            this.dtgList.DataSource = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetAllShareDetails", param);
            this.dtgList.DataBind();
        }

        protected void dtgList_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblSenddate = (Label)e.Item.FindControl("lblSenddate");
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
    }
}
