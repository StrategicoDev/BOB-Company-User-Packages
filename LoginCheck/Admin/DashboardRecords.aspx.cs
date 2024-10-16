using OfficeOpenXml;
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


namespace LocationRepresentation.Admin
{
    public partial class DashboardRecords : System.Web.UI.Page
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

        protected void btnClearFilter_Click1(object sender, EventArgs e)
        {
            txtSubParent.Text = "";
            txtParent.Text ="";

            txtFromDate.Text= "";
            txtToDate.Text= "";
            ddlStatus.SelectedIndex = 0;
            GridView1.DataBind();
        }

        protected void btnSearch_Click1(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

      

     


        public DataTable GetProducts()
        {

            string query = @"SELECT  
    SUM([UserCount]) AS [ACTIVE in Minute],
    [ParentCompany],
    [UserName],
    [Company],
    CASE 
        WHEN SUM([UserCount]) = 0 THEN 'INACTIVE'
       WHEN SUM([UserCount]) >= 1 THEN 'ACTIVE'
        ELSE ''
    END AS [Status],
    [Date]
FROM 
    [DashboardRecords]
WHERE 
[ParentCompany] LIKE  '%' + @ParentCompany + '%'
   AND [COMPANY] LIKE  '%' + @SubCompany + '%'
    AND CAST([Date] AS date) >= @FromDate AND CAST([Date] AS date) <= @ToDate
    AND [UserName] like '%' + @UserName + '%'
	and UserCount like '%' + @UserCount + '%'
GROUP BY 
    [ParentCompany],
    [UserName],
    [Company],
    [Date]
ORDER BY 
    [Date] DESC";

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["webafricaConnectionString"].ConnectionString))
            {
                {
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@ParentCompany", txtParent.Text);

                    cmd.Parameters.AddWithValue("@SubCompany", txtSubParent.Text);
                    cmd.Parameters.AddWithValue("@FromDate", txtFromDate.Text);
                    cmd.Parameters.AddWithValue("@ToDate", txtToDate.Text);
                    //cmd.Parameters.AddWithValue("@Status", DropDownList1.SelectedValue);
                    //cmd.Parameters.AddWithValue("@TOID", txtLoadID.Text);
                    cmd.Parameters.AddWithValue("@UserName", txtUserName.Text);
                    cmd.Parameters.AddWithValue("@UserCount",ddlStatus.SelectedValue);
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



        public DataTable GetProducts2()
        {

            string query = @"SELECT * FROM [UserPackagesLog]
WHERE [ParentCompany] LIKE  '%' + @ParentCompany + '%'
AND [SubCompany] LIKE  '%' + @SubCompany + '%'
AND CAST([Date] AS date) >= @FromDate 
AND CAST([Date] AS date) <= @ToDate
AND [UserName] like '%' + @UserName + '%'
order by [Date] desc";

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["webafricaConnectionString"].ConnectionString))
            {
                {
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@SubCompany", txtSubParent.Text);
                    cmd.Parameters.AddWithValue("@ParentCompany", txtParent.Text);
                    cmd.Parameters.AddWithValue("@FromDate", txtFromDate.Text);
                    cmd.Parameters.AddWithValue("@ToDate", txtToDate.Text);
                    cmd.Parameters.AddWithValue("@UserName", txtUserName.Text);
         



                    conn.Open();
                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        var ItemMaster = new DataTable();
                        adapter.Fill(ItemMaster);
                        return ItemMaster;
                    }
                }





            }
        }





        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            {
                var ItemMaster = GetProducts();

                var ItemMaster2 = GetProducts2();

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var excel = new ExcelPackage())
                {
                    var workSheet = excel.Workbook.Worksheets.Add("Login Report");
                    var totalCols = ItemMaster.Columns.Count;
                    var totalRows = ItemMaster.Rows.Count;

                    //Sheet 2

                    var workSheet2 = excel.Workbook.Worksheets.Add("User Package Log");
                    var totalCols2 = ItemMaster2.Columns.Count;
                    var totalRows2 = ItemMaster2.Rows.Count;

                    // Add column headers
                    for (var col = 1; col <= totalCols; col++)
                    {
                        workSheet.Cells[1, col].Value = ItemMaster.Columns[col - 1].ColumnName;
                        workSheet.Column(col).Width = 17;
                        workSheet.Cells[1, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left; // Align headers left

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
                            cell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        }
                    }


                    // Add 2nd column headers
                    for (var col2 = 1; col2 <= totalCols2; col2++)
                    {
                        workSheet2.Cells[1, col2].Value = ItemMaster2.Columns[col2 - 1].ColumnName;
                        workSheet2.Column(col2).Width = 17;
                        workSheet2.Cells[1, col2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left; // Align headers left

                    }

                    // Add data 2nd rows and format date columns
                    for (var row2 = 1; row2 <= totalRows2; row2++)
                    {
                        for (var col2 = 0; col2 < totalCols2; col2++)
                        {
                            var cellValue2 = ItemMaster2.Rows[row2 - 1][col2];
                            var cell2 = workSheet2.Cells[row2 + 1, col2 + 1];

                            if (ItemMaster2.Columns[col2].ColumnName == "Date")
                            {

                                if (cellValue2 != DBNull.Value)
                                {
                                    cell2.Value = cellValue2;
                                    cell2.Style.Numberformat.Format = "yyyy-MM-dd HH:mm:ss"; // Format as date
                                }
                                else
                                {
                                    cell2.Value = string.Empty;
                                }
                            }
                            else
                            {
                                cell2.Value = cellValue2;
                            }
                            cell2.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        }
                    }

                    //// Calculate the total for column A (ACTIVE in Minute) and place in A63
                    //var sumFormula = $"=SUM(A2:A{totalRows })";
                    //workSheet.Cells["A" + totalRows.ToString()].Value = sumFormula;
                    //workSheet.Cells["A63"].Formula = sumFormula;

                    int UserCount = GetUniqueUserCount(GetProducts());



               
                    int lastDataRow = totalRows + 1; // Adjust for 1-based indexing in Excel
                    int sumRow = lastDataRow + 1;
                    int FinalRow = sumRow + 1;

                    var sumFormulaWithText = $"\"Total Minutes = \" & SUM(A2:A{lastDataRow})";



                    var sumHoursText = $"\"Total Hours = \" & INT(SUM(A2:A{lastDataRow})/60)";
                    



                    workSheet.Cells[$"A{sumRow}"].Formula =sumFormulaWithText;

                    workSheet.Cells[$"A{FinalRow}"].Formula =sumHoursText;



                    workSheet.Cells["C" + sumRow.ToString()].Value = "Total Users = " + UserCount;
                    workSheet.Cells[$"A{sumRow}:B{sumRow}"].Style.Font.Bold = true;
                    workSheet.Cells[$"A{FinalRow}:B{FinalRow}"].Style.Font.Bold = true;
                    workSheet.Cells[$"C{sumRow}"].Style.Font.Bold = true;




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

        protected void btnBck_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Parent.aspx");
        }



public int GetUniqueUserCount(DataTable itemMaster)
    {
        // Use a HashSet to track unique usernames
        HashSet<string> uniqueUsernames = new HashSet<string>();

        // Loop through each row in the DataTable
        foreach (DataRow row in itemMaster.Rows)
        {
            string userName = row["UserName"].ToString(); // Assuming UserName is the column for usernames

            // Add the username to the HashSet (HashSet will ignore duplicates)
            if (!string.IsNullOrEmpty(userName))
            {
                uniqueUsernames.Add(userName);
            }
        }

        // Return the count of unique usernames
        return uniqueUsernames.Count;
    }


   

    }
}