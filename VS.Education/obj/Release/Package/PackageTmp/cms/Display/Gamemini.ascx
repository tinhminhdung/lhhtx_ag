<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Gamemini.ascx.cs" Inherits="VS.E_Commerce.cms.Display.Gamemini" %>
<div class="Nentong">
    <asp:Literal ID="ltthongbao" runat="server"></asp:Literal>
    <div>
        <div class="Udai">ĐIỂM DANH NHẬN THƯỞNG</div>
        <div class="diemcuatoi">Ví của tôi : <b>
            <asp:Literal ID="lthientai" runat="server"></asp:Literal>
            VNĐ</b></div>
        <br />
        <asp:LinkButton ID="lnknhanxungay" CssClass="Nhandenhandiem" Enabled="false" runat="server" OnClick="lnknhanxungay_Click">
            <asp:Literal ID="ltdiem" runat="server"></asp:Literal>
            <b>VNĐ</b></asp:LinkButton><br />

        <div style="clear: both"></div>
        <div class="ketquangay">
            <div class="tongngay">
                <div class="Gachke"></div>
                <asp:Literal ID="ltketqua" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
</div>
<div style="clear: both"></div>