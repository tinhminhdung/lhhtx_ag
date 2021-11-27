<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Resetpassword.ascx.cs" Inherits="VS.E_Commerce.cms.Display.Members.Resetpassword" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="recover-password" class="form-signup">
            <span>Bạn quên mật khẩu? Nhập địa chỉ email để lấy lại mật khẩu qua email.
            </span>
            <div id="recover_customer_password" >
                <div class="form-signup aaaaaaaa">
                    <div style="padding-left: 20px; padding-left: 20px; clear: both; padding-top: 10px;">
                        <asp:Label ID="ltmsg" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                        <ProgressTemplate>
                            <img src="/Resources/admin/images/loading.gif" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </div>
                <div class="form-signup clearfix">
                    <fieldset class="form-group">
                        <label>
                            Email <span class="required">*</span>
                        </label>
                        <asp:TextBox ID="txtemails" runat="server" ValidationGroup="GInfoemail" class="form-control form-control-lg"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8"  ErrorMessage="Email không được để trống !" runat="server" ControlToValidate="txtemails" Display="Dynamic" SetFocusOnError="True" ValidationGroup="GInfoemail"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RequiredFieldValidator4" ValidationGroup="GInfoemail" ErrorMessage="Vui lòng nhập một địa chỉ email hợp lệ !" runat="server" ControlToValidate="txtemails" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" meta:resourcekey="valRegExResource1"></asp:RegularExpressionValidator>
                    </fieldset>
                </div>
                <div class="action_bottom">
                    <asp:Button ID="btnresets" ValidationGroup="GInfoemail" runat="server" CssClass="btn btn-style btn-primary" OnClick="btnregisters_Click" Text="Lấy lại mật khẩu" />
                </div>
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnresets" />
    </Triggers>
</asp:UpdatePanel>
