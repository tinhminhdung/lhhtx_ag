<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LichSuCapDiem.ascx.cs" Inherits="VS.E_Commerce.cms.Display.QuanLyDangBai.LichSuCapDiem" %>
<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<div class="page-title m992">
    <h1 class="title-head margin-top-0"><a href="#">Lịch sử cấp điểm</a></h1>
</div>
 <div style=" clear:both;height:20px"></div>
<div class="table-responsive tab-all" style="overflow-x: auto;">
<div class="list_item">
    <asp:Repeater ID="rp_pagelist" runat="server" >
        <ItemTemplate>
            <tr>
                <td style="text-align: center;">
                    <%#ShowtThanhVien(DataBinder.Eval(Container.DataItem, "IDNguoiCap").ToString(),DataBinder.Eval(Container.DataItem, "NguoiTao").ToString())%>
                </td>
                <td style="text-align: center;">
                    <%#ShowtThanhVien(DataBinder.Eval(Container.DataItem, "IDNguoiNhanDiemCoin").ToString(),DataBinder.Eval(Container.DataItem, "NguoiTao").ToString())%>
                </td>
                <td style="text-align: center;">
                    <%#DataBinder.Eval(Container.DataItem, "SoDiemCoin")%>
                </td>
                 <td style="text-align: center;">
                   <%#(DataBinder.Eval(Container.DataItem, "MoTa").ToString())%>
                </td>
                <td style="text-align: center;">
                    <%#DataBinder.Eval(Container.DataItem, "NgayCap")%>
                </td>
            </tr>
        </ItemTemplate>
        <HeaderTemplate>
           <table class="table table-striped table-bordered table-advance table-hover table table-cart lichsumuahang">
                <tbody>
                    <tr class="trHeader" style="height: 25px">
                        <th style="width: 4%; font-weight: bold; display: none;" class="contentadmin">No</th>
                        <th style="font-weight: bold;text-align: center;">Tên người chuyển</th>
                        <th style="font-weight: bold;text-align: center;">Tên người Nhận</th>
                        <th style="font-weight: bold;text-align: center;">Số điểm</th>
                           <th style="font-weight: bold;text-align: center;">Loại ví</th>
                        <th style="font-weight: bold;text-align: center;">Ngày cấp</th>
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
