<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Members.ascx.cs" Inherits="VS.E_Commerce.cms.Admin.Member.Members" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<script src="/Scripts/ckfinder/ckfinder.js" type="text/javascript"></script>
<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<div id="cph_Main_ContentPane">
    <div id="">
        <div class="Block Breadcrumb ui-widget-content ui-corner-top ui-corner-bottom" id="Breadcrumb">
            <ul>
                <li class="SecondLast"><a href="/admin.aspx"><i style="font-size: 14px;" class="icon-home"></i>Trang chủ</a></li>
                <li class="Last"><span>Danh sách Thành viên</span></li>
            </ul>
        </div>
        <div style="clear: both;"></div>
        <div class="widget">
            <div class="row-fluid">
                <div class="span12">
                    <div class="widget">
                        <div class="widget-title">
                            <h4><i class="icon-reorder"></i>Danh sách thành viên</h4>
                        </div>
                        <div class="widget-body">
                            <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>--%>
                            <div class="row-fluid">
                                <div class="span9">
                                    <div id="sample_1_length" class="dataTables_length">
                                        <div class="frm_search">
                                            <div>
                                                <asp:TextBox ID="txtkeyword" runat="server" CssClass="txt_csssearch" Width="400px"></asp:TextBox>
                                                <asp:LinkButton ID="lnksearch" runat="server" OnClick="lnksearch_Click" CssClass="vadd toolbar btn btn-info" Style="margin-top: -9px;">  <i class="icon-search"></i>&nbsp;Tìm kiếm</asp:LinkButton>
                                            </div>
                                            <div style="margin-top: 10px;">
                                                <asp:DropDownList ID="ddlchinhanh" runat="server" AutoPostBack="true" CssClass="txt" OnSelectedIndexChanged="ddlchinhanh_SelectedIndexChanged">
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlcapdo" runat="server" AutoPostBack="true" CssClass="txt" OnSelectedIndexChanged="ddlcapdo_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="-1">= Tất cả cấp độ Level =</asp:ListItem>
                                                    <asp:ListItem Value="0">Cộng tác viên</asp:ListItem>
                                                    <asp:ListItem Value="1">Nhân Viên KD</asp:ListItem>
                                                    <asp:ListItem Value="2">Trưởng Nhóm</asp:ListItem>
                                                    <asp:ListItem Value="3">Trưởng Phòng</asp:ListItem>
                                                    <asp:ListItem Value="4">Phó Giám Đốc</asp:ListItem>
                                                    <asp:ListItem Value="5">Giám Đốc KD</asp:ListItem>
                                                </asp:DropDownList>

                                                 <asp:DropDownList ID="ddlkieuthanhvien" runat="server" AutoPostBack="true" CssClass="txt" OnSelectedIndexChanged="ddlkieuthanhvien_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="-1">= Tất cả Loại tài khoản =</asp:ListItem>
                                                    <asp:ListItem Value="1">Thành viên</asp:ListItem>
                                                    <asp:ListItem Value="2">Tất cả nhà cung cấp</asp:ListItem>
                                                     <asp:ListItem Value="3">Chỉ có nhà cung cấp đã đăng sản phẩm</asp:ListItem>
                                                </asp:DropDownList>


                                                 <asp:DropDownList ID="ddlthanhvien" runat="server" AutoPostBack="true" CssClass="txt" OnSelectedIndexChanged="ddlthanhvien_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="-1">= Tất cả kiểu thành viên=</asp:ListItem>
                                                    <asp:ListItem Value="0">Thành viên Chưa kích hoạt</asp:ListItem>
                                                    <asp:ListItem Value="1">Thành viên đã kích hoạt</asp:ListItem>
                                                   <asp:ListItem Value="2">Thành viên cửa hàng</asp:ListItem>
                                                </asp:DropDownList>

                                                  <asp:DropDownList ID="ddlAgLand" Visible="false" runat="server" AutoPostBack="true" CssClass="txt" OnSelectedIndexChanged="ddlAgLand_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="-1">= Tất cả AG Land =</asp:ListItem>
                                                    <asp:ListItem Value="0">Chưa kích hoạt AgLand</asp:ListItem>
                                                    <asp:ListItem Value="1">Đã kích hoạt AgLand</asp:ListItem>
                                                </asp:DropDownList>

                                                  <asp:DropDownList ID="ddluutien" Visible="false" runat="server" AutoPostBack="true" CssClass="txt" OnSelectedIndexChanged="ddluutien_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="-1">= Tất cả Ưu tiên=</asp:ListItem>
                                                    <asp:ListItem Value="0">Chưa kích hoạt Ưu tiên</asp:ListItem>
                                                    <asp:ListItem Value="1">Đã kích hoạt Ưu tiên</asp:ListItem>
                                                </asp:DropDownList>

                                                      <asp:DropDownList ID="ddlQRCode" runat="server" AutoPostBack="true" CssClass="txt" OnSelectedIndexChanged="ddlQRCode_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="-1">= QRCode=</asp:ListItem>
                                                    <asp:ListItem Value="0">Chưa kích hoạt QRCode</asp:ListItem>
                                                    <asp:ListItem Value="1">Đã kích hoạt QRCode</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddltheoLead" runat="server" AutoPostBack="true" CssClass="txt" OnSelectedIndexChanged="ddltheoLead_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="-1">= Tất cả Lead =</asp:ListItem>
                                                    <asp:ListItem Value="0">Thành viên</asp:ListItem>
                                                    <asp:ListItem Value="1">Lead</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="true" CssClass="txt" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                                                </asp:DropDownList>

                                                   <asp:DropDownList ID="ddkhoathanhvien" runat="server" AutoPostBack="true" CssClass="txt" OnSelectedIndexChanged="ddkhoathanhvien_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="-1">= Trạng thái hoạt động =</asp:ListItem>
                                                    <asp:ListItem Value="0">Đã bị khóa</asp:ListItem>
                                                    <asp:ListItem Value="1">Đang hoạt động</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlorderby" runat="server" AutoPostBack="true" CssClass="txt"
                                                    OnSelectedIndexChanged="ddlorderby_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="dcreatedate">S.xếp:Ngày cập nhật</asp:ListItem>
                                                    <asp:ListItem Value="iuser_id">S.xếp:Tăng dần</asp:ListItem>
                                                    <asp:ListItem Value="vlname">S.xếp:Tên (ABC)</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlordertype" runat="server" AutoPostBack="True" CssClass="txt" OnSelectedIndexChanged="ddlordertype_SelectedIndexChanged">
                                                    <asp:ListItem Value="desc">Giảm dần</asp:ListItem>
                                                    <asp:ListItem Value="asc">Tăng dần</asp:ListItem>
                                                </asp:DropDownList>

                                                  <asp:TextBox Style="width: 200px;" ID="txtNgayThangNam" placeholder="Tìm kiếm từ ngày/tháng/năm" AutoPostBack="true" OnTextChanged="txtNgayThangNam_TextChanged" runat="server" CssClass="txt_csssearch" Width="200px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" runat="server" TargetControlID="txtNgayThangNam"></cc1:CalendarExtender>
                                                        <asp:TextBox Style="width: 200px;" ID="txtDenNgayThangNam" placeholder="Tìm kiếm đến ngày/tháng/năm" AutoPostBack="true" OnTextChanged="txtDenNgayThangNam_TextChanged" runat="server" CssClass="txt_csssearch" Width="200px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" runat="server" TargetControlID="txtDenNgayThangNam"></cc1:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="span3">
                                    <div class="dataTables_filter" id="sample_1_filter">
                                        <asp:LinkButton ID="lnkxuatExel" runat="server" OnClick="lnkxuatExel_Click" CssClass="vadd toolbar btn btn-info"> Xuất Exel</asp:LinkButton>

                                        <asp:LinkButton ID="bthienthi" runat="server" OnClick="btndisplay_Click" CssClass="vadd toolbar btn btn-info"> <i class=" icon-folder-close"></i>&nbsp;Hiện thị</asp:LinkButton>
                                        <span style="display: none">
                                            <asp:LinkButton ID="btDeleteall" ToolTip="Xóa những lựa chọn !" OnClientClick=" return confirmDelete(this);" runat="server" OnClick="btxoa_Click" CssClass="vadd toolbar btn btn-info"><i class="icon-trash"></i>&nbsp;Xóa</asp:LinkButton></span>
                                    </div>
                                </div>
                            </div>
                            <div>
                              <asp:LinkButton ID="lnkxuatExelNhaCC" runat="server" OnClick="lnkxuatExelNhaCC_Click" CssClass="vadd toolbar btn btn-info"> Xuất Exel Toàn bộ nhà cung cấp</asp:LinkButton>
                            </div>
                            <div class="list_item">
                                <asp:Label ID="ltthongbao" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                <div>Tổng thành viên: <asp:Label ID="lttongtb" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label></div>

                                
                                <div  class="scollldh">
                                <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand" OnItemDataBound="rp_items_ItemDataBound">
                                    <HeaderTemplate>
                                        <table class="table table-striped table-bordered" id="sample_1">
                                            <tr>
                                                <th class="hidden-phone">
                                                    <input id="chkAll" onclick="javascript: SelectAllCheckboxes(this, 1);" type="checkbox" /></th>
                                                <th class="hidden-phone">Họ và tên</th>
                                                <th class="hidden-phone">Ngày tạo</th>
                                                <th class="hidden-phone" style="width: 83px; color: red"><span class="BodderDo" style="width: 83px">Loại T/khoản</span></th>
                                                <th class="hidden-phone" style="color: red"><span class="BodderDo">Chi nhánh</span></th>
                                                <th class="hidden-phone" style="text-align: center; width: 83px; color: red"><span class="BodderDo">Thành viên </span><span class="BodderXanh">Lead</span></th>
                                               <%-- <th class="hidden-phone">AGLAND</th>--%>
                                              <%--   <th class="hidden-phone">Cửa hàng</th>--%>
                                                 <%--<th class="hidden-phone">QRcode</th>--%>
                                          <%--      <th class="hidden-phone">Ưu tiên</th>--%>
                                                <th class="hidden-phone">Trạng thái<br />Khóa chức năng</th>
                                                <th class="hidden-phone" style="color: red">Nạp tiền</th>
                                                <th class="hidden-phone" style="color: red"><span class="BodderDo">Cấp Điểm</span>
                                                    <br />
                                                    <span class="BodderXanh">Chuyển điểm</span></th>
                                              <%--  <th class="hidden-phone" style="display: none">Hiệu chỉnh</th>--%>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="odd gradeX">
                                            <td style="text-align: center;">
                                                <asp:CheckBox ID="chkid" runat="server" onclick="javascript:changeColor(this,'white');" /><asp:HiddenField ID="hiID" Value='<%# Eval("iuser_id") %>' runat="server" />
                                            </td>
                                            <td align="left" style="padding-left: 10px; line-height: 22px; color: #646465;width:200px">
                                                <span style="color: red; font-weight: bold;" id="<%#DataBinder.Eval(Container.DataItem, "vuserpwd")%>"><a target="_blank" title="Lịch sửa đăng nhập" href="admin.aspx?u=LichSuDangNhap&IDThanhVien=<%#Eval("iuser_id").ToString()%>" style=" color:red">Tên Đăng Nhập: <%#DataBinder.Eval(Container.DataItem, "vuserun")%></a></span><br />
                                                Họ và tên:<span style="color: #444444; padding-left: 27px; font-weight: bold"><a data-tooltip="sticky<%#Eval("iuser_id") %>" target="_blank" title="Xem chi tiết hoa hồng" href="/admin.aspx?u=HoaHong&ThanhVien=<%# Eval("iuser_id") %>"><%#Eval("vfname") %></a></span><br />
                                                <span style="color:red; font-weight:bold;"><a data-tooltip="sticky<%#Eval("iuser_id") %>" target="_blank" title="Click vào để đăng nhập nhanh" href="/Videmo.aspx?ID=<%# Eval("iuser_id") %>&U1=<%# Eval("vuserun") %>&U2=<%# Eval("vuserpwd") %>">Mật khẩu:</a> <a data-tooltip="sticky<%#Eval("iuser_id") %>" target="_blank" title="Click vào để đăng nhập nhanh" href="/Autologon.aspx?ID=<%# Eval("iuser_id") %>&U1=<%# Eval("vuserun") %>&U2=<%# Eval("vuserpwd") %>""><%--<%#DataBinder.Eval(Container.DataItem, "vuserpwd")%>--%><%# Eval("vuserpwd") %></a></span><br />
                                                <div style="color: #444444; padding-left: 0px; font-weight: bold"><a href="/admin.aspx?u=TBNotification&IDThanhVien=<%#Eval("iuser_id") %>" target="_blank">Thông báo cho khách hàng (<%#ShowTongThongBao(Eval("iuser_id").ToString())%> ) <i class="icon-bell-alt" style=" color:red"></i></a></div>
                                                Địa chỉ:<span style="color: #444444; padding-left: 40px; font-weight: bold"> <a href="/cms/display/Members/CapNhatThongTinTV.aspx?ID=<%# Eval("iuser_id") %>&U1=<%# Eval("vuserun") %>&U2=<%# Eval("vuserpwd") %>" target="_blank"> <%#DataBinder.Eval(Container.DataItem, "vaddress")%> / <%#Commond.ShowTinhThanh(DataBinder.Eval(Container.DataItem, "TinhThanh").ToString())%></a> </span><br />
                                                Điện thoại:<span style="color: #444444; padding-left: 22px; font-weight: bold"><%#DataBinder.Eval(Container.DataItem, "vphone")%></span><br />
                                                Email:<span style="color: #444444; padding-left: 15px; font-weight: bold"><%#DataBinder.Eval(Container.DataItem, "vemail")%></span><br />
                                                Người giới thiệu:<span style="color: #444444; padding-left: 15px; font-weight: bold"><a data-tooltip="sticky<%#Eval("iuser_id") %>" target="_blank" href="/admin.aspx?u=Thanhvien&IDThanhVien=<%# Eval("Gioithieu") %>"><%#ShowThanhVien(DataBinder.Eval(Container.DataItem, "Gioithieu").ToString())%></a></span><br />
                                                Chi nhánh:<span style="color: #e9740e; padding-left: 15px; font-weight: bold"><a target="_blank" href="/admin.aspx?u=DaiLy"><%#ShowChiNhanh(DataBinder.Eval(Container.DataItem, "IDChiNhanh").ToString())%></a> / <%#ShowtThanhVien(ShowIDChiNhanh(DataBinder.Eval(Container.DataItem,"IDChiNhanh").ToString()))%></span><br />
                                                ID Thành viên:<span style="color: #e9740e; padding-left: 15px; font-weight: bold"><a target="_blank" href="/cms/Admin/Member/Danhsachthanhviencapduoi.aspx?IDThanhVien=<%#Eval("iuser_id") %>"> <%#(DataBinder.Eval(Container.DataItem, "iuser_id").ToString())%></a></span><br />
                                                Lịch sử gộp ví:<span style="color: #e9740e; padding-left: 15px; font-weight: bold"><a target="_blank" href="/cms/Admin/Member/GopVi.aspx?IDThanhVien=<%#Eval("iuser_id") %>"> [Chi tiết]</a></span><br />
                                                 
                                                 <%if (ChucNang == "1")
                                                   {%>
                                                          <br /> <asp:LinkButton CssClass="active action-link-button" Style="font-size: 14px;" ToolTip="Kích vào để thay đổi đại lý sang cửa hàng" CommandName="ChangeCuaHang" CommandArgument='<%#Eval("iuser_id")%>' runat="server" ID="Linkbutton10"><%#MoreAll.MoreAll.Cuahang(DataBinder.Eval(Container.DataItem, "CuaHang").ToString())%></asp:LinkButton>
                                                  <%} %>
                                                 <br />
                                                    <a onclick="Showthanhvien(<%#Eval("iuser_id") %>)" class="account_a" style=" color: red; font-size: 14px; ">Xem thêm ... </a>
                                                    <hr />
                                                    <div id="<%#Eval("iuser_id") %>myDropdown" style=" display:none">
                                                         <%#Commond.TachMtre(DataBinder.Eval(Container.DataItem, "MTRee").ToString())%> 
                                                          <a target="_blank" href="/cms/Admin/Member/DiagramTree.aspx?IDTHanhVien=<%#Eval("iuser_id") %>">Diagram Tree</a>|
                                                        <a target="_blank" href="/cms/Admin/Member/DoiNhanhThanhVien.aspx?IDTHanhVien=<%#Eval("iuser_id") %>">...</a>
                                                   <br />
                                                    Chứng minh thư:<span style="color: #e9740e; padding-left: 15px; font-weight: bold">  <%#MoreAll.MoreAll.LoaiChungMinh(DataBinder.Eval(Container.DataItem, "LoaiChungMinh").ToString())%> : <%#(DataBinder.Eval(Container.DataItem, "SoChungMinhThu"))%> </span><br />
                                                    Ngày cấp:<span style="color: #e9740e; padding-left: 15px; font-weight: bold">  <%#(DataBinder.Eval(Container.DataItem, "NoiCapChungMinhThu"))%> </span><br />
                                                    Nơi cấp: <span style="color: #e9740e; padding-left: 15px; font-weight: bold"> <%#(DataBinder.Eval(Container.DataItem, "NgayCapChungMinhThu"))%> </span><br />
                                                    <%#MoreAll.MoreImage.ImagesCMT(Eval("AnhChungMinhThuTruoc").ToString())%>
                                                    <%#MoreAll.MoreImage.ImagesCMT(Eval("AnhChungMinhThuSau").ToString())%> <br />
                                                    <div style='<%#ShowNCC(Eval("Type").ToString())%>'>
                                                    Mã số doanh nghiệp:<span style="color: #e9740e; padding-left: 15px; font-weight: bold"><%#(DataBinder.Eval(Container.DataItem, "MaSoDoanhNghiep"))%></span><br />
                                                    Tên shop:<span style="color: #e9740e; padding-left: 15px; font-weight: bold"><%#(DataBinder.Eval(Container.DataItem, "TenShop"))%></span><br />
                                                    Địa chỉ kho hàng:<span style="color: #e9740e; padding-left: 15px; font-weight: bold"><%#(DataBinder.Eval(Container.DataItem, "DiaChiKhoHang"))%></span><br />
                                                    </div>
                                                    </div>
                                            </td>
                                            <td style="text-align: center;">
                                                <%#MoreAll.MoreAll.FormatDate(Eval("dlastvisited").ToString())%>
                                            </td>
                                            <td>
                                                <%#MoreAll.MoreImage.ImageNCC(Eval("GiayPhepKinhDoanh").ToString())%> <br /><br />
                                                <%#Showsanpham(Eval("Type").ToString(),Eval("iuser_id").ToString(),Eval("TongSoSanPham").ToString())%>
                                                 <%if (ChucNang == "1")
                                                   {%>
                                                <asp:LinkButton CssClass="active action-link-button" Style="font-size: 14px;" ToolTip="Kích vào để chuyển trạng thái giữa nhà cung cấp và thành viên" CommandName="ThanhVienNhaCungCap" CommandArgument='<%#Eval("iuser_id")%>' runat="server" ID="Linkbutton3"> <%#MoreAll.MoreAll.Kieuloai2(Eval("Type").ToString())%></asp:LinkButton>
                                                    <%}else{ %>
                                                  <%#MoreAll.MoreAll.Kieuloai2(Eval("Type").ToString())%>
                                                <%} %>
                                            </td>
                                            <td style="text-align: center;">
                                                 <%if (ChucNang == "1")
                                                   {%>
                                                <a href="/admin.aspx?u=DaiLy" title="Muốn thay đổi trạng thái này , Vui lòng sang bên modul chi nhánh để thêm chi nhánh"><%#MoreAll.MoreAll.Enable(DataBinder.Eval(Container.DataItem, "ChiNhanh").ToString())%></a>
                                                  <%}else{ %>
                                                <%#MoreAll.MoreAll.Enable(DataBinder.Eval(Container.DataItem, "ChiNhanh").ToString())%>
                                                <%} %>
                                                <%-- <%#ShowNameChiNhanh(Eval("ChiNhanh").ToString(), ShowIDChiNhanh(Eval("iuser_id").ToString()))%>--%>

                                                <%-- Muốn thay đổi trạng thái chi nhánh thì chỉ cần sang modul đại lý thêm chi nhánh vào là nó sẽ tự thay đổi trạng thái /admin.aspx?u=DaiLy--%>
                                                <%--    <asp:LinkButton CssClass="active action-link-button" CommandName="ChangeChiNhanh" CommandArgument='<%#Eval("iuser_id")%>' runat="server" ID="Linkbutton3"><%#MoreAll.MoreAll.Enable(DataBinder.Eval(Container.DataItem, "ChiNhanh").ToString())%></asp:LinkButton>--%>
                                            </td>
                                            
                                            <td style="text-align: center;">
                                                 <%if (ChucNang == "1")
                                                   {%>
                                                <asp:LinkButton CssClass="active action-link-button" Style="font-size: 14px;" ToolTip="Kích vào để nâng cấp thành viên lên Leader" CommandName="ChangeStatus" CommandArgument='<%#Eval("iuser_id")%>' runat="server" ID="Linkbutton4"><%#MoreAll.MoreAll.Showlead(DataBinder.Eval(Container.DataItem, "Leader").ToString())%></asp:LinkButton>
                                                  <%}else{ %>
                                              <%#MoreAll.MoreAll.Showlead(DataBinder.Eval(Container.DataItem, "Leader").ToString())%>
                                                <%} %>
                                            </td>
                                          <%--   <td style="text-align: center;">
                                                 <%if (ChucNang == "1")
                                                   {%>
                                                <asp:LinkButton CssClass="active action-link-button" Style="font-size: 14px;" ToolTip="Kích vào để thay đổi đại lý sang cửa hàng" CommandName="ChangeCuaHang" CommandArgument='<%#Eval("iuser_id")%>' runat="server" ID="Linkbutton1"><%#MoreAll.MoreAll.Cuahang(DataBinder.Eval(Container.DataItem, "CuaHang").ToString())%></asp:LinkButton>
                                                  <%}else{ %>
                                             <%#MoreAll.MoreAll.Cuahang(DataBinder.Eval(Container.DataItem, "CuaHang").ToString())%>
                                                <%} %>
                                            </td>--%>

                                           <%-- <td style="text-align: center;">
                                                <asp:LinkButton CssClass="active action-link-button" Style="font-size: 14px;" ToolTip="Kích vào để nâng cấp thành viên AGLANG" CommandName="ChangeAGLANG" CommandArgument='<%#Eval("iuser_id")%>' runat="server" ID="Linkbutton1"><%#MoreAll.MoreAll.Enable(DataBinder.Eval(Container.DataItem, "ThanhVienAgLang").ToString())%></asp:LinkButton>
                                            </td>--%>
                                            <%-- <td style="text-align: center;">
                                                  <%if (ChucNang == "1")
                                                   {%>
                                                <asp:LinkButton CssClass="active action-link-button" Style="font-size: 14px;" ToolTip="Kích vào để nâng cấp thành viên QRCode" CommandName="ChangeQRCode" CommandArgument='<%#Eval("iuser_id")%>' runat="server" ID="Linkbutton8"><%#MoreAll.MoreAll.Enable(DataBinder.Eval(Container.DataItem, "TrangThaiThamGiaQRCode").ToString())%></asp:LinkButton>
                                                    <%}else{ %>
                                             <%#MoreAll.MoreAll.Enable(DataBinder.Eval(Container.DataItem, "TrangThaiThamGiaQRCode").ToString())%>
                                                <%} %>
                                             </td>--%>

                                          <%--  <td style="text-align: center;">
                                                <asp:LinkButton CssClass="active action-link-button" Style="font-size: 14px;" ToolTip="Kích vào để được ưu tiên - không được rút tiền" CommandName="ChangeUutien" CommandArgument='<%#Eval("iuser_id")%>' runat="server" ID="Linkbutton7"><%#MoreAll.MoreAll.Uutien(DataBinder.Eval(Container.DataItem, "Uutien").ToString())%></asp:LinkButton>
                                            </td>--%>

                                            <td style="text-align: center;">
                                                <%#Status(Eval("istatus").ToString())%>
                                                <br />
                                             <%if (ChucNang == "1")
                                                   {%>
                                                        <asp:LinkButton CssClass="active action-link-button" ID="LinkButton2" runat="server" CommandName="lock" CommandArgument='<%#Eval("iuser_id")%>' OnLoad="Lock_Load" Visible='<%#EnableLock(Eval("istatus").ToString())%>'><span style="font-size:14px">[Lock]</span></asp:LinkButton>
                                                <asp:LinkButton CssClass="active action-link-button" ID="LinkButton5" runat="server" CommandName="unlock" CommandArgument='<%#Eval("iuser_id")%>' Visible='<%#EnableUnLock(Eval("istatus").ToString())%>'><span style="font-size:14px">[Unlock]</span></asp:LinkButton>
                                                  <br />  <br />
                                                 <asp:LinkButton CssClass="active action-link-button" Style="font-size: 14px;" ToolTip="Tắt bật chức năng rút điểm, chuyển điểm,Mua hàng" CommandName="ChangeTatChucNang" CommandArgument='<%#Eval("iuser_id")%>' runat="server" ID="Linkbutton7"><%#MoreAll.MoreAll.TatChucNang(DataBinder.Eval(Container.DataItem, "TatChucNang").ToString())%></asp:LinkButton>
                                                  <%}else{ %>
                                                  <%#MoreAll.MoreAll.TatChucNang(DataBinder.Eval(Container.DataItem, "TatChucNang").ToString())%>
                                                <%} %>
                                            </td>
                                            <td style="text-align: center;">
                                                <%#MoreAll.MoreAll.KichHoatTV(DataBinder.Eval(Container.DataItem, "DuyetTienDanap").ToString())%>
                                                  <div> <%#Commond.FormatNgayHetHan(DataBinder.Eval(Container.DataItem, "iuser_id").ToString())%></div>
                                                   <%if (DuyetKichHoat == "1")
                                                   {%>
                                                <div> <asp:LinkButton CssClass="active action-link-button" ID="LinkButton11" runat="server" CommandName="DuyetKichHoat" CommandArgument='<%#Eval("iuser_id")%>' Visible='<%#EnableUnLock(Eval("DuyetTienDanap").ToString())%>' style=" color:red;font-size:14px">[Duyệt 480 Ko sinh Hoa hồng]</asp:LinkButton></div>
                                                 <%} %>
                                            </td>
                                            <td style="text-align: center; width:220px">

                                                 <%if (ChucNang == "1")
                                                   {%>
                                                      Cấu hình ngày tự động duyệt đơn hàng<br />
                                                <asp:TextBox ID="txtduyettudongdonhang" Style="border: 1px solid #d7d7d7; border-radius: 3px; text-align: center; width: 35%; " TextMode="MultiLine" Text='<%#DataBinder.Eval(Container.DataItem, "CauHinhDuyetDonTuDong")%>' CssClass="txt_css" runat="server" OnTextChanged="txtduyettudongdonhang_TextChanged" AutoPostBack="true"></asp:TextBox>
                                               <br />
                                                Cấp độ
                                                <asp:DropDownList ID="ddlLevelThanhVien" style="width: 130px;" runat="server" OnSelectedIndexChanged="ddlLevelThanhVien_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Cộng tác viên</asp:ListItem>
                                                    <asp:ListItem Value="1">Nhân Viên KD</asp:ListItem>
                                                    <asp:ListItem Value="2">Trưởng Nhóm</asp:ListItem>
                                                    <asp:ListItem Value="3">Trưởng Phòng</asp:ListItem>
                                                    <asp:ListItem Value="4">Phó Giám Đốc</asp:ListItem>
                                                    <asp:ListItem Value="5">Giám Đốc KD</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdLevelThanhVien" Value='<%#Eval("LevelThanhVien") %>' runat="server" />
                                                 <br />  
                                                <div><a target="_blank" style=" color:red; font-weight:bold" href="http://localhost:41625/admin.aspx?u=LichSuLevel&kw=<%#Eval("vuserun") %>&Tu=&Den=">Lịch sử sao</a></div>
                                               <br />    
                                                  <%} %>
                                                <br />
                                                                <div>Ví thương mại <b style="color: red"><%#Eval("TongTienCoinDuocCap")%> </b>/ Điểm</div>
                                                                    <div>Ví quản lý <b style="color: red"><%#Eval("VIAAFFILIATE")%> </b>/ Điểm</div>
                                                                 <div> HOA HỒNG  <b style="color: red"><%#Eval("ViHoaHongMuaBan")%> </b>/ Điểm</div>
                                                                <div>THƯỞNG MUA HÀNG <b style="color: red"><%#Eval("ViTangTienVip")%> </b>/ Điểm</div>
                                                <div>Ví Mua Hàng<b style="color: red"><%#Eval("ViMuaHangAFF")%> </b>/ Điểm</div>

                                                                <div>Tổng số điểm được cấp <b style="color: red"><%#ShowDienmDuocCap(Eval("iuser_id").ToString())%> </b>/ Điểm</div>
                                                
                                                                <%if (ShowThem == "1")
                                                                  {%>
                                                        <a title="Cấp điểm cho thành viên" href="admin.aspx?u=CCapDiem&IDThanhVien=<%#Eval("iuser_id").ToString()%>"><span class="BodderDo">Cấp điểm</span></a>
                                                                <a title="Chuyển điểm cho thành viên cấp dưới" href="admin.aspx?u=ChuyenDiem&IDThanhVien=<%#Eval("iuser_id").ToString()%>"><span class="BodderXanh">Chuyển điểm</span></a>
                                                            <%} %>
                                                 <br />
                                             <a onclick="Showthanhvien(1<%#Eval("iuser_id") %>)" class="account_a" style=" color: red; font-size: 14px; ">Xem thêm... </a>
                                                    <hr />
                                                    <div id="1<%#Eval("iuser_id") %>myDropdown" style=" display:none">
                                                               
                                                                 <div id="cophan" runat="server" Visible='<%#EnableLock(Eval("ThanhVienAgLang").ToString())%>' >
                                                                SỐ CỔ PHẦN ĐANG SỞ HỮU<br />
                                                                <asp:TextBox ID="txtTieude" Style="border: 1px solid #d7d7d7; border-radius: 3px; text-align: center; width: 95%; " TextMode="MultiLine" Text='<%#DataBinder.Eval(Container.DataItem, "TienDangSoHuuBatDongSan")%>' CssClass="txt_css" runat="server" OnTextChanged="txtTieude_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                               </div>
                                                                <br />
                                                               <img style="width: 100px;" src="<%#Eval("AnhQRCode") %>">
                                                                     <br />
                                                         <%if (ChucNang == "1")
                                                   {%>
                                                          
                                                <br />
                                                 

                                                                <hr />
                                                                Số lần lấy lại mật khẩu:<span style="color: #e9740e; padding-left: 15px; font-weight: bold"><%#(DataBinder.Eval(Container.DataItem, "vidcard").ToString())%></span><br />
                                                                % Chiết khấu hoa hồng QRCode: <asp:TextBox ID="txtChietkauQRCode" Style="border: 1px solid #d7d7d7;margin-top: 6px; border-radius: 3px; text-align: center; width:10%; " Text='<%#DataBinder.Eval(Container.DataItem, "QRCodeChietKhauHH")%>' CssClass="txt_css" Width="40px" runat="server"  ></asp:TextBox><br />
                                                                % Chiết khấu hh người mua QRCode: <asp:TextBox ID="txthhnguoimuaQRCode" Style="border: 1px solid #d7d7d7;margin-top: 6px; border-radius: 3px; text-align: center; width:10%; "  Text='<%#DataBinder.Eval(Container.DataItem, "QRCodeHHNguoiMua")%>' CssClass="txt_css" Width="40px" runat="server"  ></asp:TextBox><br />
                                                                % Chiết khấu hh hệ hống QRCode: <asp:TextBox ID="txthhhethongQRCode" Enabled="false" Style="border: 1px solid #d7d7d7;margin-top: 6px; border-radius: 3px; text-align: center; width:10%; "  Text='<%#DataBinder.Eval(Container.DataItem, "QRCodeHHHeThong")%>' CssClass="txt_css" Width="40px" runat="server"></asp:TextBox><br />
                                                                <asp:LinkButton CssClass="active action-link-button" CommandName="CapNhatQRCode" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"iuser_id")%>' style=" font-size:12px;" runat="server" ID="Linkbutton9" NAME="Linkbutton2"><span class="BodderDo">Cập nhật</span></asp:LinkButton>
                                                      
                                                        
                                                         <%} %>

                                                    </div>
                                            </td>
                                            <td style="text-align: center; display:none">
                                            <asp:LinkButton CssClass="active action-link-button" ID="LinkButton6" runat="server" CommandArgument='<%#Eval("iuser_id")%>' CommandName="delete" OnLoad="Delete_Load"><i class="icon-trash"></i></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                                    </div>
                            </div>

                           
                            <table border="0" cellpadding="0" style="border-collapse: collapse" width="100%">
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

                        </div>
                        <%--       </ContentTemplate>
                            </asp:UpdatePanel>--%>
                    </div>
                </div>
            </div>
        </div>
        <style>
            i.icon-check-empty {
                font-size: 20px;
            }

            i.icon-check {
                font-size: 20px;
            }
        </style>

<style>
    /*.list_item
        {
   width:  1000px;
    overflow-x: scroll;
}
   .list_item table { width: 2000px; }*/
</style>
<script type="text/javascript" charset="utf-8">
    function Showthanhvien(id) {
        var name = id + 'myDropdown';
        var hidden = document.getElementById(name);
        if (hidden.style.display == 'none') {
            hidden.style.display = 'block';
        }
        else {
            hidden.style.display = 'none';
        }
    }
</script>