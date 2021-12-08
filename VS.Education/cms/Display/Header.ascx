<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="VS.E_Commerce.cms.Display.Header" %>
<%@ Register Src="Products/searchbox.ascx" TagName="searchbox" TagPrefix="uc1" %>
<%@ Register Src="~/cms/Display/Page/MenuTop.ascx" TagPrefix="uc1" TagName="MenuTop" %>
<%@ Register Src="~/cms/Display/AllPage/Box_search.ascx" TagPrefix="uc1" TagName="Box_search" %>
<%@ Register Src="~/cms/Display/Page/Menuleft.ascx" TagPrefix="uc1" TagName="Menuleft" %>
<%@ Register Src="~/cms/Display/AllPage/Box_search_header.ascx" TagPrefix="uc1" TagName="Box_search_header" %>
<%@ Register Src="~/cms/Display/Page/MenuMobile.ascx" TagPrefix="uc1" TagName="MenuMobile" %>
<div id="mySidenav" class="sidenav">
    <a href="javascript:void(0)" class="closebtn" onclick="closeNav()">&times;</a>
    <div class="Danhmucmenu">Danh mục menu  <a href="javascript:void(0)" class="closebtn" onclick="closeNav()">&times;</a></div>
    <div class="Mobile">
        <nav>
            <ul data-breakpoint="1025" class="flexnav with-js opacity sm-screen flexnav-show">
                <uc1:MenuMobile runat="server" ID="MenuMobile" />
            </ul>
        </nav>
    </div>
</div>
<div class="BannnerTop"><%=Advertisings.Ad_vertisings.Advertisings_A_Images("14") %></div>

<div class="topbar Mobile">
    <div class="container">
        <div class="row">
            <div class="col-md-6 col-sm-6 d-list col-xs-12 a-right topbar_right">
                <div class="list-inline acenter">
                    <asp:Panel ID="Panel4" CssClass="dangnhaaa Mobile" runat="server">
                        <ul>
                            <li>
                                <i class="fa fa-user"></i> 
                                <a href="/dang-ky.html" title="Đăng ký" class="account_a">
                                    <span>Đăng ký</span>
                                </a>
                            </li>
                            <li>
                                <i class="fa fa-lock"></i> 
                                <a href="/dang-nhap.html" title="Đăng nhập" class="account_a">
                                    <span>Đăng nhập</span>
                                </a>
                            </li>
                            <li>
                                <i class="fa fa-map-marker"></i> 
                                <a href="/lien-he.html" title="liên hệ" class="account_a">
                                    <span>Liên hệ</span>
                                </a>
                            </li>
                        </ul>
                    </asp:Panel>

                    <asp:Panel ID="Panel5" CssClass="thanhviemobile Mobile" runat="server">
                        <ul>
                            <asp:Literal ID="ltaglang" runat="server"></asp:Literal>
                            <li>
                                <i class="fa fa-user"></i> 
                                <a onclick="Showthanhvien()" class="account_a">
                                    <asp:Literal ID="ltxinchao" runat="server"></asp:Literal>
                                </a>
                            </li>
                            <li>
                                <i class="fa fa-lock"></i> 
                                <asp:LinkButton ID="LinkButton3" CssClass="account_a" runat="server" OnClick="lnkthoat_Click"><span>Thoát</span></asp:LinkButton>
                            </li>
                            <li class="dropdown">
                                <%--<div onclick="Showthanhvien()" class="sdropbtn">
                                <asp:Literal ID="ltlavata2" runat="server"></asp:Literal>
                            </div>--%>
                                <div id="myDropdown" class="dropdown-content" style="display: none">
                                    <div><a href="/vi-tien.html">Ví điểm của bạn</a></div>
                                    <div>
                                        <asp:Literal ID="lttinnhan1" runat="server"></asp:Literal>
                                    </div>
                                    <asp:Literal ID="ltchuyengia" runat="server"></asp:Literal>
                                    <div  style=" display:none"><a href="/lich-su-tru-thue.html">LS thu nhập cá nhân</a></div>
                                     <div><a href="/tich-diem-hoa-hong-mua.html">Danh sách hoa hồng</a></div>
                                    <div><a href="/dau-tu.html">Đầu tư</a></div>
                                    <div><a href="/lich-su-dau-tu.html">Lịch sử đầu tư</a></div>

                                    <div><a href="/rut-tien.html">Rút điểm</a></div>
                                    <div><a href="/lich-su-rut-tien.html">Lịch sử rút điểm</a></div>
                                    <div style=" display:none" ><a href="/mua-diem.html">Mua điểm</a></div>
                                    <div><a href="/lich-su-mua-diem.html">Lịch sử mua điểm</a></div>
                                    <asp:Panel ID="Panel6" Visible="false" runat="server">
                                        <div><a href="/quan-ly-san-pham.html">Quản lý sản phẩm</a></div>
                                        <div style=" display:none"><a href="/danh-sach-don-ban-hang.html">Quản lý đơn bán hàng (<asp:Literal ID="ltquanly" runat="server"></asp:Literal>)</a></div>
                                    </asp:Panel>
                                    <div><a href="/lich-su-mua-hang.html">Lịch sử mua hàng (<asp:Literal ID="lichsumuahang" runat="server"></asp:Literal>)</a></div>
                                    <div>
                                        <asp:Literal ID="ltdanhsachthanhvien1" runat="server"></asp:Literal>
                                    </div>
                                    <%--  <div><a href="/Danh-sach-thanh-vien.html">Danh sách thành viên</a></div>--%>
                                    <%--           <div><a href="/tich-diem-hoa-hong-gioi-thieu.html">Hoa hồng Quản Lý</a></div>--%>
                                   
                                    <%--    <div><a href="/tich-diem-hoa-hong-ban.html">Hoa hồng nhà cung cấp</a></div>--%>
                                    <asp:Literal Visible="false" ID="lthoahongQRCode" runat="server"></asp:Literal>

                                    <asp:Literal Visible="false" ID="lthhagland" runat="server"></asp:Literal>
                                    <div style=" display:none"><a href="/chuyen-diem.html">Chuyển điểm trong hệ thống</a></div>
                                    <div style=" display:none"><a href="/chuyen-diem-vi-diem.html">Chuyển điểm trong ví điểm</a></div>
                                    <div style=" display:none"><a href="/lich-su-chuyen-diem.html">Lịch sử chuyển điểm</a></div>
                                    <div style=" display:none"><a href="/lich-su-cap-diem.html">Lịch sử cấp điểm</a></div>

                                    <div><a href="/link-gioi-thieu.html">Link giới thiêụ</a></div>
                                    <div><a href="/ho-so-thanh-vien.html">Quản lý hồ sơ</a></div>
                                    <div><a href="/thay-doi-mat-khau.html">Đổi mật khẩu</a></div>
                                    <div>
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lnkthoat_Click">[Đăng xuất]</asp:LinkButton>
                                    </div>
                                </div>
                            </li>
                            <%--<li>
							<i class="fa fa-map-marker"></i>  
							<a href="/lien-he.html" title="liên hệ" class="account_a">
								<span>Liên hệ</span>
							</a>
						</li>--%>
                        </ul>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</div>



<div class="header">
    <header class="site-header ">
        <div class="TopMenu">
            <div class="container">
                <div class="site-header-inner">
                    <div class="menubar hidden-md hidden-lg">
                        <span style="cursor: pointer" onclick="openNav()"><i class="fa fa-align-justify"></i> </span>
                    </div>
                    <div class="header-left">
                        <div class="logo">
                            <%=AllQuery.Banner.Banners() %>
                        </div>
                    </div>
                    <div class="header-left margin-left-50 hidden-xs hidden-sm">
                        <div class="header_search">
                            <uc1:Box_search runat="server" ID="Box_search" />
                        </div>
                    </div>
                    <div class="Giohang">
                        <a href="/gio-hang.html"><i class="fa fa-shopping-cart" aria-hidden="true"></i> Giỏ hàng (<%=Services.SessionCarts.LoadCart() %>)</a>
                    </div>
                    <div class="header-acount hidden-sm hidden-xs">
                    
                        <div class="heading-cart text-xs-left">
                              
                            <asp:Panel ID="Panel1" runat="server">
                                   <div class="dangkydangnhap">
                                       <span class="dangks"><i class="fa fa-user-circle-o" aria-hidden="true"></i> </span> 
                                       <div class="thongtindangnhap"><a href="/dang-nhap.html"> Đăng nhập </a>   -  <a href="/dang-ky.html">Đăng ký</a></div>
                                   </div>
                            </asp:Panel>
                            <asp:Panel ID="Panel2" runat="server">
                                <div class="dropdowndestop">
                                    <div onclick="ShowthanhvienDT()" class="sdropbtn">
                                        <b class="thanhvienxcc">
                                            <asp:Literal ID="ltwelcome" runat="server"></asp:Literal>
                                            <span class="caret"></span></b>
                                    </div>
                                    <asp:Literal ID="ltThanhvienaglang" runat="server"></asp:Literal>
                                    <div id="myDropdownDestop" class="dropdowndestop-content" style="display: none">
                                        <div><a href="/vi-tien.html"><i class="fa fa-credit-card"></i> Ví điểm của bạn </a></div>
                                        <div>
                                            <asp:Literal ID="lttinnhan" runat="server"></asp:Literal>
                                        </div>
                                        <asp:Literal ID="ltchuyengia1" runat="server"></asp:Literal>
                                        <div  style=" display:none"><a href="/lich-su-tru-thue.html"><i class="fa fa-credit-card"></i> LS chuyển điểm thu nhập cá nhân</a></div>
                                                <div><a href="/tich-diem-hoa-hong-mua.html"><i class="fa fa-gift"></i> Danh sách hoa hồng</a></div>
                                        <div><a href="/dau-tu.html"> <i class="fa fa-gift"></i>  Đầu tư</a></div>
                                         <div><a href="/lich-su-dau-tu.html"> <i class="fa fa-gift"></i> Lịch sử đầu tư</a></div>

                                        <div><a href="/rut-tien.html"><i class="fa fa-credit-card"></i> Rút điểm</a></div>
                                        <div><a href="/lich-su-rut-tien.html"><i class="fa fa-credit-card"></i> Lịch sử rút điểm</a></div>
                                        <div style=" display:none" ><a href="/mua-diem.html"><i class="fa fa-credit-card"></i> Mua điểm</a></div>
                                        <div><a href="/lich-su-mua-diem.html"><i class="fa fa-credit-card"></i> Lịch sử mua điểm</a></div>
                                        <asp:Panel ID="Panel3" Visible="false" runat="server">
                                            <div><a href="/quan-ly-san-pham.html"><i class="fa fa-th"></i> Quản lý sản phẩm</a></div>
                                            <div style=" display:none"><a href="/danh-sach-don-ban-hang.html"><i class="fa fa-clock-o"></i> Quản lý đơn bán hàng (<asp:Literal ID="ltquanly1" runat="server"></asp:Literal>)</a></div>
                                        </asp:Panel>
                                        <div><a href="/lich-su-mua-hang.html"><i class="fa fa-clock-o"></i> Lịch sử mua hàng (<asp:Literal ID="lichsumuahang1" runat="server"></asp:Literal>)</a></div>
                                        <div>
                                            <asp:Literal ID="ltdanhsachthanhvien" runat="server"></asp:Literal>
                                        </div>
                                        <%-- <div><a href="/Danh-sach-thanh-vien.html"><i class="fa fa-gift"></i>   Danh sách thành viên</a></div>--%>
                                        <%--           <div><a href="/tich-diem-hoa-hong-gioi-thieu.html"><i class="fa fa-gift"></i>  Hoa hồng Quản Lý</a></div>--%>
                                
                                        <%--          <div><a href="/tich-diem-hoa-hong-ban.html"><i class="fa fa-gift"></i>  Hoa hồng nhà cung cấp</a></div>--%>
                                        <asp:Literal  Visible="false" ID="lthoahongQRCode2" runat="server"></asp:Literal>
                                        <asp:Literal Visible="false" ID="lthhagland2" runat="server"></asp:Literal>
                                        <div style=" display:none"><a href="/chuyen-diem.html"><i class="fa fa-plus-circle"></i> Chuyển điểm trong hệ thống</a></div>
                                        <div style=" display:none"><a href="/chuyen-diem-vi-diem.html"><i class="fa fa-plus-circle"></i> Chuyển điểm trong ví điểm</a></div>
                                        <div style=" display:none"><a href="/lich-su-chuyen-diem.html"><i class="fa fa-clock-o"></i> Lịch sử chuyển điểm</a></div>
                                        <div style=" display:none"><a href="/lich-su-cap-diem.html"><i class="fa fa-clock-o"></i> Lịch sử cấp điểm</a></div>
                                        <div><a href="/link-gioi-thieu.html"><i class="fa fa-share-alt"></i> Link giới thiêụ</a></div>
                                        <div><a href="/ho-so-thanh-vien.html"><i class="fa fa-user"></i> Quản lý hồ sơ</a></div>
                                        <div><a href="/thay-doi-mat-khau.html"><i class="fa fa-cogs"></i> Đổi mật khẩu</a></div>
                                        <div>
                                            <asp:LinkButton ID="LinkButton2" style=" border:none" runat="server" OnClick="lnkthoat_Click"><i class="fa fa-cogs"></i>   [Đăng xuất]</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                        </div>
                    </div>
                </div>
            </div>
            <div class="header_search hidden-lg hidden-md">
                <uc1:Box_search_header runat="server" ID="Box_search_header" />
                <div class="header-tag hidden-sm hidden-xs">
                    <b>Từ khóa phổ biến: </b>
                    <asp:Literal ID="lttimkiem" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </header>
</div>
<div class="bot-header hidden-xs hidden-sm">
    <div class="container">
        <div onclick="Menuopen()" class="bot-header-left f-left khodattenqua">
            <a onclick="Menuopen()">Danh mục sản phẩm</a>
        </div>
        <div class="bot-header-center f-left">
            <div class="Menu">
                <ul>
                    <uc1:MenuTop runat="server" ID="MenuTop" />
                </ul>
            </div>
        </div>
    </div>
</div>


<div class="catogory-other-page khodattenqua" id="khodattenqua">
    <div class="section section-category">
        <div class="cate-overlay"></div>
        <div class="container">
            <div class="row row-noGutter">
                <div class="col-lg-3 col-260">
                    <div class="cate-sidebar">
                        <nav>
                            <div class="hidden-md hidden-lg">
                                <h2 class="mobile-title">Danh mục sản phẩm </h2>
                            </div>
                            <ul id="nav" class="site-nav vertical-nav">
                                <uc1:Menuleft runat="server" ID="Menuleft" />
                            </ul>
                        </nav>
                    </div>
                </div>
                <div class="col-lg-9 col-fix260 col-md-12">
                    <div class="cate-banner"></div>
                    <div class="banner-product"></div>
                </div>
            </div>
        </div>
    </div>
</div>
<asp:Literal ID="ltstyle" runat="server"></asp:Literal>
<asp:Literal ID="ltcssstyle" runat="server"></asp:Literal>
<asp:HiddenField ID="hdid" runat="server" />
