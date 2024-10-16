using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using LocationRepresentation.Admin;
using System.EnterpriseServices.CompensatingResourceManager;

namespace LocationRepresentation.Reports
{
    public partial class LoginReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           if (!IsPostBack)
            {
                try
                {
                    txtFromDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                    txtToDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                }
                catch (Exception)
                {

                    throw;
                }
            
            }


        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ID = GridView1.SelectedRow.Cells[1].Text;

            string Company = GridView1.SelectedRow.Cells[3].Text;
            string SubCompany = GridView1.SelectedRow.Cells[4].Text;
            Session["Parent"] = Company;

            txtComp.Text = Company;
            txtSubCompany.Text = SubCompany;

            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        protected void btnClearFilter_Click(object sender, EventArgs e)
        {
            GridView1.DataBind();
            txtComp.Text = "";
            txtFromDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            txtToDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            txtToDate.Text="";
        }


        public DataTable GetProducts()
        {

            string query = @"Select[RecordType]
      ,[ParentCompany]
      ,[Company]
      ,[Date]
      ,[UserCount]FROM[LoginRecord]
 WHERE((cast([Date] as date) >= @FromDate AND cast([Date] as date) <= @ToDate)
 and([ParentCompany] LIKE '%' + @ParentCompany + '%')
 AND[Company] LIKE '%' + @SubCompany + '%')
                        order by Date desc";

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["webafricaConnectionString"].ConnectionString))
            {
                {
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@FromDate", txtFromDate.Text);
                    cmd.Parameters.AddWithValue("@ToDate", txtToDate.Text);
                    //cmd.Parameters.AddWithValue("@Status", DropDownList1.SelectedValue);
                    //cmd.Parameters.AddWithValue("@TOID", txtLoadID.Text);
                    cmd.Parameters.AddWithValue("@ParentCompany", txtComp.Text);
                    cmd.Parameters.AddWithValue("@SubCompany", txtSubCompany.Text);
                    //cmd.Parameters.AddWithValue("@EndDate", txtEndTime.Text);
                    // cmd.Parameters.AddWithValue("@Date", txtDate.Text);

                    conn.Open();
                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        var ItemMaster = new DataTable();
                        adapter.Fill(ItemMaster);
                        return ItemMaster;
                    }
                }




                // string Item = TextBoxItem.Text;

            }
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            {
                var ItemMaster = GetProducts();

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var excel = new ExcelPackage())
                {
                    var workSheet = excel.Workbook.Worksheets.Add("Login Report");
                    var totalCols = ItemMaster.Columns.Count;
                    var totalRows = ItemMaster.Rows.Count;

                    // Add column headers
                    for (var col = 1; col <= totalCols; col++)
                    {
                        workSheet.Cells[1, col].Value = ItemMaster.Columns[col - 1].ColumnName;
                        workSheet.Column(col).Width = 16;
                    }

                    // Add data rows and format date columns
                    for (var row = 1; row <= totalRows; row++)
                    {
                        for (var col = 0; col < totalCols; col++)
                        {
                            var cellValue = ItemMaster.Rows[row - 1][col];
                            var cell = workSheet.Cells[row + 1, col + 1];

                            if (ItemMaster.Columns[col].ColumnName == "Date")
                            {

                                if (cellValue != DBNull.Value)
                                {
                                    cell.Value = cellValue;
                                    cell.Style.Numberformat.Format = "yyyy-MM-dd HH:mm:ss"; // Format as date
                                }
                                else
                                {
                                    cell.Value = string.Empty;
                                }
                            }
                            else
                            {
                                cell.Value = cellValue;
                            }
                        }
                    }

                    // Save the Excel file to the response stream
                    using (var memoryStream = new MemoryStream())
                    {
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment; filename=LoginReport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
                        excel.SaveAs(memoryStream);
                        memoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }





            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Parent.aspx");
        }
    }
}