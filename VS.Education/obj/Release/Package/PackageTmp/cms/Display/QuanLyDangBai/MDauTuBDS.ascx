<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MDauTuBDS.ascx.cs" Inherits="VS.E_Commerce.cms.Display.QuanLyDangBai.MDauTuBDS" %>
<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<link type="text/css" rel="stylesheet" href="/Resources/admins/css/font-awesome.css" />
            <div class="nentrang">
                <h1 class="title-head">
                    <span>Danh sách lịch sử đầu tư</span>
                </h1>
                <div style="clear: both"></div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pn_list" runat="server" Width="100%">
                            <div class="widget-body">
                                <div class="list_item">
                                    <div class="table-responsive tab-all" style="overflow-x: auto;">
                                        <asp:Repeater ID="rp_pagelist" runat="server" > 
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: center;">
                                                        <%= i++ %>
                                                    </td>
                                                     <td align="left" style="padding-left: 10px; line-height: 22px; color: #646465" width="350px">
                                        <%#MoreAll.MoreImage.ChuyenTien(Eval("Anh").ToString())%><%#MoreAll.MoreImage.ImagesCMT(Eval("CMNDTruoc").ToString())%><%#MoreAll.MoreImage.ImagesCMT(Eval("CMNDSau").ToString())%>
                                        <br />
                                        Họ và tên:<span style="color: #444444; padding-left: 27px; font-weight: bold"> <%#ShowtThanhVien(DataBinder.Eval(Container.DataItem,"IDThanhVien").ToString())%></span><br />
                                        Địa chỉ:<span style="color: #444444; padding-left: 40px; font-weight: bold"><%#DataBinder.Eval(Container.DataItem, "DiaChi")%></span><br />
                                        Điện thoại:<span style="color: #444444; padding-left: 40px; font-weight: bold"><%#DataBinder.Eval(Container.DataItem, "DienThoai")%></span><br />
                                        CMND/CCCD:<span style="color: #444444; padding-left: 40px; font-weight: bold"><%#DataBinder.Eval(Container.DataItem, "CMND")%></span><br />
                                        Tên ngân hàng:<span style="color: #444444; padding-left: 40px; font-weight: bold"><%#DataBinder.Eval(Container.DataItem, "TenNganHang")%></span><br />
                                        Số tài khoản:<span style="color: #444444; padding-left: 40px; font-weight: bold"><%#DataBinder.Eval(Container.DataItem, "SoTaiKHoan")%></span><br />
                                        Chi nhánh:<span style="color: #444444; padding-left: 22px; font-weight: bold"><%#DataBinder.Eval(Container.DataItem, "ChiNhanh")%></span><br />
                                        Ghi chú:<span style="color: #444444; padding-left: 15px; font-weight: bold"><%#DataBinder.Eval(Container.DataItem, "GhiChu")%></span><br />
                                    </td>

                                    <td style="text-align: center;">
                                        <%#AllQuery.MorePro.FormatMoney_NO(DataBinder.Eval(Container.DataItem,"TongTienDauTu").ToString())%> VNĐ
                                    </td>
                                    <td style="text-align: center;">
                                        <%#MoreAll.FormatDateTime.FormatDateFull(DataBinder.Eval(Container.DataItem,"NgayTao"))%> 
                                    </td>
                                    <td style="text-align: center;">
                                        <%#DataBinder.Eval(Container.DataItem,"NgayDuyet").ToString()%> 
                                    </td>
                                    <td>
                                        <%#MoreAll.MoreAll.TrangThaiEnableRut(DataBinder.Eval(Container.DataItem,"TrangThai").ToString())%> 
                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table cellspacing="0" style="border-collapse: collapse; margin-top: 18px" class="table table-striped table-bordered dataTable table-hover">
                                                        <tr class="trHeader" style="height: 25px">
                                                            <th style="width: 4%; font-weight: bold;text-align:center" class="contentadmin">STT</th>
                                                            <th style="font-weight: bold">Thông tin thành viên</th>
                                            <th style="font-weight: bold">Tiền đầu tư</th>
                                            <th style="font-weight: bold">Ngày tạo</th>
                                            <th style="font-weight: bold">Ngày duyệt</th>
                                            <th style="font-weight: bold">Trạng thái</th>
                                                        </tr>
                                            </HeaderTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div style="color: red; text-align: right; padding-top: 7px; font-weight: 600">
                                    <asp:Literal ID="ltCoin" runat="server"></asp:Literal>
                                </div>
                                <asp:Literal ID="ltpage" runat="server"></asp:Literal>
                                <div class="phantrang" style="">
                                    <cc1:CollectionPager ID="CollectionPager1" runat="server" BackNextDisplay="HyperLinks" BackNextLocation="Split"
                                        BackText="<<" ShowFirstLast="True" ResultsLocation="Bottom" PagingMode="PostBack" MaxPages="50" FirstText="Trang đầu" HideOnSinglePage="True" IgnoreQueryString="False" LabelStyle="font-weight: bold;color:red" LabelText="" LastText="Cuối cùng" NextText=">>" PageNumbersDisplay="Numbers"
                                        ResultsFormat="Hiển thị từ  {0} Đến {1} (của {2})" ResultsStyle="padding-bottom:5px;padding-top:14px;font-weight: bold;" ShowLabel="False" ShowPageNumbers="True" BackNextStyle="font-weight: bold; margin: 14px;" ControlCssClass="" ControlStyle="" UseSlider="True" PageNumbersSeparator="">
                                    </cc1:CollectionPager>
                                </div>
                        </asp:Panel>
                        <input id="hd_insertupdate" type="hidden" size="1" name="Hidden1" runat="server">
                        <input id="hd_id" type="hidden" size="1" name="Hidden2" runat="server">
                        <input id="hd_page_edit_id" type="hidden" size="1" name="Hidden2" runat="server">
                        <input id="hd_imgpath" type="hidden" size="1" name="Hidden2" runat="server">
                        <input id="hd_rootpic" type="hidden" size="1" runat="server">
                        <input id="hd_par_id" type="hidden" size="1" name="Hidden2" runat="server">
                        <asp:HiddenField ID="hdid" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <style>
                    i {
                        font-size: 20px;
                    }
                </style>
            </div>
