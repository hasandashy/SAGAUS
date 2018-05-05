using CookComputing.XmlRpc;
using DataTier;
using InfusionSoftDotNet;
using SGA.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGA
{
    public partial class emailTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void imb_Click(object sender, ImageClickEventArgs e)
        {
            StringBuilder _sb = new StringBuilder();
           
            DataSet dsUsers = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetCompletedCAA");
            if (dsUsers != null)
            {
                if (dsUsers.Tables.Count > 0 && dsUsers.Tables[0].Rows.Count > 0)
                {
                    for(int i = 0; i < dsUsers.Tables[0].Rows.Count; i++)
                    {
                        string[] strField = new string[]
                {
                            "Id"
                };
                        XmlRpcStruct[] resultFound = isdnAPI.findByEmail(dsUsers.Tables[0].Rows[i]["email"].ToString(), strField);
                        int userId;
                        XmlRpcStruct Contact = new XmlRpcStruct();
                        if (resultFound.Length > 0)
                        {
                            userId = System.Convert.ToInt32(resultFound[0]["Id"].ToString());


                            if (resultFound[0]["Id"] != null && resultFound[0]["Id"].ToString() != String.Empty)
                            {

                                SqlParameter[] paramPack = new SqlParameter[]
                  {
                new SqlParameter("@userId", SqlDbType.Int)
                  };
                                paramPack[0].Value = Convert.ToInt32(dsUsers.Tables[0].Rows[i]["Id"]);
                                DataSet dsPacks = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "spGetReportIdByUserId", paramPack);
                                if (dsPacks != null)
                                {
                                    if (dsPacks.Tables.Count > 0 && dsPacks.Tables[0].Rows.Count > 0)
                                    {
                                        if (dsPacks.Tables[0].Rows[0]["jobRole"].ToString() == "6")
                                        {
                                            string Url = "http://" + base.Request.UrlReferrer.Host + "/IndividualReport/ContractManagement.aspx?Id=" + dsPacks.Tables[0].Rows[0]["reportId"].ToString();
                                            //Contact.Add("_SAGovContractManagement18UserReportLink", Url);
                                            //Contact.Add("ContactType", "Customer");
                                            _sb.Append(paramPack[0].Value + " : "+ dsUsers.Tables[0].Rows[i]["email"].ToString() + " : " +Url +" | ");
                                            //isdnAPI.dsUpdate("Contact", userId, Contact);
                                        }
                                        else
                                        {                                            
                                            string Url = "http://" + base.Request.UrlReferrer.Host + "/IndividualReport/Procurement.aspx?Id=" + dsPacks.Tables[0].Rows[0]["reportId"].ToString();
                                            //Contact.Add("_SAGovProcurement18UserReportLink", Url);
                                            //Contact.Add("ContactType", "Customer");
                                            //isdnAPI.dsUpdate("Contact", userId, Contact);
                                            _sb.Append(paramPack[0].Value + " : " + dsUsers.Tables[0].Rows[i]["email"].ToString() + " : " + Url + " | ");
                                        }

                                    }
                                }
                            }

                        }
                    }
            }
               

            }

            lblUserId.Text = _sb.ToString();
        }
    }
}