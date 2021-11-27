<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Lefmenu_ThanhVien.ascx.cs" Inherits="VS.E_Commerce.cms.Display.Lefmenu_ThanhVien" %>
<aside class="left left-content col-md-3 col-md-pull-9 Destop">
    <div class="blog-aside aside-item aside-tags">
        <div>
            <div class="aside-title margin-top-5">
                <h2 class="title-head">
                    <span>THÔNG TIN THÀNH VIÊN</span>
                </h2>
            </div>
            <div class="aside-content list-tags">
                <span class="tag-item <%=returnCSS("ViTienThanhVien") %>">
                    <a href="/vi-tien.html"><i class="fa fa-credit-card"></i>Ví điểm thành viên</a>
                </span>
                 <span class="tag-item <%=returnCSS("TinNhan") %>">
                     <asp:Literal ID="lttinnhan" runat="server"></asp:Literal>
                </span>
                <asp:Literal ID="ltchuyengia" runat="server"></asp:Literal>
                  <span class="tag-item  <%=returnCSS("HoaHongMua") %>">
                    <a href="/tich-diem-hoa-hong-mua.html"><i class="fa fa-gift"></i>Danh sách hoa hồng</a>
                </span>
                 <span style=" display:none" class="tag-item <%=returnCSS("Lichsutruthue") %>">
                   <a href="/lich-su-tru-thue.html"><i class="fa fa-credit-card"></i>LS chuyển điểm thu nhập cá nhân</a>
                </span>

                <span class="tag-item <%=returnCSS("RutTien") %>">
                    <a href="/rut-tien.html"><i class="fa fa-credit-card"></i>Rút điểm</a>
                </span>

                <span class="tag-item <%=returnCSS("LichSuRutTien") %>">
                    <a href="/lich-su-rut-tien.html"><i class="fa fa-credit-card"></i>Lịch sử rút điểm</a>
                </span>
                <span style=" display:none"  class="tag-item <%=returnCSS("MuaDiem") %>">
                    <a href="/mua-diem.html"><i class="fa fa-credit-card"></i>Mua điểm</a>
                </span>
                <span class="tag-item <%=returnCSS("LichSuMuaDiem") %>">
                    <a href="/lich-su-mua-diem.html"><i class="fa fa-credit-card"></i>Lịch sử mua điểm</a>
                </span>
                <asp:Panel ID="Panel1" Visible="false" runat="server">
                    <span class="tag-item <%=returnCSS("quanlysanpham") %>">
                        <a href="/quan-ly-san-pham.html"><i class="fa fa-th"></i>Danh sách sản phẩm</a>
                    </span>
                    <span  style=" display:none"  class="tag-item <%=returnCSS("DonBanHang") %> <%=returnCSS("orderssold") %>">
                        <a href="/danh-sach-don-ban-hang.html"><i class="fa fa-clock-o"></i>Quản lý đơn bán hàng (<asp:Literal ID="ltquanlydonhang" runat="server"></asp:Literal>)</a>
                    </span>
                </asp:Panel>
                <span class="tag-item <%=returnCSS("Lichsu") %> <%=returnCSS("Detailorders") %>">
                    <a href="/lich-su-mua-hang.html"><i class="fa fa-clock-o"></i>Lịch sử mua hàng (<asp:Literal ID="ltlichsumuahang" runat="server"></asp:Literal>)</a>
                </span>
                <%--<span class="tag-item  <%=returnCSS("HoaHong") %>">
              <a href="/tich-diem-hoa-hong.html"><i class="fa fa-gift"></i>Tất cả danh sách hoa hồng</a>
            </span>--%>
                <span class="tag-item  <%=returnCSS("Thanhvien") %>">
                    <asp:Literal ID="ltdanhsachthanhvien" runat="server"></asp:Literal>
                </span>

               <%-- <span class="tag-item  <%=returnCSS("HoaHongGioiThieu") %>">
                    <a href="/tich-diem-hoa-hong-gioi-thieu.html"><i class="fa fa-gift"></i>Hoa hồng Quản Lý</a>
                </span>--%>
              
               <%-- <span class="tag-item  <%=returnCSS("HoaHongBan") %>">
                    <a href="/tich-diem-hoa-hong-ban.html"><i class="fa fa-gift"></i>Hoa hồng nhà cung cấp</a>
                </span>--%>
                <asp:Literal ID="lthoahongQRCode" Visible="false" runat="server"></asp:Literal>
                <asp:Literal ID="ltagland" Visible="false" runat="server"></asp:Literal>

                <span style=" display:none" class="tag-item <%=returnCSS("ChuyenDiem") %>">
                    <a href="/chuyen-diem.html"><i class="fa fa-plus-circle"></i>Chuyển điểm trong hệ thống</a>
                </span>
                <span style=" display:none" class="tag-item <%=returnCSS("ChuyenDiemViDiem") %>">
                    <a href="/chuyen-diem-vi-diem.html"><i class="fa fa-plus-circle"></i>Chuyển điểm trong ví điểm</a>
                </span>

                <span style=" display:none" class="tag-item <%=returnCSS("LichSuChuyenDiem") %>">
                    <a href="/lich-su-chuyen-diem.html"><i class="fa fa-clock-o"></i>Lịch sử chuyển điểm</a>
                </span>
                <span style=" display:none" class="tag-item <%=returnCSS("LichSuCapDiem") %>">
                    <a href="/lich-su-cap-diem.html"><i class="fa fa-clock-o"></i>Lịch sử cấp điểm</a>
                </span>
                <span class="tag-item <%=returnCSS("LinkGoiThieu") %>">
                    <a href="/link-gioi-thieu.html"><i class="fa fa-share-alt"></i>Link giới thiêụ</a>
                </span>
                <span class="tag-item <%=returnCSS("changinfo") %>">
                    <a href="/ho-so-thanh-vien.html"><i class="fa fa-user"></i>Hồ sơ thành viên</a>
                </span>
                <span class="tag-item <%=returnCSS("changepass") %>">
                    <a href="/thay-doi-mat-khau.html"><i class="fa fa-cogs"></i>Đổi mật khẩu</a>
                </span>
            </div>
        </div>
    </div>
</aside>

<asp:HiddenField ID="hdid" runat="server" />




