<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServiceLaiSuatAGLAND.ascx.cs" Inherits="VS.E_Commerce.cms.Admin.Member.ServiceLaiSuatAGLAND" %>
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
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="cph_Main_ContentPane">
            <div id="">
                <div class="Block Breadcrumb ui-widget-content ui-corner-top ui-corner-bottom" id="Breadcrumb">
                    <ul>
                        <li class="SecondLast"><a href="/admin.aspx"><i style="font-size: 14px;" class="icon-home"></i>Trang chủ</a></li>
                        <li class="Last"><span>Danh sách thành viên tham gia AgLang</span></li>
                    </ul>
                </div>
                <div style="clear: both;"></div>
                <div class="widget">
                    <asp:Panel ID="pn_list" runat="server" Width="100%">
                        <div class="widget-title">
                            <h4><i class="icon-list-alt"></i>&nbsp;Danh sách thành viên tham gia AgLang</h4>

                        </div>
                        <div class="widget-body">
                            <div class="row-fluid">
                                <div class="span9">
                                    <div>
                                        <asp:TextBox ID="txtkeyword" placeholder="Tìm kiếm theo tên User thành viên" runat="server" CssClass="txt_csssearch" Width="400px"></asp:TextBox>
                                        <asp:DropDownList CssClass="txt" runat="server" ID="ddlkieuthanhvien">
                                            <asp:ListItem Value="1">Người Bán</asp:ListItem>
                                            <asp:ListItem Value="2">Người được hưởng</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="lnksearch" runat="server" OnClick="lnksearch_Click" CssClass="vadd toolbar btn btn-info" Style="margin-top: -9px;"> <i class="icon-search"></i>&nbsp;Tìm kiếm</asp:LinkButton>
                                        <asp:Label ID="ltthongbao" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div class="dataTables_length" id="sample_1_length">
                                        <asp:DropDownList CssClass="txt" runat="server" ID="ddlPage" AutoPostBack="true" OnSelectedIndexChanged="ddlPage_SelectedIndexChanged">
                                            <asp:ListItem Value="50" Selected="True">Chọn số Bản ghi / Trang</asp:ListItem>
                                            <asp:ListItem Value="100">100 Bản ghi / Trang</asp:ListItem>
                                            <asp:ListItem Value="200">200 Bản ghi / Trang</asp:ListItem>
                                            <asp:ListItem Value="300">300 Bản ghi / Trang</asp:ListItem>
                                            <asp:ListItem Value="400">400 Bản ghi / Trang</asp:ListItem>
                                            <asp:ListItem Value="1000">1000 Bản ghi / Trang</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList CssClass="txt" runat="server" ID="ddlleader">
                                            <asp:ListItem Value="2967">Hùng thanh</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlkieu" CssClass="txt" AutoPostBack="true"
                                            runat="server" OnSelectedIndexChanged="ddlkieu_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Tất cả loại Hoa Hồng</asp:ListItem>
                                            <asp:ListItem Value="1">Hoa Hồng lãi suất 28%/Năm</asp:ListItem>
                                            <asp:ListItem Value="2">Hoa Hồng lãi suất 32%/Năm</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddluutien" runat="server" AutoPostBack="true" CssClass="txt" OnSelectedIndexChanged="ddluutien_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="-1">= Tất cả Ưu tiên=</asp:ListItem>
                                            <asp:ListItem Value="0">Không Ưu tiên</asp:ListItem>
                                            <asp:ListItem Value="1">Có Ưu tiên</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:TextBox Style="width: 200px;" ID="txtNgayThangNam" placeholder="Tìm kiếm từ ngày/tháng/năm" AutoPostBack="true" OnTextChanged="txtNgayThangNam_TextChanged" runat="server" CssClass="txt_csssearch" Width="200px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" runat="server" TargetControlID="txtNgayThangNam"></cc1:CalendarExtender>
                                        <asp:TextBox Style="width: 200px;" ID="txtDenNgayThangNam" placeholder="Tìm kiếm đến ngày/tháng/năm" AutoPostBack="true" OnTextChanged="txtDenNgayThangNam_TextChanged" runat="server" CssClass="txt_csssearch" Width="200px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" runat="server" TargetControlID="txtDenNgayThangNam"></cc1:CalendarExtender>

                                        <div>
                                            <asp:Label ID="ltmsg" runat="server" Font-Bold="True" ForeColor="Red" Visible="True"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="lbl_curpage" runat="server" Font-Bold="True" ForeColor="Red" Visible="True"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="span3">
                                    <div class="dataTables_filter" id="sample_1_filter">
                                        <asp:LinkButton ID="lnkxuatExel" runat="server" OnClick="lnkxuatExel_Click" CssClass="vadd toolbar btn btn-info"> Xuất Exel</asp:LinkButton>

                                        <asp:LinkButton ID="bthienthi" runat="server" OnClick="bthienthi_Click" CssClass="vadd toolbar btn btn-info"> <i class=" icon-folder-close"></i>&nbsp;Hiện thị</asp:LinkButton>
                                        <asp:LinkButton ID="btxoa" runat="server" OnClick="btxoa_Click" OnClientClick=" return confirmDelete(this);" Text="Xóa" ToolTip="Xóa những lựa chọn !" CssClass="vadd toolbar btn btn-info"><i class="icon-trash"></i>&nbsp;Xóa</asp:LinkButton>
                                    </div>
                                </div>
                            </div>

                            <div class="list_item">
                                <asp:Repeater ID="rp_pagelist" runat="server" OnItemCommand="rp_pagelist_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td style="text-align: center;">
                                                <asp:CheckBox ID="chkid" runat="server" onclick="javascript:changeColor(this,'white');" /><asp:HiddenField ID="hiID" Value='<%# Eval("ID") %>' runat="server" />
                                            </td>
                                            <td style="text-align: left;">
                                                <div><%#ShowPro(DataBinder.Eval(Container.DataItem,"IDSanPham").ToString())%></div>
                                                <div><a target="_blank" href="/admin.aspx?u=cartsNhanh&Code=<%#Eval("IDCart") %>">Mã đơn hàng: <%#(DataBinder.Eval(Container.DataItem,"IDCart").ToString())%></a></div>
                                            </td>
                                            <td>
                                                <%#ShowtThanhVien(DataBinder.Eval(Container.DataItem,"IDThanhVienBan").ToString())%>
                                            </td>
                                            <td style="text-align: center;">
                                                <%#ShowtThanhVien(DataBinder.Eval(Container.DataItem,"IDThanhVienHuongHH").ToString())%>
                                            </td>
                                            <td style="text-align: center;">
                                                <%#DataBinder.Eval(Container.DataItem,"LaiSuat")%> điểm
                                            </td>
                                            <td style="text-align: center;">
                                                <%#AllQuery.MorePro.Detail_Price(DataBinder.Eval(Container.DataItem,"SoTienDauTu").ToString())%> Vnđ
                                            </td>
                                           <td style="text-align: center;">
                                                <%#MoreAll.FormatDateTime.FormatDateFull(DataBinder.Eval(Container.DataItem,"NgayThamGia"))%> 
                                            </td>
                                            <td style="text-align: right;">
                                                <span id="lichsuserviceland" runat="server" visible='<%#Enable(Eval("ID").ToString())%>'>
                                                    <a target="_blank" href="/admin.aspx?u=lichsuserviceland&ID=<%#DataBinder.Eval(Container.DataItem,"ID")%>"><span class="BodderDo">Lịch sử</span></a>
                                                </span>
                                                <asp:LinkButton CssClass="active action-link-button" ID="LinkButton1" CommandName="EditDetail" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID")%>' runat="server"><i class="icon-edit"></i></asp:LinkButton>
                                                <asp:LinkButton CssClass="active action-link-button" CommandName="Delete" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID")%>' runat="server" ID="Linkbutton3" OnLoad="Delete_Load"><i class="icon-trash"></i></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <table cellspacing="0" style="border-collapse: collapse; margin-top: 18px" class="table table-striped table-bordered dataTable table-hover">
                                            <tbody>
                                                <tr class="trHeader" style="height: 25px">
                                                    <th style="width: 4%; font-weight: bold;" align="center" class="contentadmin" rowspan="">
                                                        <input id="chkAll" onclick="javascript: SelectAllCheckboxes(this, 1);" type="checkbox" />
                                                    </th>
                                                    <th style="width: 4%; font-weight: bold; display: none;" class="contentadmin">No</th>
                                                    <th style="font-weight: bold">Kiểu</th>
                                                    <th style="font-weight: bold">Thành viên bán
                                                    </th>
                                                    <th style="font-weight: bold">Người được hưởng</th>
                                                    <th style="font-weight: bold">Lãi suất</th>
                                                    <th style="font-weight: bold">Số tiền đầu tư</th>
                                                    <th style="font-weight: bold">Ngày tạo</th>
                                                    <th style="width: 140px; text-align: center; font-weight: bold;">Chức năng</th>
                                                </tr>
                                    </HeaderTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <div style="color: red; text-align: right; padding-top: 7px; font-weight: 600">
                                <asp:Literal ID="lttongtien" runat="server"></asp:Literal>
                            </div>
                            <div style="color: red; text-align: right; padding-top: 7px; font-weight: 600">
                                <asp:Literal ID="lttongtienbangchu" runat="server"></asp:Literal>
                            </div>
                            <div style="color: red; text-align: right; padding-top: 7px; font-weight: 600">
                                <asp:Literal ID="ltCoin" runat="server"></asp:Literal>
                            </div>


                            <table style="border-collapse: collapse" cellpadding="0" width="100%" border="0">
                                <tr height="20">
                                    <td align="right">
                                        <asp:Literal ID="ltpage" runat="server"></asp:Literal>
                                        <div class="phantrang" style="">
                                            <cc1:CollectionPager ID="CollectionPager1" runat="server" BackNextDisplay="HyperLinks" BackNextLocation="Split"
                                                BackText="<<" ShowFirstLast="True" ResultsLocation="Bottom" PagingMode="QueryString" MaxPages="50" FirstText="Trang đầu" HideOnSinglePage="True" IgnoreQueryString="False" LabelStyle="font-weight: bold;color:red" LabelText="" LastText="Cuối cùng" NextText=">>" PageNumbersDisplay="Numbers"
                                                ResultsFormat="Hiển thị từ  {0} Đến {1} (của {2})" ResultsStyle="padding-bottom:5px;padding-top:14px;font-weight: bold;" ShowLabel="False" ShowPageNumbers="True" BackNextStyle="font-weight: bold; margin: 14px;" ControlCssClass="" ControlStyle="" UseSlider="True" PageNumbersSeparator="">
                                            </cc1:CollectionPager>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                    </asp:Panel>
                    <asp:Panel ID="pn_insert" runat="server" Visible="False" Width="100%">
                        <div class="widget-title">
                            <h4><i class="icon-list-alt"></i>&nbsp;Cập nhật thông tin</h4>
                        </div>
                        <div class="widget-body">
                            <div class='frm-add'>
                                <table class="Tables">
                                    <tr>
                                        <td align="left" width="200"></td>
                                        <td width="10"></td>
                                        <td>
                                            <asp:Label ID="lblmsg" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">Thành viên
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:TextBox ID="txtTenthanhvien" placeholder="Tìm kiếm theo tên User thành viên" AutoPostBack="true" OnTextChanged="txtTenthanhvien_TextChanged" CssClass="txt_css" runat="server" Width="320px"></asp:TextBox>
                                            <br />
                                            <asp:Label ID="ltthongtin" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                            <asp:HiddenField ID="hdidthanhvien" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td></td>
                                        <td></td>
                                    </tr>

                                    <tr>
                                        <td align="left">Tùy chọn
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:CheckBox ID="chkcoppy" CssClass="CsCheckBox" runat="server" Text=" Sao chép số cổ phần sang thành viên mới" />

                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td></td>
                                        <td>

                                            <div style="font-size: 8pt; color: #ed1c24"><em>(Lưu ý : Khi cập nhật sẽ lưu cả (SỐ CỔ PHẦN ĐANG SỞ HỮU) Và (SỐ TIỀN CỔ PHẦN ĐANG SỞ HỮU) sang cho thành viên mới và sẽ sét thành viên cũ bằng 0)</em></div>
                                            <div style="font-size: 8pt; color: #ed1c24"><em>(Lưu ý : Đối với thành viên mua 2 sản phẩn land thì không lên kích vào Ô check này.)</em></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td></td>
                                        <td>
                                            <asp:LinkButton ID="btn_InsertUpdate" runat="server" OnClick="btn_InsertUpdate_Click" Style="background: #ed1c24" CssClass="toolbar btn btn-info"> <i class="icon-save"></i> Cập nhật </asp:LinkButton>
                                            <asp:LinkButton ID="btnCancel" runat="server" OnClick="btnCancel_Click" CssClass="toolbar btn btn-info"> <i class="icon-chevron-left"></i>Hủy</asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>

                            </div>
                            <asp:HiddenField ID="hdid" runat="server" />
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
        <asp:HiddenField ID="hdIDHuong" runat="server" />
        <asp:HiddenField ID="hdidservice" runat="server" />
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="lnkxuatExel" />
    </Triggers>
</asp:UpdatePanel>


<style>
    i {
        font-size: 20px;
    }
</style>
