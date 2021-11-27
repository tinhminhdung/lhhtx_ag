<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LichSuDangNhap.ascx.cs" Inherits="VS.E_Commerce.cms.Admin.Member.LichSuDangNhap" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<script src="/Scripts/ckfinder/ckfinder.js" type="text/javascript"></script>
<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<script language="javascript">
    var r = {
        'special': /[\W]/g,
        'quotes': /[^0-9^,]/g,
        'notnumbers': /[^a-zA]/g
    }
    function valid(o, w) {
        o.value = o.value.replace(r[w], '');
    }
</script>
        <div id="cph_Main_ContentPane">
            <div id="">
                <div class="Block Breadcrumb ui-widget-content ui-corner-top ui-corner-bottom" id="Breadcrumb">
                    <ul>
                        <li class="SecondLast"><a href="/admin.aspx"><i style="font-size: 14px;" class="icon-home"></i>Trang chủ</a></li>
                        <li class="Last"><span>Danh sách lịch sử đăng nhập</span></li>
                    </ul>
                </div>
                <div style="clear: both;"></div>
                <div class="widget">
                    <div class="widget-title">
                        <h4><i class="icon-list-alt"></i>&nbsp;Danh sách lịch sử đăng nhập</h4>
                    </div>
                    <div class="widget-body">
                        <div class="row-fluid">
                            <div class="span9">

                                <div style="font-size: 18px; padding-bottom: 20px">Thành viên :
                                    <asp:Literal ID="ltname" runat="server"></asp:Literal></div>
                                <div class="dataTables_length" id="sample_1_length">
                                    <asp:DropDownList runat="server" ID="ddlPage" AutoPostBack="true" OnSelectedIndexChanged="ddlPage_SelectedIndexChanged">
                                        <asp:ListItem Value="50" Selected="True">Chọn số Bản ghi / Trang</asp:ListItem>
                                        <asp:ListItem Value="100">100 Bản ghi / Trang</asp:ListItem>
                                        <asp:ListItem Value="200">200 Bản ghi / Trang</asp:ListItem>
                                        <asp:ListItem Value="300">300 Bản ghi / Trang</asp:ListItem>
                                        <asp:ListItem Value="400">400 Bản ghi / Trang</asp:ListItem>
                                        <asp:ListItem Value="1000">1000 Bản ghi / Trang</asp:ListItem>
                                    </asp:DropDownList>
                                    <div>
                                        <asp:Label ID="ltmsg" runat="server" Font-Bold="True" ForeColor="Red" Visible="True"></asp:Label></div>
                                    <div>
                                        <asp:Label ID="lbl_curpage" runat="server" Font-Bold="True" ForeColor="Red" Visible="True"></asp:Label></div>
                                </div>
                            </div>
                        </div>
                        <div class="list_item">
                            <asp:Repeater ID="rp_pagelist" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td style="text-align: center;">

                                           <a target="_blank" href="https://whatismyipaddress.com/ip/<%#DataBinder.Eval(Container.DataItem, "IP")%>" ><%#DataBinder.Eval(Container.DataItem, "IP")%></a>
                                        </td>
                                        <td style="text-align: center;">
                                            <%#DataBinder.Eval(Container.DataItem, "ThoiGian")%>
                                        </td>
                                        <td style="text-align: center;">
                                            <%#DataBinder.Eval(Container.DataItem, "ThietBi")%>
                                        </td>
                                         <td style="text-align: center;">
                                            <%#DataBinder.Eval(Container.DataItem, "ViTangTienVip")%>
                                        </td>
                                        <td style="text-align: center;">
                                            <%#DataBinder.Eval(Container.DataItem, "ViMuaHang")%>
                                        </td>
                                        <td style="text-align: center;">
                                            <%#DataBinder.Eval(Container.DataItem, "ViQuanLy")%>
                                        </td>
                                        <td style="text-align: center;">
                                            <%#DataBinder.Eval(Container.DataItem, "ViThuongMai")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <table cellspacing="0" style="border-collapse: collapse; margin-top: 18px" class="table table-striped table-bordered dataTable table-hover">
                                        <tbody>
                                            <tr class="trHeader" style="height: 25px">
                                                <th style="font-weight: bold">IP</th>
                                                <th style="font-weight: bold">Thời gian</th>
                                                <th style="font-weight: bold">Thiết bị</th>
                                                <th style="font-weight: bold">THƯỞNG MUA HÀNG</th>
                                                <th style="font-weight: bold">Ví mua hàng</th>
                                                <th style="font-weight: bold">Ví quản lý</th>
                                                <th style="font-weight: bold">Ví Thương mại</th>
                                            </tr>
                                </HeaderTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                        <table style="border-collapse: collapse" cellpadding="0" width="100%" border="0">
                                <tr height="20">
                                    <td align="right">
                                        <div class="phantrang" style="">
                                            <cc1:CollectionPager ID="CollectionPager1" runat="server" BackNextDisplay="HyperLinks" BackNextLocation="Split"
                                                BackText="<<" ShowFirstLast="True" ResultsLocation="Bottom" PagingMode="QueryString" MaxPages="50" FirstText="Trang đầu" HideOnSinglePage="True" IgnoreQueryString="False" LabelStyle="font-weight: bold;color:red" LabelText="" LastText="Cuối cùng" NextText=">>" PageNumbersDisplay="Numbers"
                                                ResultsFormat="Hiển thị từ  {0} Đến {1} (của {2})" ResultsStyle="padding-bottom:5px;padding-top:14px;font-weight: bold;" ShowLabel="False" ShowPageNumbers="True" BackNextStyle="font-weight: bold; margin: 14px;" ControlCssClass="" ControlStyle="" UseSlider="True" PageNumbersSeparator="">
                                            </cc1:CollectionPager>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                    </div>
                </div>
            </div>
            <input id="hd_insertupdate" type="hidden" size="1" name="Hidden1" runat="server">
            <input id="hd_id" type="hidden" size="1" name="Hidden2" runat="server">
            <input id="hd_page_edit_id" type="hidden" size="1" name="Hidden2" runat="server">
            <input id="hd_imgpath" type="hidden" size="1" name="Hidden2" runat="server">
            <input id="hd_rootpic" type="hidden" size="1" runat="server">
            <input id="hd_par_id" type="hidden" size="1" name="Hidden2" runat="server">
            <asp:HiddenField ID="hidLevel" runat="server" />
 </div>