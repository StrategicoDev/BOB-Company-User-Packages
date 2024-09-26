<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LocationRepresentation.Account.Login" Async="true"%>
<%@ MasterType VirtualPath="~/Site.master" %>
<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
    body {
    background-color: #333333;
}
        </style>
     <div style="width:100%; height: 100%;">
    <asp:Panel ID="p" runat="server" DefaultButton="Button1">
        <table class="nav-justified">
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="width: 400px" class="text-center">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
             <tr>
                <td>&nbsp;</td>
                <td>
                    <section id="loginForm">
                        <div class="form-horizontal">
                            <h4>Use BLG account to log in.</h4>
                            <span>
                            <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label></span>
                            
                            <asp:PlaceHolder ID="ErrorMessage" runat="server" Visible="false">
                                <p class="text-danger">
                                    <asp:Literal ID="FailureText" runat="server" />
                                </p>
                            </asp:PlaceHolder>
                        </div>
                    </section>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <table style="width:100%">
                        <tr>
                            <td>
                                <asp:Panel ID="pnlQRCode" runat="server" DefaultButton="btnLoginQR" Visible="false">
                                    <table style="width:100%">
                                        <tr>
                                            <td style="width:100%">
                                                <asp:Label ID="Label3" runat="server">QR Code</asp:Label> 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:100%">
                                                <asp:TextBox ID="txtQRCode" runat="server" CssClass="form-control" Width="100%" />
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:100%">
                                                <asp:Button ID="btnLoginQR" runat="server" CssClass="btn active" OnClick="btnLoginQR_Click" Text="Log in" Width="150px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:100%">

                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlUser" runat="server" DefaultButton="Button1">
                                    <table style="width:100%">
                                        <tr>
                                            <td style="width:100%">
                                                <asp:Label ID="Label1" runat="server" AssociatedControlID="Email">Username</asp:Label>                                                 
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:100%">
                                                <asp:TextBox ID="Email" runat="server" CssClass="form-control" OnTextChanged="Email_TextChanged" Width="100%" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:100%">
                                                <asp:Label ID="Label2" runat="server" AssociatedControlID="Password" >Password</asp:Label>
                                                    
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:100%">
                                                    <asp:TextBox ID="Password" runat="server" CssClass="form-control" OnTextChanged="Password_TextChanged" TextMode="Password" Width="100%" />
                                                   
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:100%">
                                                   <asp:Button ID="Button1" runat="server" CssClass="btn active" OnClick="LogIn" Text="Log in" Width="150px" />
                                                    
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:100%"></td>
                                        </tr>
                                        <tr>
                                            <td style="width:100%">
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:100%"></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>&nbsp;</td>
            </tr>
           
            <tr>
                <td>&nbsp;</td>
                <td>
                        <asp:Button ID="btnSwitch" runat="server" CssClass="btn active" OnClick="btnSwitch_Click" Text="Change Login" Width="150px" Visible="false"/>
                                                    

                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
         </div>
    <div class="row">
        <%--<div class="col-md-8">--%>

        <%--</div>--%>
    </div>
</asp:Content>
