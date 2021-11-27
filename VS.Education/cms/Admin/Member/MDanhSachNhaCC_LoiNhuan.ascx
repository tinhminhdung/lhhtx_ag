<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MDanhSachNhaCC_LoiNhuan.ascx.cs" Inherits="VS.E_Commerce.cms.Admin.Member.MDanhSachNhaCC_LoiNhuan" %>
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
                            <h4><i class="icon-reorder"></i>Danh sách nhà cung cấp</h4>
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
                                                    <asp:ListItem Value="0">1 Sao</asp:ListItem>
                                                    <asp:ListItem Value="1">2 Sao</asp:ListItem>
                                                    <asp:ListItem Value="2">3 Sao</asp:ListItem>
                                                    <asp:ListItem Value="3">4 Sao</asp:ListItem>
                                                    <asp:ListItem Value="4">5 Sao</asp:ListItem>
                                                    <asp:ListItem Value="5">6 Sao</asp:ListItem>
                                                </asp:DropDownList>

                                                 <asp:DropDownList ID="ddlkieuthanhvien" runat="server" AutoPostBack="true" CssClass="txt" OnSelectedIndexChanged="ddlkieuthanhvien_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="-1">= Tất cả NCC =</asp:ListItem>
                                                    <asp:ListItem Value="2">Tất cả nhà cung cấp</asp:ListItem>
                                                     <asp:ListItem Value="3">Chỉ có nhà cung cấp đã đăng sản phẩm</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="true" CssClass="txt" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlorderby" style="width: 242px;" runat="server" AutoPostBack="true" CssClass="txt"
                                                    OnSelectedIndexChanged="ddlorderby_SelectedIndexChanged">
                                                       <asp:ListItem Selected="True" Value="TongSoSanPham">S.xếp:Số lượng sản phẩm</asp:ListItem>
                                                       <asp:ListItem Value="TongSoSanPhamDaBan">S.xếp:Số lượng sản phẩm đã bán</asp:ListItem>
                                                    <asp:ListItem  Value="dcreatedate">S.xếp:Ngày cập nhật</asp:ListItem>
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
                                        <asp:LinkButton ID="bthienthi" runat="server" OnClick="btndisplay_Click" CssClass="vadd toolbar btn btn-info"> <i class=" icon-folder-close"></i>&nbsp;Hiện thị</asp:LinkButton>
                                       
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
                                <asp:Repeater ID="Repeater1" runat="server" >
                                    <HeaderTemplate>
                                        <table class="table table-striped table-bordered" id="sample_1">
                                            <tr>
                                                <th class="hidden-phone"><input id="chkAll" onclick="javascript: SelectAllCheckboxes(this, 1);" type="checkbox" /></th>
                                                <th class="hidden-phone">Họ và tên</th>
                                                <th class="hidden-phone">Thông tin</th>
                                                <th class="hidden-phone">Ngày tạo</th>
                                                <th class="hidden-phone">Sản phẩm</th>
                                                 <th class="hidden-phone">Đã bán</th>
                                                 <th class="hidden-phone">Số điểm gốc</th>
                                                 <th class="hidden-phone">Số điểm đã chia HH</th>
                                                 <th class="hidden-phone">Số điểm còn lại</th>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="odd gradeX">
                                            <td style="text-align: center;">
                                                <asp:CheckBox ID="chkid" runat="server" onclick="javascript:changeColor(this,'white');" /><asp:HiddenField ID="hiID" Value='<%# Eval("iuser_id") %>' runat="server" />
                                            </td>
                                            <td align="left" style="padding-left: 10px; line-height: 22px; color: #646465" width="250px">
                                                <span style="color: red; font-weight: bold;" id="<%#DataBinder.Eval(Container.DataItem, "vuserpwd")%>">Tên Đăng Nhập: <%#DataBinder.Eval(Container.DataItem, "vuserun")%></span><br />
                                                Họ và tên:<span style="color: #444444; padding-left: 27px; font-weight: bold"><a data-tooltip="sticky<%#Eval("iuser_id") %>" target="_blank" title="Xem chi tiết hoa hồng" href="/admin.aspx?u=HoaHong&ThanhVien=<%# Eval("iuser_id") %>"><%#Eval("vfname") %></a></span><br />
                                                <span style="color:red; font-weight:bold;">Mật khẩu: <a data-tooltip="sticky<%#Eval("iuser_id") %>" target="_blank" title="Click vào để đăng nhập nhanh" href="/Autologon.aspx?ID=<%# Eval("iuser_id") %>&U1=<%# Eval("vuserun") %>&U2=<%# Eval("vuserpwd") %>"><%--<%#DataBinder.Eval(Container.DataItem, "vuserpwd")%>--%>********</a></span><br />
                                                Địa chỉ:<span style="color: #444444; padding-left: 40px; font-weight: bold"><%#DataBinder.Eval(Container.DataItem, "vaddress")%></span><br />
                                                Điện thoại:<span style="color: #444444; padding-left: 22px; font-weight: bold"><%#DataBinder.Eval(Container.DataItem, "vphone")%></span><br />
                                                Email:<span style="color: #444444; padding-left: 15px; font-weight: bold"><%#DataBinder.Eval(Container.DataItem, "vemail")%></span><br />
                                            </td>
                                                 <td align="left" style="padding-left: 10px; line-height: 22px; color: #646465" width="250px">
                                                Mã số doanh nghiệp:<span style="color: #e9740e; padding-left: 15px; font-weight: bold"><%#(DataBinder.Eval(Container.DataItem, "MaSoDoanhNghiep"))%></span><br />
                                                Tên shop:<span style="color: #e9740e; padding-left: 15px; font-weight: bold"><a style=" color:red" target="_blank" href="/shop/<%# Eval("vuserun") %>"><%#(DataBinder.Eval(Container.DataItem, "TenShop"))%></a></span><br />
                                                Địa chỉ kho hàng:<span style="color: #e9740e; padding-left: 15px; font-weight: bold"><%#(DataBinder.Eval(Container.DataItem, "DiaChiKhoHang"))%></span><br />
                                                  <%#MoreAll.MoreImage.ImageNCC(Eval("GiayPhepKinhDoanh").ToString())%> 
                                                  <%#MoreAll.MoreImage.ImagesCMT(Eval("AnhChungMinhThuTruoc").ToString())%>
                                                  <%#MoreAll.MoreImage.ImagesCMT(Eval("AnhChungMinhThuSau").ToString())%> <br />
                                                 </td>
                                            <td style="text-align: center;">
                                                <%#MoreAll.MoreAll.FormatDate(Eval("dlastvisited").ToString())%>
                                            </td>
                                               <td style="text-align: center;">   <span class="Bodderx2"> <%#Showsanpham2(Eval("Type").ToString(),Eval("iuser_id").ToString(),Eval("TongSoSanPham").ToString())%></span></td>
                                                <td style="text-align: center;"> <span class="Bodderx3"> <%#ShowSanPhamDaBan(Eval("iuser_id").ToString()) %></span></td>
                                                <td style="text-align: center;"> <%#ShowDiemGoc(Eval("iuser_id").ToString()) %>  </td>
                                                 <td style="text-align: center;"> <%#ShowSoDiemDaChia(Eval("iuser_id").ToString()) %>  </td>
                                                 <td style="text-align: center;"><%#ShowDiemConLai(Eval("iuser_id").ToString()) %> </td>
                                           
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                                    </div>
                            </div>

                            <style>
                                .scollldh {
                                    width: 1200px;
                                }
                            </style>
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
   width:  1000p     overflow-x: scroll;
}
   .list_item table { width: 2000px; }*/
</style>