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
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SGA.Controller
{
    public class HomeController : System.Web.Mvc.Controller
    {
        [Authorize]
        public ActionResult Index(int? id)
        {
            DataSet Topic = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSgaGraph", new SqlParameter[]
			{
				new SqlParameter("@testId", id.Value)
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
                    strTopic = strTopic + Topic.Tables[0].Rows[i]["topicName"].ToString().Replace("'", "\\'") + ",";
                    DataSet QuestionByTopic = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetQuestionByTopicSGA", new SqlParameter[]
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
                                    obj.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 1:
                                    obj2.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 2:
                                    obj3.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 3:
                                    obj4.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 4:
                                    obj5.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 5:
                                    obj6.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 6:
                                    obj7.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 7:
                                    obj8.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
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
                base.Response.Redirect("~/webadmin/listusers.aspx", false);
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
                Text = "Category Management Challenge Result"
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
                    Text = "Percentage per topic"
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
                        Formatter = "function() { return this.y; }",
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

        [Authorize]
        public ActionResult SSAResult(int? id)
        {
            DataSet Topic = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSsaGraph", new SqlParameter[]
			{
				new SqlParameter("@testId", id.Value)
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
                    strTopic = strTopic + Topic.Tables[0].Rows[i]["topictitle"].ToString().Replace("'", "\\'") + ",";
                    DataSet QuestionByTopic = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetQuestionByTopicSSA", new SqlParameter[]
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
                                    obj.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 1:
                                    obj2.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 2:
                                    obj3.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 3:
                                    obj4.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 4:
                                    obj5.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 5:
                                    obj6.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 6:
                                    obj7.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 7:
                                    obj8.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
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
                base.Response.Redirect("~/webadmin/listusers.aspx", false);
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
                    Text = "Percentage per topic"
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
                        Formatter = "function() { return this.y; }",
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

        [Authorize]
        public ActionResult BAResult(int? id)
        {
            DataSet Topic = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetBaGraph", new SqlParameter[]
			{
				new SqlParameter("@testId", id.Value)
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
                    strTopic = strTopic + Topic.Tables[0].Rows[i]["topictitle"].ToString().Replace("'", "\\'") + ",";
                    DataSet QuestionByTopic = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetQuestionByTopicBA", new SqlParameter[]
					{
						new SqlParameter("@testId", id.Value),
						new SqlParameter("@topicId", Topic.Tables[0].Rows[i]["topicId"].ToString())
					});
                    if (QuestionByTopic != null)
                    {
                        topicMarks[i] = Topic.Tables[0].Rows[i]["Percentage"].ToString();
                        for (int j = 0; j < QuestionByTopic.Tables[0].Rows.Count; j++)
                        {
                            strQuestion = strQuestion + QuestionByTopic.Tables[0].Rows[j]["questionName"].ToString().Replace("'", "\\'") + "#";
                            switch (i)
                            {
                                case 0:
                                    obj.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["Percentage"]), j);
                                    break;
                                case 1:
                                    obj2.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["Percentage"]), j);
                                    break;
                                case 2:
                                    obj3.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["Percentage"]), j);
                                    break;
                                case 3:
                                    obj4.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["Percentage"]), j);
                                    break;
                                case 4:
                                    obj5.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["Percentage"]), j);
                                    break;
                                case 5:
                                    obj6.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["Percentage"]), j);
                                    break;
                                case 6:
                                    obj7.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["Percentage"]), j);
                                    break;
                                case 7:
                                    obj8.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["Percentage"]), j);
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
                base.Response.Redirect("~/webadmin/listusers.aspx", false);
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
                Text = "Behavioural Assessment Result"
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
                    Text = "Percentage per topic"
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
                        Formatter = "function() { return this.y; }",
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

        [Authorize]
        public ActionResult NPResult(int? id)
        {
            DataSet Topic = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetNPGraph", new SqlParameter[]
			{
				new SqlParameter("@testId", id.Value)
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
                    strTopic = strTopic + Topic.Tables[0].Rows[i]["topictitle"].ToString().Replace("'", "\\'") + ",";
                    DataSet QuestionByTopic = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetQuestionByTopicNP", new SqlParameter[]
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
                                    obj.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 1:
                                    obj2.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 2:
                                    obj3.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 3:
                                    obj4.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 4:
                                    obj5.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 5:
                                    obj6.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 6:
                                    obj7.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 7:
                                    obj8.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
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
                base.Response.Redirect("~/webadmin/listusers.aspx", false);
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
                Text = "Negotiation Profile Assessment Result"
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
                    Text = "Percentage per topic"
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
                        Formatter = "function() { return this.y; }",
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

        [Authorize]
        public ActionResult MPResult(int? id)
        {
            DataSet Topic = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetMPGraph", new SqlParameter[]
			{
				new SqlParameter("@testId", id.Value)
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
                    strTopic = strTopic + Topic.Tables[0].Rows[i]["topictitle"].ToString().Replace("'", "\\'") + ",";
                    DataSet QuestionByTopic = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetQuestionByTopicMP", new SqlParameter[]
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
                                    obj.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 1:
                                    obj2.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 2:
                                    obj3.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 3:
                                    obj4.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 4:
                                    obj5.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 5:
                                    obj6.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 6:
                                    obj7.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 7:
                                    obj8.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
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
                base.Response.Redirect("~/webadmin/listusers.aspx", false);
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
                Text = "Maturity Profile Assessment Result"
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
                    Text = "Percentage per topic"
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
                        Formatter = "function() { return this.y; }",
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

        [Authorize]
        public ActionResult CMAResult(int? id)
        {
            DataSet Topic = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetCMAGraph", new SqlParameter[]
			{
				new SqlParameter("@testId", id.Value)
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
                    strTopic = strTopic + Topic.Tables[0].Rows[i]["topictitle"].ToString().Replace("'", "\\'") + ",";
                    DataSet QuestionByTopic = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetQuestionByTopicCMA", new SqlParameter[]
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
                                    obj.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 1:
                                    obj2.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 2:
                                    obj3.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 3:
                                    obj4.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 4:
                                    obj5.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 5:
                                    obj6.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 6:
                                    obj7.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
                                    break;
                                case 7:
                                    obj8.SetValue(System.Convert.ToDouble(QuestionByTopic.Tables[0].Rows[j]["percentage"]), j);
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
                base.Response.Redirect("~/webadmin/listusers.aspx", false);
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
                Text = "Contract Management Assessment Result"
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
                    Text = "Percentage per topic"
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
                        Formatter = "function() { return this.y; }",
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

        [Authorize]
        public ActionResult CompareResult()
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetUsersByCompany", new SqlParameter[]
			{
				new SqlParameter("@userId", SGACommon.LoginUserInfo.userId)
			});
            base.ViewData["ds"] = ds;
            base.ViewData["message"] = "";
            base.ViewData["users"] = null;
            base.ViewData["assessment"] = null;
            base.ViewData["jobrole"] = null;
            Highcharts chart = null;
            return base.View(chart);
        }

        [Authorize, HttpPost]
        public ActionResult CompareResult(FormCollection frm)
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetUsersByCompany", new SqlParameter[]
			{
				new SqlParameter("@userId", SGACommon.LoginUserInfo.userId)
			});
            Highcharts chart = null;
            base.ViewData["ds"] = ds;
            base.ViewData["message"] = "";
            base.ViewData["users"] = base.Request["user"];
            base.ViewData["assessment"] = base.Request["AssessmentType"];
            base.ViewData["jobrole"] = base.Request["jobRole"];
            if (base.Request["user"] == null)
            {
                base.ViewData["message"] = "Select at least a user to view compare results";
            }
            else if (base.Request["AssessmentType"] == null || base.Request["AssessmentType"] == "0")
            {
                base.ViewData["message"] = "Select assessment type to compare";
            }
            else
            {
                string[] topicNames = new string[8];
                string testIds = SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spGetTestsIdByType", new SqlParameter[]
				{
					new SqlParameter("@userIds", base.Request["user"].ToString()),
					new SqlParameter("@type", base.Request["AssessmentType"].ToString()),
					new SqlParameter("@jobroleId", base.Request["jobRole"].ToString())
				}).ToString();
                if (testIds.Length > 0)
                {
                    string[] testId = testIds.Split(new char[]
					{
						','
					});
                    object[] obj = new object[8];
                    Series[] ser = new Series[testId.Length];
                    if (testId.Length > 0)
                    {
                        for (int i = 0; i < testId.Length; i++)
                        {
                            SqlParameter[] sqlParamTopic = new SqlParameter[]
							{
								new SqlParameter("@testId", testId[i])
							};
                            DataSet Topic = null;
                            if (System.Convert.ToInt32(base.Request["AssessmentType"].ToString()) == 1)
                            {
                                Topic = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSsaGraph", sqlParamTopic);
                            }
                            else if (System.Convert.ToInt32(base.Request["AssessmentType"].ToString()) == 2)
                            {
                                Topic = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetBaGraph", sqlParamTopic);
                            }
                            else if (System.Convert.ToInt32(base.Request["AssessmentType"].ToString()) == 3)
                            {
                                Topic = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetNpGraph", sqlParamTopic);
                            }
                            else if (System.Convert.ToInt32(base.Request["AssessmentType"].ToString()) == 4)
                            {
                                Topic = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetMPGraph", sqlParamTopic);
                            }
                            else if (System.Convert.ToInt32(base.Request["AssessmentType"].ToString()) == 5)
                            {
                                Topic = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetCMAGraph", sqlParamTopic);
                            }
                            if (i == 0)
                            {
                                for (int j = 0; j < Topic.Tables[0].Rows.Count; j++)
                                {
                                    topicNames[j] = Topic.Tables[0].Rows[j]["topicTitle"].ToString().Replace("<br />", " ");
                                }
                            }
                            ser[i] = new Series
                            {
                                Name = HttpUtility.JavaScriptStringEncode(Topic.Tables[0].Rows[0]["name"].ToString()),
                                Data = new Data(new object[]
								{
									System.Convert.ToDouble(Topic.Tables[0].Rows[0]["percentage"].ToString()),
									System.Convert.ToDouble(Topic.Tables[0].Rows[1]["percentage"].ToString()),
									System.Convert.ToDouble(Topic.Tables[0].Rows[2]["percentage"].ToString()),
									System.Convert.ToDouble(Topic.Tables[0].Rows[3]["percentage"].ToString()),
									System.Convert.ToDouble(Topic.Tables[0].Rows[4]["percentage"].ToString()),
									System.Convert.ToDouble(Topic.Tables[0].Rows[5]["percentage"].ToString()),
									System.Convert.ToDouble(Topic.Tables[0].Rows[6]["percentage"].ToString()),
									System.Convert.ToDouble(Topic.Tables[0].Rows[7]["percentage"].ToString())
								})
                            };
                        }
                    }
                    chart = new Highcharts("chart").InitChart(new Chart
                    {
                        DefaultSeriesType = new ChartTypes?(ChartTypes.Column)
                    }).SetTitle(new Title
                    {
                        Text = "Compare Result (Percentage based)"
                    }).SetSubtitle(new Subtitle
                    {
                        Text = "Source: SkillsGapAnalysis"
                    }).SetXAxis(new XAxis
                    {
                        Categories = topicNames
                    }).SetYAxis(new YAxis
                    {
                        Min = new Number?(0),
                        Title = new YAxisTitle
                        {
                            Text = "Score (Percentage)"
                        }
                    }).SetLegend(new Legend
                    {
                        Layout = Layouts.Vertical,
                        Align = new HorizontalAligns?(HorizontalAligns.Left),
                        VerticalAlign = new VerticalAligns?(VerticalAligns.Top),
                        X = new Number?(100),
                        Y = new Number?(70),
                        Floating = new bool?(true),
                        BackgroundColor = new Color?(ColorTranslator.FromHtml("#FFFFFF")),
                        Shadow = new bool?(true)
                    }).SetTooltip(new Tooltip
                    {
                        Formatter = "function() { return ''+ this.x +': '+ this.y +' %'; }"
                    }).SetPlotOptions(new PlotOptions
                    {
                        Column = new PlotOptionsColumn
                        {
                            PointPadding = new Number?(0.2),
                            BorderWidth = new Number?(0)
                        }
                    }).SetSeries(ser);
                }
            }
            return base.View(chart);
        }

        [HttpPost]
        public JsonResult GetTopicsByTest(int id)
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetTopicByTest", new SqlParameter[]
			{
				new SqlParameter("@assessmentType", id)
			});
            EnumerableRowCollection<Topic> myData = from r in ds.Tables[0].AsEnumerable()
                                                    select new Topic
                                                    {
                                                        topicTitle = r.Field<string>("topicTitle"),
                                                        topicId = r.Field<int>("topicId")
                                                    };
            return new JsonResult
            {
                Data = myData.ToList<Topic>()
            };
        }

        [Authorize]
        public ActionResult ReviewResult()
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetUsersByCompany", new SqlParameter[]
			{
				new SqlParameter("@userId", SGACommon.LoginUserInfo.userId)
			});
            base.ViewData["ds"] = ds;
            base.ViewData["message"] = "";
            base.ViewData["users"] = null;
            base.ViewData["assessment"] = null;
            base.ViewData["jobrole"] = null;
            Highcharts chart = null;
            return base.View(chart);
        }

        [Authorize, HttpPost]
        public ActionResult ReviewResult(FormCollection frm)
        {
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetUsersByCompany", new SqlParameter[]
			{
				new SqlParameter("@userId", SGACommon.LoginUserInfo.userId)
			});
            Highcharts chart = null;
            base.ViewData["ds"] = ds;
            base.ViewData["message"] = "";
            base.ViewData["users"] = base.Request["user"];
            base.ViewData["assessment"] = base.Request["AssessmentType"];
            base.ViewData["jobrole"] = base.Request["jobRole"];
            if (base.Request["user"] == null)
            {
                base.ViewData["message"] = "Select at least a user to view compare results";
            }
            else if (base.Request["AssessmentType"] == null || base.Request["AssessmentType"] == "0")
            {
                base.ViewData["message"] = "Select assessment type to compare";
            }
            else
            {
                string[] topicNames = new string[8];
                string testIds = SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "spGetTestsIdByType", new SqlParameter[]
				{
					new SqlParameter("@userIds", base.Request["user"].ToString()),
					new SqlParameter("@type", base.Request["AssessmentType"].ToString()),
					new SqlParameter("@jobroleId", base.Request["jobRole"].ToString())
				}).ToString();
                if (testIds.Length > 0)
                {
                    string[] testId = testIds.Split(new char[]
					{
						','
					});
                    object[] obj = new object[8];
                    Series[] ser = new Series[testId.Length * 2];
                    if (testId.Length > 0)
                    {
                        for (int i = 0; i < testId.Length; i++)
                        {
                            SqlParameter[] sqlParamTopic = new SqlParameter[]
							{
								new SqlParameter("@testId", testId[i])
							};
                            DataSet Topic = null;
                            DataSet TopicReviewed = null;
                            if (System.Convert.ToInt32(base.Request["AssessmentType"].ToString()) == 1)
                            {
                                Topic = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSsaGraph", sqlParamTopic);
                                TopicReviewed = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetSsaGraphReviewed", sqlParamTopic);
                            }
                            else if (System.Convert.ToInt32(base.Request["AssessmentType"].ToString()) == 2)
                            {
                                Topic = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetBaGraph", sqlParamTopic);
                                TopicReviewed = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetBaGraphReviewed", sqlParamTopic);
                            }
                            else if (System.Convert.ToInt32(base.Request["AssessmentType"].ToString()) == 3)
                            {
                                Topic = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetNpGraph", sqlParamTopic);
                                TopicReviewed = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetNPGraphReviewed", sqlParamTopic);
                            }
                            else if (System.Convert.ToInt32(base.Request["AssessmentType"].ToString()) == 4)
                            {
                                Topic = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetMPGraph", sqlParamTopic);
                                TopicReviewed = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetMpGraphReviewed", sqlParamTopic);
                            }
                            else if (System.Convert.ToInt32(base.Request["AssessmentType"].ToString()) == 5)
                            {
                                Topic = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetCMAGraph", sqlParamTopic);
                                TopicReviewed = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetCMAGraphReviewed", sqlParamTopic);
                            }
                            if (i == 0)
                            {
                                for (int j = 0; j < Topic.Tables[0].Rows.Count; j++)
                                {
                                    topicNames[j] = Topic.Tables[0].Rows[j]["topicTitle"].ToString().Replace("<br />", " ");
                                }
                                ser[i] = new Series
                                {
                                    Name = HttpUtility.JavaScriptStringEncode(Topic.Tables[0].Rows[0]["name"].ToString()),
                                    Data = new Data(new object[]
									{
										System.Convert.ToDouble(Topic.Tables[0].Rows[0]["percentage"].ToString()),
										System.Convert.ToDouble(Topic.Tables[0].Rows[1]["percentage"].ToString()),
										System.Convert.ToDouble(Topic.Tables[0].Rows[2]["percentage"].ToString()),
										System.Convert.ToDouble(Topic.Tables[0].Rows[3]["percentage"].ToString()),
										System.Convert.ToDouble(Topic.Tables[0].Rows[4]["percentage"].ToString()),
										System.Convert.ToDouble(Topic.Tables[0].Rows[5]["percentage"].ToString()),
										System.Convert.ToDouble(Topic.Tables[0].Rows[6]["percentage"].ToString()),
										System.Convert.ToDouble(Topic.Tables[0].Rows[7]["percentage"].ToString())
									})
                                };
                                ser[i + 1] = new Series
                                {
                                    Name = HttpUtility.JavaScriptStringEncode(TopicReviewed.Tables[0].Rows[0]["name"].ToString()),
                                    Data = new Data(new object[]
									{
										System.Convert.ToDouble(TopicReviewed.Tables[0].Rows[0]["percentage"].ToString()),
										System.Convert.ToDouble(TopicReviewed.Tables[0].Rows[1]["percentage"].ToString()),
										System.Convert.ToDouble(TopicReviewed.Tables[0].Rows[2]["percentage"].ToString()),
										System.Convert.ToDouble(TopicReviewed.Tables[0].Rows[3]["percentage"].ToString()),
										System.Convert.ToDouble(TopicReviewed.Tables[0].Rows[4]["percentage"].ToString()),
										System.Convert.ToDouble(TopicReviewed.Tables[0].Rows[5]["percentage"].ToString()),
										System.Convert.ToDouble(TopicReviewed.Tables[0].Rows[6]["percentage"].ToString()),
										System.Convert.ToDouble(TopicReviewed.Tables[0].Rows[7]["percentage"].ToString())
									})
                                };
                            }
                            else
                            {
                                int k = i + 1;
                                ser[k] = new Series
                                {
                                    Name = HttpUtility.JavaScriptStringEncode(Topic.Tables[0].Rows[0]["name"].ToString()),
                                    Data = new Data(new object[]
									{
										System.Convert.ToDouble(Topic.Tables[0].Rows[0]["percentage"].ToString()),
										System.Convert.ToDouble(Topic.Tables[0].Rows[1]["percentage"].ToString()),
										System.Convert.ToDouble(Topic.Tables[0].Rows[2]["percentage"].ToString()),
										System.Convert.ToDouble(Topic.Tables[0].Rows[3]["percentage"].ToString()),
										System.Convert.ToDouble(Topic.Tables[0].Rows[4]["percentage"].ToString()),
										System.Convert.ToDouble(Topic.Tables[0].Rows[5]["percentage"].ToString()),
										System.Convert.ToDouble(Topic.Tables[0].Rows[6]["percentage"].ToString()),
										System.Convert.ToDouble(Topic.Tables[0].Rows[7]["percentage"].ToString())
									})
                                };
                                ser[k + 1] = new Series
                                {
                                    Name = HttpUtility.JavaScriptStringEncode(TopicReviewed.Tables[0].Rows[0]["name"].ToString()),
                                    Data = new Data(new object[]
									{
										System.Convert.ToDouble(TopicReviewed.Tables[0].Rows[0]["percentage"].ToString()),
										System.Convert.ToDouble(TopicReviewed.Tables[0].Rows[1]["percentage"].ToString()),
										System.Convert.ToDouble(TopicReviewed.Tables[0].Rows[2]["percentage"].ToString()),
										System.Convert.ToDouble(TopicReviewed.Tables[0].Rows[3]["percentage"].ToString()),
										System.Convert.ToDouble(TopicReviewed.Tables[0].Rows[4]["percentage"].ToString()),
										System.Convert.ToDouble(TopicReviewed.Tables[0].Rows[5]["percentage"].ToString()),
										System.Convert.ToDouble(TopicReviewed.Tables[0].Rows[6]["percentage"].ToString()),
										System.Convert.ToDouble(TopicReviewed.Tables[0].Rows[7]["percentage"].ToString())
									})
                                };
                            }
                        }
                    }
                    chart = new Highcharts("chart").InitChart(new Chart
                    {
                        DefaultSeriesType = new ChartTypes?(ChartTypes.Column)
                    }).SetTitle(new Title
                    {
                        Text = "Compare Result (Percentage based)"
                    }).SetSubtitle(new Subtitle
                    {
                        Text = "Source: SkillsGapAnalysis"
                    }).SetXAxis(new XAxis
                    {
                        Categories = topicNames
                    }).SetYAxis(new YAxis
                    {
                        Min = new Number?(0),
                        Title = new YAxisTitle
                        {
                            Text = "Score (Percentage)"
                        }
                    }).SetLegend(new Legend
                    {
                        Layout = Layouts.Vertical,
                        Align = new HorizontalAligns?(HorizontalAligns.Left),
                        VerticalAlign = new VerticalAligns?(VerticalAligns.Top),
                        X = new Number?(100),
                        Y = new Number?(70),
                        Floating = new bool?(true),
                        BackgroundColor = new Color?(ColorTranslator.FromHtml("#FFFFFF")),
                        Shadow = new bool?(true)
                    }).SetTooltip(new Tooltip
                    {
                        Formatter = "function() { return ''+ this.x +': '+ this.y +' %'; }"
                    }).SetPlotOptions(new PlotOptions
                    {
                        Column = new PlotOptionsColumn
                        {
                            PointPadding = new Number?(0.2),
                            BorderWidth = new Number?(0)
                        }
                    }).SetSeries(ser);
                }
            }
            return base.View(chart);
        }

        public ActionResult Logout()
        {
            base.Session.Abandon();
            FormsAuthentication.SignOut();
            return this.Redirect("~/index.aspx");
        }
    }
}
