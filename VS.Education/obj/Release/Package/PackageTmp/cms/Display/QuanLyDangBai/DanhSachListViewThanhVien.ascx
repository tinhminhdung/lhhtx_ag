<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DanhSachListViewThanhVien.ascx.cs" Inherits="VS.E_Commerce.cms.Display.QuanLyDangBai.DanhSachListViewThanhVien" %>
<link rel="stylesheet" href="/Resources/ListView/jsLists.css" />
<h1 class="title-head">
    <span>Danh sách Thành viên cấp dưới</span>
</h1>
<div style="clear: both"></div>
<div style='height: 100%; width: 100%;'>
    <ul id='f1teams' class='jslists'>
        <asp:Literal ID="ltshow" runat="server"></asp:Literal>
    </ul>
    <br>
</div>
<script src="/Resources/ListView/jsLists.js"></script>
<script>
    JSLists.createTree("f1teams");
</script>