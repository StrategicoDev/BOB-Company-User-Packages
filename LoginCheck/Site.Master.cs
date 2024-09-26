
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Collections;

namespace LocationRepresentation
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            //Enforce https
           //if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("http:"))
           //{
           //    Response.Redirect(HttpContext.Current.Request.Url.ToString().ToLower().Replace("http:", "https:"));
           //}
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Session["UserNameVariable"] as string))
            {
                //Label2.Text = Session["UserNameVariable"].ToString();
                
            }
            else
            {
                
            }
            if (!string.IsNullOrEmpty(Session["Company"] as string))
            {
                //Label3.Text = Session["Company"].ToString();
            }
            if (!string.IsNullOrEmpty(Session["UserBranch"] as string))
            {
                //Label3.Text = Session["Company"].ToString() +" (" + Session["UserBranch"].ToString() + ")";
            }

            //Service service1 = new Service();
            //Session["CompanyLogo"] = service1.GetLogo(Session["Company"].ToString());
            //Session["CompanyVAT"] = service1.GetCompanyVAT(Session["Company"].ToString());
            //Session["CompanyDesc"] = service1.GetCompanyDesc(Session["Company"].ToString());
            //ImageButton2.ImageUrl = Session["CompanyLogo"].ToString();


            //public string CompanyLogo

            //Label2.Text = Session["CurrentUserName"].ToString();
            //Label3.Text = Session["UserBranch"].ToString();
            //    get
            //    {
            //        return ImageButton2.ImageUrl;
            //    }
            //    set
            //    {
            //        ImageButton2.ImageUrl = value;
            //    }
        }

    protected void btnLogout_Click(object sender, EventArgs e)
        {
            Service service1 = new Service();
          //  service1.RecordLogout(Session["Company"].ToString(), Session["UserBranch"].ToString(), Session["UsernameVariable"].ToString(), Session["UserType"].ToString());
            try
            {
                IDictionaryEnumerator allCaches = HttpRuntime.Cache.GetEnumerator();

                while (allCaches.MoveNext())
                {
                    Cache.Remove(allCaches.Key.ToString());
                }
            }
            catch (Exception)
            {

            }

            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("~/Account/Login");

            if (Request.Cookies["JD"] != null)
            {

                HttpCookie aCookie = new HttpCookie("JD");
                aCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(aCookie);
                Session["CurrentUserName"] = null;
                Session["Company"] = null;
                Session["UserBranch"] = null;

            }
        }
       

        //protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        //{
        //    Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        //}
        protected void RemoveCookie(object sender,EventArgs e)
        {
       
       
            Session["UsernameVariable"] = null;
            Response.Redirect("~/Account/Login");
        
            if (Request.Cookies["JD"] != null)
            {
                
                HttpCookie aCookie = new HttpCookie("JD");
                aCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(aCookie);
                Session["CurrentUserName"] = null;
            }
            //HttpCookie aCookie;
            //string cookieName;
            //int limit = Request.Cookies.Count;
            //for (int i = 0; i < limit; i++)
            //{
            //    cookieName = Request.Cookies[i].Name;
            //    aCookie = new HttpCookie(cookieName);
            //    aCookie.Expires = DateTime.Now.AddDays(-1); // make it expire yesterday
            //    Response.Cookies.Add(aCookie); // overwrite it
            //}

            if (Session["RoleID"].ToString() == "2")
            {
                Session["KeepCount"] = 0;
                Response.Redirect("~/CompanyUseradmin.aspx");
            }
            else
            {
                Label4.Text = "";
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Session["KeepCount"] = "0";
            Response.Redirect("~/Default");
        }

        protected void btnHelp_Click(object sender, EventArgs e)
        {
            Session["KeepCount"] = "0";
            Response.Redirect("~/Help");
        }
    }

}