<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LoginReport.aspx.cs" Inherits="LocationRepresentation.Reports.LoginReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <style>
        body {
            background-color: #333333;
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
                                <asp:Button ID="btnBack" runat="server" CssClass="btn active"  Text="Back" Width="180px" OnClick="btnBack_Click"  />
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
                    <h3>Login Record Details</h3>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td style="width: 200px">Parent Company</td>
                             <td style="width: 200px">Sub Company</td>
                            <td style="width: 200px">From Date: </td>
                            <td style="width: 200px">To Date: </td>
                            <td style="width: 200px"></td>
                            <td style="width: 200px"></td>
                            <td style="width: 200px">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 200px">
                                <asp:TextBox ID="txtComp" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                            </td>
                            <td style="width: 200px">
    <asp:TextBox ID="txtSubCompany" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
</td>
                            <td style="width: 200px">
                                <asp:TextBox ID="txtFromDate" TextMode="Date" runat="server" MaxLength="20" CssClass="form-control" Width="150px" AutoPostBack="true" ></asp:TextBox>
                            </td>
                            <td style="width: 200px">
                                <asp:TextBox ID="txtToDate" TextMode="Date" runat="server" MaxLength="20" CssClass="form-control" Width="150px" AutoPostBack="true" ></asp:TextBox>
                            </td>
                            <td style="width: 200px">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn active" OnClick="btnSearch_Click" />
                            </td>
                            <td style="width: 200px">
                                <asp:Button ID="btnClearFilter" runat="server" CssClass="btn active" OnClick="btnClearFilter_Click" Text="Clear Filters" Width="190px" />
                            </td>
                            <td style="width: 200px">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AllowSorting="True" >
                        <AlternatingRowStyle BackColor="White" ForeColor="#3076f5" />
                        <Columns>
                            <asp:BoundField DataField="RecordID" HeaderText="RecordID" InsertVisible="False" ReadOnly="True" SortExpression="RecordID" />
                            <asp:BoundField DataField="RecordType" HeaderText="RecordType" SortExpression="RecordType" />
                            <asp:BoundField DataField="ParentCompany" HeaderText="ParentCompany" SortExpression="ParentCompany" />
                            <asp:BoundField DataField="Company" HeaderText="Sub Company" SortExpression="Company" />
                            <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                            <%--                   <asp:BoundField DataField="STATUS" HeaderText="STATUS" SortExpression="STATUS" />--%>
                            <asp:BoundField DataField="UserCount" HeaderText="UserCount" SortExpression="UserCount" />
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
                  
                             
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:webafricaConnectionString %>" SelectCommand="select*
 FROM [LoginRecord]
 WHERE ((CAST([Date] AS date) >= @FromDate AND CAST([Date] AS date) <= @ToDate)
 and ([ParentCompany] LIKE '%' + @ParentCompany + '%')
 AND  [Company] LIKE '%' + @SubCompany + '%')
                        order by [Date] desc">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtFromDate" Name="FromDate" PropertyName="Text" type="DateTime" />
                            <asp:ControlParameter ControlID="txtToDate" Name="ToDate" PropertyName="Text" Type="DateTime" />
                            <asp:ControlParameter ControlID="txtComp" DefaultValue="%" Name="ParentCompany" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="txtSubCompany" DefaultValue="%" Name="SubCompany" PropertyName="Text" type="String"/>
                        </SelectParameters>
                    </asp:SqlDataSource>
                  
                             
        </td>
    </tr>
                 
                    
          
        

 
</table>
    </div>

</asp:Content>
