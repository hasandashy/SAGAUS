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
    public partial class ManageEmailTemplates : Page
    {
       

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.BindTemplates();
            }
        }

        private void BindTemplates()
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spManageTemplate", new SqlParameter[]
			{
				new SqlParameter("@flag", "0")
			});
            this.rptUsers.DataSource = ds;
            this.rptUsers.DataBind();
        }

        protected void rptUsers_ItemDataBound(object Sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblDate = (Label)e.Item.FindControl("lblDate");
                if (lblDate != null)
                {
                    lblDate.Text = SGACommon.ToAusTimeZone(System.Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "insDt").ToString())).ToString("dd/MM/yyyy HH:mm tt");
                }
            }
        }
    }
}
