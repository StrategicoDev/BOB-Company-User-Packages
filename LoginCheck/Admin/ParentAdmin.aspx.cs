using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace LocationRepresentation.admin.CCAdmin
{
    public partial class ParentAdmin : System.Web.UI.Page
    {
        Service service1 = new Service();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

     

        protected void btnClearFilter_Click(System.Object sender, System.EventArgs e)
        {
            txtCompany.Text = "";
            txtIdle.Text = "";
            txtLicense.Text = "";
            txtUserStructure.Text="";
            GridView1.DataBind();
            GridView2.DataBind();
           

        }

        protected void btnSubCompany_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/SubCompany.aspx");
        }

        protected void btnParentCompany_Click(object sender, EventArgs e)
        {
            //public int RecordPrimaryCompany(string Company, int License, string UserStructure, string idle)


            service1.RecordPrimaryCompany(txtCompany.Text, Convert.ToInt32(txtLicense.Text), txtUserStructure.Text, txtIdle.Text);

        }
    }
}