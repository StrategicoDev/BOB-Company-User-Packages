using LocationRepresentation.Admin;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace LocationRepresentation
{
    /// <summary>
    /// Summary description for Service
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {

  
        [WebMethod]
        public string CheckLoginStatus(string User, string Company, string Branch)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["webafricaConnectionString"].ConnectionString))
            {
                conn.Open();

                //Variable Declaration
                string bReturn = "";
                string UserStructure = "";
                string STATUS = "";
                int UserCount = 0;
                string LoginStatus = "";
                int ID = 0;
                string ParentCompany = "";

                //Check Company UserType Details
                string query = "SELECT * FROM [dbo].[SubCompany] WHERE [Company] = '" + Company + "'AND Branch = '" + Branch + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        UserStructure = dr["UserStructure"].ToString();
                        //  UserCount = Convert.ToInt32( dr["License"].ToString());
                        ID = Convert.ToInt32(dr["ID"].ToString());
                        STATUS = dr["STATUS"].ToString();
                        ParentCompany = dr["ParentCompany"].ToString();
                        break;
                    }
                } //Get user structure
                else
                {
                    return "Company Is Not Setup To Allow Login. Please Contact Your Administrator";
                    //No user structure setup, assuming that the userStructure is per user
                } //no user structure setup

                if (STATUS == "ACTIVE")
                {
                    if (UserStructure == "CL")
                    {
                        //Check If User is currently marked as active
                        query = "SELECT [LoginStatus] FROM [dbo].[UserLoginStatus] " +
                            "WHERE [UserName] = '" + User + "' and Company = '" + Company + "' and LoginStatus = 'ACTIVE'";
                        cmd = new SqlCommand(query, conn);
                        dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            //Update Last active                                
                            query = "UPDATE [dbo].[UserLoginStatus] " +
                                "SET [LoginStatus] = 'ACTIVE',[LastActivity] = getdate() WHERE [Username] = '" + User + "' and Company = '" + Company + "'";
                            cmd = new SqlCommand(query, conn);
                            cmd.ExecuteNonQuery();
                            return "1";
                        }//User is active
                        else
                        {
                            dr.Close();
                            //Get total users allowed to be logged in
                            query = "SELECT * FROM [dbo].[ParentCompany] WHERE [Company] = '" + ParentCompany + "'";
                            cmd = new SqlCommand(query, conn);
                            dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    UserCount = Convert.ToInt32(dr["License"].ToString());
                                    break;
                                }
                            }

                            //Get Total Users Currently Logged In
                            query = " SELECT COUNT([UserName]) FROM [dbo].[UserLoginStatus] " +
                                        "WHERE LoginStatus = 'ACTIVE' and Company in (SELECT [Company] FROM [dbo].[SubCompany] " +
                                                            "WHERE ParentCompany = '" + ParentCompany + "')";
                            cmd = new SqlCommand(query, conn);
                            int UsersLoggedInCount = Convert.ToInt32(cmd.ExecuteScalar());
                            if (UsersLoggedInCount < UserCount)
                            {
                                query = "SELECT [LoginStatus] FROM [dbo].[UserLoginStatus] " +
                                        "WHERE [UserName] = '" + User + "' and Company = '" + Company + "'";
                                cmd = new SqlCommand(query, conn);
                                dr = cmd.ExecuteReader();
                                if (dr.HasRows)
                                {
                                    query = "UPDATE [dbo].[UserLoginStatus] " +
                                            "SET [LoginStatus] = 'ACTIVE',[LastActivity] = getdate() " +
                                            "WHERE [Username] = '" + User + "' and Company = '" + Company + "'";
                                    cmd = new SqlCommand(query, conn);
                                    cmd.ExecuteNonQuery();
                                    return "1";
                                }//Update user
                                else
                                {
                                    query = "INSERT INTO [dbo].[UserLoginStatus] " +
                                            "([Company ID],[Company], [UserName], [LastActivity], [LoginStatus]) " +
                                            "VALUES " +
                                            "('" + ID + "','" + Company + "', '" + User + "',getdate(), 'ACTIVE')";
                                    cmd = new SqlCommand(query, conn);
                                    cmd.ExecuteNonQuery();
                                    return "1";
                                }//insert user
                            }//Their is space to allow another login
                            else
                            {
                                return "There Are Currently No Licenses Available. Please Try Again Later";
                            }//No Space Available
                        }//User is either not active, or first time login in
                    } //Concurrent licenses
                    else
                    {
                        query = "SELECT [LoginStatus] FROM [dbo].[UserLoginStatus] WHERE [UserName] = '" + User + "' and Company = '"+Company+"'";
                        cmd = new SqlCommand(query, conn);
                        dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            query = "UPDATE [dbo].[UserLoginStatus] " +
                                "SET [LoginStatus] = 'ACTIVE', [LastActivity] = getdate() WHERE [UserName] = '" + User + "' and Company = '"+Company+"'";
                            cmd = new SqlCommand(query, conn);
                            cmd.ExecuteNonQuery();
                        }//Update Login Status
                        else
                        {
                            query = "INSERT INTO [dbo].[UserLoginStatus] " +
                                "([Company ID],[Company], [UserName], [LastActivity], [LoginStatus]) " +
                                "VALUES " +
                                "('" + ID + "','" + Company + "', '" + User + "',getdate(), 'ACTIVE')";
                            cmd = new SqlCommand(query, conn);
                            cmd.ExecuteNonQuery();
                        }//Insert Login Status

                        return "1";
                    }//Individual Licenses
                }
                else
                {
                    return "Company Is Not Active. If This Is Incorrect, Please Contact Your Administrator";
                }
            }


        }


        [WebMethod]
        public void CleanActiveUser()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["webafricaConnectionString"].ConnectionString))
            {
                conn.Open();

                // Get current DateTime
                DateTime currentTime = DateTime.Now;

                // Check Parent Company Details
                string query = "SELECT * FROM [dbo].[ParentCompany]";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        string ParentCompany = dr["Company"].ToString();
                        int idleMinutes = Convert.ToInt32(dr["Idle"]);


                        query = "UPDATE [dbo].[UserLoginStatus] SET LoginStatus = 'INACTIVE' " +
                                "WHERE DATEDIFF( minute , LastActivity , GetDate() ) >= '" + idleMinutes + "' " +
                                "and Company in (SELECT [Company] FROM [dbo].[SubCompany] WHERE ParentCompany = '" + ParentCompany + "')";
                        cmd = new SqlCommand(query, conn);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        [WebMethod]
        public void RecordLoginStats()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["webafricaConnectionString"].ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM [ParentCompany] WHERE Status = 'ACTIVE'";
                SqlCommand cmd = new SqlCommand(query,conn); 
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        //Record current login usage for parent Company
                        query = "SELECT COUNT([UserName]) FROM [dbo].[UserLoginStatus] " +
                            "WHERE LoginStatus = 'ACTIVE' and Company in (SELECT [Company] FROM [dbo].[SubCompany] " +
                                                                          "WHERE ParentCompany = '" + dr["Company"].ToString() + "')";
                        cmd = new SqlCommand(query, conn);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        query = "INSERT INTO [LoginRecord] " +
                            "([RecordType],[ParentCompany],[Company],[UserCount],[Date]) " +
                            "VALUES " +
                            "('PARENT','" + dr["Company"].ToString() + "','" + dr["Company"].ToString() + "','" + count + "',getdate())";
                        cmd = new SqlCommand(query, conn);
                        cmd.ExecuteNonQuery();

                        //Record current login usage for all sub companies
                        query = "SELECT * FROM [dbo].[SubCompany] WHERE ParentCompany = '" + dr["Company"].ToString() + "'";
                        cmd = new SqlCommand(query, conn);
                        SqlDataReader dr1 = cmd.ExecuteReader();
                        if (dr1.HasRows)
                        {
                            while (dr1.Read())
                            {
                                count = 0;
                                query = "SELECT COUNT([UserName]) FROM [dbo].[UserLoginStatus] " +
                                "WHERE LoginStatus = 'ACTIVE' and Company = '" + dr1["Company"].ToString() +"'";
                                cmd = new SqlCommand(query, conn);
                                count = Convert.ToInt32(cmd.ExecuteScalar());
                                query = "INSERT INTO [LoginRecord] " +
                                    "([RecordType],[ParentCompany],[Company],[UserCount],[Date]) " +
                                    "VALUES " +
                                    "('SUB','" + dr["Company"].ToString() + "','" + dr1["Company"].ToString() + "','" + count + "',getdate())";
                                cmd = new SqlCommand(query, conn);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }               
            }
        }



        [WebMethod]
        public void RecordLogin(string SubCompany)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["webafricaConnectionString"].ConnectionString))
            {
                conn.Open();

                string query = "SELECT COUNT([UserName]) AS [Total Users], [Company ID] " +
               "FROM [UserLoginStatus] " +
               "WHERE LoginStatus = 'ACTIVE' " +
               "AND [Company] = '" + SubCompany + "' " +
               "GROUP BY [Company ID]";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    try
                    {
                        while (dr.Read())
                        {

                            int Count = Convert.ToInt32(dr["TotalCount"].ToString());
                            int ID = Convert.ToInt32(dr["Company ID"].ToString());


                            query = "SELECT [Company] FROM ParentCompany WHERE ID = '" + ID + "'";
                            cmd = new SqlCommand(query, conn);
                            string ParentCompany = cmd.ExecuteScalar().ToString();

                            query = "INSERT INTO [UserLoginStatus] ([ParentCompany],[Company],[UserCount],[Date]) " +
                                                               "VALUES ('" + ParentCompany + "','" + SubCompany + "','" + Count + "','" + DateTime.Now + "')";
                            cmd = new SqlCommand(query, conn);
                            cmd.ExecuteNonQuery();



                        }
                    }
                    catch (Exception)
                    {

                    }
                }







            }
        }


        [WebMethod]
        public string RecordPrimaryCompany(string Company, int License, string UserStructure, string idle)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["webafricaConnectionString"].ConnectionString))
                {
                    //Check if company already exists

                    conn.Open();
                    string query = "SELECT * FROM ParentCompany WHERE Company = '" + Company + "'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        return "Company Already Exists";
                    }
                    else
                    {
                        dr.Close();
                        query = "INSERT INTO [dbo].[ParentCompany] " +
                                  "([Company],[Licenses],[UserStructure],[IdleTime],[STATUS]) " +
                                  "VALUES " +
                                  "('" + Company + "','" + License + "','" + UserStructure + "','" + idle + "','ACTIVE')";
                        cmd = new SqlCommand(query, conn);
                        cmd.ExecuteNonQuery();
                        return "1";
                    }
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }


        [WebMethod]
        public string RecordSubCompany(string Company, string PCompany)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["webafricaConnectionString"].ConnectionString))

                {
                  
                    conn.Open();

                  
                    string query = "SELECT * FROM [SubCompany] WHERE Company = '" + Company + "'and [ParentCompany] = '" + PCompany +  "'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        return "Company Already Exists";
                    }
                    else

                    {
                     query = "INSERT INTO [dbo].[SubCompany] " +
                "([Company], [DateOfCapture], [ParentCompany], [Status]) " +
                "VALUES " +
                "('" + Company + "', getdate(), '" + PCompany + "', 'ACTIVE')";

                        SqlCommand cmd1 = new SqlCommand(query, conn);
                        cmd1.ExecuteNonQuery();

                        return "1";
                    }

                    

                }
            }
            catch (Exception)
            {
                return "0";
            }
        }



        [WebMethod]
        public string VIReMail(string EmailTo, string NameTo, string SubjectText, string user)
        {
           
            try
            {
           
                SmtpClient mySmtpClient = new SmtpClient("serverm.everywhere.co.za");
                // set smtp-client with basicAuthentication
                mySmtpClient.UseDefaultCredentials = false;
                System.Net.NetworkCredential basicAuthenticationInfo = new
                   System.Net.NetworkCredential("vir@bllsa.co.za", "wBQ7Q#*Duvz2");
                mySmtpClient.Credentials = basicAuthenticationInfo;

                // add from,to mailaddresses
                MailAddress from = new MailAddress("vir@bllsa.co.za", "Check Online User");
                MailAddress to = new MailAddress(EmailTo, NameTo);
                MailMessage myMail = new System.Net.Mail.MailMessage(from, to);

                // add ReplyTo
                //MailAddress replyTo = new MailAddress("reply@example.com");
                //myMail.ReplyToList.Add(replyTo);

                // set subject and encoding
                myMail.Subject = SubjectText;
                myMail.SubjectEncoding = System.Text.Encoding.UTF8;

                // set body-message and encoding
                myMail.Body = "<b>DO NOT RESPOND</b>";
                myMail.BodyEncoding = System.Text.Encoding.UTF8;
                // text or html
                myMail.IsBodyHtml = true;

                //Attachment
                //System.Net.Mail.Attachment attachment;
              
                //myMail.Attachments.Add(attachment);

                mySmtpClient.Send(myMail);
                return "";
            }

            catch (SmtpException ex)
            {
                return ex.ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


       


    }
}
