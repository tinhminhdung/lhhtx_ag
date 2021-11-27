<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetailCart.ascx.cs" Inherits="VS.E_Commerce.cms.Admin.Products.DetailCart" %>
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

            <div id="cph_Main_ContentPane">
                <div class="widget">
                    <div class="widget-title">
                        <h4><i class="icon-reorder"></i>&nbsp;Thông tin giỏ hàng</h4>
                        <div class="ui-widget-content ui-corner-top ui-corner-bottom">
                            <div id="toolbox">
                                <div style="float: right;" class="toolbox-content">
                                    <table class="toolbar">
                                        <tbody>
                                            <tr>
                                                <td align="center">
                                                    <a id="" class="toolbar btn btn-info" href="javascript:{}" onclick="window.print()"><i class="fa fa-print"></i>Print </a></td>
                                                <td align="center">
                                                    <a id="" class="toolbar btn btn-info" href="/admin.aspx?u=pro&su=carts"><i class="icon-chevron-left"></i>Quay lại</a></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="widget-body">
                        <table style="width: 100%;">
                            <tbody>
                                <tr>
                                    <td style="text-align: right; padding-right: 15px">
                                        <span id="" class="lbleft">Mã đơn hàng:</span>
                                    </td>
                                    <td style="font-weight: bold">
                                        <b style="color: red">#<asp:Literal ID="ltmadonhang" runat="server"></asp:Literal></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 18%; text-align: right; padding-right: 15px">
                                        <span id="" class="lbleft">Tên khách hàng:</span>
                                    </td>
                                    <td style="font-weight: bold">
                                        <asp:Literal ID="ltname" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; padding-right: 15px">
                                        <span id="" class="lbleft">Địa chỉ:</span>
                                    </td>
                                    <td style="font-weight: bold">
                                        <asp:Literal ID="ltdiachi" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; padding-right: 15px">
                                        <span id="" class="lbleft">Điện thoại:</span>
                                    </td>
                                    <td style="font-weight: bold">
                                        <asp:Literal ID="ltdienthoai" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; padding-right: 15px">
                                        <span id="" class="lbleft">Email:</span>
                                    </td>
                                    <td style="font-weight: bold">
                                        <asp:Literal ID="ltemail" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td style="text-align: right; padding-right: 15px">
                                        <span id="" class="lbleft">Hình thức thanh toán:</span>
                                    </td>
                                    <td style="font-weight: bold">
                                        <asp:Literal ID="ltthanhtoan" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td style="text-align: right; padding-right: 15px">
                                        <span id="" class="lbleft">Phương thức giao hàng:</span>
                                    </td>
                                    <td style="font-weight: bold">
                                        <asp:Literal ID="lthinhthucgiaohang" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; padding-right: 15px">
                                        <span id="" class="lbleft">Nội dung yêu cầu:</span>
                                    </td>
                                    <td style="font-weight: bold">
                                        <asp:Literal ID="ltghichu" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; padding-right: 15px">
                                        <span id="" class="lbleft" style="text-transform: uppercase; color: red; font-size: 12px">Tổng số tiền:</span>
                                    </td>
                                    <td style="font-weight: bold; color: red">
                                        <asp:HiddenField ID="hdtongtien" runat="server" />
                                        <asp:Literal ID="lttong" runat="server"></asp:Literal>
                                        <%=Commond.Setting("Dongiapro") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; padding-right: 15px">
                                        <span id="" class="lbleft" style="text-transform: uppercase; color: red; font-size: 12px">Tổng số điểm phải thanh toán:</span>
                                    </td>
                                    <td style="font-weight: bold; color: red">
                                        <asp:Literal ID="lttongdiem" runat="server"></asp:Literal>
                                        điểm
                                    </td>
                                </tr>
 <tr style=" display:none">
                                    <td style="text-align: right; padding-right: 15px">
                                        <span id="" class="lbleft" style="text-transform: uppercase; color: red; font-size: 12px">Tích lũy được :</span>
                                    </td>
                                    <td style="font-weight: bold; color: red">
                                        <asp:Literal ID="ltCoin" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; padding-right: 15px">
                                        <span id="" class="lbleft" style="text-transform: uppercase; color: red; font-size: 12px">Tổng số tiền bằng chữ:</span>
                                    </td>
                                    <td style="font-weight: bold; color: red">
                                        <asp:Literal ID="ltvietchu" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr style=" display:none">
                                    <td style="text-align: right; padding-right: 15px">
                                        <span id="" class="lbleft" style="text-transform: uppercase; color: red; font-size: 12px">Duyệt đơn hàng:</span>
                                    </td>
                                    <td style="font-weight: bold; color: red">
                                        <asp:HiddenField ID="hdid" runat="server" />
                                        <asp:Label ID="ltmess" BorderColor="Red" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                        <div style="width: 1295px;overflow-x: scroll;">
                        <table cellspacing="0" style="border-collapse: collapse; margin-top: 18px;width: 1700px;" class="table table-striped table-bordered dataTable table-hover">
                            <tbody>
                                <tr class="trHeader" style="height: 40px">
                                    <td style="width: 6px" class="contentadmin">STT</td>
                                    <td style="width: 100px; text-align: center !important" class="contentadmin">Ảnh</td>
                                    <td style="width:200px; text-align: center !important" class="contentadmin">Tên sản phẩm</td>
                                    <td class="contentadmin" style="width: 10%; text-align: center !important">Nhà cung cấp</td>
                                      <td style="width:220px; text-align: center !important" class="contentadmin">Trạng thái<br /> Nhà CC</td>
                                       <td style="width: 220px; text-align: center !important" class="contentadmin">Trạng thái<br /> Người Mua</td>
                                       <td style="width: 220px; text-align: center !important" class="contentadmin">Trạng thái<br /> Khiếu kiện</td>
                                    <td style="width: 100px; text-align: center !important" class="contentadmin">Giá đại lý</td>
                                    <td style="width: 100px; text-align: center !important" class="contentadmin">Giá 
                                                <br />
                                        công ty nhập vào</td>
                                    <td style="width: 100px; text-align: center !important" class="contentadmin">Số lượng</td>
                                    <td style="width: 100px; text-align: center !important" class="contentadmin">Số tiền
                                                <br />
                                        thành viên thanh toán</td>
                                    <td style="width: 100px; text-align: center !important" class="contentadmin">Số tiền
                                                <br />
                                        Nhà cung cấp sẽ nhận</td>
                                    <td style="width: 100px; text-align: center !important" class="contentadmin">Điểm
                                                <br />
                                        thanh toán</td>
                                    <td style="width:100px; text-align: center !important" class="contentadmin">Điểm thưởng</td>
                                     
                                </tr>
                                <asp:Repeater ID="rpcartdetail" runat="server" OnItemCommand="rpcartdetail_ItemCommand">
                                    <ItemTemplate>
                                        <tr style="height: 20px" class="tr_while" onmouseover="this.className='tr_while_over'" onmouseout="this.className='tr_while'">
                                            <td style="padding-left: 5px; text-align: center !important" class="cartadmin"><%=i++ %><asp:HiddenField ID="hdids" Value='<%# Eval("ID") %>' runat="server" /></td></td>
                                            <td style="padding-left: 5px; text-align: center !important" class="cartadmin">
                                                <img src="<%#Anh(Eval("ipid").ToString())%>" style="width: 50px; height: 50px;" /></td>
                                            <td style="padding-left: 5px; text-align: left !important" class="cartadmin">Mã sp: <%#Codes(Eval("ipid").ToString())%>
                                                <br />
                                                <%#DataBinder.Eval(Container.DataItem,"Name")%><br />
                                            </td>
                                            <td style="padding-left: 5px; text-align: left !important" class="cartadmin">
                                                <%#ShowtThanhVien(DataBinder.Eval(Container.DataItem,"IDNhaCungCap").ToString())%><br />
                                                <b style="color:#0093dc"> <%#SentMail(DataBinder.Eval(Container.DataItem,"SentMail").ToString())%></b>
                                            </td>
                                           <td style="padding-left: 5px; text-align: center !important" class="cartadmin">
                                                <%#ShowtrangthaiNCC(DataBinder.Eval(Container.DataItem, "TrangThaiNhaCungCap").ToString(),DataBinder.Eval(Container.DataItem, "LyDoHuyHang").ToString())%>
                                            </td>
                                            <td style="padding-left: 5px; text-align: center !important" class="cartadmin">
                                                       <div class="TTDuyetdon" style='<%#NhaCungCapDaDuyet(Eval("TrangThaiNhaCungCap").ToString())%>'>
                                                    <asp:LinkButton Visible='<%#EnableLock_DuyetHang(Eval("TrangThaiNguoiMuaHang").ToString())%>' ID="LinkButton1" OnLoad="ChapNhan_Load" CommandName="ChapNhan" CssClass="Duyetdon" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID")%>' runat="server">Chấp nhận</asp:LinkButton>
                                                    <asp:LinkButton Visible='<%#EnableLock_DuyetHang(Eval("TrangThaiNguoiMuaHang").ToString())%>' ID="LinkButton2" OnLoad="TraHang_Load" CommandName="TraHang" CssClass="huydon" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID")%>' runat="server">Trả hàng</asp:LinkButton>
                                                </div>
                                                <%#ShowtrangthaiNMH(DataBinder.Eval(Container.DataItem, "TrangThaiNguoiMuaHang").ToString())%><br />
                                                <asp:TextBox ID="txtlydotrahang" Visible='<%#EnableLock_HuyDonHang(Eval("TrangThaiNguoiMuaHang").ToString())%>' Text='<%#DataBinder.Eval(Container.DataItem, "LyDoTraHang")%>' placeholder="Ghi rõ lý do Trả lại hàng" Style="border: 1px solid #a9a9a9; border-radius: 3px; text-align: left; width: 200px; height: 65px; margin-top: 10px;" TextMode="MultiLine" CssClass="txt_css" Width="40px" runat="server" AutoPostBack="true" OnTextChanged="txtLyDoTraHang_TextChanged"></asp:TextBox>
                                            </td>

                                               <td style="padding-left: 5px; text-align: center !important" class="cartadmin">
                                                <div class="TTDuyetdon">
                                                    <asp:LinkButton Visible='<%#EnableLock_KhieuKien(Eval("TrangThaiKhieuKien").ToString())%>' ID="LinkButton3" OnLoad="KhieuKien_Load" CommandName="ChapNhanThanhToanTien" CssClass="Duyetdon" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID")%>' ToolTip="Chấp nhận thanh toán tiền cho nhà cung cấp" runat="server">Chấp nhận TT</asp:LinkButton>
                                                    <asp:LinkButton Visible='<%#EnableLock_KhieuKien(Eval("TrangThaiKhieuKien").ToString())%>' ID="LinkButton4" OnLoad="HoanTienMua_Load" CommandName="ChapNhanHoanTien" CssClass="huydon" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID")%>' ToolTip="Hoàn tiền cho người mua" runat="server">Hoàn tiền</asp:LinkButton>
                                                </div>
                                                <%#ShowtrangthaiKhieuKien(DataBinder.Eval(Container.DataItem, "TrangThaiKhieuKien").ToString())%><br />
                                            </td>


                                             <td style="padding-left: 5px; text-align: center !important" class="cartadmin"><%#AllQuery.MorePro.Detail_Price(Eval("Price").ToString())%> <%=Commond.Setting("Dongiapro") %> 
                                            </td>
                                            <td style="padding-left: 5px; text-align: center !important" class="cartadmin"><%#GiaNhap1(Eval("ipid").ToString())%> <%=Commond.Setting("Dongiapro") %><br />
                                            </td>
                                            <td style="padding-left: 5px; text-align: center !important" class="cartadmin"><%#DataBinder.Eval(Container.DataItem,"Quantity")%></td>
                                            <td style="padding-left: 5px; text-align: center !important" class="cartadmin"><%#AllQuery.MorePro.Detail_Price(Eval("Money").ToString())%> <%=Commond.Setting("Dongiapro") %></td>
                                            <td style="padding-left: 5px; text-align: center !important" class="cartadmin"><%#GiaNhap(Eval("ipid").ToString(),Eval("Quantity").ToString())%> <%=Commond.Setting("Dongiapro") %><br />
                                            </td>
                                            <td style="padding-left: 5px; text-align: center !important" class="cartadmin"><%#FormatMoneyDiemMuaHang(Eval("Money").ToString())%> <br />
                                                   <%#LayTuViNao(DataBinder.Eval(Container.DataItem, "TrangThaiNhaCungCap").ToString(),DataBinder.Eval(Container.DataItem, "TrangThaiNguoiMuaHang").ToString(),DataBinder.Eval(Container.DataItem, "TrangThaiKhieuKien").ToString(),DataBinder.Eval(Container.DataItem, "TienTuViNao").ToString())%><br />
                                            </td>
                                            <td style="padding-left: 5px; text-align: center !important" class="cartadmin"><%#HoaHongTheoLevel_TheoThoiDiemMuahang(Eval("ID_Cart").ToString(),Eval("ID").ToString(),Eval("Diemcoin").ToString(),Eval("IDThanhVien").ToString())%> /<%#(Eval("Diemcoin").ToString())%></td>
                                        </tr>
                                    </ItemTemplate>
                                    <%--   <AlternatingItemTemplate>
                                                <tr style="height: 20px; background: #fafafa" class="tr_while" onmouseover="this.className='tr_while_over'" onmouseout="this.className='tr_while'">
                                                  
                                             </tr>
                                            </AlternatingItemTemplate>--%>
                                </asp:Repeater>
                            </tbody>
                        </table>
                            </div>

                         <div style="clear: both; height: 20px"></div>
            <div>
                <asp:Literal ID="ltthongtin" runat="server"></asp:Literal>
            </div>
            <div style="clear: both; height: 20px"></div>
                        <br />
                        <br />
                        <br />
                    </div>
                </div>
            </div>
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

    .thongbaosv {
        font-size: 16px;
        background: #f3f200;
        padding: 2px;
        margin-bottom: 8px;
        width: 900px;
        padding-top: 6px;
        text-align: center;
        border-radius: 6px;
        color: red;
        margin-top: 5px;
    }
</style>

<asp:HiddenField ID="hdvalue" Value="0" runat="server" />
<asp:HiddenField ID="hdHoaHongGioiThieuF" Value="0" runat="server" />

