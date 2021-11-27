<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Lich_su_mua_hang.ascx.cs" Inherits="VS.E_Commerce.cms.Display.QuanLyDangBai.Lich_su_mua_hang" %>
<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<div class="page-title m992">
    <h1 class="title-head margin-top-0"><a href="#">Lịch sử mua hàng</a></h1>
</div>
<div class="dataTables_filter" id="sample_1_filter" style="display:none">
    <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged" Width="144px">
        <asp:ListItem Value="-1" Selected="True">Tất cả các mục</asp:ListItem>
        <asp:ListItem Value="1">Đơn hàng đã duyệt</asp:ListItem>
        <asp:ListItem Value="0">Đơn hàng chưa duyệt</asp:ListItem>
        <asp:ListItem Value="2">Đơn hàng đang chờ xử lý</asp:ListItem>
        <asp:ListItem Value="3">Đơn hàng đang vận chuyển</asp:ListItem>
    </asp:DropDownList>
    <asp:TextBox ID="txtkeyword" runat="server" CssClass="form-control" ></asp:TextBox>

    <asp:Button ID="btnshow" runat="server" Text="Hiển thị" OnClick="btnshow_Click" CssClass="vadd toolbar btn btn-info"></asp:Button>
</div>

 <asp:Button ID="Export" runat="server" OnClick="Export_Click" CssClass="vadd toolbar btn btn-info" Text="Export dữ liệu" ToolTip="Export dữ liệu" />
<div class="my-account">
    <div class="dashboard">
        <div class="recent-orders">
            <div class="table-responsive tab-all" style="overflow-x: auto;">
                  <table class="table table-striped table-bordered table-advance table-hover table table-cart lichsumuahang">
                    <thead class="thead-default">
                        <tr>
                            <th>Tên người mua</th>
                            <th>Ngày</th>
                              <th style="width: 200px">Trạng thái Người Mua</th>
                                    <th style="width: 200px">Trạng thái Nhà CC</th>
                                     <th style="width: 200px">Trạng thái Khiếu kiện</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rp_items" runat="server">
                            <ItemTemplate>
                                <tr class="first odd">
                                     <td style="text-align: left">
                                        <a style="color: red; font-weight: bold" title="Click vào để xem chi tiết đơn hàng" href="/account/orders/<%#Eval("ID") %>">Mã đơn hàng:#<%#Eval("ID") %></a><br />
                                        Họ và tên:<span style="color: #444444; padding-left: 27px; font-weight: bold"><%#DataBinder.Eval(Container.DataItem, "Name")%></span><br />
                                        Địa chỉ:<span style="color: #444444; padding-left: 40px; font-weight: bold"><%#DataBinder.Eval(Container.DataItem, "Address")%></span><br />
                                        Điện thoại:<span style="color: #444444; padding-left: 22px; font-weight: bold"><%#DataBinder.Eval(Container.DataItem, "Phone")%></span><br />
                                        Email:<span style="color: #444444; padding-left: 15px; font-weight: bold"><%#DataBinder.Eval(Container.DataItem, "Email")%></span><br />
                                        Tổng số sản phẩm:<span style="color: #444444; padding-left: 15px; font-weight: bold">  <%#TongDonHang(Eval("ID").ToString()) %></span><br />
                                    </td>
                                    <td style="text-align: center"><%#MoreAll.MoreAll.FormatDate(DataBinder.Eval(Container.DataItem, "Create_Date").ToString())%></td>
                                     <td style="text-align: center">
                                                <table class="priceView">
                                                    <tbody>
                                                        <tr>
                                                            <td align="left" width="65%">Chấp nhận</td>
                                                            <td>:</td>
                                                            <td align="right" width="45%"><%#ShowtrangthaiXL(Eval("ID").ToString(),"and TrangThaiNguoiMuaHang=1","0b98ea","0b98ea") %></td>
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
                                            </td>
                                            <td style="text-align: center">
                                                 <table class="priceView">
                                                    <tbody>
                                                        <tr>
                                                            <td align="left" width="65%">Chấp nhận</td>
                                                            <td>:</td>
                                                            <td align="right" width="45%"><%#ShowtrangthaiXL(Eval("ID").ToString(),"and TrangThaiNhaCungCap=1","0b98ea","ed1c24") %> </td>
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
                                            </td>
                                            <td style="text-align: center">
                                                 <table class="priceView">
                                                    <tbody>
                                                        <tr>
                                                            <td align="left" width="60%">Khiếu kiện</td>
                                                            <td>:</td>
                                                            <td align="right" width="35%"><%#ShowtrangthaiXL(Eval("ID").ToString(),"and TrangThaiKhieuKien=1","0b98ea","ed1c24") %> </td>
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
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
            <div class="text-xs-right">
            </div>
            <div class="phantrang" style="">
                <cc1:CollectionPager ID="CollectionPager1" runat="server" BackNextDisplay="HyperLinks" BackNextLocation="Split"
                    BackText="<<" ShowFirstLast="True" ResultsLocation="Bottom" PagingMode="PostBack" MaxPages="50" FirstText="Trang đầu" HideOnSinglePage="True" IgnoreQueryString="False" LabelStyle="font-weight: bold;color:red" LabelText="" LastText="Cuối cùng" NextText=">>" PageNumbersDisplay="Numbers"
                    ResultsFormat="Hiển thị từ  {0} Đến {1} (của {2})" ResultsStyle="padding-bottom:5px;padding-top:14px;font-weight: bold;" ShowLabel="False" ShowPageNumbers="True" BackNextStyle="font-weight: bold; margin: 14px;" ControlCssClass="" ControlStyle="" UseSlider="True" PageNumbersSeparator="">
                </cc1:CollectionPager>
            </div>
        </div>
    </div>

</div>
<asp:HiddenField ID="hdid" runat="server" />
