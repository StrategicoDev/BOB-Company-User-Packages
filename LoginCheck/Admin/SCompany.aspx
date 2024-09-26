<%@ Page Title="Create Sub Company" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SCompany.aspx.cs" Inherits="LocationRepresentation.Admin.SCompany" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
                <style>
    body {
    background-color: #333333;
}
                    .auto-style1 {
                        width: 519px;
                    }
                    .auto-style2 {
                        width: 218px;
                    }
        </style>
     <div style="width:100%; height: 100%;">
    <table class="nav-justified">
        <tr>
            <td class="auto-style1">
                <asp:Button ID="btnBack" runat="server" CssClass="btn active"  Text="Back" Width="150px" OnClick="btnBack_Click" />
                        </td>
        </tr>
        <tr>
            <td class="auto-style1">
                <asp:Label ID="lblMessage" runat="server" style="font-size: small"></asp:Label>
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
                        <td>Branch: </td>
                        <td>
                            <asp:TextBox ID="txtBranch" runat="server" MaxLength="20" CssClass="form-control" Width="150px"></asp:TextBox>
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
                        <td>ID:</td>
                        <td>
                            <asp:TextBox ID="txtID" runat="server" Enabled="false" MaxLength="20" CssClass="form-control" Width="150px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td class="auto-style2">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Parent Company</td>
                        <td>
                            <asp:TextBox ID="txtParentCompany" runat="server" Enabled="false" MaxLength="20" CssClass="form-control" Width="150px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td class="auto-style2">
                <asp:Button ID="btnSubCompany" runat="server" CssClass="btn active"  Text="Create Sub Company" Width="202px" OnClick="btnSubCompany_Click" />
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
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">
                <h4>&nbsp;</h4>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">
                <h4>&nbsp;</h4>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">&nbsp;</td>
        </tr>
    </table>
         </div>
</asp:Content>
