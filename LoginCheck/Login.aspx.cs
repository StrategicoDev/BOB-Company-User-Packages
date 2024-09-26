using LocationRepresentation.BoBService;
using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Web;
using Owin;
using LocationRepresentation.Models;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Net.NetworkInformation;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;


namespace LocationRepresentation.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Session["METHOD"] = "QR";
                    txtQRCode.Focus();
                    lblError.Text = Session["LoginMessage"].ToString();
                }
                catch (Exception)
                {
                }
                
            }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            string result = "";
            Service service1 = new Service();
            bool loggedIn = service1.LoginService(Email.Text.Trim(), Password.Text.Trim());
            if (loggedIn == true)
            {
                Session["LoginMessage"] = "";
                Response.Redirect("~/Default.aspx",false);
            }
            else
            {
                lblError.Text = Session["LoginMessage"].ToString();
            }          
        }

        protected void Password_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Email_TextChanged(object sender, EventArgs e)
        {

        }
        protected string GetIPAddress()
        {
            Random random = new Random();
            string r = "";
            int i;
            for (i = 1; i < 11; i++)
            {
                r += random.Next(0, 9).ToString();
            }
            Session["LoginControl"] = r;
            return r;

        }

        protected void btnLoginQR_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["webafricaConnectionString"].ConnectionString))
            {
                conn.Open();
                string query = "SELECT [Username],[Password] From Login WHERE UserID = '" + txtQRCode.Text + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Service service1 = new Service();
                        bool loggedIn = service1.LoginService(dr["Username"].ToString(), dr["Password"].ToString());
                        if (loggedIn == true)
                        {
                            Session["LoginMessage"] = "";
                            Response.Redirect("~/Default.aspx", false);
                        }
                        else
                        {
                            lblError.Text = Session["LoginMessage"].ToString();
                        }
                        break;
                    }
                }
                else
                {
                    txtQRCode.Text = "";
                    txtQRCode.Focus();
                    lblError.Text = "Failed To Retrieve User Information";
                }
            }
        }

        protected void btnSwitch_Click(object sender, EventArgs e)
        {
            if (Session["METHOD"].ToString() == "QR")
            {
                Email.Text = "";
                Password.Text = "";
                pnlQRCode.Visible = false;
                pnlUser.Visible = true;
                Session["METHOD"] = "USERPASS";
            }
            else
            {
                txtQRCode.Text = "";
                txtQRCode.Focus();
                pnlQRCode.Visible = true;
                pnlUser.Visible = false;
                Session["METHOD"] = "QR";
            }
        }
    }
}