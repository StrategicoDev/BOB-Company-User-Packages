using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

namespace LocationRepresentation.Admin
{
    public partial class Parent : System.Web.UI.Page
    {
        Service service1 = new Service();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

    

        protected void btnClearFilter_Click(object sender, EventArgs e)
        {
            txtComp.Text = "";
            txtIdle.Text = "";
            txtLicense.Text = "";
            Label2.Text ="";
            ddlPackage.SelectedIndex = 0;
            GridView1.DataBind();
            GridView2.DataBind();
            txtSubCompany.Text = "";
            panelSubCompany.Visible = false;
            //GridView2.DataBind();
        }

        protected void btnParentCompany_Click(object sender, EventArgs e)
        {
            if (ddlPackage.SelectedIndex > 0)
            {
                if (txtCompany.Text != string.Empty)
                {
                    if (txtIdle.Text != string.Empty)
                    {
                        if (txtLicense.Text != string.Empty)
                        {
                            string str = service1.RecordPrimaryCompany(txtCompany.Text, Convert.ToInt32(txtLicense.Text), ddlPackage.SelectedItem.Value, txtIdle.Text);
                            if (str != "1")
                            {
                                Label2.Text = "Failed To Create Company <br> " + str;
                            }
                            else
                            {
                                txtCompany.Text = "";
                                txtIdle.Text = "";
                                txtLicense.Text = "";
                                ddlPackage.SelectedIndex = 0;
                                GridView1.DataBind();
                                //GridView2.DataBind();
                                Label2.Text = "Successfully Created Company";
                            }
                        }
                        else
                        {
                            Label2.Text = "Enter Amount of Licenses";
                            txtLicense.Focus();
                        }
                    }
                    else
                    {
                        Label2.Text = "Enter Idle Time";
                        txtIdle.Focus();
                    }
                }
                else
                {
                    Label2.Text = "Enter Company Name";
                    txtCompany.Focus();
                }
            }
            else
            {
                Label2.Text = "Select User Package Type";
            }
            
            
            
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ID = GridView1.SelectedRow.Cells[1].Text;

            string Company = GridView1.SelectedRow.Cells[2].Text;
           Session["Parent"] = Company;

            txtComp.Text = Company;

            panelSubCompany.Visible = true;
           



        }
        private DataTable GetTParentStatus()
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["webafricaConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT  [STATUS]  FROM [BobParentStatus]", conn))
                {


                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex == GridView1.EditIndex)
            //{
            //    DropDownList ddl = (DropDownList)e.Row.FindControl("DDStatus");

            //    if (ddl != null)
            //    {
            //        ddl.DataSource = GetTParentStatus();
            //        ddl.DataTextField = "STATUS";
            //        ddl.DataValueField = "STATUS";

            //        ddl.DataBind();


            //        string STATUS = DataBinder.Eval(e.Row.DataItem, "STATUS").ToString();
            //        if (!string.IsNullOrEmpty(STATUS))
            //        {
            //            ddl.SelectedValue = STATUS;
            //        }


            //        //string toLocationName = DataBinder.Eval(e.Row.DataItem, "ToLocationName").ToString();
            //        //if (!string.IsNullOrEmpty(toLocationName))
            //        //{
            //        //    ddl.SelectedValue = toLocationName;
            //        //}
            //    }
            //}


            if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                DropDownList ddl = (DropDownList)e.Row.FindControl("DDStatus");
                if (ddl != null)
                {
            
                    ddl.Items.Add(new ListItem("ACTIVE", "ACTIVE"));
                    ddl.Items.Add(new ListItem("DEACTIVE", "DEACTIVE"));
                    

                    
                    DataRowView rowView = (DataRowView)e.Row.DataItem;
                    string status = rowView["STATUS"].ToString();
                    ddl.SelectedValue = status;
                }
            }

        }

        //protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    GridViewRow row = GridView1.Rows[e.RowIndex];
    
        //    string License = ((TextBox)row.Cells[3].Controls[0]).Text; // License
        //    string UserStructure = ((TextBox)row.Cells[4].Controls[0]).Text; // UserStructure
        //    string idle = ((TextBox)row.Cells[5].Controls[0]).Text; // Idle
        //    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["webafricaConnectionString"].ConnectionString))
        //    {
        //        conn.Open();

        //        //DropDownList ddl = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("DDStatus");
        //        //string STATUS = "";
        //        //if (ddl == null)
        //        //{
        //        //    STATUS = string.Empty;
        //        //}
        //        //else
        //        //{
        //        //    STATUS = ddl.SelectedValue;
        //        //}


        //        //using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["webafricaConnectionString"].ConnectionString))
        //        //{
        //        //    conn.Open();
        //        //    try
        //        //    {
        //        //        string query = "UPDATE [ParentCompany] SET [License] = '" + License + "', [UserStructure] = '" + UserStructure + "',[Idle] = '" + idle + "', [Status] = '" + STATUS + "' WHERE [ID] = '" + id + "'";
        //        //        SqlCommand cmd = new SqlCommand(query, conn);
        //        //        cmd.ExecuteNonQuery();
        //        //        GridView1.EditIndex = -1;
        //        //        GridView1.DataBind();
        //        //      //  lblMessage.Text = "";
        //        //      //  lblMessage.Visible = false;
        //        //    }
        //        //    catch (Exception)
        //        //    {
        //        //      //  lblMessage.Text = "Failed To Update Data";
        //        //    }

        //        //}




        //        DropDownList ddl = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("DDStatus");

        //        if (ddl != null)
        //        {
        //            string STATUS = ddl.SelectedItem.Text;

        //            //string selectedTransporterCode = ddl.SelectedValue;


        //            SqlDataSource3.UpdateParameters["License"].DefaultValue = License;
        //            SqlDataSource3.UpdateParameters["Idle"].DefaultValue = idle;
        //            SqlDataSource3.UpdateParameters["UserStructure"].DefaultValue = UserStructure;
        //            SqlDataSource3.UpdateParameters["STATUS"].DefaultValue = STATUS;
        //            SqlDataSource3.UpdateParameters["ID"].DefaultValue = e.Keys["ID"].ToString();

        //            try
        //            {
        //                SqlDataSource3.Update();




        //                string query = "UPDATE [SubCompany] SET [STATUS] = '" + STATUS + "' WHERE [ID] = '" + e.Keys["ID"].ToString() + "'";
        //                SqlCommand cmd = new SqlCommand(query, conn);
        //                cmd.ExecuteNonQuery();




        //            }
        //            catch (Exception)
        //            {

        //            }

        //            GridView1.EditIndex = -1;
        //        }
        //    }

        //}

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GridView1.DataBind();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GridView1.DataBind();
        }

        protected void btnSubCompany_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        protected void btnSubCompany_Click1(object sender, EventArgs e)
        {
            string Exist = "";
            if (txtSubCompany.Text =="")
            {
                Label2.Text = "Please Enter A Sub Company";
                txtSubCompany.Focus();
            }
            else
            {
                Exist = service1.RecordSubCompany(txtSubCompany.Text, Session["Parent"].ToString());


                if (Exist != "1")
                {
                    Label2.Text = "Failed To Create Sub Company <br> " + Exist;
                    txtSubCompany.Text ="";
                    txtSubCompany.Focus();
                }
                else
                {
                    txtCompany.Text = "";
                    txtIdle.Text = "";
                    txtLicense.Text = "";
                    ddlPackage.SelectedIndex = 0;
                    txtSubCompany.Text = "";
                    GridView2.DataBind();
                    GridView1.DataBind();
                    //GridView2.DataBind();
                    Label2.Text = "Successfully Created A Sub Company";
                }

            }

        }
    }
}