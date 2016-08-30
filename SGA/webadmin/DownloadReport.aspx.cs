using AjaxControlToolkit;
using DataTier;
using SGA.controls;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SGA.webadmin
{
    public partial class DownloadReport : Page
    {
        
        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.txtFrom.Attributes.Add("readonly", "readonly");
            this.txtTo.Attributes.Add("readonly", "readonly");
            if (!base.IsPostBack)
            {
            }
        }

        protected void btnExport_Click(object sender, System.EventArgs e)
        {
            this.lblError.Text = "";
            int status = 0;
            if (this.rdoAll.Checked)
            {
                status = 0;
            }
            else if (this.rdoCompleted.Checked)
            {
                status = 1;
            }
            else if (this.rdoUncomplete.Checked)
            {
                status = 2;
            }
            SqlParameter[] param = new SqlParameter[]
			{
				new SqlParameter("@firstname", this.txtFname.Value.Trim()),
				new SqlParameter("@lastname", this.txtLname.Value.Trim()),
				new SqlParameter("@company", ""),
				new SqlParameter("@email", this.txtEmail.Value.Trim()),
				new SqlParameter("@dateFrom", this.txtFrom.Text.Trim()),
				new SqlParameter("@dateTo", this.txtTo.Text.Trim()),
				null,
				null,
				new SqlParameter("@status", status)
			};
            int num = System.Convert.ToInt32(this.ddlAssessmentType.SelectedValue);
            switch (num)
            {
                case 2:
                    param[6] = new SqlParameter("@programType", "4");
                    param[7] = new SqlParameter("@SGAType", "SSA");
                    break;
                case 3:
                    param[6] = new SqlParameter("@programType", "4");
                    param[7] = new SqlParameter("@SGAType", "BA");
                    break;
                default:
                    if (num != 7)
                    {
                        this.lblError.Text = "Select assessment type to download report";
                    }
                    else
                    {
                        param[6] = new SqlParameter("@programType", "4");
                        param[7] = new SqlParameter("@SGAType", "CMA");
                    }
                    break;
            }
            SqlHelper.ExecuteNonQuery(new SqlConnection(ConfigurationManager.ConnectionStrings["EmailConfigConn"].ToString()), "spInsertSearchPara", param);
            this.lblError.Text = "Your report download has started - this request may take up to 1 hour to process.";
        }

        private void DownloadSSAResults()
        {
            try
            {
                string html = "";
                string value = "";
                int status = 0;
                if (this.rdoAll.Checked)
                {
                    status = 0;
                }
                else if (this.rdoCompleted.Checked)
                {
                    status = 1;
                }
                else if (this.rdoUncomplete.Checked)
                {
                    status = 2;
                }
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSsaReportUsers", new SqlParameter[]
				{
					new SqlParameter("@firstname", this.txtFname.Value.Trim()),
					new SqlParameter("@lastname", this.txtLname.Value.Trim()),
					null,
					new SqlParameter("@email", this.txtEmail.Value.Trim()),
					new SqlParameter("@dateFrom", this.txtFrom.Text.Trim()),
					new SqlParameter("@dateTo", this.txtTo.Text.Trim()),
					new SqlParameter("@status", status)
				});
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            html += ds.Tables[0].Rows[i]["innerValue"].ToString();
                        }
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            value += ds.Tables[1].Rows[i]["Value"].ToString();
                        }
                    }
                }
                this.downloadFile(value.Replace("#", html), "SSAAllResult");
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private void DownloadBAResults()
        {
            try
            {
                string html = "";
                string value = "";
                int status = 0;
                if (this.rdoAll.Checked)
                {
                    status = 0;
                }
                else if (this.rdoCompleted.Checked)
                {
                    status = 1;
                }
                else if (this.rdoUncomplete.Checked)
                {
                    status = 2;
                }
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetBaReportUsers", new SqlParameter[]
				{
					new SqlParameter("@firstname", this.txtFname.Value.Trim()),
					new SqlParameter("@lastname", this.txtLname.Value.Trim()),
					null,
					new SqlParameter("@email", this.txtEmail.Value.Trim()),
					new SqlParameter("@dateFrom", this.txtFrom.Text.Trim()),
					new SqlParameter("@dateTo", this.txtTo.Text.Trim()),
					new SqlParameter("@status", status)
				});
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            html += ds.Tables[0].Rows[i]["innerValue"].ToString();
                        }
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            value += ds.Tables[1].Rows[i]["Value"].ToString();
                        }
                    }
                }
                this.downloadFile(value.Replace("#", html), "BAAllResult");
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private void DownloadMPResults()
        {
            try
            {
                string html = "";
                string value = "";
                int status = 0;
                if (this.rdoAll.Checked)
                {
                    status = 0;
                }
                else if (this.rdoCompleted.Checked)
                {
                    status = 1;
                }
                else if (this.rdoUncomplete.Checked)
                {
                    status = 2;
                }
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetMPReportUsers", new SqlParameter[]
				{
					new SqlParameter("@firstname", this.txtFname.Value.Trim()),
					new SqlParameter("@lastname", this.txtLname.Value.Trim()),
					null,
					new SqlParameter("@email", this.txtEmail.Value.Trim()),
					new SqlParameter("@dateFrom", this.txtFrom.Text.Trim()),
					new SqlParameter("@dateTo", this.txtTo.Text.Trim()),
					new SqlParameter("@status", status)
				});
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            html += ds.Tables[0].Rows[i]["innerValue"].ToString();
                        }
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            value += ds.Tables[1].Rows[i]["Value"].ToString();
                        }
                    }
                }
                this.downloadFile(value.Replace("#", html), "MPAllResult");
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private void DownloadNPResults()
        {
            try
            {
                string html = "";
                string value = "";
                int status = 0;
                if (this.rdoAll.Checked)
                {
                    status = 0;
                }
                else if (this.rdoCompleted.Checked)
                {
                    status = 1;
                }
                else if (this.rdoUncomplete.Checked)
                {
                    status = 2;
                }
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetNpReportUsers", new SqlParameter[]
				{
					new SqlParameter("@firstname", this.txtFname.Value.Trim()),
					new SqlParameter("@lastname", this.txtLname.Value.Trim()),
					null,
					new SqlParameter("@email", this.txtEmail.Value.Trim()),
					new SqlParameter("@dateFrom", this.txtFrom.Text.Trim()),
					new SqlParameter("@dateTo", this.txtTo.Text.Trim()),
					new SqlParameter("@status", status)
				});
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            html += ds.Tables[0].Rows[i]["innerValue"].ToString();
                        }
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            value += ds.Tables[1].Rows[i]["Value"].ToString();
                        }
                    }
                }
                this.downloadFile(value.Replace("#", html), "NPAllResult");
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private void DownloadCMAResults()
        {
            try
            {
                string html = "";
                string value = "";
                int status = 0;
                if (this.rdoAll.Checked)
                {
                    status = 0;
                }
                else if (this.rdoCompleted.Checked)
                {
                    status = 1;
                }
                else if (this.rdoUncomplete.Checked)
                {
                    status = 2;
                }
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetCMAReportUsers", new SqlParameter[]
				{
					new SqlParameter("@firstname", this.txtFname.Value.Trim()),
					new SqlParameter("@lastname", this.txtLname.Value.Trim()),
					null,
					new SqlParameter("@email", this.txtEmail.Value.Trim()),
					new SqlParameter("@dateFrom", this.txtFrom.Text.Trim()),
					new SqlParameter("@dateTo", this.txtTo.Text.Trim()),
					new SqlParameter("@status", status)
				});
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            html += ds.Tables[0].Rows[i]["innerValue"].ToString();
                        }
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            value += ds.Tables[1].Rows[i]["Value"].ToString();
                        }
                    }
                }
                this.downloadFile(value.Replace("#", html), "CMAAllResult");
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private void downloadFile(string html, string name)
        {
            TableRow tRow = new TableRow();
            tRow.CssClass = "tableRow";
            this.excel.Rows.Add(tRow);
            TableCell tCell = new TableCell();
            tCell.Text = html;
            tCell.CssClass = "tableCell";
            tRow.Cells.Add(tCell);
            base.Response.Clear();
            base.Response.AddHeader("content-disposition", "attachment;filename=" + name + ".xls");
            base.Response.Charset = "";
            base.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            base.Response.ContentType = "application/vnd.xls";
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    this.excel.RenderControl(htw);
                    base.Response.Write(sw.ToString());
                    base.Response.End();
                }
            }
        }
    }
}
