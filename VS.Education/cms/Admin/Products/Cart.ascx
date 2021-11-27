<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Cart.ascx.cs" Inherits="VS.E_Commerce.cms.Admin.Products.Cart" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<script src="/Scripts/ckfinder/ckfinder.js" type="text/javascript"></script>
<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>

<div id="cph_Main_ContentPane">
    <div id="">
        <div class="Block Breadcrumb ui-widget-content ui-corner-top ui-corner-bottom" id="Breadcrumb">
            <ul>
                <li class="SecondLast"><a href="/admin.aspx"><i style="font-size: 14px;" class="icon-home"></i>Trang chủ</a></li>
                <li class="Last"><span>Quản lý giỏ hàng</span></li>
            </ul>
        </div>
        <div style="clear: both;"></div>
        <div class="widget">
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="View1" runat="server">
                    <div class="widget-title">
                        <h4><i class="icon-list-alt"></i>&nbsp;Quản lý giỏ hàng     </h4>
                    </div>
                    <div class="widget-body">

                        <div class="row-fluid">
                            <div class="span2">
                                <div id="sample_1_length" class="dataTables_length">
                                    <div class="frm_search">
                                        <div>
                                            <asp:Literal ID="lttotal1" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="span12">
                                <div class="dataTables_filter" id="sample_1_filter">
                                    <asp:DropDownList ID="ddlstatus" Style="width: 320px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                                        <asp:ListItem Value="-1" Selected="True">Trạng Thái Nhà Cung Cấp & (Trạng thái Admin)</asp:ListItem>
                                        <asp:ListItem Value="1">Đơn hàng Đã duyệt</asp:ListItem>
                                        <asp:ListItem Value="2">Đơn hàng Bị hủy</asp:ListItem>
                                        <asp:ListItem Value="3">Đơn hàng Chưa xử lý</asp:ListItem>
                                        <asp:ListItem Value="4">Đơn hàng Bị người mua trả lại hàng</asp:ListItem>
                                        <asp:ListItem Value="5">Hoàn tiền</asp:ListItem>
                                        <asp:ListItem Value="6">Khiếu kiện gửi Admin</asp:ListItem>
                                        <asp:ListItem Value="7">(Admin) chập nhận thanh toán</asp:ListItem>
                                        <asp:ListItem Value="8">(Admin) chập nhận Hoàn tiền</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddltrangthainguoimuahang" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddltrangthainguoimuahang_SelectedIndexChanged">
                                        <asp:ListItem Value="-1" Selected="True">Trạng Thái Người Mua Hàng</asp:ListItem>
                                        <asp:ListItem Value="1">Chấp nhận đơn hàng</asp:ListItem>
                                        <asp:ListItem Value="2">Trả lại hàng</asp:ListItem>
                                        <asp:ListItem Value="3">Chưa xử lý đơn hàng</asp:ListItem>
                                    </asp:DropDownList>
                                     <asp:TextBox ID="txtkeywordma" placeholder="Tìm theo mã đơn hàng" runat="server" CssClass="txt_csssearch"></asp:TextBox>
                                    <asp:TextBox ID="txtkeyword" placeholder="Tìm theo tên và thông tin khách hàng" runat="server" CssClass="txt_csssearch"></asp:TextBox>
                                    <asp:Button ID="btnshow" runat="server" Text="Hiển thị" OnClick="btnshow_Click" CssClass="vadd toolbar btn btn-info"></asp:Button>
                                    <asp:Button ID="btxoa" runat="server" OnClick="btxoa_Click" OnClientClick=" return confirmDelete(this);" Text="Xóa" ToolTip="Xóa những lựa chọn !" CssClass="vadd toolbar btn btn-info" />
                                    <asp:Button ID="lbtExport"  runat="server" OnClick="Export_Click" CssClass="vadd toolbar btn btn-info" Text="Export dữ liệu" ToolTip="Export dữ liệu" />
                                </div>
                            </div>
                        </div>
                        <div class="list_item">
                            <table cellspacing="0" style="border-collapse: collapse; margin-top: 18px" class="table table-striped table-bordered dataTable table-hover">
                                <tr height="40">
                                    <td class="header">
                                        <input id="chkAll" onclick="javascript: SelectAllCheckboxes(this, 1);" type="checkbox" /></td>
                                    <td class="header">Thông tin khách hàng</td>
                                    <td class="header" style="text-align: center;">Ngày gửi</td>
                                    <th style="width: 200px">Trạng thái Người Mua</th>
                                    <th style="width: 200px">Trạng thái Nhà CC</th>
                                     <th style="width: 200px">Trạng thái Admin</th>
                                    <td class="header" style="text-align: center;">Xem chi tiết</td>
                                    <td class="header">Xóa</td>
                                </tr>
                                <asp:Repeater ID="rp_items" runat="server" OnItemCommand="rp_items_ItemCommand">
                                    <ItemTemplate>
                                        <tr height="40">
                                            <td style="text-align: center;">
                                                <asp:CheckBox ID="chkid" runat="server" onclick="javascript:changeColor(this,'white');" /><asp:HiddenField ID="hiID" Value='<%# Eval("ID") %>' runat="server" />
                                            </td>
                                            <td align="left" style="padding-left: 10px; line-height: 22px; color: #646465" width="300px">
                                                <div style="color: red; font-weight: bold">Mã đơn hàng: #<%# Eval("ID") %> </div>
                                                Họ và tên:<span style="color: #444444; padding-left: 27px; font-weight: bold"><%#DataBinder.Eval(Container.DataItem, "Name")%></span><br />
                                                Địa chỉ:<span style="color: #444444; padding-left: 40px; font-weight: bold"><%#DataBinder.Eval(Container.DataItem, "Address")%></span><br />
                                                Điện thoại:<span style="color: #444444; padding-left: 22px; font-weight: bold"><%#DataBinder.Eval(Container.DataItem, "Phone")%></span><br />
                                                Email:<span style="color: #444444; padding-left: 15px; font-weight: bold"><%#DataBinder.Eval(Container.DataItem, "Email")%></span><br />
                                                Tổng số đơn hàng:<span style="color: #444444; padding-left: 15px; font-weight: bold"><%#TongDonHang(Eval("ID").ToString()) %></span><br />
                                            </td>
                                            <td style="text-align: center;">
                                                <%#MoreAll.MoreAll.FormatDate(DataBinder.Eval(Container.DataItem, "Create_Date").ToString())%>
                                            </td>
                                            <td style="text-align: center">
                                                <table class="priceView">
                                                    <tbody>
                                                        <tr>
                                                            <td align="left" width="50%">Chấp nhận</td>
                                                            <td>:</td>
                                                            <td align="right" width="45%"><%#ShowtrangthaiXL(Eval("ID").ToString(),"and TrangThaiNguoiMuaHang=1","0b98ea","0b98ea") %> </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">Trả lại</td>
                                                            <td>:</td>
                                                            <td align="right"><%#ShowtrangthaiXL(Eval("ID").ToString(),"and TrangThaiNguoiMuaHang=2","0b98ea","ed1c24") %> </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">Chưa xử lý</td>
                                                            <td>:</td>
                                                            <td align="right"><%#ShowtrangthaiXL(Eval("ID").ToString(),"and TrangThaiNguoiMuaHang=3","0b98ea","ed1c24") %> </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <%--<%#ShowtrangthaiNMH(Eval("ID").ToString()) %>--%>
                                            </td>
                                            <td style="text-align: center">

                                                 <table class="priceView">
                                                    <tbody>
                                                        <tr>
                                                            <td align="left" width="50%">Chấp nhận</td>
                                                            <td>:</td>
                                                            <td align="right" width="46%"><%#ShowtrangthaiXL(Eval("ID").ToString(),"and TrangThaiNhaCungCap=1","0b98ea","0b98ea") %> </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">Hủy Đơn</td>
                                                            <td>:</td>
                                                            <td align="right"><%#ShowtrangthaiXL(Eval("ID").ToString(),"and TrangThaiNhaCungCap=2","0b98ea","ed1c24") %> </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">Chưa xử lý</td>
                                                            <td>:</td>
                                                            <td align="right"><%#ShowtrangthaiXL(Eval("ID").ToString(),"and TrangThaiNhaCungCap=3","0b98ea","ed1c24") %> </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                               <%-- <%#ShowtrangthaiNCC(Eval("ID").ToString()) %>--%>
                                            </td>
                                            <td style="text-align: center">
                                                 <table class="priceView">
                                                    <tbody>
                                                        <tr>
                                                            <td align="left" width="60%">Khiếu kiện</td>
                                                            <td>:</td>
                                                            <td align="right" width="55%"><%#ShowtrangthaiXL(Eval("ID").ToString(),"and TrangThaiKhieuKien=1","0b98ea","ed1c24") %> </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">Chấp nhận Thanh Toán</td>
                                                            <td>:</td>
                                                            <td align="right"><%#ShowtrangthaiXL(Eval("ID").ToString(),"and TrangThaiKhieuKien=2","0b98ea","0b98ea") %> </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">Hoàn Tiền</td>
                                                            <td>:</td>
                                                            <td align="right"><%#ShowtrangthaiXL(Eval("ID").ToString(),"and TrangThaiKhieuKien=3","0b98ea","0b98ea") %> </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:LinkButton ID="LinkButton1" CommandName="Detail" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID")%>' runat="server"><img src="/Resources/admin/images/chitiet.png" border=0 /></asp:LinkButton>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:LinkButton CssClass="active action-link-button" ID="LinkButton2" OnLoad="Delete_Load" CommandName="Delete" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID")%>' runat="server"><i class="icon-trash"></i></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                        <table border="0" cellpadding="0" style="border-collapse: collapse" width="100%">
                            <tr height="20">
                                <td style="text-align: center;">
                                    <asp:Literal ID="ltpage" runat="server"></asp:Literal>
                                </td>
                        </table>
                    </div>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <div class="widget-title">
                        <h4><i class="icon-list-alt"></i>&nbsp;Gửi Email </h4>
                    </div>
                    <div class="widget-body">
                        <div class='frm-add'>
                            <table style="border-collapse: collapse" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="lblmsg" runat="server" Font-Bold="true" ForeColor="red"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>Tiêu đề</td>
                                    <td></td>
                                    <td>
                                        <asp:TextBox ID="txttitle" runat="server" Width="350px" CssClass="txt_css"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>Tên người nhận</td>
                                    <td></td>
                                    <td>
                                        <asp:TextBox ID="txttoname" runat="server" CssClass="txt_css" Width="350px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="height: 24px"></td>
                                    <td style="height: 24px">Email đến</td>
                                    <td style="height: 24px"></td>
                                    <td style="height: 24px">
                                        <asp:TextBox ID="txtTo" runat="server" CssClass="txt_css" Width="350px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>Nội dung<br />
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td colspan="3">
                                        <CKEditor:CKEditorControl ID="txtContent" runat="server" Toolbar="Basic"></CKEditor:CKEditorControl>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="padding-left: 0px; height: 0px;">
                            <br />
                            <asp:LinkButton ID="btnSend" runat="server" OnClick="btnSend_Click" CssClass="toolbar btn btn-info"> <i class="icon-ok"></i>Gửi mail</asp:LinkButton>
                            <asp:LinkButton ID="btncancel" runat="server" OnClick="btncancel_Click" CssClass="toolbar btn btn-info"> <i class="icon-chevron-left"></i>Hủy</asp:LinkButton>
                        </div>
                        <div style="clear: both;"></div>
                    </div>
                </asp:View>
            </asp:MultiView>
        </div>
    </div>
</div>
<asp:HiddenField ID="hdIDGiohang" runat="server" />
<div style="clear: both;"></div>
<style>
    .duyetdon {
        background: red;
        border: navajowhite;
        color: #fff;
        padding: 9px;
        font-weight: bold;
    }

    .thongbaos {
        font-size: 18px;
        background: #f3f200;
        padding: 2px;
        margin-bottom: 8px;
        width: 400px;
        height: 26px;
        padding-top: 6px;
        text-align: center;
        border-radius: 6px;
        color: red;
    }

    .priceView {
        width: 100%;
        font-size: 12px;
        margin: 0;
        background-color: #fff;
        border-top:1px solid #d7d7d7;
    }

        .priceView tbody td {
            vertical-align: middle;
            border: 1px solid #DDDAD6;
            padding: 6px;
        }
</style>

