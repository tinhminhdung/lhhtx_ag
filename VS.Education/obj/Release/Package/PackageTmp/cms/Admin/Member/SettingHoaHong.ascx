<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SettingHoaHong.ascx.cs" Inherits="VS.E_Commerce.cms.Admin.Member.SettingHoaHong" %>
<div id="cph_Main_ContentPane">
    <div class="Block Breadcrumb ui-widget-content ui-corner-top ui-corner-bottom" id="Breadcrumb">
        <ul>
            <li class="SecondLast"><a href="/admin.aspx"><i style="font-size: 14px;" class="icon-home"></i>Trang chủ</a></li>
            <li class="Last"><span>Cấu hình hoa hồng</span></li>
        </ul>
    </div>
    <div style="clear: both;"></div>
    <div class="widget">

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row-fluid">
                    <div class="span12">
                        <div class="widget">
                            <div class="widget-title">
                                <h4><i class="icon-reorder"></i>Cấu hình hoa hồng</h4>
                            </div>
                            <div class="widget-body">
                                <div class='frm-add'>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td width="400px"></td>
                                            <td></td>
                                            <td>
                                                <strong><font color="#ed1f27"><asp:Literal ID="ltmsg" runat="server"></asp:Literal></font></strong>
                                            </td>
                                        </tr>
                                        <asp:Panel ID="Panel1" Visible="false" runat="server">
                                        <tr>
                                            <td>
                                                <strong style="text-transform: uppercase;color:red"> <img src="/Resources/admin/images/bullet-red.png" border="0" /> Cấu hình hoa hồng giới thiệu đăng ký thành viên</strong>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                        </tr>
                                        
                                         <tr>
                                            <td style="padding-left: 15px">Số tiền kích hoạt thành viên</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txttienKichHoat" runat="server" CssClass="txt" Width="50px">2000</asp:TextBox> 2 triệu VNĐ
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px">Hoa hồng trực tiếp </td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txthoahonggttructiep" runat="server" CssClass="txt" Width="50px">25</asp:TextBox> %
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Hoa hồng F1</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtHHGTF1" runat="server" CssClass="txt" Width="50px">25</asp:TextBox> %
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Hoa hồng F2</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtHHGTF2" runat="server" CssClass="txt" Width="50px">25</asp:TextBox> %
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Hoa hồng F3</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtHHGTF3" runat="server" CssClass="txt" Width="50px">25</asp:TextBox> %
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Hoa hồng F4</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtHHGTF4" runat="server" CssClass="txt" Width="50px">25</asp:TextBox> %
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Hoa hồng F5</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtHHGTF5" runat="server" CssClass="txt" Width="50px">25</asp:TextBox> %
                                            </td>
                                        </tr>
                                        <tr style="height: 7px;">
                                            <td colspan="3"></td>
                                        </tr>

                                        <tr style=" display:none">
                                            <td style="padding-left: 15px">Hoa hồng trực gián tiếp</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txthoahonggtgiantiep" runat="server" CssClass="txt" Width="50px">10</asp:TextBox> %
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Hoa Hồng Leader</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txthoahonggtLeader" runat="server" CssClass="txt" Width="50px">10</asp:TextBox> %
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Hoa Hồng chi nhánh</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txthoahonggtchinhanh" runat="server" CssClass="txt" Width="50px">10</asp:TextBox> %
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Hoa Hồng (Ban Đào tạo- Chuyên gia _ ID Thành viên: <a target="_blank"  href="admin.aspx?u=Thanhvien&IDThanhVien=67357">67357</a>)</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtbanchuyengianDangKy" runat="server" CssClass="txt" Width="50px">5</asp:TextBox> %
                                              <a  style=" color:red; font-weight:bold" target="_blank"  href="/admin.aspx?u=HoaHong&kw=chuyengia&kieu=2&Tu=&Den=&type=400">Xem danh sách hoa hồng</a>
                                            </td>
                                        </tr>
                                          <tr>
                                            <td style="padding-left: 15px">Hoa Hồng (Doanh số đồng hưởng)</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtdoanhsodonghuongDangKy" runat="server" CssClass="txt" Width="50px">5</asp:TextBox> %
                                                 <a  style=" color:red; font-weight:bold" target="_blank"  href="/admin.aspx?u=HoaHong&kw=thuongquanly&kieu=2&Tu=&Den=&type=406">Xem danh sách hoa hồng</a>
                                            </td>
                                        </tr>
                                          <tr>
                                            <td style="padding-left: 15px">Thưởng quản  lý</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtthuongquanlyThanhVien" runat="server" CssClass="txt" Width="50px">5</asp:TextBox> %
                                                 <a  style=" color:red; font-weight:bold" target="_blank"  href="/admin.aspx?u=HoaHong&kw=donghuongdoanhso&kieu=2&Tu=&Den=&type=404">Xem danh sách hoa hồng</a>
                                            </td>
                                        </tr>

                                        </asp:Panel>
                                         <tr>
                                            <td>
                                                <strong style="text-transform: uppercase;color:red"> <img src="/Resources/admin/images/bullet-red.png" border="0" /> Cấu hình Hoa hồng Mua Bán</strong>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                          <tr style="">
                                            <td style="padding-left: 15px">Hoa hồng giới thiệu F1</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtHoaHongGioiThieuTrucTiepmuahangVaF1" runat="server" CssClass="txt" Width="50px">30</asp:TextBox> %
                                                <span style="font-size: 8pt; color: #ed1c24"><em>(Hoa hồng giới thiệu F1 sản phẩm và Hoa Hồng Mua hàng trực tiếp F1)</em></span>
                                            </td>
                                        </tr>


                                         <tr style="display:none">
                                            <td style="padding-left: 15px">Hoa hồng giới thiệu F1</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtHoaHongGioiThieuF1" runat="server" CssClass="txt" Width="50px">10</asp:TextBox> %
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px">Hoa hồng giới thiệu F2</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtHoaHongGioiThieuF2" runat="server" CssClass="txt" Width="50px">10</asp:TextBox> %
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px">Hoa hồng giới thiệu F3</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtHoaHongGioiThieuF3" runat="server" CssClass="txt" Width="50px">10</asp:TextBox> %
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px">Hoa hồng giới thiệu F4</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtHoaHongGioiThieuF4" runat="server" CssClass="txt" Width="50px">10</asp:TextBox> %
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px">Hoa hồng giới thiệu F5</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtHoaHongGioiThieuF5" runat="server" CssClass="txt" Width="50px">10</asp:TextBox> %
                                            </td>
                                        </tr>


                                         <tr>
                                            <td style="padding-left: 15px">Hoa Hồng (Chi Nhánh Mua Hàng)</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtHoaHongChiNhanhMuaHang" runat="server" CssClass="txt" Width="50px">10</asp:TextBox> %
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Hoa Hồng (Leader - Mua Hàng)</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtHoaHongLeaderMuaHang" runat="server" CssClass="txt" Width="50px">10</asp:TextBox> %
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Hoa Hồng (Thưởng quản lý - Mua Hàng) - ID:(thuongquanly)</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtthuongquanly" runat="server" CssClass="txt" Width="50px">10</asp:TextBox> %
                                             <a  style=" color:red; font-weight:bold" target="_blank"  href="/admin.aspx?u=HoaHong&kw=thuongquanly&kieu=2&Tu=&Den=&type=0">Xem danh sách hoa hồng</a>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Hoa hồng kết nối nhà cung cấp</td><%--Trực tiếp --%>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtHoaHongGioiThieuTrucTiepNhaCungCap" runat="server" CssClass="txt" Width="50px">10</asp:TextBox> %
                                            </td>
                                        </tr>
                                         <tr style="display:none">
                                            <td style="padding-left: 15px">Hoa hồng (Giới Thiệu Gián tiếp Nhà Cung Cấp)</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtHoaHongGioiThieuGianTiepNhaCungCap" runat="server" CssClass="txt" Width="50px">10</asp:TextBox> %
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px">Hoa Hồng (Chi Nhánh Bán Hàng)</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtHoaHongChiNhanhBanHang" runat="server" CssClass="txt" Width="50px">10</asp:TextBox> %
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Hoa Hồng (Ban Đào tạo- Chuyên gia - ID:(chuyengia)</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtbanchuyengia" runat="server" CssClass="txt" Width="50px">5</asp:TextBox> %
                                                 <a  style=" color:red; font-weight:bold" target="_blank"  href="/admin.aspx?u=HoaHong&kw=chuyengia&kieu=2&Tu=&Den=&type=401">Xem danh sách hoa hồng</a>
                                            </td>
                                        </tr>
                                           <tr>
                                            <td style="padding-left: 15px">Hoa Hồng (Doanh số đồng hưởng) - ID:(donghuongdoanhso)</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtdoanhsodonghuongmuaban" runat="server" CssClass="txt" Width="50px">5</asp:TextBox> %
                                             <a  style=" color:red; font-weight:bold" target="_blank"  href="/admin.aspx?u=HoaHong&kw=donghuongdoanhso&kieu=2&Tu=&Den=&type=403">Xem danh sách hoa hồng</a>
                                            </td>
                                        </tr>
                                        <%-- <tr>
                                            <td style="padding-left: 15px">Hoa Hồng  Thu nhập / Thu nhập của giới thiệu NCC F1-->F5</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtThuNhapNCC" runat="server" CssClass="txt" Width="50px">35</asp:TextBox> %
                                            </td>
                                        </tr>--%>


                                          <tr>
                                            <td style="padding-left: 15px; ">Thông báo tự động duyệt đơn hàng</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtthongbaoduyethang" runat="server" CssClass="txt" Width="50px">5</asp:TextBox> / Ngày
                                                <span style="font-size: 8pt; color: #ed1c24"><em>(Xuất hiện thông báo sau 5 ngày khi gần đến ngày hệ thống chạy tự động duyệt đơn hàng)</em></span>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="padding-left: 15px">Tự động duyệt đơn hàng</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtduyetdontudong" runat="server" CssClass="txt" Width="50px">10</asp:TextBox> / Ngày
                                                <span style="font-size: 8pt; color: #ed1c24"><em>(Tự động duyệt đơn khi đại lý không chịu vào bấm nút chấp nhận đơn hàng, lúc này 10 ngày thì tiền sẽ tự lấy từ ví tạm chuyển sang ví thương mại cho nhà cung cấp)</em></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px">Tự động hủy đơn hàng</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txthuydonhang" runat="server" CssClass="txt" Width="50px">10</asp:TextBox> / Ngày
                                                <span style="font-size: 8pt; color: #ed1c24"><em>(Tự động hủy đơn hàng sau 3 ngày nhà cung cấp ko vào duyệt đơn hàng.)</em></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong style="text-transform: uppercase;color:red"> <img src="/Resources/admin/images/bullet-red.png" border="0" /> Tặng điểm cho thành viên theo tỷ lệ</strong>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Thành viên Free</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txttangFree" runat="server" CssClass="txt" Width="50px">25</asp:TextBox> %
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Thành viên đại lý</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txttangthanhvien" runat="server" CssClass="txt" Width="50px">70</asp:TextBox> %
                                            </td>
                                        </tr>
                                          <tr>
                                            <td style="padding-left: 15px">Thành viên cửa hàng</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtThanhViencuahang" runat="server" CssClass="txt" Width="50px">70</asp:TextBox> %
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Tổng tiền mua hàng - kích hoạt trở thành đại lý</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtTienkichhoatdaily" runat="server" CssClass="txt" Width="50px">998</asp:TextBox> điểm
                                            </td>
                                        </tr>


                                        <tr>
                                            <td>
                                                <strong style="text-transform: uppercase;color:red"> <img src="/Resources/admin/images/bullet-red.png" border="0" /> Tặng điểm cho thành viên khi điểm danh</strong>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Ngày thứ 1 </td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtngaythu1" runat="server" CssClass="txt" Width="50px">0.2</asp:TextBox>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Ngày thứ 2</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtngaythu2" runat="server" CssClass="txt" Width="50px">0.2</asp:TextBox>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Ngày thứ 3</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtngaythu3" runat="server" CssClass="txt" Width="50px">0.2</asp:TextBox>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Ngày thứ 4</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtngaythu4" runat="server" CssClass="txt" Width="50px">0.2</asp:TextBox>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Ngày thứ 5</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtngaythu5" runat="server" CssClass="txt" Width="50px">0.2</asp:TextBox>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Ngày thứ 6</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtngaythu6" runat="server" CssClass="txt" Width="50px">0.2</asp:TextBox>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Ngày thứ 7</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtngaythu7" runat="server" CssClass="txt" Width="50px">0.5</asp:TextBox>
                                            </td>
                                        </tr>

                                         <tr>
                                            <td>
                                                <strong style="text-transform: uppercase;color:red"> <img src="/Resources/admin/images/bullet-red.png" border="0" /> Cấu hình trừ % thuế</strong>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        
                                         <tr>
                                            <td style="padding-left: 15px">Trừ thuế</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtthue" runat="server" CssClass="txt" Width="50px">10</asp:TextBox> %
                                            </td>
                                        </tr>

                                         <tr>
                                            <td>
                                                <strong style="text-transform: uppercase;color:red"> <img src="/Resources/admin/images/bullet-red.png" border="0" /> Cấu hình ăn theo Agland</strong>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        
                                         <tr>
                                            <td style="padding-left: 15px">F1 ăn theo Agland</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtF1AnTheoAgland" runat="server" CssClass="txt" Width="50px">10</asp:TextBox> %
                                            </td>
                                        </tr>


                                          <tr>
                                            <td>
                                                <strong style="text-transform: uppercase;color:red"> <img src="/Resources/admin/images/bullet-red.png" border="0" /> Cấu hình hoa hồng chuyên gia AFF và Agland</strong>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        
                                         <tr>
                                            <td style="padding-left: 15px">Hoa hồng AFF Chuyên gia</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtAFFChuyengia" runat="server" CssClass="txt" Width="50px">5</asp:TextBox> %
                                            </td>
                                        </tr>
                                          <tr>
                                            <td style="padding-left: 15px">Hoa hồng AGLand Chuyên gia</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtAGLandChuyengia" runat="server" CssClass="txt" Width="50px">2</asp:TextBox> %
                                            </td>
                                        </tr>

                                        

                                         <tr>
                                            <td>
                                                <strong style="text-transform: uppercase;color:red"> <img src="/Resources/admin/images/bullet-red.png" border="0" /> Cấu hình số tiền nâng cấp lên level</strong>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        
                                         <tr>
                                            <td style="padding-left: 15px"> Thành viên tích lũy đủ 5 triệu vnd thì dc lên level1</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txttienlenlevel" runat="server" CssClass="txt" Width="50px">5000</asp:TextBox> Điểm
                                            </td>
                                        </tr>

                                         <tr>
                                            <td>
                                                <strong style="text-transform: uppercase;color:red"> <img src="/Resources/admin/images/bullet-red.png" border="0" /> Cấu hình Hoa hồng AG LAND</strong>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        
                                         <tr>
                                            <td style="padding-left: 15px">Lãi suất <= 200 triệu</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtHaiTamPhanTram" runat="server" CssClass="txt" Width="50px">28</asp:TextBox> %
                                            </td>
                                        </tr>
                                        
                                         <tr>
                                            <td style="padding-left: 15px">Lãi suất >= 200 triệu</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtBaHaiPhanTram" runat="server" CssClass="txt" Width="50px">32</asp:TextBox> %
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Tiền 1 cổ phẩn</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txttienmotcophan" runat="server" CssClass="txt" Width="150px">0</asp:TextBox> 
                                            </td>
                                        </tr>
                                        <tr style="height: 7px;">
                                            <td colspan="3"></td>
                                        </tr>

                                        <tr style="height: 7px;">
                                            <td colspan="3"></td>
                                        </tr>

                                          <tr>
                                            <td></td>
                                            <td></td>
                                            <td>
                                                <asp:LinkButton ID="btnsetup" runat="server" OnClick="btnsetup_Click" CssClass="btn btn-primary" Style="background: #ed1c24"> <i class="icon-save"></i> Cập nhật </asp:LinkButton>
                                            </td>
                                        </tr>


                                           <tr>
                                            <td>
                                                <strong style="text-transform: uppercase;color:red"> <img src="/Resources/admin/images/bullet-red.png" border="0" /> Cấu hình Hoa hồng QRcode Cho tất cả</strong>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                          <tr>
                                            <td style="padding-left: 15px">Ngày cập nhật cuối cùng</td>
                                            <td></td>
                                            <td>
                                              <%=MoreAll.Other.Giatri("NgayCapNhapQRCode") %>
                                            </td>
                                        </tr>

                                         <tr>
                                            <td style="padding-left: 15px">% Chiết khấu QRcode</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txtChietKhauQRcode" runat="server" CssClass="txt" Width="50px">0</asp:TextBox> %
                                            </td>
                                        </tr>

                                           <tr>
                                            <td style="padding-left: 15px">% Hoa hồng cho người mua</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txthoahongnguoimuaQRcode" runat="server" CssClass="txt" Width="50px">0</asp:TextBox> %
                                            </td>
                                        </tr>

                                           <tr>
                                            <td style="padding-left: 15px">% Hoa hồng cho hệ thống</td>
                                            <td></td>
                                            <td>
                                                <asp:TextBox ID="txthoahonghethongQRcode" Enabled="false" runat="server" CssClass="txt" Width="50px">0</asp:TextBox> %
                                            </td>
                                        </tr>

                                          <tr>
                                            <td></td>
                                            <td></td>
                                            <td>
                                                <asp:LinkButton ID="btcapnhatQRcode" OnLoad="ThaydoiCauHinh" runat="server" OnClick="btcapnhatQRcode_Click" CssClass="btn btn-primary" Style="background: #ed1c24"> <i class="icon-save"></i> Cập nhật hoa hồng QRcode </asp:LinkButton>
                                            </td>
                                        </tr>
                                         <tr style="height: 7px;">
                                            <td colspan="3"></td>
                                        </tr>

                                        <tr style="height: 7px;">
                                            <td colspan="3"></td>
                                        </tr>

                                      
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
