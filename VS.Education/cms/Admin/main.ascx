<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="main.ascx.cs" Inherits="VS.E_Commerce.cms.Admin.main" %>

<div id="container" class="row-fluid">
    <div class="boder_menu">
        <div id="menu">
            <ul>
                <li class="content1" id="Quantri" runat="server">
                    <a class="TopMenuItem" href="javascript:;">
                        <span class="MenuText"><i class="icon-cogs"></i>Quản trị</span>
                    </a>
                    <ul>
                        <li id="set" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=set"><span class="SubMenuText">Cấu hình</span></a>
                        </li>
                        <li>
                            <a class="LinkButton" href="/admin.aspx?u=301"><span class="SubMenuText">Chuyển trang 301 </span></a>
                        </li>
                        <li id="Marketing" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=Marketing&su=MarketingSenmail"><span class="SubMenuText">Gửi thông báo đến thành viên</span></a>
                        </li>
                        <li id="Contacts" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=Contacts"><span class="SubMenuText">Liên hệ, phản hồi</span></a>
                        </li>
                    </ul>
                </li>
                <li class="content2" id="User" runat="server">
                    <a class="TopMenuItem" href="javascript:;">
                        <span class="MenuText"><i class="icon-user"></i>Thành viên</span>
                    </a>
                    <ul>
                        <li id="AdminUser" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=AdminUser"><span class="SubMenuText">Thành viên quản trị</span></a>
                        </li>
                        <li id="lnkthanhvien" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=Thanhvien"><span class="SubMenuText">Thành viên đăng ký</span></a>
                        </li>
                        <li runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=LSchuyendiem"><span class="SubMenuText">Danh sách chuyển điểm</span></a>
                        </li>
                        <li runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=LScapdiem"><span class="SubMenuText">Danh sách cấp điểm</span></a>
                        </li>
                        <li runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=checkIP"><span class="SubMenuText">Check IP đăng nhập</span></a>
                        </li>
                        <li runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=DSMuaHangTheoThang"><span class="SubMenuText">DS kích hoạt THƯỞNG MUA HÀNG</span></a>
                        </li>

                        <li id="Level" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=Level"><span class="SubMenuText">Danh sách Level</span></a>
                        </li>
                        <li id="SettingHoaHong" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=SettingHoaHong"><span class="SubMenuText">Cấu hình % hoa hồng</span></a>
                        </li>
                        <li id="DaiLy" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=DaiLy"><span class="SubMenuText">Danh sách Chi nhánh</span></a>
                        </li>
                        <li id="HoaHong" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=HoaHong"><span class="SubMenuText">Danh sách Hoa hồng</span></a>
                        </li>
                        <li id="LoiNhuanChechLechGia" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=LoiNhuanChechLechGia"><span class="SubMenuText">D/S Lợi nhuận chênh lệch giá</span></a>
                        </li>
                        <li id="LoiNhuan" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=LoiNhuan"><span class="SubMenuText">Danh sách Lợi nhuận Mua Bán</span></a>
                        </li>
                        <li id="LoiNhuanDangKy" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=LoiNhuanDangKy"><span class="SubMenuText">Danh sách Lợi nhuận đăng ký</span></a>
                        </li>
                        <li id="LoiNhuanQRCode" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=LoiNhuanQRCode"><span class="SubMenuText">Danh sách Lợi nhuận QRcode</span></a>
                        </li>
                        <li id="LoiNhuanNCC" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=LoiNhuanNCC"><span class="SubMenuText">Danh sách Lợi nhuận NCC </span></a>
                        </li>
                        <li id="Li2" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=MThue"><span class="SubMenuText">Danh sách trừ thuế</span></a>
                        </li>
                        <li id="Li4" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=LichSuLevel"><span class="SubMenuText">Danh sách lên sao</span></a>
                        </li>
                        <li id="Li1" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=ViTamMuaHang"><span class="SubMenuText">Danh sách ví tạm giữ mua bán </span></a>
                        </li>
                        <li id="HoaHongAGLANGD" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=HoaHongAGLANGD"><span class="SubMenuText">Danh sách HH AG LAND</span></a>
                        </li>
                        <li id="ServiceAGLANGD" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=ServiceAGLANGD"><span class="SubMenuText">Danh sách Service AG LAND</span></a>
                        </li>

                        <li id="LaiSuatTheoAgLand" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=LaiSuatTheoAgLand"><span class="SubMenuText">Danh sách 10% theo AG LAND</span></a>
                        </li>

                        <li id="MLichSuRutTien" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=MLichSuRutTien"><span class="SubMenuText">Danh sách rút tiền</span></a>
                        </li>
                        <li id="MLichSuMuaDiem" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=MLichSuMuaDiem"><span class="SubMenuText">Danh sách mua điểm</span></a>
                        </li>
                        <li id="LichSuQRcode" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=LichSuQRcode"><span class="SubMenuText">Lịch sử cấu hình QRCode</span></a>
                        </li>
                        <li id="LichSuThanhToanQRCode" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=LichSuThanhToanQRCode"><span class="SubMenuText">Lịch sử thanh toán QRCode</span></a>
                        </li>
                        <li id="LichsuDuyetTamGiu" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=LichsuDuyetTamGiu"><span class="SubMenuText">Lịch sử duyệt sau khi tạm giữ</span></a>
                        </li>
                        <li id="Li3" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=chuyengia"><span class="SubMenuText">Lịch sử ví chuyên gia</span></a>
                        </li>
                        <li id="LichSuDG" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=LichSuDG"><span class="SubMenuText">Lịch sử giao dịch</span></a>
                        </li>


                    </ul>
                </li>

                <li class="content2" id="Li5" runat="server">
                    <a class="TopMenuItem" href="javascript:;">
                        <span class="MenuText"><i class="icon-user"></i>Bất động sản</span>
                    </a>
                    <ul>
                        <li id="Li6" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=CauHinhBDS"><span class="SubMenuText">Cấu hình hoa hồng</span></a>
                        </li>
                         <li id="Li7" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=LichSuLoiNhuanBDS"><span class="SubMenuText">Lợi nhuận HH</span></a>
                        </li>
                         <li id="Li8" runat="server">
                            <a class="LinkButton" href="/admin.aspx?u=ChiaHHBDS"><span class="SubMenuText">Chia Hoa hồng</span></a>
                        </li>
                    </ul>
                </li>

                <li class="content3" id="Danhmuc" runat="server">
                    <a class="TopMenuItem" href="javascript:;">
                        <span class="MenuText"><i class="icon-th-large"></i>Danh mục</span>
                    </a>
                    <ul>
                        <li id="Page" runat="server"><a class="LinkButton" href="/admin.aspx?u=Page"><span class="SubMenuText">Quản trị menu</span></a></li>
                        <li id="Advertisings" runat="server"><a class="LinkButton" href="/admin.aspx?u=Advertisings"><span class="SubMenuText">Quản trị quảng cáo</span></a></li>

                        <li><a class="LinkButton" href="/admin.aspx?u=Advertisings&su=DMAdvertising"><span class="SubMenuText">Quảng cáo theo nhóm sản phẩm</span></a></li>
                        <%-- <li><a  class="LinkButton" href="/admin.aspx?u=Advertisings&su=DMAdvertisingNews"><span class="SubMenuText">Quảng cáo theo nhóm tin</span></a></li>
                        --%>
                        <%-- <li id="Tienich" runat="server"><a class="LinkButton" href="/admin.aspx?u=Tienich"><span class="SubMenuText">Tư vấn online</span></a></li>--%>
                        <%-- <li id="faq" runat="server"><a class="LinkButton" href="/admin.aspx?u=faq"><span class="SubMenuText">Hỏi đáp</span></a></li>
                        <li><a  class="LinkButton" href="/admin.aspx?u=info"><span class="SubMenuText">thông tin chân trang</span></a></li>
                        <li><a  class="LinkButton" href="/admin.aspx?u=Gioithieu"><span class="SubMenuText">Gioithieu</span></a></li>
                        <li><a  class="LinkButton" href="/admin.aspx?u=Dichvu"><span class="SubMenuText">Dichvu</span></a></li>
                        <li><a  class="LinkButton" href="/admin.aspx?u=Download&su=Download"><span class="SubMenuText">Thư viện tải file</span></a></li>
                        --%>
                    </ul>
                </li>
                <li class="content4" id="News" runat="server">
                    <a class="TopMenuItem" href="javascript:;">
                        <span class="MenuText"><i class="icon-globe"></i>Tin tức</span>
                    </a>
                    <ul>
                        <li><a class='linkmainmenu' href='?u=news&su=news'>Danh mục tin tức</a></li>
                        <li><a class='linkmainmenu' href='?u=news&su=Tintuc'>Danh sách tin</a></li>
                        <li><a class='linkmainmenu' href='?u=news&su=nset'>Cấu hình</a></li>
                    </ul>
                </li>
                <li class="content5" id="Products" runat="server">
                    <a class="TopMenuItem" href="javascript:;">
                        <span class="MenuText"><i class="icon-tags"></i>Sản phẩm</span>
                    </a>
                    <ul>
                        <li><a class='linkmainmenu' href='?u=pro&su=pro'>Danh mục sản phẩm</a></li>
                        <%-- <li><a class='linkmainmenu'  href='?u=pro&su=Manufacturer'>Thương hiệu</a></li>--%>
                        <li><a class='linkmainmenu' href='?u=pro&su=ManagementPrice'>Khoảng giá</a></li>
                        <%--                  
                        
                        <li><a class='linkmainmenu' href='?u=pro&su=Size'>Kích thước</a></li>
                        <li><a class='linkmainmenu'  href='?u=pro&su=Color'>Mầu sắc</a></li>--%>
                        <li><a class='linkmainmenu' href='?u=pro&su=items'>Danh sách sản phẩm</a></li>
                        <li><a class='linkmainmenu' href='?u=pro&su=set'>Cấu hình</a></li>
                    </ul>
                </li>
                <%-- <li class="content6" id="Album" runat="server">
                    <a class="TopMenuItem" href="javascript:;">
                        <span class="MenuText"><i class="icon-picture"></i>Thư viện</span>
                    </a>
                    <ul>
                        <li><a class='linkmainmenu' href='?u=Album&su=Album'>Danh mục thư viện</a></li>
                        <li><a class='linkmainmenu' href='?u=Album&su=items'>Danh sách thư viện</a></li>
                        <li><a class='linkmainmenu' href='?u=Album&su=set'>Cấu hình</a></li>
                    </ul>
                </li>
                <li class="content7" id="Video" runat="server">
                    <a class="TopMenuItem" href="javascript:;">
                        <span class="MenuText"><i class="fa fa-youtube-play"></i>Thư viện video</span>
                    </a>
                    <ul>
                        <li><a class='linkmainmenu' href='?u=Video&su=Video'>Danh mục video</a></li>
                        <li><a class='linkmainmenu' href='?u=Video&su=items'>Danh sách video</a></li>
                        <li><a class='linkmainmenu' href='?u=Video&su=set'>Cấu hình</a></li>
                    </ul>
                </li>--%>

                <li class="content6" id="Album" runat="server">
                    <a class="TopMenuItem" href="javascript:;">
                        <span class="MenuText"><i class="icon-picture"></i>Thống kê</span>
                    </a>
                    <ul>
                        <%-- <li><a class='linkmainmenu' href='?u=ThongKe'>Thống kê thành viên</a></li>--%>
                        <li><a class='linkmainmenu' href='?u=MThongKeThanhVien'>Thống kê thành viên </a></li>
                        <li><a class='linkmainmenu' href="/admin.aspx?u=Thongke"><span class="SubMenuText">Thống kê</span></a></li>
                    </ul>
                </li>

                <li class="ctgiohang" id="carts" runat="server">
                    <a class="TopMenuItem" href="javascript:;">
                        <span class="MenuText"><i class="icon-shopping-cart"></i>Giỏ hàng</span>
                    </a>
                    <ul style="width: 300px">
                        <li><a class='linkmainmenu' href='?u=carts'>Quản lý đơn đặt hàng - Duyệt từng đơn</a></li>
                        <li><a class='linkmainmenu' href='?u=cartsNhanh'>Quản lý đơn đặt hàng  - Duyệt Nhanh</a></li>
                    </ul>
                </li>
                <li class="giohangs" id="giohang" runat="server"><span title="Số đơn hàng bị khiếu kiện" class="badges"><%=TCarts() %></span></li>
            </ul>
        </div>
    </div>
</div>
<div id="main">
    <div id="main-content">
        <div class="container-fluid">
            <div style="width: 100%; margin: 0 auto;">
                <asp:PlaceHolder ID="phcontrol" runat="server"></asp:PlaceHolder>
            </div>
        </div>
    </div>
</div>

