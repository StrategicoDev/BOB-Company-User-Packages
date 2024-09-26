using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LocationRepresentation.Admin
{
    public partial class SCompany : System.Web.UI.Page
    {
        Service service1 = new Service();
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

                if (!IsPostBack)
                {
                    if (Session["ID"] != null)
                    {
                        txtID.Text = Session["ID"].ToString();
                        txtParentCompany.Text = Session["Parent"].ToString();
                    }
                    else
                    {
                        Response.Redirect("~/Admin/Parent.aspx");
                    }

                    
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/Admin/Parent.aspx");
            }


        }

        protected void btnSubCompany_Click(object sender, EventArgs e)
        {
            //  public string RecordSubCompany(string Company,string Branch, string UserStructure, int ID)
            service1.RecordSubCompany(txtCompany.Text, txtBranch.Text, txtUserStructure.Text, Convert.ToInt32(txtID.Text), txtParentCompany.Text);
            lblMessage.Text = "Successfully Created A Sub Company";
            txtBranch.Text = "";
            txtCompany.Text = "";
            txtID.Text = "";
            txtUserStructure.Text="";
            txtParentCompany.Text="";
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Parent.aspx");
        }
    }
}