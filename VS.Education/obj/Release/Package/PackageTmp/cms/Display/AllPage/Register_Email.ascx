<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Register_Email.ascx.cs" Inherits="VS.E_Commerce.cms.Display.AllPage.Register_Email" %>
<div class="Register_Email">
    <div style="color: red" class="thongbaoRegisterEmail"><asp:Literal ID="ltmgs" runat="server"></asp:Literal></div>
    <asp:TextBox ID="txtRegisterEmail" ValidationGroup="emaildk" class="RegisterEmail" placeholder="Nhập email của bạn.." runat="server"></asp:TextBox>
    <asp:Button ID="btRegisterbtnSubmit" OnClick="btRegisterbtnSubmit_Click" class="RegisterbtnSubmit" ValidationGroup="emaildk" runat="server" Text="Đăng ký" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtRegisterEmail" Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="emaildk"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RequiredFieldValidator4" ValidationGroup="emaildk" runat="server" ControlToValidate="txtRegisterEmail" ErrorMessage="Vui lòng nhập email hợp lệ." ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" meta:resourcekey="valRegExResource1"></asp:RegularExpressionValidator>
</div>
