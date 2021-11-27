<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="index.ascx.cs" Inherits="VS.E_Commerce.cms.Admin.index" %>
<div style="height:3px;"></div>
<div>

    <div class="div2">&nbsp;</div>
    <div class="div2">
        <div class="div1">
            <div class="adm_index_div">
                <strong>
                   Trang chủ
                    :</strong></div>
            <div class="adm_index_div">
                &nbsp;<asp:Literal ID="lthomepage" runat="server"></asp:Literal></div>
        </div>
        <div class="div1" style=" padding-top:20px">
            <div class="adm_index_div">
                <strong>
                   Tên trang web
                    :</strong></div>
            <div class="adm_index_div">
                &nbsp;<asp:Literal ID="ltwebname" runat="server"></asp:Literal></div>
        </div>
        <div class="div1" style=" padding-top:20px">
            <div class="adm_index_div">
                <strong>
                   Từ khóa tìm kiếm
                    :</strong></div>
            <div class="adm_index_div">
                &nbsp;<asp:Literal ID="ltkeyword" runat="server"></asp:Literal></div>
        </div>
        <div class="div1" style=" padding-top:20px">
            <div class="adm_index_div">
                <strong>
                    Từ khóa mô tả
                    :</strong></div>
            <div class="adm_index_div">
                &nbsp;<asp:Literal ID="txtsitekeyworddescription" runat="server"></asp:Literal></div>
        </div>
        <div class="div1" style=" padding-top:20px">
            <div class="adm_index_div">
                <strong>
                   Số lượt khách vào trang
                    :</strong></div>
            <div class="adm_index_div">
                &nbsp;<asp:Literal ID="ltvisittime" runat="server"></asp:Literal></div>
        </div>
    </div>
</div>
