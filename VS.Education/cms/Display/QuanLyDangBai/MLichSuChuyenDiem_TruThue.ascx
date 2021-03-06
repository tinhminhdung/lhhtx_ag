<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MLichSuChuyenDiem_TruThue.ascx.cs" Inherits="VS.E_Commerce.cms.Display.QuanLyDangBai.MLichSuChuyenDiem_TruThue" %>
<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<link type="text/css" rel="stylesheet" href="/Resources/admins/css/font-awesome.css" />
            <div class="nentrang">
                <h1 class="title-head">
                    <span>Danh sách kích thu nhập cá nhân</span>
                </h1>
                <div style="clear: both"></div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pn_list" runat="server" Width="100%">
                            <div class="widget-body">
                                <div class="row-fluid">
                                    <div class="span10">
                                        <div class="dataTables_length" id="sample_1_length">
                                            <div class="frm_hoahong">
                                                <asp:DropDownList ID="DropDownList1" CssClass="form-control" AutoPostBack="true" Width="170px"
                                                    runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Chọn ngày</asp:ListItem>
                                                    <asp:ListItem Value="01">Ngày 1</asp:ListItem>
                                                    <asp:ListItem Value="02">Ngày 2</asp:ListItem>
                                                    <asp:ListItem Value="03">Ngày 3</asp:ListItem>
                                                    <asp:ListItem Value="04">Ngày 4</asp:ListItem>
                                                    <asp:ListItem Value="05">Ngày 5</asp:ListItem>
                                                    <asp:ListItem Value="06">Ngày 6</asp:ListItem>
                                                    <asp:ListItem Value="07">Ngày 7</asp:ListItem>
                                                    <asp:ListItem Value="08">Ngày 8</asp:ListItem>
                                                    <asp:ListItem Value="09">Ngày 9</asp:ListItem>
                                                    <asp:ListItem Value="10">Ngày 10</asp:ListItem>
                                                    <asp:ListItem Value="11">Ngày 11</asp:ListItem>
                                                    <asp:ListItem Value="12">Ngày 12</asp:ListItem>
                                                    <asp:ListItem Value="13">Ngày 13</asp:ListItem>
                                                    <asp:ListItem Value="14">Ngày 14</asp:ListItem>
                                                    <asp:ListItem Value="15">Ngày 15</asp:ListItem>
                                                    <asp:ListItem Value="16">Ngày 16</asp:ListItem>
                                                    <asp:ListItem Value="17">Ngày 17</asp:ListItem>
                                                    <asp:ListItem Value="18">Ngày 18</asp:ListItem>
                                                    <asp:ListItem Value="19">Ngày 19</asp:ListItem>
                                                    <asp:ListItem Value="20">Ngày 20</asp:ListItem>
                                                    <asp:ListItem Value="21">Ngày 21</asp:ListItem>
                                                    <asp:ListItem Value="22">Ngày 22</asp:ListItem>
                                                    <asp:ListItem Value="23">Ngày 23</asp:ListItem>
                                                    <asp:ListItem Value="24">Ngày 24</asp:ListItem>
                                                    <asp:ListItem Value="25">Ngày 25</asp:ListItem>
                                                    <asp:ListItem Value="26">Ngày 26</asp:ListItem>
                                                    <asp:ListItem Value="27">Ngày 27</asp:ListItem>
                                                    <asp:ListItem Value="28">Ngày 28</asp:ListItem>
                                                    <asp:ListItem Value="29">Ngày 29</asp:ListItem>
                                                    <asp:ListItem Value="30">Ngày 30</asp:ListItem>
                                                    <asp:ListItem Value="31">Ngày 31</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="DropDownList2" CssClass="form-control" AutoPostBack="true" Width="170px"
                                                    runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Chọn tháng</asp:ListItem>
                                                    <asp:ListItem Value="01"> Tháng 1</asp:ListItem>
                                                    <asp:ListItem Value="02">Tháng 2</asp:ListItem>
                                                    <asp:ListItem Value="03">Tháng 3</asp:ListItem>
                                                    <asp:ListItem Value="04">Tháng 4</asp:ListItem>
                                                    <asp:ListItem Value="05">Tháng 5</asp:ListItem>
                                                    <asp:ListItem Value="06">Tháng 6</asp:ListItem>
                                                    <asp:ListItem Value="07">Tháng 7</asp:ListItem>
                                                    <asp:ListItem Value="08">Tháng 8</asp:ListItem>
                                                    <asp:ListItem Value="09">Tháng 9</asp:ListItem>
                                                    <asp:ListItem Value="10">Tháng 10</asp:ListItem>
                                                    <asp:ListItem Value="11">Tháng 11</asp:ListItem>
                                                    <asp:ListItem Value="12">Tháng 12</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="DropDownList3" CssClass="form-control" AutoPostBack="true" Width="170px" runat="server"
                                                    OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
                                                </asp:DropDownList>

                                            </div>
                                            <div>
                                                <asp:Label ID="ltmsg" runat="server" Font-Bold="True" ForeColor="Red" Visible="True"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="lbl_curpage" runat="server" Font-Bold="True" ForeColor="Red" Visible="True"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span2">
                                        <div class="dataTables_filter" id="sample_1_filter">
                                            <asp:LinkButton ID="bthienthi" runat="server" OnClick="bthienthi_Click" CssClass="vadd toolbar btn btn-info"> <i class=" icon-folder-close"></i>&nbsp;Hiện thị</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div style="clear: both; height: 20px"></div>
                                <div class="list_item">
                                    <div class="table-responsive tab-all" style="overflow-x: auto;">
                                        <asp:Repeater ID="rp_pagelist" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: center;">
                                                        <%= i++ %>
                                                    </td>
                                                      <td style="text-align: center;">
                                                        <%#DataBinder.Eval(Container.DataItem,"SoDiemViHoaHong").ToString()%>
                                                    </td>
                                                      <td style="text-align: center;">
                                                        <%#DataBinder.Eval(Container.DataItem,"PhanTramThue").ToString()%> %
                                                    </td>
                                                     <td style="text-align: center;">
                                                        <%#DataBinder.Eval(Container.DataItem,"SoDienBiTru").ToString()%> 
                                                    </td>
                                                      <td style="text-align: center;">
                                                        <%#DataBinder.Eval(Container.DataItem,"SoDiemSauKhiTru").ToString()%>
                                                    </td>
                                                       <td style="text-align: center;">
                                                        <%#DataBinder.Eval(Container.DataItem,"ViThuongMai").ToString()%>
                                                    </td>
                                                       <td style="text-align: center;">
                                                        <%#DataBinder.Eval(Container.DataItem,"ViMuaHang").ToString()%>
                                                    </td>
                                                       <td style="text-align: center;">
                                                        <%#(DataBinder.Eval(Container.DataItem,"NgayGiaoDich"))%> 
                                                    </td>
                                                   <%-- <td style="text-align: center;">
                                                        <%#MoreAll.MoreAll.Vinao(DataBinder.Eval(Container.DataItem,"LoaiVi").ToString())%> 
                                                    </td>--%>
                                                </tr>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table cellspacing="0" style="border-collapse: collapse; margin-top: 18px" class="table table-striped table-bordered dataTable table-hover">
                                                        <tr class="trHeader" style="height: 25px">
                                                            <th style="width: 4%; font-weight: bold;text-align:center" class="contentadmin">STT</th>
                                                             <th style="font-weight: bold; text-align:center">Số điểm hoa hồng</th>   
                                                            <th style="font-weight: bold; text-align:center;">% Trừ</th>
                                                            <th style="font-weight: bold; text-align:center">Số điểm bị trừ</th>
                                                             <th style="font-weight: bold; text-align:center">Số điểm sau khi bị trừ</th>
                                                           
                                                            <th style="font-weight: bold; text-align:center">Ví Thương mại</th>
                                                            <th style="font-weight: bold; text-align:center">Ví Mua Hàng</th>
                                                              <th style="font-weight: bold; text-align:center">Ngày Duyệt</th>
                                                             <%--<th style="font-weight: bold; text-align:center; ">Duyệt ví</th>--%>
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
