using DataTier;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using SGA.App_Code;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.Mvc;

namespace SGA.Controller
{
    public class SgtResultController : System.Web.Mvc.Controller
    {
        public ActionResult Index(int? id)
        {
            SGACommon.IsViewResult("viewSgaResult");
            DataSet Topic = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetTestTopics", new SqlParameter[]
			{
				new SqlParameter("@testId", id.Value),
				new SqlParameter("@username", SGACommon.LoginUserInfo.name)
			});
            string strTopic = "";
            string strQuestion = "";
            string strMarks = "";
            string[] topicMarks = new string[8];
            string[] questionText = new string[8];
            object[] obj = new object[9];
            object[] obj2 = new object[9];
            object[] obj3 = new object[9];
            object[] obj4 = new object[9];
            object[] obj5 = new object[9];
            object[] obj6 = new object[9];
            object[] obj7 = new object[9];
            object[] obj8 = new object[9];
            if (Topic != null)
            {
                for (int i = 0; i < Topic.Tables[0].Rows.Count; i++)
                {
                    strTopic = strTopic + Topic.Tables[0].Rows[i]["topic"].ToString().Replace("'", "\\'") + ",";
                    DataSet QuestionByTopic = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetQuestionByTopic", new SqlParameter[]
					{
						new SqlParameter("@testId", id.Value),
						new SqlParameter("@topicId", Topic.Tables[0].Rows[i]["topicId"].ToString())
					});
                    if (QuestionByTopic != null)
                    {
                        topicMarks[i] = Topic.Tables[0].Rows[i]["percentage"].ToString();
                        for (int j = 0; j < QuestionByTopic.Tables[0].Rows.Count; j++)
                        {
                            strQuestion = strQuestion + QuestionByTopic.Tables[0].Rows[j]["questionName"].ToString().Replace("'", "\\'") + "#";
                            switch (i)
                            {
                                case 0:
                                    obj.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["marks"]), j);
                                    break;
                                case 1:
                                    obj2.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["marks"]), j);
                                    break;
                                case 2:
                                    obj3.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["marks"]), j);
                                    break;
                                case 3:
                                    obj4.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["marks"]), j);
                                    break;
                                case 4:
                                    obj5.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["marks"]), j);
                                    break;
                                case 5:
                                    obj6.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["marks"]), j);
                                    break;
                                case 6:
                                    obj7.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["marks"]), j);
                                    break;
                                case 7:
                                    obj8.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["marks"]), j);
                                    break;
                            }
                        }
                        strQuestion = SGACommon.RemoveLastCharacter(strQuestion);
                        strMarks = SGACommon.RemoveLastCharacter(strMarks);
                        questionText[i] = strQuestion;
                        strQuestion = "";
                        strMarks = "";
                    }
                }
                strTopic = SGACommon.RemoveLastCharacter(strTopic);
            }
            else
            {
                base.Response.Redirect("default.aspx", false);
            }
            string[] categories = strTopic.Split(new char[]
			{
				','
			});
            Data data = new Data(new DotNet.Highcharts.Options.Point[]
			{
				new DotNet.Highcharts.Options.Point
				{
					Y = new Number?(System.Convert.ToDouble(topicMarks[0])),
					Color = new Color?(Color.FromName("colors[0]")),
					Drilldown = new Drilldown
					{
						Name = categories[0],
						Categories = questionText[0].Split(new char[]
						{
							'#'
						}),
						Data = new Data(obj),
						Color = Color.FromName("colors[0]")
					}
				},
				new DotNet.Highcharts.Options.Point
				{
					Y = new Number?(System.Convert.ToDouble(topicMarks[1])),
					Color = new Color?(Color.FromName("colors[1]")),
					Drilldown = new Drilldown
					{
						Name = categories[1],
						Categories = questionText[1].Split(new char[]
						{
							'#'
						}),
						Data = new Data(obj2),
						Color = Color.FromName("colors[1]")
					}
				},
				new DotNet.Highcharts.Options.Point
				{
					Y = new Number?(System.Convert.ToDouble(topicMarks[2])),
					Color = new Color?(Color.FromName("colors[2]")),
					Drilldown = new Drilldown
					{
						Name = categories[2],
						Categories = questionText[2].Split(new char[]
						{
							'#'
						}),
						Data = new Data(obj3),
						Color = Color.FromName("colors[2]")
					}
				},
				new DotNet.Highcharts.Options.Point
				{
					Y = new Number?(System.Convert.ToDouble(topicMarks[3])),
					Color = new Color?(Color.FromName("colors[3]")),
					Drilldown = new Drilldown
					{
						Name = categories[3],
						Categories = questionText[3].Split(new char[]
						{
							'#'
						}),
						Data = new Data(obj4),
						Color = Color.FromName("colors[3]")
					}
				},
				new DotNet.Highcharts.Options.Point
				{
					Y = new Number?(System.Convert.ToDouble(topicMarks[4])),
					Color = new Color?(Color.FromName("colors[4]")),
					Drilldown = new Drilldown
					{
						Name = categories[4],
						Categories = questionText[4].Split(new char[]
						{
							'#'
						}),
						Data = new Data(obj5),
						Color = Color.FromName("colors[4]")
					}
				},
				new DotNet.Highcharts.Options.Point
				{
					Y = new Number?(System.Convert.ToDouble(topicMarks[5])),
					Color = new Color?(Color.FromName("colors[5]")),
					Drilldown = new Drilldown
					{
						Name = categories[5],
						Categories = questionText[5].Split(new char[]
						{
							'#'
						}),
						Data = new Data(obj6),
						Color = Color.FromName("colors[5]")
					}
				},
				new DotNet.Highcharts.Options.Point
				{
					Y = new Number?(System.Convert.ToDouble(topicMarks[6])),
					Color = new Color?(Color.FromName("colors[6]")),
					Drilldown = new Drilldown
					{
						Name = categories[6],
						Categories = questionText[6].Split(new char[]
						{
							'#'
						}),
						Data = new Data(obj7),
						Color = Color.FromName("colors[6]")
					}
				},
				new DotNet.Highcharts.Options.Point
				{
					Y = new Number?(System.Convert.ToDouble(topicMarks[7])),
					Color = new Color?(Color.FromName("colors[7]")),
					Drilldown = new Drilldown
					{
						Name = categories[7],
						Categories = questionText[7].Split(new char[]
						{
							'#'
						}),
						Data = new Data(obj8),
						Color = Color.FromName("colors[7]")
					}
				}
			});
            Highcharts chart = new Highcharts("chart").InitChart(new Chart
            {
                DefaultSeriesType = new ChartTypes?(ChartTypes.Column)
            }).SetTitle(new Title
            {
                Text = "Skills Gap Test Result"
            }).SetSubtitle(new Subtitle
            {
                Text = "Click the columns to view questions per topic. Click again to view Topics."
            }).SetXAxis(new XAxis
            {
                Categories = categories
            }).SetYAxis(new YAxis
            {
                Title = new YAxisTitle
                {
                    Text = "Total marks per topic"
                }
            }).SetLegend(new Legend
            {
                Enabled = new bool?(true)
            }).SetTooltip(new Tooltip
            {
                Formatter = "TooltipFormatter"
            }).SetPlotOptions(new PlotOptions
            {
                Column = new PlotOptionsColumn
                {
                    Cursor = new Cursors?(Cursors.Pointer),
                    Point = new PlotOptionsColumnPoint
                    {
                        Events = new PlotOptionsColumnPointEvents
                        {
                            Click = "ColumnPointClick"
                        }
                    },
                    DataLabels = new PlotOptionsColumnDataLabels
                    {
                        Enabled = new bool?(true),
                        Color = new Color?(Color.FromName("colors[0]")),
                        Formatter = "function() { return this.y+'%'; }",
                        Style = "fontWeight: 'bold'"
                    }
                }
            }).SetSeries(new Series
            {
                Name = "Topic marks",
                Data = data,
                Color = new Color?(Color.White)
            }).SetExporting(new Exporting
            {
                Enabled = new bool?(true)
            }).AddJavascripFunction("TooltipFormatter", "var point = this.point, s = this.x +':<b>'+ this.y +'</b>%<br/>';\r\n                      if (point.drilldown) {\r\n                        s += 'Click to view '+ point.category +' Topics';\r\n                      } else {\r\n                        s += 'Click to return to Topics';\r\n                      }\r\n                      return s;", new string[0]).AddJavascripFunction("ColumnPointClick", "var drilldown = this.drilldown;\r\n                      if (drilldown) { // drill down\r\n                        setChart(drilldown.name, drilldown.categories, drilldown.data.data, drilldown.color);\r\n                      } else { // restore\r\n                        setChart(name, categories, data.data);\r\n                      }", new string[0]).AddJavascripFunction("setChart", "chart.xAxis[0].setCategories(categories);\r\n                      chart.series[0].remove();\r\n                      chart.addSeries({\r\n                         name: name,\r\n                         data: data,\r\n                         color: color || 'white'\r\n                      });", new string[]
			{
				"name",
				"categories",
				"data",
				"color"
			}).AddJavascripVariable("colors", "Highcharts.getOptions().colors").AddJavascripVariable("name", "'{0}'".FormatWith(new object[]
			{
				"Topic marks"
			})).AddJavascripVariable("categories", JsonSerializer.Serialize<string[]>(categories)).AddJavascripVariable("data", JsonSerializer.Serialize<Data>(data));
            return base.View(chart);
        }
    }
}
