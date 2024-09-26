<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SubCompany.aspx.cs" Inherits="LocationRepresentation.admin.CCAdmin.SubCompany" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
            <style>
    body {
    background-color: #333333;
}
        </style>
     <div style="width:100%; height: 100%;">
    <table class="nav-justified">
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMessage" runat="server" style="font-size: small"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    </table>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>Company: </td>
                        <td>
                <asp:Label ID="lblCompany" runat="server" style="font-size: small"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>Branch: </td>
                        <td>
                <asp:Label ID="lblBranch" runat="server" style="font-size: small"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>User Structure: </td>
                        <td>
                            <asp:TextBox ID="txtUserStructure" runat="server" MaxLength="20" CssClass="form-control" Width="150px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>ID:</td>
                        <td>
                            <asp:TextBox ID="txtID" runat="server" Enabled="false" MaxLength="20" CssClass="form-control" Width="150px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                <asp:Button ID="btnSubCompany" runat="server" CssClass="btn active"  Text="Create Sub Company" Width="190px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
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
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <h4>&nbsp;</h4>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <h4>&nbsp;</h4>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
         </div>

</asp:Content>
