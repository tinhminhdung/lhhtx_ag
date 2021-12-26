<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Register_Phone.ascx.cs" Inherits="VS.E_Commerce.cms.Display.AllPage.Register_Phone" %>
<div class="Register_Email">
    <div style="color: red" class="thongbaoRegisterEmail"><asp:Literal ID="ltmgs" runat="server"></asp:Literal></div>
    <asp:TextBox ID="txtRegisterPhone" ValidationGroup="emaildk" class="RegisterEmail" placeholder="Nhập số điện thoại.." runat="server"></asp:TextBox>
    <asp:Button ID="btRegisterbtnSubmit" OnClick="btRegisterbtnSubmit_Click" class="RegisterbtnSubmit" ValidationGroup="emaildk" runat="server" Text="Đăng ký" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtRegisterPhone" Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="emaildk"></asp:RequiredFieldValidator>
</div>
