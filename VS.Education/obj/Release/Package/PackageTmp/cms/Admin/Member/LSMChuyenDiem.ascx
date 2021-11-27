<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LSMChuyenDiem.ascx.cs" Inherits="VS.E_Commerce.cms.Admin.Member.LSMChuyenDiem" %>
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
                        <li class="Last"><span>Danh sách chuyển điểm</span></li>
                    </ul>
                </div>
                <div style="clear: both;"></div>
                <div class="widget">
                    <asp:Panel ID="pn_list" runat="server" Width="100%">
                        <div class="widget-title">
                            <h4><i class="icon-list-alt"></i>&nbsp; Danh sách chuyển điểm</h4>
                            <div class="ui-widget-content ui-corner-top ui-corner-bottom">
                                <div id="toolbox">
                                    <div class="toolbox-content" style="float: right;">
                                        <table class="toolbar">
                                            <tbody>
                                                <tr>
                                                    <td align="center">
                                                        <a href="/admin.aspx?u=Thanhvien" class="vadd toolbar btn btn-info"><i class=" icon-undo"></i>&nbsp; Quay về danh sách thành viên</a>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                      

                        <div class="widget-body">
                            <div class="row-fluid">
                                <div class="span12">
                                    <div>
                                        <asp:TextBox ID="txtkeyword" placeholder="Tìm kiếm theo tên User thành viên" runat="server" CssClass="txt_csssearch" Width="400px"></asp:TextBox>
                                        <asp:DropDownList CssClass="txt" runat="server" ID="ddlkieuthanhvien">
                                            <asp:ListItem Value="1">Người chuyển điểm</asp:ListItem>
                                            <asp:ListItem Value="2">Người nhận điểm</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox Style="width: 200px;" ID="txtNgayThangNam" placeholder="Tìm kiếm từ ngày/tháng/năm" AutoPostBack="true" OnTextChanged="txtNgayThangNam_TextChanged" runat="server" CssClass="txt_csssearch" Width="200px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" runat="server" TargetControlID="txtNgayThangNam"></cc1:CalendarExtender>
                                        <asp:TextBox Style="width: 200px;" ID="txtDenNgayThangNam" placeholder="Tìm kiếm đến ngày/tháng/năm" AutoPostBack="true" OnTextChanged="txtDenNgayThangNam_TextChanged" runat="server" CssClass="txt_csssearch" Width="200px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" runat="server" TargetControlID="txtDenNgayThangNam"></cc1:CalendarExtender>
                                        <asp:LinkButton ID="lnksearch" runat="server" OnClick="lnksearch_Click" CssClass="vadd toolbar btn btn-info" Style="margin-top: -8px;"> <i class="icon-search"></i>&nbsp;Tìm kiếm</asp:LinkButton>
                                        <asp:LinkButton ID="lnkxuatExel" runat="server" OnClick="lnkxuatExel_Click" CssClass="vadd toolbar btn btn-info" Style="margin-top: -8px;"> Xuất Exel</asp:LinkButton>
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
                                            <asp:Label ID="ltmsg" runat="server" Font-Bold="True" ForeColor="Red" Visible="True"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="lbl_curpage" runat="server" Font-Bold="True" ForeColor="Red" Visible="True"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="list_item">
                                <table cellspacing="0" style="border-collapse: collapse; margin-top: 18px" class="table table-striped table-bordered dataTable table-hover">
                                    <tbody>
                                        <tr class="trHeader" style="height: 25px">
                                            <th style="width: 4%; font-weight: bold; display: none;" class="contentadmin">No</th>
                                            <th style="font-weight: bold">Tên người chuyển</th>
                                            <th style="font-weight: bold">Tên người Nhận</th>
                                            <th style="font-weight: bold">Số điểm</th>
                                            <th style="font-weight: bold">Ngày cấp</th>
                                            <th style="font-weight: bold">Nội dung</th>
                                        </tr>
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
                                        </asp:Repeater>
                                        <tr>
                                            <td style="text-align: right; color: red; font-weight: bold" colspan="4">Tổng cộng :
                        <asp:Literal ID="ltCoin" runat="server"></asp:Literal>
                                            </td>
                                            <td style="text-align: center; color: red; font-weight: bold">Tổng tiền / Trang : 
                        <asp:Literal ID="ltCoinPage" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                </table>
                            </div>
                            <table style="border-collapse: collapse" cellpadding="0" width="100%" border="0">
                                <tr height="20">
                                    <td align="right">
                                        <div class="phantrang" style="">
                                            <asp:Literal ID="ltpage" runat="server"></asp:Literal>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                    </asp:Panel>
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

