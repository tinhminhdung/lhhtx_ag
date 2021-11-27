<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="VS.E_Commerce.cms.Display.Members.Login" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/cms/Display/Members/Resetpassword.ascx" TagPrefix="uc1" TagName="Resetpassword" %>

<div class="container margin-bottom-20">
    <h1 class="title-head">
        <span>Đăng nhập tài khoản</span>
    </h1>
    <div class="row">
        <div class="col-lg-6">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="page-login margin-bottom-30">
                        <div id="login">
                            <span>Nếu bạn đã có tài khoản, đăng nhập tại đây.
                            </span>
                            <div id="customer_login">
                                <div class="form-signup">
                                    <div>
                                        <asp:Label ID="ltmsg" runat="server" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-signup clearfix">
                                    <fieldset class="form-group">
                                        <label>
                                            Số điện thoại <span class="required">*</span>
                                        </label>
                                        <asp:TextBox ID="txt_Uname" runat="server" class="form-control form-control-lg" ValidationGroup="GInfo"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server" ValidationGroup="GInfo" ControlToValidate="txt_Uname" ErrorMessage=" Tên đăng nhập không được để trống !"></asp:RequiredFieldValidator>
                                    </fieldset>
                                    <fieldset class="form-group">
                                        <label>
                                            <%=label("lt_password")%> <span class="required">*</span>
                                        </label>
                                        <asp:TextBox ID="txt_password" runat="server" TextMode="password" ValidationGroup="GInfo" class="form-control form-control-lg"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" runat="server" ValidationGroup="GInfo" ControlToValidate="txt_password" ErrorMessage="Mật khẩu không được để trống !"></asp:RequiredFieldValidator>
                                    </fieldset>

                                    <div class="pull-xs-left" style="margin-top: 25px;">
                                        <asp:Button ID="btnlogin" runat="server" Text="Đăng nhập" class="btnadd" ValidationGroup="GInfo" OnClick="btnlogin_Click" CssClass="btn btn-style btn-primary" />
                                        <a href="/dang-ky.html" class="btn-link-style btn-register" style="margin-left: 20px; text-decoration: underline;">Đăng ký</a>
                                        <div class="block social-login--facebooks">
                                            <p class="a-center">
                                                Hoặc đăng nhập bằng
                                            </p>
                                            <a href="javascript:void(0)" class="social-login--facebook" onclick="loginFacebook()">
                                                <img width="129px" height="37px" alt="facebook-login-button" src="/Resources/images/fb-btn.svg">
                                            </a>
                                            <a href="javascript:void(0)" class="social-login--google" onclick="loginGoogle()">
                                                <img width="129px" height="37px" alt="google-login-button" src="/Resources/images/gp-btn.svg">
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="col-lg-6">
            <uc1:Resetpassword runat="server" ID="Resetpassword" />
        </div>
    </div>
</div>