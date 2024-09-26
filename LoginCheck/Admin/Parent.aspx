<%@ Page Title="Create Parent Company" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Parent.aspx.cs" Inherits="LocationRepresentation.Admin.Parent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <style>
    body {
    background-color: #333333;
}
            .auto-style1 {
                width: 525px;
            }
            .auto-style2 {
                width: 220px;
            }
        </style>
     <div style="width:100%; height: 100%;">
    <table class="nav-justified">
        <tr>
            <td class="auto-style1">
                <asp:Label ID="labelUser" runat="server" style="font-size: small"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                <table>
                        <tr>
        <td>
<asp:Button ID="btnSubCompany" runat="server" CssClass="btn active" Text="Add Sub Company" Width="184px" OnClick="btnSubCompany_Click" />
        </td>
        <td>
            &nbsp;</td>
        <td>
            <asp:Button ID="btnClearFilter" runat="server" Text="Clear Filters" CssClass="btn active" Width="190px" OnClick="btnClearFilter_Click" />
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
                </table>

                </td>
        </tr>
        <tr>
            <td class="auto-style1">
                <table>
                    </table>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                <table>
                    <tr>
                        <td>Company: </td>
                        <td>
                            <asp:TextBox ID="txtCompany" runat="server" MaxLength="20" CssClass="form-control" Width="150px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td class="auto-style2">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>License: </td>
                        <td>
                            <asp:TextBox ID="txtLicense" runat="server" MaxLength="20" CssClass="form-control" Width="150px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td class="auto-style2">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>User Structure: </td>
                        <td>
                            <asp:TextBox ID="txtUserStructure" runat="server" MaxLength="20" CssClass="form-control" Width="150px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td class="auto-style2">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>Idle:</td>
                        <td>
                            <asp:TextBox ID="txtIdle" runat="server" MaxLength="20" CssClass="form-control" Width="150px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td class="auto-style2">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            ID:</td>
                        <td>
                            <asp:TextBox ID="txtID" runat="server" Enabled="false" MaxLength="20" CssClass="form-control" Width="150px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td class="auto-style2">
                <asp:Button ID="btnParentCompany" runat="server" CssClass="btn active"  Text="Create Parent Company" Width="220px" OnClick="btnParentCompany_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td class="auto-style2">&nbsp;</td>
                    </tr>
                    </table>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                <strong>
                <asp:Label ID="Label2" runat="server" style="color: #FF0000"></asp:Label>
                </strong>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                <h4>Parent Company Details</h4>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" EmptyDataText="No Orders found" ForeColor="#333333" GridLines="None" Width="100%"  DataKeyNames="ID" DataSourceID="SqlDataSource3" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing">
                    <AlternatingRowStyle BackColor="White" ForeColor="#3076f5" />
                    <Columns>
                        <asp:CommandField ShowEditButton="True" ShowSelectButton="True" ButtonType="Button" />
                        <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                        <asp:BoundField DataField="Company" HeaderText="Company" SortExpression="Company" InsertVisible="False" ReadOnly="True" />
                        <asp:BoundField DataField="License" HeaderText="License" SortExpression="License" />
                        <asp:BoundField DataField="UserStructure" HeaderText="UserStructure" SortExpression="UserStructure"  />
                        <asp:BoundField DataField="Idle" HeaderText="Idle" SortExpression="Idle" />
     <%--                   <asp:BoundField DataField="STATUS" HeaderText="STATUS" SortExpression="STATUS" />--%>
                        <asp:TemplateField HeaderText="STATUS" SortExpression="STATUS">
    <EditItemTemplate>
        <asp:DropDownList ID="DDStatus" runat="server"></asp:DropDownList>
    </EditItemTemplate>
    <ItemTemplate>
        <asp:Label ID="Label1" runat="server" Text='<%# Bind("STATUS") %>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>
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
            <td class="auto-style1">
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:webafricaConnectionString %>" SelectCommand="SELECT [ID], [Company], [License], [UserStructure], [Idle] FROM [ParentCompany] WHERE ([Company] LIKE '%' + @Company + '%')">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtCompany" DefaultValue="%" Name="Company" PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:webafricaConnectionString %>" 
    SelectCommand="SELECT [ID], [Company], [License], [UserStructure], [Idle],[STATUS] FROM [ParentCompany] WHERE ([Company] LIKE '%' + @Company + '%')"
    UpdateCommand="UPDATE [ParentCompany] SET [License] = @License, [Idle] = @Idle, [UserStructure] = @UserStructure, [STATUS] = @STATUS WHERE [ID] = @ID" >
    <SelectParameters>
        <asp:ControlParameter ControlID="txtCompany" DefaultValue="%" Name="Company" PropertyName="Text" Type="String" />
    </SelectParameters>
    <UpdateParameters>
     
        <asp:Parameter Name="License" Type="String" />
        <asp:Parameter Name="Idle" Type="String" />
        <asp:Parameter Name="UserStructure" Type="String" />
         <asp:Parameter Name="STATUS" Type="String" />
        <asp:Parameter Name="ID" Type="Int32" />
    </UpdateParameters>
</asp:SqlDataSource>

            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                <h4>Sub Company Details</h4>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" EmptyDataText="No Lines for selected Order" DataSourceID="SqlDataSource2">
                    <AlternatingRowStyle BackColor="White" ForeColor="#3076f5" />
                    <Columns>
                        <asp:BoundField DataField="Company" HeaderText="Company" SortExpression="Company" />
                        <asp:BoundField DataField="Branch" HeaderText="Branch" SortExpression="Branch" />
                        <asp:BoundField DataField="UserStructure" HeaderText="UserStructure" SortExpression="UserStructure" />
                        <asp:BoundField DataField="ParentCompany" HeaderText="ParentCompany" SortExpression="ParentCompany" />
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
            <td class="auto-style1">
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:webafricaConnectionString %>" SelectCommand="SELECT [Company], [Branch], [UserStructure], [ParentCompany] FROM [SubCompany] WHERE ([ID] = @ID)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtID" Name="ID" PropertyName="Text" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">&nbsp;</td>
        </tr>
    </table>
         </div>
</asp:Content>
