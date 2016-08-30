using DataTier;
using SGA.App_Code;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SGA.webadmin
{
    public partial class ReportsDownload : Page
    {
       
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (base.Request.QueryString["id"] == "1")
                {
                    this.DownloadCMCResults();
                }
                else if (base.Request.QueryString["id"] == "2")
                {
                    this.DownloadSSAResults();
                }
                else if (base.Request.QueryString["id"] == "3")
                {
                    this.DownloadBAResults();
                }
                else if (base.Request.QueryString["id"] == "4")
                {
                    this.DownloadMPResults();
                }
                else if (base.Request.QueryString["id"] == "5")
                {
                    this.DownloadNPResults();
                }
            }
        }

        private void DownloadCMCResults()
        {
            try
            {
                int NoOfColumn = 0;
                string html = "";
                DataTable dtquestion = new DataTable();
                DataSet dsAll = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spCMCExcelExport", new SqlParameter[]
				{
					new SqlParameter("@firstname", ""),
					new SqlParameter("@lastname", ""),
					new SqlParameter("@company", ""),
					new SqlParameter("@userIds", ""),
					new SqlParameter("@dateFrom", ""),
					new SqlParameter("@dateTo", "")
				});
                html = "<table border='2' width='100%'>";
                SqlParameter[] param = new SqlParameter[]
				{
					new SqlParameter("@firstname", base.Request.QueryString["fname"].ToString()),
					new SqlParameter("@lastname", base.Request.QueryString["lname"].ToString()),
					new SqlParameter("@company", base.Request.QueryString["company"].ToString()),
					new SqlParameter("@userIds", base.Request.QueryString["userIds"].ToString()),
					new SqlParameter("@dateFrom", base.Request.QueryString["dfrom"].ToString()),
					new SqlParameter("@dateTo", base.Request.QueryString["dto"].ToString())
				};
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spCMCExcelExport", param);
                System.Collections.ArrayList topicId = new System.Collections.ArrayList();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        dtquestion = ds.Tables[1];
                        DataView dvData = new DataView(dtquestion);
                        html += "<tr><td colspan='29'></td>";
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            int NoOfQuestions = System.Convert.ToInt32(ds.Tables[0].Rows[i]["questioncount"].ToString()) + 2;
                            object obj = html;
                            html = string.Concat(new object[]
							{
								obj,
								"<td style='padding:0px 5px 0px; font-size:11px' valign='middle' align='center' colspan='",
								NoOfQuestions,
								"'>",
								ds.Tables[0].Rows[i]["topicName"].ToString(),
								"</td>"
							});
                            topicId.Add(System.Convert.ToInt32(ds.Tables[0].Rows[i]["topicId"].ToString()));
                        }
                        html += "</tr>";
                        html += "<tr><td colspan='29'></td>";
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            dvData.RowFilter = "TopicId = '" + System.Convert.ToInt32(ds.Tables[0].Rows[i]["topicId"].ToString()) + "'";
                            foreach (DataRowView drV in dvData)
                            {
                                object obj = html;
                                html = string.Concat(new object[]
								{
									obj,
									"<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>",
									drV["questionText"],
									"</td>"
								});
                            }
                            html += "<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>Percentage</td><td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>Rank</td>";
                        }
                        html += "</tr>";
                        DataSet dsSGAUsers = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spCMCTestUsers", param);
                        if (dsSGAUsers != null)
                        {
                            if (dsSGAUsers.Tables.Count > 0 && dsSGAUsers.Tables[0].Rows.Count > 0)
                            {
                                NoOfColumn = dsSGAUsers.Tables[0].Columns.Count;
                                html += "<tr>";
                                foreach (DataColumn column in dsSGAUsers.Tables[0].Columns)
                                {
                                    html = html + "<td style='padding:0px 5px 0px; font-size:11px' valign='middle' align='center'><b>" + column.ColumnName + "</b></td>";
                                }
                                for (int i = 0; i < dsAll.Tables[0].Rows.Count; i++)
                                {
                                    dvData.RowFilter = "TopicId = '" + System.Convert.ToInt32(ds.Tables[0].Rows[i]["topicId"].ToString()) + "'";
                                    int j = 0;
                                    foreach (DataRowView drV in dvData)
                                    {
                                        object obj = html;
                                        html = string.Concat(new object[]
										{
											obj,
											"<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>",
											drV["questionSuffix"],
											j + 1,
											"</td>"
										});
                                        j++;
                                    }
                                    html += "<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>Score</td><td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>Rating</td>";
                                }
                                html += "</tr>";
                                DataTable dtUserInfo = new DataTable();
                                DataTable dtQuestionReply = new DataTable();
                                string[] selectedColumns = new string[]
								{
									"marks"
								};
                                for (int i = 0; i <= 3; i++)
                                {
                                    html += "<tr>";
                                    for (int k = 0; k < NoOfColumn; k++)
                                    {
                                        if (dsSGAUsers.Tables[0].Columns[k].ToString().ToLower() == "first name")
                                        {
                                            string str = "";
                                            switch (i)
                                            {
                                                case 0:
                                                    str = "Average";
                                                    break;
                                                case 1:
                                                    str = "Lower Quartile";
                                                    break;
                                                case 2:
                                                    str = "Median";
                                                    break;
                                                case 3:
                                                    str = "Upper Quartile";
                                                    break;
                                            }
                                            html = html + "<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>" + str + "</td>";
                                        }
                                        else
                                        {
                                            html += "<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>&nbsp;</td>";
                                        }
                                    }
                                    dtUserInfo = dsAll.Tables[3];
                                    dtQuestionReply = dsAll.Tables[1];
                                    DataView dvData2 = new DataView(dtUserInfo);
                                    DataView dvData3 = new DataView(dtQuestionReply);
                                    dvData2.RowFilter = "testId = '" + System.Convert.ToInt32(dsAll.Tables[2].Rows[i]["testId"].ToString()) + "'";
                                    foreach (int id in topicId)
                                    {
                                        dvData3.RowFilter = "topicId = '" + id + "'";
                                        System.Collections.IEnumerator enumerator2 = dvData3.GetEnumerator();
                                        try
                                        {
                                            DataRowView drV1;
                                            while (enumerator2.MoveNext())
                                            {
                                                drV1 = (DataRowView)enumerator2.Current;
                                                EnumerableRowCollection<DataRow> result = from r in dvData2.Table.AsEnumerable()
                                                                                          where r.Field<int>("QuestionId") == System.Convert.ToInt32(drV1["QuestionId"])
                                                                                          select r;
                                                DataTable dtResult = result.CopyToDataTable<DataRow>();
                                                DataTable dt = new DataView(dtResult).ToTable(false, selectedColumns);
                                                System.Collections.Generic.IEnumerable<double> list = ReportsDownload.ConvertToDecimals(dt);
                                                double value = 0.0;
                                                switch (i)
                                                {
                                                    case 0:
                                                        value = System.Math.Round(list.Average(), 2);
                                                        break;
                                                    case 1:
                                                        value = Quartiles.FirstQuartile(from m in list
                                                                                        orderby m
                                                                                        select m);
                                                        break;
                                                    case 2:
                                                        value = Quartiles.MiddleQuartile(from m in list
                                                                                         orderby m
                                                                                         select m);
                                                        break;
                                                    case 3:
                                                        value = Quartiles.ThirdQuartile(from m in list
                                                                                        orderby m
                                                                                        select m);
                                                        break;
                                                }
                                                object obj = html;
                                                html = string.Concat(new object[]
												{
													obj,
													"<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top'  align='center'>",
													value,
													"</td>"
												});
                                            }
                                        }
                                        finally
                                        {
                                            System.IDisposable disposable = enumerator2 as System.IDisposable;
                                            if (disposable != null)
                                            {
                                                disposable.Dispose();
                                            }
                                        }
                                        param = new SqlParameter[]
										{
											new SqlParameter("@topicId", id)
										};
                                        DataSet dsMarks = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetPercentageByTopics", param);
                                        if (dsMarks != null)
                                        {
                                            if (dsMarks.Tables.Count > 0 && dsMarks.Tables[0].Rows.Count > 0)
                                            {
                                                EnumerableRowCollection<DataRow> result = from r in dsMarks.Tables[0].AsEnumerable()
                                                                                          select r;
                                                DataTable dtResult = result.CopyToDataTable<DataRow>();
                                                DataTable dt = new DataView(dtResult).ToTable(false, selectedColumns);
                                                System.Collections.Generic.IEnumerable<double> list = ReportsDownload.ConvertToDecimals(dt);
                                                double value = 0.0;
                                                switch (i)
                                                {
                                                    case 0:
                                                        value = System.Math.Round(list.Average(), 2);
                                                        break;
                                                    case 1:
                                                        value = Quartiles.FirstQuartile(from m in list
                                                                                        orderby m
                                                                                        select m);
                                                        break;
                                                    case 2:
                                                        value = Quartiles.MiddleQuartile(from m in list
                                                                                         orderby m
                                                                                         select m);
                                                        break;
                                                    case 3:
                                                        value = Quartiles.ThirdQuartile(from m in list
                                                                                        orderby m
                                                                                        select m);
                                                        break;
                                                }
                                                object obj = html;
                                                html = string.Concat(new object[]
												{
													obj,
													"<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top'  align='center'>",
													value,
													"</td><td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top'  align='center'></td>"
												});
                                            }
                                        }
                                    }
                                    html += "</tr>";
                                }
                                for (int i = 0; i < dsSGAUsers.Tables[0].Rows.Count; i++)
                                {
                                    html += "<tr>";
                                    for (int k = 0; k < NoOfColumn; k++)
                                    {
                                        if (dsSGAUsers.Tables[0].Columns[k].ToString().ToLower() == "test date")
                                        {
                                            html = html + "<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>" + SGACommon.ToAusTimeZone(System.Convert.ToDateTime(dsSGAUsers.Tables[0].Rows[i][k].ToString())).ToString("dd/MM/yyyy HH:mm tt") + "</td>";
                                        }
                                        else
                                        {
                                            html = html + "<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>" + dsSGAUsers.Tables[0].Rows[i][k].ToString() + "</td>";
                                        }
                                    }
                                    dtUserInfo = ds.Tables[3];
                                    dtQuestionReply = ds.Tables[1];
                                    DataView dvData2 = new DataView(dtUserInfo);
                                    DataView dvData3 = new DataView(dtQuestionReply);
                                    dvData2.RowFilter = "testId = '" + System.Convert.ToInt32(ds.Tables[2].Rows[i]["testId"].ToString()) + "'";
                                    foreach (int id in topicId)
                                    {
                                        dvData3.RowFilter = "topicId = '" + id + "'";
                                        foreach (DataRowView drV2 in dvData3)
                                        {
                                            dvData2.RowFilter = string.Concat(new object[]
											{
												"testId = '",
												System.Convert.ToInt32(ds.Tables[2].Rows[i]["testId"].ToString()),
												"' AND QuestionId = '",
												drV2["QuestionId"],
												"'"
											});
                                            if (dvData2.Count > 0)
                                            {
                                                foreach (DataRowView drV3 in dvData2)
                                                {
                                                    object obj = html;
                                                    html = string.Concat(new object[]
													{
														obj,
														"<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top'  align='center'>",
														drV3["marks"],
														"</td>"
													});
                                                }
                                            }
                                            else
                                            {
                                                html += "<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top'  align='center'>0</td>";
                                            }
                                        }
                                        param = new SqlParameter[]
										{
											new SqlParameter("@testId", System.Convert.ToInt32(ds.Tables[2].Rows[i]["testId"].ToString())),
											new SqlParameter("@topicId", id)
										};
                                        DataSet dsMarks = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetCMCRankingByTopic", param);
                                        if (dsMarks != null)
                                        {
                                            if (dsMarks.Tables.Count > 0 && dsMarks.Tables[0].Rows.Count > 0)
                                            {
                                                object obj = html;
                                                html = string.Concat(new object[]
												{
													obj,
													"<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top'  align='center'>",
													dsMarks.Tables[0].Rows[0]["percentage"],
													"%</td><td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top'  align='center'>",
													dsMarks.Tables[0].Rows[0]["Level"],
													"</td>"
												});
                                            }
                                        }
                                    }
                                    html += "</tr>";
                                }
                            }
                        }
                    }
                }
                html += "</table>";
                TableRow tRow = new TableRow();
                tRow.CssClass = "tableRow";
                this.excel.Rows.Add(tRow);
                TableCell tCell = new TableCell();
                tCell.Text = html;
                tCell.CssClass = "tableCell";
                tRow.Cells.Add(tCell);
                base.Response.Clear();
                base.Response.AddHeader("content-disposition", "attachment;filename=CMCAllReults.xls");
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
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private static System.Collections.Generic.IEnumerable<double> ConvertToDecimals(DataTable dataTable)
        {
            return from row in dataTable.AsEnumerable()
                   select System.Convert.ToDouble(row["marks"]);
        }

        private void DownloadSSAResults()
        {
            try
            {
                int NoOfColumn = 0;
                string html = "";
                DataTable dtquestion = new DataTable();
                html = "<table border='2' width='100%'>";
                SqlParameter[] param = new SqlParameter[]
				{
					new SqlParameter("@firstname", base.Request.QueryString["fname"].ToString()),
					new SqlParameter("@lastname", base.Request.QueryString["lname"].ToString()),
					new SqlParameter("@company", base.Request.QueryString["company"].ToString()),
					new SqlParameter("@userIds", base.Request.QueryString["userIds"].ToString()),
					new SqlParameter("@dateFrom", base.Request.QueryString["dfrom"].ToString()),
					new SqlParameter("@dateTo", base.Request.QueryString["dto"].ToString())
				};
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spSSAExcelExport", param);
                System.Collections.ArrayList topicId = new System.Collections.ArrayList();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        dtquestion = ds.Tables[1];
                        DataView dvData = new DataView(dtquestion);
                        html += "<tr><td colspan='29'></td>";
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            int NoOfQuestions = System.Convert.ToInt32(ds.Tables[0].Rows[i]["questioncount"].ToString()) + 2;
                            object obj = html;
                            html = string.Concat(new object[]
							{
								obj,
								"<td style='padding:0px 5px 0px; font-size:11px' valign='middle' align='center' colspan='",
								NoOfQuestions,
								"'>",
								ds.Tables[0].Rows[i]["topicName"].ToString(),
								"</td>"
							});
                            topicId.Add(System.Convert.ToInt32(ds.Tables[0].Rows[i]["topicId"].ToString()));
                        }
                        html += "</tr>";
                        html += "<tr><td colspan='29'></td>";
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            dvData.RowFilter = "TopicId = '" + System.Convert.ToInt32(ds.Tables[0].Rows[i]["topicId"].ToString()) + "'";
                            foreach (DataRowView drV in dvData)
                            {
                                object obj = html;
                                html = string.Concat(new object[]
								{
									obj,
									"<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>",
									drV["questionText"],
									"</td>"
								});
                            }
                            html += "<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>Rating</td><td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>Level</td>";
                        }
                        html += "</tr>";
                        DataSet dsSGAUsers = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spSSATestUsers", param);
                        if (dsSGAUsers != null)
                        {
                            if (dsSGAUsers.Tables.Count > 0 && dsSGAUsers.Tables[0].Rows.Count > 0)
                            {
                                NoOfColumn = dsSGAUsers.Tables[0].Columns.Count;
                                html += "<tr>";
                                foreach (DataColumn column in dsSGAUsers.Tables[0].Columns)
                                {
                                    html = html + "<td style='padding:0px 5px 0px; font-size:11px' valign='middle' align='center'><b>" + column.ColumnName + "</b></td>";
                                }
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    dvData.RowFilter = "TopicId = '" + System.Convert.ToInt32(ds.Tables[0].Rows[i]["topicId"].ToString()) + "'";
                                    int j = 0;
                                    foreach (DataRowView drV in dvData)
                                    {
                                        object obj = html;
                                        html = string.Concat(new object[]
										{
											obj,
											"<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>",
											drV["questionSuffix"],
											j + 1,
											"</td>"
										});
                                        j++;
                                    }
                                    html += "<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>Marks</td><td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>Rank</td>";
                                }
                                html += "</tr>";
                                DataTable dtUserInfo = new DataTable();
                                DataTable dtQuestionReply = new DataTable();
                                for (int i = 0; i < dsSGAUsers.Tables[0].Rows.Count; i++)
                                {
                                    html += "<tr>";
                                    for (int k = 0; k < NoOfColumn; k++)
                                    {
                                        if (dsSGAUsers.Tables[0].Columns[k].ToString().ToLower() == "test date")
                                        {
                                            html = html + "<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>" + SGACommon.ToAusTimeZone(System.Convert.ToDateTime(dsSGAUsers.Tables[0].Rows[i][k].ToString())).ToString("dd/MM/yyyy HH:mm tt") + "</td>";
                                        }
                                        else
                                        {
                                            html = html + "<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>" + dsSGAUsers.Tables[0].Rows[i][k].ToString() + "</td>";
                                        }
                                    }
                                    dtUserInfo = ds.Tables[3];
                                    dtQuestionReply = ds.Tables[1];
                                    DataView dvData2 = new DataView(dtUserInfo);
                                    DataView dvData3 = new DataView(dtQuestionReply);
                                    dvData2.RowFilter = "testId = '" + System.Convert.ToInt32(ds.Tables[2].Rows[i]["testId"].ToString()) + "'";
                                    foreach (int id in topicId)
                                    {
                                        dvData3.RowFilter = "topicId = '" + id + "'";
                                        foreach (DataRowView drV2 in dvData3)
                                        {
                                            dvData2.RowFilter = string.Concat(new object[]
											{
												"testId = '",
												System.Convert.ToInt32(ds.Tables[2].Rows[i]["testId"].ToString()),
												"' AND QuestionId = '",
												drV2["QuestionId"],
												"'"
											});
                                            if (dvData2.Count > 0)
                                            {
                                                foreach (DataRowView drV3 in dvData2)
                                                {
                                                    object obj = html;
                                                    html = string.Concat(new object[]
													{
														obj,
														"<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top'  align='center'>",
														drV3["marks"],
														"</td>"
													});
                                                }
                                            }
                                            else
                                            {
                                                html += "<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top'  align='center'>0</td>";
                                            }
                                        }
                                        param = new SqlParameter[]
										{
											new SqlParameter("@testId", System.Convert.ToInt32(ds.Tables[2].Rows[i]["testId"].ToString())),
											new SqlParameter("@topicId", id)
										};
                                        DataSet dsMarks = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSSARankingByTopic", param);
                                        if (dsMarks != null)
                                        {
                                            if (dsMarks.Tables.Count > 0 && dsMarks.Tables[0].Rows.Count > 0)
                                            {
                                                object obj = html;
                                                html = string.Concat(new object[]
												{
													obj,
													"<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top'  align='center'>",
													dsMarks.Tables[0].Rows[0]["topicmarks"],
													"</td><td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top'  align='center'>",
													dsMarks.Tables[0].Rows[0]["Level"],
													"</td>"
												});
                                            }
                                        }
                                    }
                                    html += "</tr>";
                                }
                            }
                        }
                    }
                }
                html += "</table>";
                TableRow tRow = new TableRow();
                tRow.CssClass = "tableRow";
                this.excel.Rows.Add(tRow);
                TableCell tCell = new TableCell();
                tCell.Text = html;
                tCell.CssClass = "tableCell";
                tRow.Cells.Add(tCell);
                base.Response.Clear();
                base.Response.AddHeader("content-disposition", "attachment;filename=SSAAllReults.xls");
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
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private void DownloadBAResults()
        {
            try
            {
                int NoOfColumn = 0;
                string html = "";
                DataTable dtquestion = new DataTable();
                html = "<table border='2' width='100%'>";
                SqlParameter[] param = new SqlParameter[]
				{
					new SqlParameter("@firstname", base.Request.QueryString["fname"].ToString()),
					new SqlParameter("@lastname", base.Request.QueryString["lname"].ToString()),
					new SqlParameter("@company", base.Request.QueryString["company"].ToString()),
					new SqlParameter("@userIds", base.Request.QueryString["userIds"].ToString()),
					new SqlParameter("@dateFrom", base.Request.QueryString["dfrom"].ToString()),
					new SqlParameter("@dateTo", base.Request.QueryString["dto"].ToString())
				};
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spBAExcelExport", param);
                System.Collections.ArrayList topicId = new System.Collections.ArrayList();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        dtquestion = ds.Tables[1];
                        DataView dvData = new DataView(dtquestion);
                        html += "<tr><td colspan='29'></td>";
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            int NoOfQuestions = System.Convert.ToInt32(ds.Tables[0].Rows[i]["questioncount"].ToString()) + 2;
                            object obj = html;
                            html = string.Concat(new object[]
							{
								obj,
								"<td style='padding:0px 5px 0px; font-size:11px' valign='middle' align='center' colspan='",
								NoOfQuestions,
								"'>",
								ds.Tables[0].Rows[i]["topicName"].ToString(),
								"</td>"
							});
                            topicId.Add(System.Convert.ToInt32(ds.Tables[0].Rows[i]["topicId"].ToString()));
                        }
                        html += "</tr>";
                        html += "<tr><td colspan='29'></td>";
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            dvData.RowFilter = "TopicId = '" + System.Convert.ToInt32(ds.Tables[0].Rows[i]["topicId"].ToString()) + "'";
                            foreach (DataRowView drV in dvData)
                            {
                                object obj = html;
                                html = string.Concat(new object[]
								{
									obj,
									"<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>",
									drV["questionText"],
									"</td>"
								});
                            }
                            html += "<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>Rating</td><td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>Level</td>";
                        }
                        html += "</tr>";
                        DataSet dsSGAUsers = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spBATestUsers", param);
                        if (dsSGAUsers != null)
                        {
                            if (dsSGAUsers.Tables.Count > 0 && dsSGAUsers.Tables[0].Rows.Count > 0)
                            {
                                NoOfColumn = dsSGAUsers.Tables[0].Columns.Count;
                                html += "<tr>";
                                foreach (DataColumn column in dsSGAUsers.Tables[0].Columns)
                                {
                                    html = html + "<td style='padding:0px 5px 0px; font-size:11px' valign='middle' align='center'><b>" + column.ColumnName + "</b></td>";
                                }
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    dvData.RowFilter = "TopicId = '" + System.Convert.ToInt32(ds.Tables[0].Rows[i]["topicId"].ToString()) + "'";
                                    int j = 0;
                                    foreach (DataRowView drV in dvData)
                                    {
                                        object obj = html;
                                        html = string.Concat(new object[]
										{
											obj,
											"<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>",
											drV["questionSuffix"],
											j + 1,
											"</td>"
										});
                                        j++;
                                    }
                                    html += "<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>Average</td><td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>Rank</td>";
                                }
                                html += "</tr>";
                                DataTable dtUserInfo = new DataTable();
                                DataTable dtQuestionReply = new DataTable();
                                for (int i = 0; i < dsSGAUsers.Tables[0].Rows.Count; i++)
                                {
                                    html += "<tr>";
                                    for (int k = 0; k < NoOfColumn; k++)
                                    {
                                        if (dsSGAUsers.Tables[0].Columns[k].ToString().ToLower() == "test date")
                                        {
                                            html = html + "<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>" + SGACommon.ToAusTimeZone(System.Convert.ToDateTime(dsSGAUsers.Tables[0].Rows[i][k].ToString())).ToString("dd/MM/yyyy HH:mm tt") + "</td>";
                                        }
                                        else
                                        {
                                            html = html + "<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>" + dsSGAUsers.Tables[0].Rows[i][k].ToString() + "</td>";
                                        }
                                    }
                                    dtUserInfo = ds.Tables[3];
                                    dtQuestionReply = ds.Tables[1];
                                    DataView dvData2 = new DataView(dtUserInfo);
                                    DataView dvData3 = new DataView(dtQuestionReply);
                                    dvData2.RowFilter = "testId = '" + System.Convert.ToInt32(ds.Tables[2].Rows[i]["testId"].ToString()) + "'";
                                    foreach (int id in topicId)
                                    {
                                        dvData3.RowFilter = "topicId = '" + id + "'";
                                        foreach (DataRowView drV2 in dvData3)
                                        {
                                            dvData2.RowFilter = string.Concat(new object[]
											{
												"testId = '",
												System.Convert.ToInt32(ds.Tables[2].Rows[i]["testId"].ToString()),
												"' AND QuestionId = '",
												drV2["QuestionId"],
												"'"
											});
                                            if (dvData2.Count > 0)
                                            {
                                                foreach (DataRowView drV3 in dvData2)
                                                {
                                                    object obj = html;
                                                    html = string.Concat(new object[]
													{
														obj,
														"<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top'  align='center'>",
														drV3["marks"],
														"</td>"
													});
                                                }
                                            }
                                            else
                                            {
                                                html += "<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top'  align='center'>0</td>";
                                            }
                                        }
                                        param = new SqlParameter[]
										{
											new SqlParameter("@testId", System.Convert.ToInt32(ds.Tables[2].Rows[i]["testId"].ToString())),
											new SqlParameter("@topicId", id)
										};
                                        DataSet dsMarks = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetBARankingByTopic", param);
                                        if (dsMarks != null)
                                        {
                                            if (dsMarks.Tables.Count > 0 && dsMarks.Tables[0].Rows.Count > 0)
                                            {
                                                object obj = html;
                                                html = string.Concat(new object[]
												{
													obj,
													"<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top'  align='center'>",
													dsMarks.Tables[0].Rows[0]["topicmarks"],
													"</td><td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top'  align='center'>",
													dsMarks.Tables[0].Rows[0]["Level"],
													"</td>"
												});
                                            }
                                        }
                                    }
                                    html += "</tr>";
                                }
                            }
                        }
                    }
                }
                html += "</table>";
                TableRow tRow = new TableRow();
                tRow.CssClass = "tableRow";
                this.excel.Rows.Add(tRow);
                TableCell tCell = new TableCell();
                tCell.Text = html;
                tCell.CssClass = "tableCell";
                tRow.Cells.Add(tCell);
                base.Response.Clear();
                base.Response.AddHeader("content-disposition", "attachment;filename=BAAllReults.xls");
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
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private void DownloadNPResults()
        {
            try
            {
                int NoOfColumn = 0;
                string html = "";
                DataTable dtquestion = new DataTable();
                html = "<table border='2' width='100%'>";
                SqlParameter[] param = new SqlParameter[]
				{
					new SqlParameter("@firstname", base.Request.QueryString["fname"].ToString()),
					new SqlParameter("@lastname", base.Request.QueryString["lname"].ToString()),
					new SqlParameter("@company", base.Request.QueryString["company"].ToString()),
					new SqlParameter("@userIds", base.Request.QueryString["userIds"].ToString()),
					new SqlParameter("@dateFrom", base.Request.QueryString["dfrom"].ToString()),
					new SqlParameter("@dateTo", base.Request.QueryString["dto"].ToString())
				};
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spNPExcelExport", param);
                System.Collections.ArrayList topicId = new System.Collections.ArrayList();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        dtquestion = ds.Tables[1];
                        DataView dvData = new DataView(dtquestion);
                        html += "<tr><td colspan='29'></td>";
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            int NoOfQuestions = System.Convert.ToInt32(ds.Tables[0].Rows[i]["questioncount"].ToString()) + 2;
                            object obj = html;
                            html = string.Concat(new object[]
							{
								obj,
								"<td style='padding:0px 5px 0px; font-size:11px' valign='middle' align='center' colspan='",
								NoOfQuestions,
								"'>",
								ds.Tables[0].Rows[i]["topicName"].ToString(),
								"</td>"
							});
                            topicId.Add(System.Convert.ToInt32(ds.Tables[0].Rows[i]["topicId"].ToString()));
                        }
                        html += "</tr>";
                        html += "<tr><td colspan='29'></td>";
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            dvData.RowFilter = "TopicId = '" + System.Convert.ToInt32(ds.Tables[0].Rows[i]["topicId"].ToString()) + "'";
                            foreach (DataRowView drV in dvData)
                            {
                                object obj = html;
                                html = string.Concat(new object[]
								{
									obj,
									"<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>",
									drV["questionText"],
									"</td>"
								});
                            }
                            html += "<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>Rating</td><td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>Level</td>";
                        }
                        html += "</tr>";
                        DataSet dsSGAUsers = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spNPTestUsers", param);
                        if (dsSGAUsers != null)
                        {
                            if (dsSGAUsers.Tables.Count > 0 && dsSGAUsers.Tables[0].Rows.Count > 0)
                            {
                                NoOfColumn = dsSGAUsers.Tables[0].Columns.Count;
                                html += "<tr>";
                                foreach (DataColumn column in dsSGAUsers.Tables[0].Columns)
                                {
                                    html = html + "<td style='padding:0px 5px 0px; font-size:11px' valign='middle' align='center'><b>" + column.ColumnName + "</b></td>";
                                }
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    dvData.RowFilter = "TopicId = '" + System.Convert.ToInt32(ds.Tables[0].Rows[i]["topicId"].ToString()) + "'";
                                    int j = 0;
                                    foreach (DataRowView drV in dvData)
                                    {
                                        object obj = html;
                                        html = string.Concat(new object[]
										{
											obj,
											"<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>",
											drV["questionSuffix"],
											j + 1,
											"</td>"
										});
                                        j++;
                                    }
                                    html += "<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>Average</td><td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>Rank</td>";
                                }
                                html += "</tr>";
                                DataTable dtUserInfo = new DataTable();
                                DataTable dtQuestionReply = new DataTable();
                                for (int i = 0; i < dsSGAUsers.Tables[0].Rows.Count; i++)
                                {
                                    html += "<tr>";
                                    for (int k = 0; k < NoOfColumn; k++)
                                    {
                                        if (dsSGAUsers.Tables[0].Columns[k].ToString().ToLower() == "test date")
                                        {
                                            html = html + "<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>" + SGACommon.ToAusTimeZone(System.Convert.ToDateTime(dsSGAUsers.Tables[0].Rows[i][k].ToString())).ToString("dd/MM/yyyy HH:mm tt") + "</td>";
                                        }
                                        else
                                        {
                                            html = html + "<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>" + dsSGAUsers.Tables[0].Rows[i][k].ToString() + "</td>";
                                        }
                                    }
                                    dtUserInfo = ds.Tables[3];
                                    dtQuestionReply = ds.Tables[1];
                                    DataView dvData2 = new DataView(dtUserInfo);
                                    DataView dvData3 = new DataView(dtQuestionReply);
                                    dvData2.RowFilter = "testId = '" + System.Convert.ToInt32(ds.Tables[2].Rows[i]["testId"].ToString()) + "'";
                                    foreach (int id in topicId)
                                    {
                                        dvData3.RowFilter = "topicId = '" + id + "'";
                                        foreach (DataRowView drV2 in dvData3)
                                        {
                                            dvData2.RowFilter = string.Concat(new object[]
											{
												"testId = '",
												System.Convert.ToInt32(ds.Tables[2].Rows[i]["testId"].ToString()),
												"' AND QuestionId = '",
												drV2["QuestionId"],
												"'"
											});
                                            if (dvData2.Count > 0)
                                            {
                                                foreach (DataRowView drV3 in dvData2)
                                                {
                                                    object obj = html;
                                                    html = string.Concat(new object[]
													{
														obj,
														"<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top'  align='center'>",
														drV3["marks"],
														"</td>"
													});
                                                }
                                            }
                                            else
                                            {
                                                html += "<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top'  align='center'>0</td>";
                                            }
                                        }
                                        param = new SqlParameter[]
										{
											new SqlParameter("@testId", System.Convert.ToInt32(ds.Tables[2].Rows[i]["testId"].ToString())),
											new SqlParameter("@topicId", id)
										};
                                        DataSet dsMarks = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetNPRankingByTopic", param);
                                        if (dsMarks != null)
                                        {
                                            if (dsMarks.Tables.Count > 0 && dsMarks.Tables[0].Rows.Count > 0)
                                            {
                                                object obj = html;
                                                html = string.Concat(new object[]
												{
													obj,
													"<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top'  align='center'>",
													dsMarks.Tables[0].Rows[0]["topicmarks"],
													"</td><td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top'  align='center'>",
													dsMarks.Tables[0].Rows[0]["Level"],
													"</td>"
												});
                                            }
                                        }
                                    }
                                    html += "</tr>";
                                }
                            }
                        }
                    }
                }
                html += "</table>";
                TableRow tRow = new TableRow();
                tRow.CssClass = "tableRow";
                this.excel.Rows.Add(tRow);
                TableCell tCell = new TableCell();
                tCell.Text = html;
                tCell.CssClass = "tableCell";
                tRow.Cells.Add(tCell);
                base.Response.Clear();
                base.Response.AddHeader("content-disposition", "attachment;filename=NPAllReults.xls");
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
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private void DownloadMPResults()
        {
            try
            {
                int NoOfColumn = 0;
                string html = "";
                DataTable dtquestion = new DataTable();
                html = "<table border='2' width='100%'>";
                SqlParameter[] param = new SqlParameter[]
				{
					new SqlParameter("@firstname", base.Request.QueryString["fname"].ToString()),
					new SqlParameter("@lastname", base.Request.QueryString["lname"].ToString()),
					new SqlParameter("@company", base.Request.QueryString["company"].ToString()),
					new SqlParameter("@userIds", base.Request.QueryString["userIds"].ToString()),
					new SqlParameter("@dateFrom", base.Request.QueryString["dfrom"].ToString()),
					new SqlParameter("@dateTo", base.Request.QueryString["dto"].ToString())
				};
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spMPExcelExport", param);
                System.Collections.ArrayList topicId = new System.Collections.ArrayList();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        dtquestion = ds.Tables[1];
                        DataView dvData = new DataView(dtquestion);
                        html += "<tr><td colspan='29'></td>";
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            int NoOfQuestions = System.Convert.ToInt32(ds.Tables[0].Rows[i]["questioncount"].ToString()) + 2;
                            object obj = html;
                            html = string.Concat(new object[]
							{
								obj,
								"<td style='padding:0px 5px 0px; font-size:11px' valign='middle' align='center' colspan='",
								NoOfQuestions,
								"'>",
								ds.Tables[0].Rows[i]["topicName"].ToString(),
								"</td>"
							});
                            topicId.Add(System.Convert.ToInt32(ds.Tables[0].Rows[i]["topicId"].ToString()));
                        }
                        html += "</tr>";
                        html += "<tr><td colspan='29'></td>";
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            dvData.RowFilter = "TopicId = '" + System.Convert.ToInt32(ds.Tables[0].Rows[i]["topicId"].ToString()) + "'";
                            foreach (DataRowView drV in dvData)
                            {
                                object obj = html;
                                html = string.Concat(new object[]
								{
									obj,
									"<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>",
									drV["questionText"],
									"</td>"
								});
                            }
                            html += "<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>Rating</td><td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>Level</td>";
                        }
                        html += "</tr>";
                        DataSet dsSGAUsers = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spMPTestUsers", param);
                        if (dsSGAUsers != null)
                        {
                            if (dsSGAUsers.Tables.Count > 0 && dsSGAUsers.Tables[0].Rows.Count > 0)
                            {
                                NoOfColumn = dsSGAUsers.Tables[0].Columns.Count;
                                html += "<tr>";
                                foreach (DataColumn column in dsSGAUsers.Tables[0].Columns)
                                {
                                    html = html + "<td style='padding:0px 5px 0px; font-size:11px' valign='middle' align='center'><b>" + column.ColumnName + "</b></td>";
                                }
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    dvData.RowFilter = "TopicId = '" + System.Convert.ToInt32(ds.Tables[0].Rows[i]["topicId"].ToString()) + "'";
                                    int j = 0;
                                    foreach (DataRowView drV in dvData)
                                    {
                                        object obj = html;
                                        html = string.Concat(new object[]
										{
											obj,
											"<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>",
											drV["questionSuffix"],
											j + 1,
											"</td>"
										});
                                        j++;
                                    }
                                    html += "<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>Average</td><td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>Rank</td>";
                                }
                                html += "</tr>";
                                DataTable dtUserInfo = new DataTable();
                                DataTable dtQuestionReply = new DataTable();
                                for (int i = 0; i < dsSGAUsers.Tables[0].Rows.Count; i++)
                                {
                                    html += "<tr>";
                                    for (int k = 0; k < NoOfColumn; k++)
                                    {
                                        if (dsSGAUsers.Tables[0].Columns[k].ToString().ToLower() == "test date")
                                        {
                                            html = html + "<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>" + SGACommon.ToAusTimeZone(System.Convert.ToDateTime(dsSGAUsers.Tables[0].Rows[i][k].ToString())).ToString("dd/MM/yyyy HH:mm tt") + "</td>";
                                        }
                                        else
                                        {
                                            html = html + "<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top' align='left'>" + dsSGAUsers.Tables[0].Rows[i][k].ToString() + "</td>";
                                        }
                                    }
                                    dtUserInfo = ds.Tables[3];
                                    dtQuestionReply = ds.Tables[1];
                                    DataView dvData2 = new DataView(dtUserInfo);
                                    DataView dvData3 = new DataView(dtQuestionReply);
                                    dvData2.RowFilter = "testId = '" + System.Convert.ToInt32(ds.Tables[2].Rows[i]["testId"].ToString()) + "'";
                                    foreach (int id in topicId)
                                    {
                                        dvData3.RowFilter = "topicId = '" + id + "'";
                                        foreach (DataRowView drV2 in dvData3)
                                        {
                                            dvData2.RowFilter = string.Concat(new object[]
											{
												"testId = '",
												System.Convert.ToInt32(ds.Tables[2].Rows[i]["testId"].ToString()),
												"' AND QuestionId = '",
												drV2["QuestionId"],
												"'"
											});
                                            if (dvData2.Count > 0)
                                            {
                                                foreach (DataRowView drV3 in dvData2)
                                                {
                                                    object obj = html;
                                                    html = string.Concat(new object[]
													{
														obj,
														"<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top'  align='center'>",
														drV3["marks"],
														"</td>"
													});
                                                }
                                            }
                                            else
                                            {
                                                html += "<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top'  align='center'>0</td>";
                                            }
                                        }
                                        param = new SqlParameter[]
										{
											new SqlParameter("@testId", System.Convert.ToInt32(ds.Tables[2].Rows[i]["testId"].ToString())),
											new SqlParameter("@topicId", id)
										};
                                        DataSet dsMarks = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetMPRankingByTopic", param);
                                        if (dsMarks != null)
                                        {
                                            if (dsMarks.Tables.Count > 0 && dsMarks.Tables[0].Rows.Count > 0)
                                            {
                                                object obj = html;
                                                html = string.Concat(new object[]
												{
													obj,
													"<td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top'  align='center'>",
													dsMarks.Tables[0].Rows[0]["topicmarks"],
													"</td><td style='padding:0px 5px 0px;font-size:11px; width:30%' valign='top'  align='center'>",
													dsMarks.Tables[0].Rows[0]["Level"],
													"</td>"
												});
                                            }
                                        }
                                    }
                                    html += "</tr>";
                                }
                            }
                        }
                    }
                }
                html += "</table>";
                TableRow tRow = new TableRow();
                tRow.CssClass = "tableRow";
                this.excel.Rows.Add(tRow);
                TableCell tCell = new TableCell();
                tCell.Text = html;
                tCell.CssClass = "tableCell";
                tRow.Cells.Add(tCell);
                base.Response.Clear();
                base.Response.AddHeader("content-disposition", "attachment;filename=MPAllReults.xls");
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
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
