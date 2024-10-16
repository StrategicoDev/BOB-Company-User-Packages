<%@ Page Title="DashBoard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DashboardRecords.aspx.cs" Inherits="LocationRepresentation.Admin.DashboardRecords" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <style>
        body {
            background-color: #333333;
        }

            .auto-style1 {
                width: 200px;
                height: 25px;
            }

        </style>
    <div style="width: 100%; height: 100%;">
        <table class="nav-justified" style="width: 100%">
            <tr>
                <td></td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                
                                <asp:Button ID="btnBck" runat="server" CssClass="btn active"  Text="Back" Width="180px" OnClick="btnBck_Click"/>
                                
                            </td>
                            <td>
                                <asp:Button ID="btnExportToExcel" runat="server" CssClass="btn active"  Text="Export To Excel" Width="180px" OnClick="btnExportToExcel_Click" />
                            </td>
                            <td>
                                
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>

                </td>
            </tr>
            <tr>
                <td>
                    <h3>&nbsp;</h3>
                </td>
            </tr>
            <tr>
                <td>
                    <h3>Login Details</h3>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td class="auto-style1">Parent Company</td>
                            <td class="auto-style1">Sub Company</td>
                            <td class="auto-style1">From Date</td>
                            <td class="auto-style1">To Date</td>
                            <td class="auto-style1">User Name</td>
                            <td class="auto-style1">Status</td>
                            <td class="auto-style1"></td>
                            <td class="auto-style1"></td>
                            <td class="auto-style1">&nbsp;</td>
                        </tr>
                        <tr>

                            <td style="width: 200px">
                                <asp:TextBox ID="txtParent" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                            </td>

                            <td style="width: 200px">
                                <asp:TextBox ID="txtSubParent" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                            </td>

                            <td style="width: 200px">
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TextMode="Date" AutoPostBack="true"></asp:TextBox>
                                </td>

                            <td style="width: 200px">
                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TextMode="Date" AutoPostBack="true"></asp:TextBox>
                                </td>

                            <td style="width: 200px">
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"  AutoPostBack="true"></asp:TextBox>
                            </td>

                            <td style="width: 200px">
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AutoPostBack="true">
     <asp:ListItem Value="%">SELECT STATUS</asp:ListItem>
    <asp:ListItem Value="1">ACTIVE</asp:ListItem>
    <asp:ListItem Value="0"> INACTIVE</asp:ListItem>
</asp:DropDownList>
                            </td>
                            <td style="width: 200px">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn active" OnClick="btnSearch_Click1"/>
                            </td>
                            <td style="width: 200px">
                                <asp:Button ID="btnClearFilter" runat="server" CssClass="btn active" Text="Clear Filters" Width="190px" OnClick="btnClearFilter_Click1"/>
                            </td>
                            <td style="width: 200px">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" DataSourceID="SqlDataSource2" AllowSorting="True" AllowPaging="True" >
                        <AlternatingRowStyle BackColor="White" ForeColor="#3076f5" />
                        <Columns>

                            <asp:BoundField DataField="Total Active Minute" HeaderText="Total Active Minute" SortExpression="Total Active Minute" ReadOnly="True" />
                            <asp:BoundField DataField="ParentCompany" HeaderText="ParentCompany" SortExpression="ParentCompany" />
                            <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                            <asp:BoundField DataField="Company" HeaderText="Company" SortExpression="Company" />
                            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" ReadOnly="True" />
                            <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#3076f5" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#3076f5" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#3076f5" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                  
                             
                                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:webafricaConnectionString %>" SelectCommand="SELECT  
    SUM([UserCount]) AS [Total Active Minute],
    [ParentCompany],
    [UserName],
    [Company],
    CASE 
        WHEN SUM([UserCount]) = 0 THEN 'INACTIVE'
    WHEN SUM([UserCount]) &gt;= 1 THEN 'ACTIVE'
        ELSE ''
    END AS [Status],
    [Date]
FROM 
    [DashboardRecords]
WHERE 
 [ParentCompany] LIKE  '%' + @ParentCompany + '%'
AND    [COMPANY] LIKE  '%' + @SubCompany + '%'
    AND CAST([Date] AS date) &gt;= @FromDate AND CAST([Date] AS date) &lt;= @ToDate
    AND [UserName] like '%' + @UserName + '%'
	and UserCount like '%' + @UserCount + '%'
GROUP BY 
    [ParentCompany],
    [UserName],
    [Company],
    [Date]
ORDER BY 
    [Date] DESC">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="txtParent" DefaultValue="%" Name="ParentCompany" PropertyName="Text" />
                                                <asp:ControlParameter ControlID="txtSubParent" DefaultValue="%" Name="SubCompany" PropertyName="Text" />
                                                <asp:ControlParameter ControlID="txtFromDate" DefaultValue="" Name="FromDate" PropertyName="Text" />
                                                 <asp:ControlParameter ControlID="txtToDate" Name="ToDate" PropertyName="Text" />
                                                <asp:ControlParameter ControlID="txtUserName" DefaultValue="%" Name="UserName" PropertyName="Text" />
                                                <asp:ControlParameter ControlID="ddlStatus" DefaultValue="%" Name="UserCount" PropertyName="SelectedValue" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                  
                             
        </td>
    </tr>
                 
            <tr>
                <td>
                           <h3>User Packages Logs</h3>
                </td>
            </tr>
                         <td>
                    <table>
                        </table>
                </td>
            <tr>
                <td>

                    <asp:GridView ID="GridView2" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" Width="100%">
                        <AlternatingRowStyle BackColor="White" ForeColor="#3076f5" />
                        <Columns>
                            <asp:BoundField DataField="ParentCompany" HeaderText="ParentCompany" SortExpression="ParentCompany" />
                            <asp:BoundField DataField="SubCompany" HeaderText="SubCompany" SortExpression="SubCompany" />
                            <asp:BoundField DataField="Username" HeaderText="Username" SortExpression="Username" />
                            <asp:BoundField DataField="Message" HeaderText="Message" SortExpression="Message" />
                            <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#3076f5" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#3076f5" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#3076f5" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>

                </td>
            </tr>
            <tr>
                <td>

                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:webafricaConnectionString %>" SelectCommand="  SELECT *
  FROM [UserPackagesLog]
  WHERE [ParentCompany] LIKE  '%' + @ParentCompany + '%'
AND [SubCompany] LIKE  '%' + @SubCompany + '%'
AND CAST([Date] AS date) &gt;= @FromDate 
AND CAST([Date] AS date) &lt;= @ToDate
AND [UserName] like '%' + @UserName + '%'
order by [Date] desc
">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtParent" DefaultValue="%" Name="ParentCompany" PropertyName="Text" />
                            <asp:ControlParameter ControlID="txtSubParent" DefaultValue="%" Name="SubCompany" PropertyName="Text" />
                            <asp:ControlParameter ControlID="txtFromDate" DefaultValue="" Name="FromDate" PropertyName="Text" />
                            <asp:ControlParameter ControlID="txtToDate" Name="ToDate" PropertyName="Text" />
                            <asp:ControlParameter ControlID="txtUserName" DefaultValue="%" Name="UserName" PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                </td>
            </tr>
                    
          
        

 
</table>
    </div>

</asp:Content>
