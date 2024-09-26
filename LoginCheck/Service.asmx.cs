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

                //Check Company UserType Details
                string query = "SELECT * FROM [dbo].[SubCompany] WHERE [Company] = '" + Company + "'AND Branch = '" + Branch + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    try
                    {
                        while (dr.Read())
                        {
                            try
                            {
                                UserStructure = dr["UserStructure"].ToString();
                                //  UserCount = Convert.ToInt32( dr["License"].ToString());
                                ID = Convert.ToInt32(dr["ID"].ToString());
                                STATUS = dr["STATUS"].ToString();
                            }
                            catch (Exception)
                            { }

                            break;
                        }
                    }
                    catch (Exception)
                    {

                    }
                } //Get user structure
                else
                {
                    //No user structure setup, assuming that the userStructure is per user
                }


                //Check UserStructure
                if (UserStructure != string.Empty)
                {
                    if (STATUS=="ACTIVE")
                    {
                        if (UserStructure == "CL")
                        {
                            //Check Current User status
                            query = "SELECT [LoginStatus] FROM [dbo].[UserLoginStatus] WHERE [UserName] = '" + User + "'";
                            cmd = new SqlCommand(query, conn);
                            dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    try
                                    {
                                        LoginStatus = dr.GetString(0);
                                        break;
                                    }
                                    catch (Exception)
                                    {
                                    }
                                }

                                //Check User Loging Status

                                if (LoginStatus.ToUpper() == "ACTIVE")
                                {
                                    //Update Last active                                
                                    query = "UPDATE [dbo].[UserLoginStatus] SET [LoginStatus] = 'ACTIVE',[LastActivity] = getdate() WHERE [Username] = '" + User + "'";
                                    cmd = new SqlCommand(query, conn);
                                    cmd.ExecuteNonQuery();
                                    bReturn = "1";
                                    //Return TRUE
                                }//Allow user to proceed
                                else if (LoginStatus.ToUpper() == "INACTIVE")
                                {
                                    query = "SELECT * FROM [dbo].[ParentCompany] WHERE [ID] = '" + ID + "'";
                                    cmd = new SqlCommand(query, conn);
                                    dr = cmd.ExecuteReader();
                                    string ParentCompany = "";
                                    
                                    if (dr.HasRows)
                                    {
                                        while (dr.Read())
                                        {

                                            try
                                            {
                                                ParentCompany = dr["Company"].ToString();
                                                UserCount = Convert.ToInt32(dr["License"].ToString());
                                            }
                                            catch (Exception)
                                            {

                                            }
                                            break;
                                        }
                                    }
                                    else
                                    {

                                    }
                                    int UserCountReturn = 0;
                                    if (ParentCompany == string.Empty)
                                    {
                                        //Get Current active Users
                                        query = "SELECT COUNT([UserName]) FROM [dbo].[UserLoginStatus] WHERE [LoginStatus] = 'ACTIVE'";
                                        cmd = new SqlCommand(query, conn);
                                        dr = cmd.ExecuteReader();
                                        if (dr.HasRows)
                                        {

                                            while (dr.Read())
                                            {
                                                try
                                                {
                                                    UserCountReturn = dr.GetInt32(0);
                                                    break;
                                                }
                                                catch (Exception)
                                                {

                                                }
                                            }

                                        }//Compare Users Allowed to Current Users information
                                        else
                                        {

                                        }//No users logged in for company
                                        if (UserCountReturn < UserCount || UserCountReturn == 0)
                                        {
                                            //Update Last active                                
                                            query = "UPDATE [dbo].[UserLoginStatus] SET [LoginStatus] = 'ACTIVE',[LastActivity] = getdate() WHERE [UserName] = '" + User + "'";
                                            cmd = new SqlCommand(query, conn);
                                            cmd.ExecuteNonQuery();

                                            //Insert
                                          //  query = "INSERT INTO [UserLoginStatus] ([Company ID], [UserName], [LoginStatus], [LastActivity]) " +
                                          //"VALUES ('" + ID + "', '" + User + "', 'ACTIVE', getdate());";
                                          //  cmd = new SqlCommand(query, conn);
                                          //  cmd.ExecuteNonQuery();


                                            bReturn = "1";
                                            //Return TRUE
                                        }
                                        else
                                        {

                                          
                                            //Update User Status                           
                                            query = "UPDATE [dbo].[UserLoginStatus] SET [LoginStatus] = 'LOGGEDOUT' WHERE [UserName] = '" + User + "'";
                                            cmd = new SqlCommand(query, conn);
                                            cmd.ExecuteNonQuery();
                                            //Dispose Session Variable
                                            Session["Company"] = "";
                                            Session["UsernameVariable"] = "";
                                            Session["UserBranch"] = "";
                                            Session["LoginMessage"] = "You have been logged out due to inactivity";
                                            bReturn = "0";
                                        }
                                    } //No parent company assigned to user's company
                                    else
                                    {
                                        query = "SELECT [Company] FROM [dbo].[ParentCompany] WHERE [ID] = '" + ID + "'";
                                        cmd = new SqlCommand(query, conn);
                                        dr = cmd.ExecuteReader();
                                        if (dr.HasRows)
                                        {
                                            while (dr.Read())
                                            {
                                                string CompName = dr.GetString(0);
                                                query = "SELECT COUNT([Username]) FROM [dbo].[UserLoginStatus] WHERE [LoginStatus] = 'ACTIVE'";
                                                cmd = new SqlCommand(query, conn);
                                                SqlDataReader dr1 = cmd.ExecuteReader();
                                                if (dr1.HasRows)
                                                {
                                                    while (dr1.Read())
                                                    {
                                                        try
                                                        {
                                                            int iCount = dr1.GetInt32(0);
                                                            UserCountReturn = UserCountReturn + iCount;
                                                        }
                                                        catch (Exception)
                                                        {

                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        if (UserCountReturn < UserCount || UserCountReturn == 0)
                                        {
                                            //Update Last active                                
                                            query = "UPDATE [dbo].[UserLoginStatus] SET [LoginStatus] = 'ACTIVE',[LastActivity] = getdate() WHERE [UserName] = '" + User + "'";
                                            cmd = new SqlCommand(query, conn);
                                            cmd.ExecuteNonQuery();
                                            bReturn = "1";
                                            //Return TRUE
                                        }
                                        else
                                        {
                                            //Update User Status                           
                                            query = "UPDATE [dbo].[UserLoginStatus] SET [LoginStatus] = 'LOGGEDOUT' WHERE [UserName] = '" + User + "'";
                                            cmd = new SqlCommand(query, conn);
                                            cmd.ExecuteNonQuery();
                                            //Dispose Session Variable
                                            Session["Company"] = "";
                                            Session["UsernameVariable"] = "";
                                            Session["UserBranch"] = "";
                                            Session["LoginMessage"] = "You have been logged out due to inactivity";
                                            bReturn = "0";
                                        }
                                    }// Parent Company assigned to users company
                                }//Check if there are licenses available
                                else if (LoginStatus.ToUpper() == "LOGGEDOUT")
                                {
                                    query = "UPDATE [dbo].[UserLoginStatus] SET [LoginStatus] = 'LOGGEDOUT' WHERE [UserName] = '" + User + "'";
                                    cmd = new SqlCommand(query, conn);
                                    cmd.ExecuteNonQuery();
                                    try
                                    {
                                        // RecordLogout(Session["Company"].ToString(), Session["UserBranch"].ToString(), username, Session["UserType"].ToString());
                                    }
                                    catch (Exception)
                                    { }

                                    //Dispose Session Variable
                                    Session["Company"] = "";
                                    Session["UsernameVariable"] = "";
                                    Session["UserBranch"] = "";
                                    Session["LoginMessage"] = "You have been logged out due to inactivity";
                                    bReturn = "0";
                                }//Log user out

                                else
                                {
                                    bReturn = "0";
                                }
                            }
                            else
                            {

                                query = "INSERT INTO [dbo].[UserLoginStatus] ([Company ID],[Company], [UserName], [LastActivity], [LoginStatus]) " +
                                 "VALUES('" + ID + "','" + Company + "', '" + User + "',getdate(), 'ACTIVE')";

                                cmd = new SqlCommand(query, conn);
                                cmd.ExecuteNonQuery();

                                //Session["Company"] = "";
                                //Session["UsernameVariable"] = "";
                                //Session["UserBranch"] = "";
                                //Session["LoginMessage"] = "No Record of your login, please login again";
                                bReturn = "1";
                            }

                        } //Cumulative licenses
                        else
                        {
                            //UserStructure == IL
                            bReturn = "1";
                        }
                    }
                    else
                    {
                        bReturn = "0";
                    }
                }//User Structure Exists
                else
                {
                    query = "UPDATE [dbo].[UserLoginStatus] SET [LoginStatus] = 'ACTIVE',[LastActivity] = getdate() WHERE [UserName] = '" + User + "'";
                    cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    bReturn = "1";

                    //Assume Per User
                }//No User structure exists



                return bReturn;
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
                    try
                    {
                        while (dr.Read())
                        {
                            int ID = Convert.ToInt32(dr["ID"]);


                           

                            int idleMinutes = Convert.ToInt32(dr["Idle"]);
                            TimeSpan allowedIdleTime = TimeSpan.FromMinutes(idleMinutes);

                            //Get SubCompany 
                            query = "SELECT * FROM [dbo].[SubCompany] WHERE [ID] = '" + ID + "'";
                            cmd = new SqlCommand(query, conn);
                            SqlDataReader dr2 = cmd.ExecuteReader();

                            if (dr2.HasRows)
                            {
                                while (dr2.Read())
                                {
                                    string subCompanyName = dr2["Company"].ToString();


                                    query = "SELECT * FROM [dbo].[UserLoginStatus] WHERE [Company] = '" + subCompanyName + "'";
                                    cmd = new SqlCommand(query, conn);
                                    SqlDataReader dr3 = cmd.ExecuteReader();

                                    if (dr3.HasRows)
                                    {
                                        while (dr3.Read())
                                        {
                                            DateTime lastActivity = Convert.ToDateTime(dr3["LastActivity"]);
                                            string userName = dr3["UserName"].ToString();
                                            string loginStatus = dr3["LoginStatus"].ToString();
                                            TimeSpan idleTime = currentTime - lastActivity;

                                            if (idleTime.TotalMinutes > allowedIdleTime.TotalMinutes && loginStatus.ToUpper() == "ACTIVE")
                                            {
                                                string updateQuery = "UPDATE [dbo].[UserLoginStatus] SET LoginStatus = 'INACTIVE' WHERE UserName = '" + userName + "'";
                                                SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn);
                                                cmdUpdate.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {


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
        public int RecordPrimaryCompany(string Company, int License, string UserStructure, string idle)
        {
           try
           {
             using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["webafricaConnectionString"].ConnectionString))
                   
             {
                        int ID = 0;
                        conn.Open();
                    string query = "INSERT INTO [dbo].[ParentCompany] " +
                  "([Company],[License],[UserStructure],[Idle],[STATUS]) " +
                  "VALUES " +
                  "('" + Company + "','" + License + "','" + UserStructure + "','" + idle + "','ACTIVE')";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    query = "SELECT TOP(1)ID FROM [dbo].[ParentCompany] WHERE Company = '" + Company + "'";
                        cmd = new SqlCommand(query, conn);
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                ID  = Convert.ToInt32(dr["ID"].ToString());
                                break;
                            }
                        }
                        return ID;

             }
           }
                catch (Exception)
                {
                    return 0;
                }
                return 0;

        }


        [WebMethod]
        public string RecordSubCompany(string Company,string Branch, string UserStructure, int ID, string PCompany)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["webafricaConnectionString"].ConnectionString))

                {
                  
                    conn.Open();
                    string query = "INSERT INTO [dbo].[SubCompany]" +
                                "([ID],[Company],[Branch],[UserStructure],[DateOfCapture], [ParentCompany],[STATUS])" +
                    "VALUES" +
                      "('" + ID + "','" + Company + "','" + Branch + "','" + UserStructure + "','" + DateTime.Now + "','" + PCompany + "','ACTIVE')";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();

                    return "1";

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
