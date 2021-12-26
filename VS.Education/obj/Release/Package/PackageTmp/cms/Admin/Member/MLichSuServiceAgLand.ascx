<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MLichSuServiceAgLand.ascx.cs" Inherits="VS.E_Commerce.cms.Admin.Member.MLichSuServiceAgLand" %>
<div id="cph_Main_ContentPane">
    <div id="">
        <div class="Block Breadcrumb ui-widget-content ui-corner-top ui-corner-bottom" id="Breadcrumb">
            <ul>
                <li class="SecondLast"><a href="/admin.aspx"><i style="font-size: 14px;" class="icon-home"></i>Trang chủ</a></li>
                <li class="Last"><span>Danh sách lịch sử thay đổi agland</span></li>
            </ul>
        </div>
        <div style="clear: both;"></div>
        <div class="widget">
            <div class="list_item">
                <asp:Repeater ID="rp_pagelist" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="text-align: center;">
                                 <%= i++ %>
                            </td>
                             <td style="text-align: center;">
                                <%#Eval("IDCart") %>
                            </td>
                            <td style="text-align: center;">
                                <%#ShowtThanhVien(DataBinder.Eval(Container.DataItem,"IDThanhVienCu").ToString())%>
                            </td>
                            <td style="text-align: center;">
                                <%#ShowtThanhVien(DataBinder.Eval(Container.DataItem,"IDThanhVienMoi").ToString())%>
                            </td>
                            <td style="text-align: center;">
                                <%#Eval("NguoiTao") %>
                            </td>
                            <td style="text-align: center;">
                                <%#MoreAll.FormatDateTime.FormatDateFull(DataBinder.Eval(Container.DataItem,"NgayTao"))%> 
                            </td>
                            <td style="text-align: center;">
                                <%#Eval("TrangThai") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <HeaderTemplate>
                        <table cellspacing="0" style="border-collapse: collapse; margin-top: 18px" class="table table-striped table-bordered dataTable table-hover">
                            <tbody>
                                <tr class="trHeader" style="height: 25px">
                                    <th style="width: 4%; font-weight: bold;" class="contentadmin">STT</th>
                                    <th style="font-weight: bold">Mã đơn hàng </th>
                                    <th style="font-weight: bold">Thành viên cũ </th>
                                    <th style="font-weight: bold">Thành viên mới</th>
                                    <th style="font-weight: bold">Người tạo</th>
                                    <th style="font-weight: bold">Ngày tạo</th>
                                    <th style="font-weight: bold">Nội dung</th>
                                </tr>
                    </HeaderTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>

        </div>
    </div>
</div>
