<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CleanActiveUsersReports.aspx.cs" Inherits="LocationRepresentation.Reports.CleanActiveUsersReports" %>
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
                    <h3>Clean Active Details</h3>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td class="auto-style1">UserName</td>
                            <td class="auto-style1">Parent Company</td>
                            <td class="auto-style1">Status</td>
                            <td class="auto-style1"></td>
                            <td class="auto-style1"></td>
                            <td class="auto-style1">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 200px">
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                            </td>

                            <td style="width: 200px">
                                <asp:TextBox ID="txtComp" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                            </td>

                            <td style="width: 200px">
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AutoPostBack="true">
     <asp:ListItem Value="%">SELECT STATUS</asp:ListItem>
    <asp:ListItem>ACTIVE</asp:ListItem>
    <asp:ListItem>INACTIVE</asp:ListItem>
</asp:DropDownList>
                            </td>
                            <td style="width: 200px">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn active" OnClick="btnSearch_Click1"/>
                            </td>
                            <td style="width: 200px">
                                <asp:Button ID="btnClearFilter" runat="server" CssClass="btn active" Text="Clear Filters" Width="190px" OnClick="btnClearFilter_Click1" />
                            </td>
                            <td style="width: 200px">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" DataSourceID="sdsParentCompany" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AllowSorting="True" >
                        <AlternatingRowStyle BackColor="White" ForeColor="#3076f5" />
                        <Columns>

                            <asp:BoundField DataField="Company ID" HeaderText="Company ID" SortExpression="Company ID" />
                            <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                            <asp:BoundField DataField="LastActivity" HeaderText="LastActivity" SortExpression="LastActivity" />
                            <asp:BoundField DataField="LoginStatus" HeaderText="LoginStatus" SortExpression="LoginStatus" />
                            <asp:BoundField DataField="Company" HeaderText="Company" SortExpression="Company" />
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
                  
                             
                                        <asp:SqlDataSource ID="sdsParentCompany" runat="server" ConnectionString="<%$ ConnectionStrings:webafricaConnectionString %>" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT *
FROM [UserLoginStatus] 
WHERE (([Company] LIKE '%' + @Company +'%') 
AND ([LoginStatus] LIKE ''+ @LoginStatus +'%')
AND [UserName] LIKE ''+ @UserName +'%')
 ORDER BY LastActivity DESC">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtComp" DefaultValue="%" Name="Company" PropertyName="Text" Type="String" />
                            <asp:ControlParameter  ControlID="ddlStatus" DefaultValue="%" Name="LoginStatus" PropertyName="SelectedValue" Type="String" />
            
                       
                            <asp:ControlParameter ControlID="txtUserName" DefaultValue="%" Name="UserName" PropertyName="Text" />
            
                       
                        </SelectParameters>
                    </asp:SqlDataSource>
                  
                             
        </td>
    </tr>
                 
                    
          
        

 
</table>
    </div>

</asp:Content>
