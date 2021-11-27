<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DSMuaHangTheoThang.ascx.cs" Inherits="VS.E_Commerce.cms.Admin.Member.DSMuaHangTheoThang" %>
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
                <li class="Last"><span>Danh sách thành viên mua hàng</span></li>
            </ul>
        </div>
        <div style="clear: both;"></div>
        <div class="widget">
            <div class="widget-title">
                <h4><i class="icon-list-alt"></i>&nbsp;Danh sách thành viên mua hàng</h4>

            </div>
            <div class="widget-body">
                <div class="row-fluid">
                    <div class="span9">
                        <div>
                            <asp:TextBox Style="width: 200px;" ID="txtNgayThangNam" placeholder="Tìm kiếm từ ngày/tháng/năm" AutoPostBack="true" OnTextChanged="txtNgayThangNam_TextChanged" runat="server" CssClass="txt_csssearch" Width="200px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" runat="server" TargetControlID="txtNgayThangNam"></cc1:CalendarExtender>
                            <asp:TextBox Style="width: 200px;" ID="txtDenNgayThangNam" placeholder="Tìm kiếm đến ngày/tháng/năm" AutoPostBack="true" OnTextChanged="txtDenNgayThangNam_TextChanged" runat="server" CssClass="txt_csssearch" Width="200px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" runat="server" TargetControlID="txtDenNgayThangNam"></cc1:CalendarExtender>
                            <asp:LinkButton ID="lnksearch" runat="server" OnClick="lnksearch_Click" CssClass="vadd toolbar btn btn-info" Style="margin-top: -9px;"> <i class="icon-search"></i>&nbsp;Tìm kiếm</asp:LinkButton>
                            <asp:Label ID="ltthongbao" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                        </div>
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
                                <td style="text-align: left;">
                                  <%-- <b><a style=" color:red" target="_blank" href="/admin.aspx?u=Thanhvien&IDThanhVien=<%#DataBinder.Eval(Container.DataItem, "IDThanhVien")%>"><%#DataBinder.Eval(Container.DataItem, "vfname")%></a></b>--%>
                                     <%#ShowtThanhVien(DataBinder.Eval(Container.DataItem,"IDThanhVien").ToString())%>
                                </td>
                                 <td style="text-align: center;">
                                 <a  target="_blank" href="admin.aspx?u=DetailCartNhanh&ID=<%#DataBinder.Eval(Container.DataItem, "ID").ToString()%>">Đơn hàng: # <%#DataBinder.Eval(Container.DataItem, "ID").ToString()%></a>
                                </td>
                                  <td style="text-align: center;">
                                  <%#MoreAll.MoreAll.FormatDate(DataBinder.Eval(Container.DataItem, "Create_Date").ToString())%>
                                </td>
                                  <td style="text-align: center;">
                                    <%#AllQuery.MorePro.Detail_Price(TongTien(DataBinder.Eval(Container.DataItem, "ID").ToString()))%>
                                </td>
                                 <td style="text-align: center;">
                                      <%#ShowViTangTienVip(DataBinder.Eval(Container.DataItem,"IDThanhVien").ToString())%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <table cellspacing="0" style="border-collapse: collapse; margin-top: 18px" class="table table-striped table-bordered dataTable table-hover">
                                <tbody>
                                    <tr class="trHeader" style="height: 25px">
                                        <th style="width: 4%; font-weight: bold; display: none;" class="contentadmin">No</th>
                                        <th style="font-weight: bold; text-align:left;">Tên thành viên</th>
                                         <th style="font-weight: bold">Đơn hàng</th>

                                        <th style="font-weight: bold">Ngày mua hàng</th>
                                          <th style="font-weight: bold">Giá tiền theo NY </th>
                                        <th style="font-weight: bold">THƯỞNG MUA HÀNG</th>
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
                                <asp:Literal ID="ltpage" runat="server"></asp:Literal>
                            </div>
                             <div class="phantrang">
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
