using SGA.controls;
using System;
using System.Web.UI;

namespace SGA.tna
{
    public partial class showSSAPdf : Page
    {
       
        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.ssa1.Id = base.Request.QueryString["id"].ToString();
        }
    }
}
