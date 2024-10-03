<%@ Page Title="Create Parent Company" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Parent.aspx.cs" Inherits="LocationRepresentation.Admin.Parent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        body {
            background-color: #333333;
        }

        .auto-style2 {
            width: 220px;
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
                                &nbsp;</td>
                            <td>&nbsp;</td>
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
                    <h3>Create New Parent Company</h3>
                </td>
            </tr>
            <tr>
                <td>
                    <table>

                        <tr>
                            <td>Company: </td>
                            <td>
                                <asp:TextBox ID="txtCompany" runat="server" MaxLength="20" CssClass="form-control" Width="150px"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                            <td class="auto-style2">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>Licenses: </td>
                            <td>
                                <asp:TextBox ID="txtLicense" runat="server" MaxLength="20" CssClass="form-control" Width="150px" TextMode="Number" ></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                            <td class="auto-style2">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>User Structure: </td>
                            <td>
                                <asp:DropDownList ID="ddlPackage" runat="server" width="150px" CssClass="form-control">
    <asp:ListItem Value="%">Select Package</asp:ListItem>
    <asp:ListItem Value="CL">Concurrent</asp:ListItem>
    <asp:ListItem Value="IL">Individual</asp:ListItem>
</asp:DropDownList>
                            </td>
                            <td>&nbsp;</td>
                            <td class="auto-style2">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>Idle:</td>
                            <td>
                                <asp:TextBox ID="txtIdle" runat="server" TextMode="Number" CssClass="form-control" Width="150px"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                            <td class="auto-style2">
                                <asp:Button ID="btnParentCompany" runat="server" CssClass="btn active" OnClick="btnParentCompany_Click" Text="Create Parent Company" Width="220px" />
                            </td>
                        </tr>

                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>
                        <asp:Label ID="Label2" runat="server" Style="color: #FF0000"></asp:Label>
                    </strong>
                </td>
            </tr>
            <tr>
                <td>
                    <h3>Parent Company Details</h3>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td style="width: 200px">Company</td>
                            <td style="width: 200px">License Type</td>
                            <td style="width: 200px">Status</td>
                            <td style="width: 200px"></td>
                            <td style="width: 200px"></td>
                            <td style="width: 200px"></td>
                            <td style="width: 200px"></td>
                            <td style="width: 200px"></td>
                        </tr>
                        <tr>
                            <td style="width: 200px">
                                <asp:TextBox ID="txtComp" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td style="width: 200px">
                                <asp:DropDownList ID="ddlUserStructure" runat="server"  CssClass="form-control">
                                    <asp:ListItem Value="%">All Packages</asp:ListItem>
                                    <asp:ListItem Value="CL">Concurrent</asp:ListItem>
                                    <asp:ListItem Value="IL">Individual</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 200px" >
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                    <asp:ListItem>ACTIVE</asp:ListItem>
                                    <asp:ListItem>DEACTIVE</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 200px">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn active" OnClick="btnSearch_Click" />
                            </td>
                            <td style="width: 200px">
                                <asp:Button ID="btnClearFilter" runat="server" CssClass="btn active" OnClick="btnClearFilter_Click" Text="Clear Filters" Width="190px" />
                            </td>
                            <td style="width: 200px"></td>
                            <td style="width: 200px"></td>
                            <td style="width: 200px"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" DataSourceID="sdsParentCompany" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AllowSorting="True" >
                        <AlternatingRowStyle BackColor="White" ForeColor="#3076f5" />
                        <Columns>
                            <asp:CommandField ShowEditButton="True" ShowSelectButton="True" ButtonType="Button" />
                            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                            <asp:BoundField DataField="Company" HeaderText="Company" SortExpression="Company" />
                            <asp:BoundField DataField="Licenses" HeaderText="Licenses" SortExpression="Licenses" />
                            <asp:BoundField DataField="UserStructure" HeaderText="UserStructure" SortExpression="UserStructure" />
                            <asp:BoundField DataField="IdleTime" HeaderText="IdleTime" SortExpression="IdleTime" />
                            <%--                   <asp:BoundField DataField="STATUS" HeaderText="STATUS" SortExpression="STATUS" />--%>
                            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
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
                    <asp:SqlDataSource ID="sdsParentCompany" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:webafricaConnectionString %>" DeleteCommand="DELETE FROM [ParentCompany] WHERE [ID] = @original_ID AND (([Company] = @original_Company) OR ([Company] IS NULL AND @original_Company IS NULL)) AND (([Licenses] = @original_Licenses) OR ([Licenses] IS NULL AND @original_Licenses IS NULL)) AND (([UserStructure] = @original_UserStructure) OR ([UserStructure] IS NULL AND @original_UserStructure IS NULL)) AND (([IdleTime] = @original_IdleTime) OR ([IdleTime] IS NULL AND @original_IdleTime IS NULL)) AND (([Status] = @original_Status) OR ([Status] IS NULL AND @original_Status IS NULL))" InsertCommand="INSERT INTO [ParentCompany] ([Company], [Licenses], [UserStructure], [IdleTime], [Status]) VALUES (@Company, @Licenses, @UserStructure, @IdleTime, @Status)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [ParentCompany] WHERE (([Company] LIKE '%' + @Company + '%') AND ([UserStructure] LIKE '%' + @UserStructure + '%') AND ([Status] LIKE '%' + @Status + '%')) ORDER BY [Company]" UpdateCommand="UPDATE [ParentCompany] SET [Company] = @Company, [Licenses] = @Licenses, [UserStructure] = @UserStructure, [IdleTime] = @IdleTime, [Status] = @Status WHERE [ID] = @original_ID AND (([Company] = @original_Company) OR ([Company] IS NULL AND @original_Company IS NULL)) AND (([Licenses] = @original_Licenses) OR ([Licenses] IS NULL AND @original_Licenses IS NULL)) AND (([UserStructure] = @original_UserStructure) OR ([UserStructure] IS NULL AND @original_UserStructure IS NULL)) AND (([IdleTime] = @original_IdleTime) OR ([IdleTime] IS NULL AND @original_IdleTime IS NULL)) AND (([Status] = @original_Status) OR ([Status] IS NULL AND @original_Status IS NULL))">
                        <DeleteParameters>
                            <asp:Parameter Name="original_ID" Type="Int32" />
                            <asp:Parameter Name="original_Company" Type="String" />
                            <asp:Parameter Name="original_Licenses" Type="Int32" />
                            <asp:Parameter Name="original_UserStructure" Type="String" />
                            <asp:Parameter Name="original_IdleTime" Type="Int32" />
                            <asp:Parameter Name="original_Status" Type="String" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="Company" Type="String" />
                            <asp:Parameter Name="Licenses" Type="Int32" />
                            <asp:Parameter Name="UserStructure" Type="String" />
                            <asp:Parameter Name="IdleTime" Type="Int32" />
                            <asp:Parameter Name="Status" Type="String" />
                        </InsertParameters>
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtComp" DefaultValue="%" Name="Company" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="ddlUserStructure" DefaultValue="%" Name="UserStructure" PropertyName="SelectedValue" Type="String" />
                            <asp:ControlParameter ControlID="ddlStatus" DefaultValue="%" Name="Status" PropertyName="SelectedValue" Type="String" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="Company" Type="String" />
                            <asp:Parameter Name="Licenses" Type="Int32" />
                            <asp:Parameter Name="UserStructure" Type="String" />
                            <asp:Parameter Name="IdleTime" Type="Int32" />
                            <asp:Parameter Name="Status" Type="String" />
                            <asp:Parameter Name="original_ID" Type="Int32" />
                            <asp:Parameter Name="original_Company" Type="String" />
                            <asp:Parameter Name="original_Licenses" Type="Int32" />
                            <asp:Parameter Name="original_UserStructure" Type="String" />
                            <asp:Parameter Name="original_IdleTime" Type="Int32" />
                            <asp:Parameter Name="original_Status" Type="String" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td>
                  
                             
        </td>
    </tr>
                 
                    <tr>
                        <td>
                            <asp:Panel ID="panelSubCompany" Visible="false" runat="server">
                            <table>
    <td>
<tr>
    <td>
        <h3>Create New Sub Company</h3>
    </td>
</tr>
<tr>
    <td>
        <table>

            <tr>
                <td>Sub Company: </td>
                <td>
                    <asp:TextBox ID="txtSubCompany" runat="server" MaxLength="20" CssClass="form-control" Width="150px" ></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td class="auto-style2">
                    <asp:Button ID="btnSubCompany" runat="server" CssClass="btn active"  Text="Create Sub Company" Width="220px" OnClick="btnSubCompany_Click1" />
                </td>
            </tr>
            
            <tr>
                <td>&nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td class="auto-style2">
                    &nbsp;</td>
            </tr>

        </table>
    </td>
</tr>
    </td>
                            </table>
                                </asp:Panel>
                        </td>
      
   
    </tr>


          
        

 
</table>
            <tr>
                <td>
                    <h4>Sub Company Details</h4>  <td>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" EmptyDataText="No Lines for selected Order" DataSourceID="SqlDataSource1">
                        <AlternatingRowStyle BackColor="White" ForeColor="#3076f5" />
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" InsertVisible="False" ReadOnly="True" />
                            <asp:BoundField DataField="Company" HeaderText="Company" SortExpression="Company" />
                            <asp:BoundField DataField="DateOfCapture" HeaderText="DateOfCapture" SortExpression="DateOfCapture" />
                            <asp:BoundField DataField="ParentCompany" HeaderText="ParentCompany" SortExpression="ParentCompany" />
                            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
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
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:webafricaConnectionString %>" SelectCommand="SELECT * FROM [SubCompany] WHERE ([ParentCompany] = @Company)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtComp" Name="Company" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>
