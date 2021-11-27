<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LichSuChuyenDiem.ascx.cs" Inherits="VS.E_Commerce.cms.Display.QuanLyDangBai.LichSuChuyenDiem" %>
<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<div class="page-title m992">
    <h1 class="title-head margin-top-0"><a href="#">Lịch sử chuyển điểm</a></h1>
</div>
<div style="clear: both; height: 20px"></div>

<asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control droploc" AutoPostBack="true" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged" >
<asp:ListItem Value="-1" Selected="True"> = Lọc theo tiêu chí =</asp:ListItem>
<asp:ListItem Value="1">Chuyển điểm trong hệ thống</asp:ListItem>
<asp:ListItem Value="2">Chuyển điểm cho chính mình</asp:ListItem>
</asp:DropDownList>

<asp:DropDownList ID="ddlvicanchuyen" CssClass="form-control droploc" ValidationGroup="GInfo" AutoPostBack="true" OnSelectedIndexChanged="ddlvicanchuyen_SelectedIndexChanged"  runat="server">
<asp:ListItem Value="0">= Chọn ví chuyển điểm =</asp:ListItem>
<asp:ListItem Value="1">Ví thương mại</asp:ListItem>
<asp:ListItem Value="2">Ví điểm quản lý</asp:ListItem>
<%--<asp:ListItem Value="3">Ví điểm mua hàng</asp:ListItem>--%>
</asp:DropDownList>


<asp:DropDownList ID="ddlViNhanDiem" CssClass="form-control droploc" ValidationGroup="GInfo" AutoPostBack="true" OnSelectedIndexChanged="ddlViNhanDiem_SelectedIndexChanged"  runat="server">
<asp:ListItem Value="0">= Chọn ví nhận điểm =</asp:ListItem>
<asp:ListItem Value="1">Ví thương mại</asp:ListItem>
<asp:ListItem Value="2">Ví điểm quản lý</asp:ListItem>
<%--<asp:ListItem Value="3">Ví điểm mua hàng</asp:ListItem>--%>
</asp:DropDownList>
<div style=" clear:both; height:10px"></div>
<div class="table-responsive tab-all" style="overflow-x: auto;">
    <div class="list_item">
        <asp:Repeater ID="rp_pagelist" runat="server">
            <ItemTemplate>
                <tr>
                    <td style="text-align: center;">
                        <%#ShowtThanhVien(DataBinder.Eval(Container.DataItem, "IDNguoiCap").ToString())%>
                    </td>
                    <td style="text-align: center;">
                        <%#ShowtThanhVien(DataBinder.Eval(Container.DataItem, "IDNguoiNhan").ToString())%>
                    </td>
                    <td style="text-align: center;">
                        <%#DataBinder.Eval(Container.DataItem, "SoCoin")%>
                    </td>
                    <td style="text-align: center;">
                        <%#DataBinder.Eval(Container.DataItem, "NgayCap")%>
                    </td>
                    <td style="text-align: center;">
                        <%#DataBinder.Eval(Container.DataItem, "MoTa")%>
                    </td>
                </tr>
            </ItemTemplate>
            <HeaderTemplate>
                <table class="table table-striped table-bordered table-advance table-hover table table-cart lichsumuahang">
                    <tbody>
                        <tr class="trHeader" style="height: 25px">
                            <th style="width: 4%; font-weight: bold; display: none;" class="contentadmin">No</th>
                            <th style="font-weight: bold; text-align: center;">Tên người chuyển</th>
                            <th style="font-weight: bold; text-align: center;">Tên người Nhận</th>
                            <th style="font-weight: bold; text-align: center;">Số điểm</th>
                            <th style="font-weight: bold; text-align: center;">Ngày cấp</th>
                            <th style="font-weight: bold; text-align: center;">Nội dung</th>
                        </tr>
            </HeaderTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>

        <div style="color: red; text-align: right; padding-top: 7px; font-weight: 600">
            <asp:Literal ID="ltCoin" runat="server"></asp:Literal>
        </div>
    </div>
</div>
<div class="phantrang" style="">
    <cc1:CollectionPager ID="CollectionPager1" runat="server" BackNextDisplay="HyperLinks" BackNextLocation="Split"
        BackText="<<" ShowFirstLast="True" ResultsLocation="Bottom" PagingMode="PostBack" MaxPages="50" FirstText="Trang đầu" HideOnSinglePage="True" IgnoreQueryString="False" LabelStyle="font-weight: bold;color:red" LabelText="" LastText="Cuối cùng" NextText=">>" PageNumbersDisplay="Numbers"
        ResultsFormat="Hiển thị từ  {0} Đến {1} (của {2})" ResultsStyle="padding-bottom:5px;padding-top:14px;font-weight: bold;" ShowLabel="False" ShowPageNumbers="True" BackNextStyle="font-weight: bold; margin: 14px;" ControlCssClass="" ControlStyle="" UseSlider="True" PageNumbersSeparator="">
    </cc1:CollectionPager>
</div>
<asp:HiddenField ID="hdid" runat="server" />


        <div style="clear: both"></div>
                <div><span style="font-size: 11pt; color: #ed1c24; text-transform: initial; width: 90%; padding: 6px;"><em>Lưu ý : Bộ lọc chỉ áp dụng từ ngày 18/08/2020 trở đi.</em></span></div>

    <div style="clear: both"></div>